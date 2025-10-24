const request = require('supertest');
const express = require('express');
const os = require('os');

const app = express();

app.get('/status', (req, res) => {
  const uptime = os.uptime();
  res.json({ uptime });
});

// Test for /status endpoint
describe('GET /status', () => {
  it('should return system uptime in JSON format', async () => {
    const response = await request(app).get('/status');
    expect(response.status).toBe(200);
    expect(response.headers['content-type']).toEqual(expect.stringContaining('json'));
    expect(response.body).toHaveProperty('uptime');
    expect(typeof response.body.uptime).toBe('number');
  });
});
