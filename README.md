# OAuth2 Authentication

## Overview
This project implements OAuth2-based user authentication using Node.js and Express. Users can log in using Google, Facebook, or GitHub.

## Setup

1. Clone the repository.
2. Install dependencies:
   ```
   npm install
   ```
3. Create a `.env` file with your OAuth2 credentials:
   ```
   GOOGLE_CLIENT_ID=your_google_client_id
   GOOGLE_CLIENT_SECRET=your_google_client_secret
   FACEBOOK_CLIENT_ID=your_facebook_client_id
   FACEBOOK_CLIENT_SECRET=your_facebook_client_secret
   GITHUB_CLIENT_ID=your_github_client_id
   GITHUB_CLIENT_SECRET=your_github_client_secret
   PORT=3000
   ```

## Running the Application

Start the server:
```
npm start
```

## Testing

To run the tests:
```
npm test
```