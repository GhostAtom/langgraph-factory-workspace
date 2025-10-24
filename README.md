# Status Endpoint

This project implements a simple Express.js application with a `/status` endpoint that returns the system's uptime.

## Installation

To install the dependencies, run:

```bash
npm install
```

## Running the Application

To start the application, use:

```bash
npm start
```

The application will be running on `http://localhost:3000` by default.

## Testing

To run the tests, use:

```bash
npm test
```

## Endpoint

- `/status`: Returns the system uptime in seconds as a JSON response. Example:

  ```json
  {
    "uptime": 123456
  }
  ````

## Notes

- Make sure Node.js and npm are installed on your system.
- Adjust the port by setting the `PORT` environment variable if needed.
