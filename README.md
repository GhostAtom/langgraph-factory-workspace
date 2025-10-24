# OAuth2 Authentication

## Overview

This project implements OAuth2 authentication with Google, Facebook, and GitHub using Node.js and Express. Passport.js is used to manage authentication strategies.

## Getting Started

### Prerequisites
- Node.js
- npm

### Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```bash
   cd oauth2-authentication
   ```
3. Install the dependencies:
   ```bash
   npm install
   ```
4. Create a `.env` file in the root directory and add your OAuth2 credentials:
   ```
   GOOGLE_CLIENT_ID=your_google_client_id
   GOOGLE_CLIENT_SECRET=your_google_client_secret
   FACEBOOK_APP_ID=your_facebook_app_id
   FACEBOOK_APP_SECRET=your_facebook_app_secret
   GITHUB_CLIENT_ID=your_github_client_id
   GITHUB_CLIENT_SECRET=your_github_client_secret
   ```

5. Start the server:
   ```bash
   npm start
   ```

6. Visit `http://localhost:3000` in your browser.

## Testing
Run tests using
```
npm test
```

## License
This project is licensed under the MIT License.