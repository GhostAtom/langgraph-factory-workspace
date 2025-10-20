# FastAPI Book API

This is a simple REST API built with FastAPI, providing CRUD operations for a "Book" resource. It serves as an example for building more complex APIs.

## Requirements
- Python 3.7+

## Setup
1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd fastapi-book-api
   ```
2. Create a virtual environment:
   ```sh
   python -m venv venv
   source venv/bin/activate  # On Windows use: venv\Scripts\activate
   ```
3. Install dependencies:
   ```sh
   pip install fastapi uvicorn pytest
   ```
4. Run the application:
   ```sh
   uvicorn src.main:app --reload
   ```

## API Documentation
Visit `http://127.0.0.1:8000/docs` for interactive API documentation.

## Running Tests
Execute tests with:
```sh
pytest
```
