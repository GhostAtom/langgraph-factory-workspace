const request = require('supertest');
const express = require('express');

const app = express();

app.get('/api/test', (req, res) => {
  res.json({ message: 'Application is running' });
});

describe('GET /api/test', () => {
  it('should return the correct message', async () => {
    const res = await request(app).get('/api/test');
    expect(res.statusCode).toEqual(200);
    expect(res.body).toHaveProperty('message', 'Application is running');
  });

  it('should return method not allowed for POST method', async () => {
    const res = await request(app).post('/api/test');
    expect(res.statusCode).toEqual(404);
  });
});
