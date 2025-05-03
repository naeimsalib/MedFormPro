# MedForm Pro - Prescription Management System

MedForm Pro is a comprehensive web-based prescription management system built with ASP.NET Core MVC. It facilitates the entire prescription lifecycle, from creation to delivery, with role-based access control and a streamlined workflow.

## System Overview

### Landing Page

![Landing Page](wwwroot/images/docs/landing.png)

The landing page provides an overview of the system's core functionalities:

- Clear role descriptions for Pharmacists, Review Team, and Delivery Team
- Key features highlighting secure access control and real-time tracking
- Simple login access for authorized users
- Modern, clean interface design

### Login Interface

![Login Page](wwwroot/images/docs/login.png)

Secure authentication system featuring:

- Email-based login
- Password protection
- Remember me functionality
- Clean, minimalist design for easy access

## Role-Based Interfaces

### 1. Pharmacist Interface

#### Prescription Creation

![Create Prescription](wwwroot/images/docs/create-prescription.png)

Pharmacists can create new prescriptions with:

- Patient information input
- Medication details
- Dosage specifications
- Detailed instructions
- Simple form validation
- Quick submission process

#### My Prescriptions View

![Pharmacist Dashboard](wwwroot/images/docs/pharmacist-dashboard.png)

Pharmacists can track their prescriptions with:

- Complete list of submitted prescriptions
- Status tracking (Submitted, Approved, Denied)
- Shipment status monitoring
- Creation date tracking
- Action buttons for details and management

### 2. Review Team Interface

#### Active Prescriptions Review

![Review Interface](wwwroot/images/docs/review-prescriptions.png)

Review team members can:

- View all submitted prescriptions
- Access prescription details
- Make approval/denial decisions
- Track prescription status
- Monitor shipment status

#### Review History

![Review History](wwwroot/images/docs/review-history.png)

Comprehensive history tracking showing:

- Previously reviewed prescriptions
- Review decisions (Approved/Denied)
- Review timestamps
- Current shipment status
- Quick access to prescription details

### 3. Delivery Team Interface

#### Delivery Management

![Delivery Management](wwwroot/images/docs/delivery-management.png)

Delivery team features include:

- List of approved prescriptions
- Current shipment status
- Delivery tracking
- Status update capabilities
- Detailed prescription information

### 4. Admin Interface

#### All Prescriptions View

![Admin Dashboard](wwwroot/images/docs/admin-dashboard.png)

Administrators have access to:

- Complete prescription database
- Status monitoring across all stages
- Shipment tracking
- System-wide oversight
- Detailed prescription information

## Status Indicators

The system uses clear visual indicators:

### Prescription Status

- **Submitted** (Yellow): Initial state after pharmacist creation
- **Approved** (Green): Reviewed and approved by review team
- **Denied** (Red): Reviewed and denied by review team

### Shipment Status

- **Pending** (Blue): Awaiting processing
- **Fulfilled** (Light Blue): Ready for delivery
- **Delivered** (Green): Successfully delivered to patient

## Action Buttons

- **Details**: View complete prescription information
- **Review**: Access review interface (Review Team only)
- **Update Shipment**: Modify shipment status (Delivery Team only)
- **Back to List**: Return to previous view

## Security Features

1. Role-Based Access Control

- Distinct interfaces for each role
- Protected routes and actions
- Secure authentication

2. Data Protection

- Secure password handling
- Protected prescription information
- Audit trail of all actions

## Database Integration

The system maintains detailed records of:

- All prescriptions and their status
- Review decisions and timestamps
- Shipment tracking information
- User actions and changes

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## Installation

1. Clone the repository:

```bash
git clone https://github.com/yourusername/MedFormPro.git
cd MedFormPro
```

2. Install .NET 6.0 SDK if not already installed:

- Download from [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

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
dotnet run --project MedFormPro.Web
```

## Default Admin Account

- Email: admin@medformpro.com
- Password: Admin123!

## Support

For support, please email support@medformpro.com or open an issue in the repository.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Built with ASP.NET Core MVC
- Uses Entity Framework Core
- SQLite Database
- Bootstrap for UI
