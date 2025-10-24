# Health Check Endpoint

## Overview
The health check endpoint provides a simple way to verify that the application is running and responsive, primarily for use by monitoring tools.

## Endpoint
- **Route**: `/health`
- **Method**: `GET`

## Responses
- **200 OK**: Application is healthy
  - `{ "status": "OK", "message": "Application is healthy" }`
- **500 Internal Server Error**: Application is unhealthy
  - `{ "status": "ERROR", "message": "Application is unhealthy", "error": "Description of error" }`

## Testing
To run the tests, use the following command:

```bash
npm test
```
