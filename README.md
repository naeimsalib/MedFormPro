# MedForm Pro - Prescription Management System

MedForm Pro is a comprehensive web-based prescription management system built with ASP.NET Core MVC. It facilitates the entire prescription lifecycle, from creation to delivery, with role-based access control and a streamlined workflow.

## Quick Start Guide

### Default Admin Account

To access the system as an administrator:

- Email: admin@medformpro.com
- Password: Admin123!

### User Roles and Access

1. **Administrator**

   - Full system access
   - User management (create, edit, delete users)
   - View all prescriptions
   - Manage system settings

2. **Pharmacist**

   - Create new prescriptions
   - View own prescriptions
   - Track prescription status

3. **Review Team**

   - Review submitted prescriptions
   - Approve or deny prescriptions
   - View review history

4. **Delivery Team**
   - Manage approved prescriptions
   - Update delivery status
   - Track shipments

## Installation and Setup

### Prerequisites

- .NET 8.0 SDK
- SQLite (included)
- Git

### Installation Steps

1. Clone the repository:

```bash
git clone https://github.com/yourusername/MedFormPro.git
cd MedFormPro
```

2. Install .NET 8.0 SDK if not already installed:

- Download from [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

3. Restore dependencies:

```bash
dotnet restore
```

4. Update the database:

```bash
dotnet ef database update
```

5. Run the application:

```bash
cd MedFormPro.Web
dotnet run
```

6. Access the application:

- Open your browser and navigate to `http://localhost:5160`
- Log in using the default admin credentials

## User Management Guide

### Creating New Users (Admin Only)

1. Log in as administrator
2. Click "Manage Users" in the navigation menu
3. Click "Create New User" button
4. Fill in the user details:
   - Username
   - Email
   - First Name
   - Last Name
   - Role (select from dropdown)
   - Password
   - Confirm Password
5. Click "Create User"

### Editing Users (Admin Only)

1. Go to "Manage Users"
2. Click "Edit" next to the user
3. Update the required information
4. To change password, enter new password (optional)
5. Click "Save Changes"

### Deleting Users (Admin Only)

1. Go to "Manage Users"
2. Click "Delete" next to the user
3. Confirm deletion
   Note: The last administrator cannot be deleted

## System Features

### Prescription Management

- Create new prescriptions (Pharmacists)
- Review and approve/deny (Review Team)
- Track delivery status (Delivery Team)
- Monitor all prescriptions (Administrators)

### Status Tracking

- Prescription Status: Submitted → Approved/Denied
- Shipment Status: Pending → Fulfilled → Delivered

### Security Features

- Role-based access control
- Secure password handling
- Protected routes and actions
- Audit trail of all actions

## Troubleshooting

### Common Issues

1. **Database Connection Issues**

   - Ensure SQLite is properly installed
   - Check database file permissions
   - Run database migrations

2. **Authentication Problems**

   - Verify correct email format
   - Check password requirements
   - Clear browser cookies if needed

3. **Role Access Issues**
   - Verify user role assignment
   - Check role-based route protection
   - Ensure proper login/logout

## Support

For support:

- Email: support@medformpro.com
- Open an issue in the repository
- Check the documentation

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Technical Stack

- ASP.NET Core 8.0
- Entity Framework Core
- SQLite Database
- Bootstrap 5
- Font Awesome Icons
- jQuery
