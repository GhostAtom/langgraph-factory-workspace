# User Status API

## Overview
This project implements a new API endpoint that allows clients to retrieve the status of a user. The status indicates whether a user is active, inactive, or suspended.

## Endpoint
`GET /api/users/:id/status`

- **Response**
  - `200 OK`: Returns the user status
    ```json
    { "status": "active" }
    ```
  - `404 Not Found`: User not found

## Testing
To run tests, execute:

```bash
npm test
```

## Setup
1. Install dependencies:
```
npm install
```
2. Run the server:
```
npm start
```

## Development Notes
- Ensure MongoDB is running locally.
- Security, performance, and documentation considerations should be addressed as per the project plan.