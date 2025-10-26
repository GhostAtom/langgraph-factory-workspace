# Test Endpoint Application

## Overview
This application is a basic Express.js server with a single test endpoint at `/api/test` that returns a message indicating the application is running.

## Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
2. Change into the project directory:
   ```bash
   cd test-endpoint-app
   ```
3. Install dependencies:
   ```bash
   npm install
   ```

## Running the Application

To start the application, run:
```bash
npm start
```
The server will be running at `http://localhost:3000`.

## Testing

To run the test suite, use:
```bash
npm test
```

## API Endpoint
### GET /api/test
Returns a JSON message:
- **Response:**
  - `200 OK`
  - `{ message: "Application is running" }`

### Errors
- **405 Method Not Allowed**: If you try to access the endpoint with a method other than GET.
- **404 Not Found**: If you access a route that doesn't exist.
