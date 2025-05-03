using MedFormPro.Web.Models;
using System.Security.Cryptography;
using System.Text;

namespace MedFormPro.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if there are any users
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Username = "admin",
                    Email = "admin@medformpro.com",
                    PasswordHash = HashPassword("Admin123!"),
                    FirstName = "System",
                    LastName = "Administrator",
                    Role = UserRole.Admin
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }

            // Check if we already have guides
            if (context.Guides.Any())
            {
                return;   // DB has been seeded
            }

            var guides = new Guide[]
            {
                // General Guides
                new Guide {
                    Title = "Welcome to MedForm Pro",
                    Category = "General",
                    Content = "<h3>Welcome to MedForm Pro</h3><p>MedForm Pro is a comprehensive prescription management system designed to streamline the process of creating, reviewing, and delivering prescriptions. This guide will help you understand the basic workflow and features available to you.</p><p>The system supports different roles with specific responsibilities:</p><ul><li><strong>Pharmacists:</strong> Create and manage prescriptions</li><li><strong>Review Team:</strong> Review and approve prescriptions</li><li><strong>Delivery Team:</strong> Manage prescription deliveries</li><li><strong>Administrators:</strong> Manage users and oversee all operations</li></ul>",
                    DisplayOrder = 1,
                    Icon = "fas fa-home"
                },
                
                // Pharmacist Guides
                new Guide {
                    Title = "Creating a Prescription",
                    Category = "Pharmacist",
                    Content = "<h3>Creating a Prescription</h3><p>To create a new prescription:</p><ol><li>Click on 'My Prescriptions' in the navigation menu</li><li>Click the 'Create New Prescription' button</li><li>Fill in all required fields:<ul><li>Patient Name</li><li>Medication Name</li><li>Dosage</li><li>Instructions</li></ul></li><li>Click 'Submit' to send for review</li></ol><p>The prescription will be sent to the Review Team for approval.</p>",
                    DisplayOrder = 1,
                    Icon = "fas fa-prescription",
                    RoleAccess = "Pharmacist"
                },
                new Guide {
                    Title = "Managing Your Prescriptions",
                    Category = "Pharmacist",
                    Content = "<h3>Managing Your Prescriptions</h3><p>From the 'My Prescriptions' page, you can:</p><ul><li>View all prescriptions you've created</li><li>Check the status of each prescription</li><li>View review notes when available</li><li>Track delivery status</li></ul>",
                    DisplayOrder = 2,
                    Icon = "fas fa-list-check",
                    RoleAccess = "Pharmacist"
                },

                // Review Team Guides
                new Guide {
                    Title = "Reviewing Prescriptions",
                    Category = "Review",
                    Content = "<h3>Reviewing Prescriptions</h3><p>To review prescriptions:</p><ol><li>Click on 'Review Prescriptions' in the navigation menu</li><li>Select a prescription from the pending list</li><li>Review all prescription details carefully</li><li>Add review notes if necessary</li><li>Click 'Approve' or 'Reject' based on your review</li></ol><p>Approved prescriptions will be sent to the Delivery Team.</p>",
                    DisplayOrder = 1,
                    Icon = "fas fa-clipboard-check",
                    RoleAccess = "ReviewTeam"
                },
                new Guide {
                    Title = "Review History",
                    Category = "Review",
                    Content = "<h3>Accessing Review History</h3><p>To view your review history:</p><ol><li>Click on 'Review History' in the navigation menu</li><li>View all prescriptions you've reviewed</li><li>Access detailed information about each review</li><li>Filter by date or status if needed</li></ol>",
                    DisplayOrder = 2,
                    Icon = "fas fa-history",
                    RoleAccess = "ReviewTeam"
                },

                // Delivery Team Guides
                new Guide {
                    Title = "Managing Deliveries",
                    Category = "Delivery",
                    Content = "<h3>Managing Prescription Deliveries</h3><p>To manage deliveries:</p><ol><li>Access the 'Delivery Management' section</li><li>View all approved prescriptions ready for delivery</li><li>Update delivery status as needed:<ul><li>Preparing</li><li>Out for Delivery</li><li>Delivered</li></ul></li><li>Add any delivery notes or tracking information</li></ol>",
                    DisplayOrder = 1,
                    Icon = "fas fa-truck",
                    RoleAccess = "DeliveryTeam"
                },

                // Admin Guides
                new Guide {
                    Title = "User Management",
                    Category = "Admin",
                    Content = "<h3>Managing Users</h3><p>As an administrator, you can:</p><ul><li>Create new user accounts</li><li>Assign user roles:<ul><li>Pharmacist</li><li>Review Team</li><li>Delivery Team</li><li>Administrator</li></ul></li><li>Reset passwords if needed</li><li>Deactivate user accounts</li></ul>",
                    DisplayOrder = 1,
                    Icon = "fas fa-users-gear",
                    RoleAccess = "Admin"
                },
                new Guide {
                    Title = "System Overview",
                    Category = "Admin",
                    Content = "<h3>System Overview</h3><p>As an administrator, you have access to:</p><ul><li>All prescriptions in the system</li><li>User management functions</li><li>System statistics and reports</li><li>Audit logs and history</li></ul><p>Use these tools to ensure smooth operation of the prescription management system.</p>",
                    DisplayOrder = 2,
                    Icon = "fas fa-chart-line",
                    RoleAccess = "Admin"
                }
            };

            context.Guides.AddRange(guides);
            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}