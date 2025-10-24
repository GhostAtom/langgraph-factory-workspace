const request = require('supertest');
const express = require('express');
const app = express();

app.get('/health', (req, res) => {
  const healthStatus = { healthy: true };
  if (healthStatus.healthy) {
    return res.status(200).json({ status: 'healthy', message: 'Application is running smoothly.' });
  }
  return res.status(500).json({ status: 'unhealthy', message: 'Application is facing some issues.' });
});

describe('GET /health', () => {
  it('should return 200 OK when the application is healthy', (done) => {
    request(app)
      .get('/health')
      .expect('Content-Type', /json/)
      .expect(200)
      .expect({ status: 'healthy', message: 'Application is running smoothly.' })
      .end(done);
  });

  it('should return 500 Internal Server Error when the application is not healthy', (done) => {
    app.get('/health', (req, res) => res.status(500).json({ status: 'unhealthy', message: 'Application is facing some issues.' }));
    request(app)
      .get('/health')
      .expect('Content-Type', /json/)
      .expect(500)
      .expect({ status: 'unhealthy', message: 'Application is facing some issues.' })
      .end(done);
  });
});
