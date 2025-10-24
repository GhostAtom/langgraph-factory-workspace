# Status Endpoint

This is a simple Express.js application that implements a `/status` endpoint to return the system uptime.

## Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd status-endpoint
   ```

2. Install the dependencies:
   ```bash
   npm install
   ```

## Running the Application

Start the server:
```bash
npm start
```

The application will run on `http://localhost:3000`.

## Testing

To run the tests:
```bash
npm test
```

## API

- `GET /status`: Returns the system uptime in seconds.

Example response:
```json
{
    "uptime": 12345.678
}
```
