const request = require('supertest');
const express = require('express');

// Set up express app
const app = express();
app.use(express.json());
app.post('/api/login', (req, res) => {
    const { username, password } = req.body;
    if (username === 'user' && password === 'pass') {
        res.status(200).json({ message: 'Login successful' });
    } else {
        res.status(401).json({ message: 'Invalid credentials' });
    }
});

// Tests
describe('POST /api/login', () => {
    it('should login successfully with valid credentials', (done) => {
        request(app)
            .post('/api/login')
            .send({ username: 'user', password: 'pass' })
            .expect('Content-Type', /json/)
            .expect(200)
            .expect({ message: 'Login successful' }, done);
    });

    it('should return error message with invalid credentials', (done) => {
        request(app)
            .post('/api/login')
            .send({ username: 'invalid', password: 'invalid' })
            .expect('Content-Type', /json/)
            .expect(401)
            .expect({ message: 'Invalid credentials' }, done);
    });

    it('should return error for missing credentials', (done) => {
        request(app)
            .post('/api/login')
            .send({})
            .expect('Content-Type', /json/)
            .expect(401)
            .expect({ message: 'Invalid credentials' }, done);
    });
});
