from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from typing import Optional, List

app = FastAPI()

# Pydantic model for Book
class Book(BaseModel):
    title: str
    author: str
    year: int

# In-memory 'database'
books_db = {}
book_id_seq = 1

# Create a book
@app.post("/books", response_model=Book, status_code=201)
def create_book(book: Book):
    global book_id_seq
    books_db[book_id_seq] = book
    book_id_seq += 1
    return book

# Read all books
@app.get("/books", response_model=List[Book])
def read_books():
    return list(books_db.values())

# Read a book by ID
@app.get("/books/{book_id}", response_model=Book)
def read_book(book_id: int):
    book = books_db.get(book_id)
    if not book:
        raise HTTPException(status_code=404, detail="Book not found")
    return book

# Update a book by ID
@app.put("/books/{book_id}", response_model=Book)
def update_book(book_id: int, book: Book):
    if book_id not in books_db:
        raise HTTPException(status_code=404, detail="Book not found")
    books_db[book_id] = book
    return book

# Delete a book by ID
@app.delete("/books/{book_id}", status_code=204)
def delete_book(book_id: int):
    if book_id not in books_db:
        raise HTTPException(status_code=404, detail="Book not found")
    del books_db[book_id]
