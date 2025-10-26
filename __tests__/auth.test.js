const request = require('supertest');
const mongoose = require('mongoose');
const app = require('../src/server');

beforeAll(async () => {
  await mongoose.connect(process.env.DB_URI, { useNewUrlParser: true, useUnifiedTopology: true });
});

afterAll(async () => {
  await mongoose.disconnect();
});

describe('Authentication', () => {
  it('should register a new user', async () => {
    const res = await request(app).post('/auth/register').send({
      email: 'test@example.com',
      password: 'password123'
    });
    expect(res.statusCode).toEqual(201);
    expect(res.body).toHaveProperty('token');
  });

  it('should login an existing user', async () => {
    const res = await request(app)
      .post('/auth/login')
      .send({
        email: 'test@example.com',
        password: 'password123'
      });
    expect(res.statusCode).toEqual(200);
    expect(res.body).toHaveProperty('token');
  });

  it('should not login non-existing user', async () => {
    const res = await request(app)
      .post('/auth/login')
      .send({
        email: 'nonexisting@example.com',
        password: 'wrongpassword'
      });
    expect(res.statusCode).toEqual(401);
  });

  it('should send a password reset email', async () => {
    const res = await request(app)
      .post('/auth/reset-password')
      .send({
        email: 'test@example.com'
      });
    expect(res.statusCode).toEqual(200);
    expect(res.text).toBe('Password reset link sent');
  });
});
