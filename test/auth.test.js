const request = require('supertest');
const app = require('../src/index'); // Assuming app is exported from index.js

describe('Authentication System', () => {
  it('should login with valid credentials', async () => {
    const res = await request(app)
      .post('/api/auth/login')
      .send({ email: 'test@example.com', password: 'password123' });

    expect(res.statusCode).toEqual(200);
    expect(res.body).toHaveProperty('token');
  });

  it('should fail login with invalid credentials', async () => {
    const res = await request(app)
      .post('/api/auth/login')
      .send({ email: 'wrong@example.com', password: 'wrongpassword' });

    expect(res.statusCode).toEqual(400);
    expect(res.body).toEqual({ msg: 'Invalid credentials' });
  });
});