# Health Endpoint

This application includes a `/health` endpoint that provides a simple way to verify the application's status.

## Getting Started

1. **Installation**

   ```
   npm install
   ```

2. **Running the Application**

   ```
   npm start
   ```

   This will start the application on `http://localhost:3000`.

3. **Accessing the Health Endpoint**

   Visit `http://localhost:3000/health` to receive a JSON response:

   ```json
   { "status": "healthy" }
   ```

## Testing

Run the following command to execute tests:

```bash
npm test
```

This will ensure the `/health` endpoint is functioning correctly.
