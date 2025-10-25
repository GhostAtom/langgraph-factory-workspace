const request = require('supertest');
const express = require('express');
const app = express();

// Setup the health endpoint
app.get('/health', (req, res) => {
  res.status(200).json({ status: 'healthy' });
});

describe('GET /health', () => {
  it('should return 200 OK with status healthy', async () => {
    const res = await request(app).get('/health');
    expect(res.statusCode).toEqual(200);
    expect(res.body).toHaveProperty('status', 'healthy');
  });
});
