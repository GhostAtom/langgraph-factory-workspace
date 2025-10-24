const request = require('supertest');
const express = require('express');
const os = require('os');

const app = express();
app.get('/status', (req, res) => {
  const uptime = os.uptime();
  res.json({ uptime });
});

describe('GET /status', function() {
  it('responds with a JSON object containing the uptime', function(done) {
    request(app)
      .get('/status')
      .expect('Content-Type', /json/)
      .expect(200)
      .then(response => {
        expect(response.body).toHaveProperty('uptime');
        done();
      })
      .catch(err => done(err));
  });
});
