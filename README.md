# Delete Config Endpoint

## Overview

This is a Node.js application built with Express and MongoDB for managing configurations. It includes a secure delete configuration endpoint that allows authorized users to remove configurations from the database.

## Getting Started

1. **Clone the Repository**
   ```bash
   git clone <repo-url>
   cd <repo-directory>
   ```

2. **Install Dependencies**
   ```bash
   npm install
   ```

3. **Set up Environment Variables**
   Create a `.env` file in the root directory and set the following variables:
   ```bash
   ACCESS_TOKEN_SECRET=your_secret_key
   ```

4. **Run the Application**
   ```bash
   node src/server.js
   ```

5. **Running Tests**
   ```bash
   npm test
   ```

## API Endpoints

- **DELETE /config/:id** - Deletes a configuration by ID
  - Headers: 
    - Authorization: Bearer `<JWT>`

## Security

- JWT Authentication is used to secure endpoints.
- Ensure your `ACCESS_TOKEN_SECRET` is strong and kept confidential.

## License

This project is licensed under the MIT License.
