# Health Check Endpoint

## Overview
This project provides a simple Express.js application with a health check endpoint to verify the application's status.

## Running the Application

1. Install dependencies:
   ```bash
   npm install
   ```

2. Start the server:
   ```bash
   npm start
   ```

3. Access the health check endpoint at `http://localhost:3000/health`.

## Testing

Run the test suite using Mocha:

```bash
npm test
```

## API Documentation

- **GET /health**
  - **Response 200**: Application is healthy.
  - **Response 500**: Application is unhealthy.
  - Description: Returns the health status of the application.
