# Login Button Bug Fix

## Overview

This project resolves a critical bug where users are unable to log in when clicking the login button. This document provides an overview of the implementation and how to run the project locally.

## Prerequisites

- Node.js
- npm

## Getting Started

1. Clone the repository:
   ```
   git clone [repository_url]
   cd project_directory
   ```

2. Install dependencies:
   ```
   npm install
   ```

3. Start the server:
   ```
   node server.js
   ```

4. Open your browser and go to `http://localhost:3000` to see the login page.

## Running Tests

To execute tests, run:
```bash
npm test
```

## Project Structure

- `src/` - Contains the HTML, CSS, and JavaScript files.
- `server.js` - The Express server handling login POST requests.
- `test/` - Contains test cases for the login functionality.

## Test Cases

- **Test Case 1:** Click the login button with valid credentials.
- **Test Case 2:** Click the login button with invalid credentials.
- **Test Case 3:** Click the login button with no credentials.

## Acceptance Criteria

- Login button successfully logs in users with valid credentials.
- Appropriate error messages are shown for invalid or missing credentials.
- The fix does not introduce new bugs or regressions.
