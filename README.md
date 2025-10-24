# Health Check Endpoint

## Overview
This application includes a health check endpoint at `/health` that returns the health status of the application and its dependencies.

## Usage
- `GET /health`
  - Returns 200 OK if the application and its services are healthy.
  - Returns 500 Internal Server Error if any major component is failing.

## How It Works
- The health endpoint checks the following services:
  - **Database connection**
  - **Service A**

## Testing
To test the health check endpoint, you can run the tests located in the `test/` directory. Use the command:

```
npm test
```
