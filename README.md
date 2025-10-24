# Status Endpoint

This application provides a `/status` endpoint that returns the system's uptime in seconds.

## Getting Started

### Prerequisites

- Node.js
- npm

### Installing

1. Clone the repository:
   ```
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```
   cd status-endpoint
   ```
3. Install dependencies:
   ```
   npm install
   ```

### Running the Application

To start the server, run:
``` 
npm start 
```

The application will be running on `http://localhost:3000`.

### Using the `/status` Endpoint

Send a GET request to:
```
http://localhost:3000/status
```
The response will be a JSON object:
```
{ "uptime": <uptime_in_seconds> }
```

### Running Tests

To execute tests, run:
```
npm test
```
