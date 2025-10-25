const request = require('supertest');
const express = require('express');

const app = express();
app.use(express.json());

app.post('/api/login', (req, res) => {
    const { username, password } = req.body;

    if (username === 'user' && password === 'pass') {
        res.json({ success: true, message: 'Login successful' });
    } else {
        res.json({ success: false, message: 'Invalid username or password' });
    }
});

describe('POST /api/login', () => {
    test('valid credentials', (done) => {
        request(app)
            .post('/api/login')
            .send({ username: 'user', password: 'pass' })
            .expect(200)
            .expect((res) => {
                expect(res.body.success).toBe(true);
            })
            .end(done);
    });

    test('invalid credentials', (done) => {
        request(app)
            .post('/api/login')
            .send({ username: 'user', password: 'wrong' })
            .expect(200)
            .expect((res) => {
                expect(res.body.success).toBe(false);
            })
            .end(done);
    });

    test('missing credentials', (done) => {
        request(app)
            .post('/api/login')
            .send({ username: '', password: '' })
            .expect(200)
            .expect((res) => {
                expect(res.body.success).toBe(false);
            })
            .end(done);
    });
});
