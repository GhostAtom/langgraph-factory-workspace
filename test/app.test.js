const request = require('supertest');
const express = require('express');

const app = express();

app.get('/health', (req, res) => {
  return res.status(200).json({ status: 'healthy' });
});

describe('GET /health', () => {
  it('should return 200 OK and healthy status', async () => {
    const res = await request(app)
      .get('/health')
      .expect('Content-Type', /json/)
      .expect(200);

    expect(res.body).toEqual({ status: 'healthy' });
  });

  // Additional tests for unhealthy scenarios or complex logic can be added here
});
