# Configuration Check API

## Overview

This API provides an endpoint to check the current configuration settings of the application, excluding sensitive information.

## Endpoint

### GET /api/config-check

- **Description**: Retrieve the safe configuration of the application.
- **Authentication**: Requires JWT token.

#### Responses:
- `200 OK` - Successful retrieval of configuration.
- `401 Unauthorized` - If no token is provided.
- `403 Forbidden` - If the token is invalid.

## Environment Variables

- `ACCESS_TOKEN_SECRET` - Secret key for JWT token verification.

## Testing

Run the tests using `npm test` to verify the functionality of the endpoint.