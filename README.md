# Health Check Endpoint

## Overview
The objective of this project is to implement a health check endpoint in the existing application. This endpoint provides a simple way to verify that the application is running and operational. It returns a status indicating the health of the application, which can be used for monitoring and alerting purposes.

## API Documentation
### Health Check Endpoint
- **Endpoint**: `/health`
- **Method**: GET
- **Success Response**: 
   - **Status**: 200 OK
   - **Body**: `{ "status": "Healthy", "message": "Application is running" }`
- **Failure Response**:
   - **Status**: 500 Internal Server Error
   - **Body**: `{ "status": "Unhealthy", "message": "<error_message>" }`
   
## Run the Application
1. Clone the repository: `git clone <repository_url>`
2. Install dependencies: `npm install`
3. Start the application: `npm start`

## Tests
Run tests using:
```
npm test
```
