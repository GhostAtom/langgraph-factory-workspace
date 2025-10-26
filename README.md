# Configuration API

## Overview
This API allows the deletion of configurations from the database.

## Endpoints
- **DELETE /api/configs/:id** - Deletes a configuration by its ID. Requires authentication.

## Setup
1. Clone the repository.
2. Run `npm install` to install dependencies.
3. Set MongoDB URI and JWT secret in `src/config.js`.
4. Start the server with `node src/server.js`.
5. To run tests, execute `jest` or `npm test`.

## Security
- Each request must include a valid JWT token in the Authorization header.

## Tests
- Tests are written using Jest and Supertest.
- Ensure MongoDB instance is running before executing tests.
