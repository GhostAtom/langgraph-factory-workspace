# Email Notification System

This project implements an email notification system that sends automated emails to users for specific actions such as account creation, password reset, and important updates.

## Setup and Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/email-notification-system.git
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Set the following environment variables:
   - `EMAIL_USER`: Your email address used for sending emails.
   - `EMAIL_PASS`: Your email password or app-specific password.
4. Run the server:
   ```bash
   npm start
   ```

## Testing

Run tests using the following command:
```bash
npm test
```

## Email Templates
- **Welcome Email**: Sent upon user account creation.
- **Password Reset Email**: Sent when a user requests a password reset.
- **Important Updates Email**: Sent for important updates or announcements.

## Security and Performance
- Uses TLS for secure email transmission.
- Designed to handle high email volumes with scalability in mind.
