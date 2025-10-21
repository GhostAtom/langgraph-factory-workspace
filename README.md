# Book API Azure Functions

This project is an Azure Functions-based API for managing books. It provides operations for adding, retrieving, updating, and deleting book records.

## Prerequisites
- Azure Functions Core Tools
- .NET Core SDK
- Azure Subscription

## Setup
1. Clone the repository
2. Install the necessary tools and dependencies
3. Configure the database

## Deployment
The project includes CI/CD pipelines configured via Azure DevOps.

## Endpoints
- `POST /books`: Add a new book
- `GET /books/{id}`: Retrieve a book by ID
- `PUT /books/{id}`: Update a book
- `DELETE /books/{id}`: Delete a book

## Security
Ensure authentication and authorization are properly configured.