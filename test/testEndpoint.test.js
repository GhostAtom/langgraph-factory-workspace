const request = require('supertest');
const express = require('express');
const testRoute = require('../src/routes/testRoute');

const app = express();
app.use('/api/test', testRoute);

describe('GET /api/test', () => {
  it('should return a message indicating the application is running', async () => {
    const response = await request(app).get('/api/test');
    expect(response.statusCode).toBe(200);
    expect(response.body).toHaveProperty('message', 'Application is running');
  });
  it('should return 405 for incorrect method', async () => {
    const response = await request(app).post('/api/test');
    expect(response.statusCode).toBe(405);
  });
  it('should return 404 for incorrect URL', async () => {
    const response = await request(app).get('/api/wrong');
    expect(response.statusCode).toBe(404);
  });
});
