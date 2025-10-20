from fastapi.testclient import TestClient
from src.main import app, books_db

client = TestClient(app)

# Test Case 1: Create a new book
def test_create_book():
    response = client.post("/books", json={"title": "Test Book", "author": "Test Author", "year": 2021})
    assert response.status_code == 201
    assert response.json() == {"title": "Test Book", "author": "Test Author", "year": 2021}

# Test Case 2: Retrieve a book by ID
def test_read_book():
    # Create a book first
    client.post("/books", json={"title": "Test Book", "author": "Test Author", "year": 2021})
    response = client.get("/books/1")
    assert response.status_code == 200
    assert response.json() == {"title": "Test Book", "author": "Test Author", "year": 2021}

# Test Case 3: Update a book's information
def test_update_book():
    # Create a book first
    client.post("/books", json={"title": "Test Book", "author": "Test Author", "year": 2021})
    response = client.put("/books/1", json={"title": "Updated Book", "author": "Updated Author", "year": 2022})
    assert response.status_code == 200
    assert response.json() == {"title": "Updated Book", "author": "Updated Author", "year": 2022}

# Test Case 4: Delete a book
def test_delete_book():
    # Create a book first
    client.post("/books", json={"title": "Test Book", "author": "Test Author", "year": 2021})
    response = client.delete("/books/1")
    assert response.status_code == 204
    assert 1 not in books_db
