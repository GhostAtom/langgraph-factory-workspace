# Configuration API

## Overview
This project adds a secure API endpoint to retrieve configuration settings. It returns a JSON response with necessary configuration data.

## Setup and Installation
1. Clone the repository
2. Install dependencies:
   ```bash
   npm install
   ```
3. Set environment variables:
   - `JWT_SECRET`
   - `MONGO_URI`
4. Run the server:
   ```bash
   npm start
   ```

## Testing
To run tests, use
```bash
npm test
```

## API Endpoint
- GET `/api/config`
  - Retrieves configuration settings
  - Requires JWT authentication in the `Authorization` header