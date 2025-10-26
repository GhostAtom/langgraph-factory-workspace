# User Authentication System

This project implements a basic user authentication system using Node.js, Express, and MongoDB. It includes functionalities for user registration, login, logout, password reset, and session management.

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd auth-system
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Create a `.env` file based on the `.env.example` and fill in your credentials.

4. Run the server:
   ```bash
   npm start
   ```

5. Run tests:
   ```bash
   npm test
   ```

## API Endpoints

- `POST /auth/login`: Logs in a user
- `POST /auth/logout`: Logs out a user
- `POST /auth/reset-password`: Sends a password reset email
- `POST /auth/update-password`: Updates a user's password

## Security Considerations

- Passwords are hashed using `bcrypt` before saving to the database.
- JWT is used for secure session management.
- Sensitive data such as database URI and email credentials are stored in environment variables.
