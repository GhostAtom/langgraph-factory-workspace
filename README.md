# Status Endpoint Documentation

## Overview

This express application includes a `/status` endpoint that provides the system's uptime in a human-readable format. The uptime tells you how long the application has been running.

## How to Run

1. Ensure Node.js and npm are installed.
2. Clone the repository.
3. Install dependencies using `npm install`.
4. Start the application using `node src/server.js`.

## Endpoint

- **GET /status**
  - **Response**
    - 200 OK
    - Content: `{ "uptime": "<days> days, <hours> hours, <minutes> minutes" }`

## Testing

To run tests, use the command: `npm test`.
