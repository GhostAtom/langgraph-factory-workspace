const request = require('supertest');
const express = require('express');
const jwt = require('jsonwebtoken');
const configCheckRoute = require('../src/config-check');

const app = express();
app.use('/api', configCheckRoute);

// Mock JWT secret
const TOKEN_SECRET = 'testsecret';

// Create a test token
const testToken = jwt.sign({ username: 'testuser' }, TOKEN_SECRET);

// Mock environment variables
process.env.ACCESS_TOKEN_SECRET = TOKEN_SECRET;

// Test cases
describe('GET /config-check', () => {
  it('should return config data with valid token', (done) => {
    request(app)
      .get('/api/config-check')
      .set('Authorization', `Bearer ${testToken}`)
      .expect('Content-Type', /json/)
      .expect(200)
      .expect((res) => {
        expect(res.body).toEqual({
          server: 'localhost',
          port: 3000
        });
      })
      .end(done);
  });

  it('should return 401 without token', (done) => {
    request(app)
      .get('/api/config-check')
      .expect(401, done);
  });

  it('should return 403 with invalid token', (done) => {
    request(app)
      .get('/api/config-check')
      .set('Authorization', 'Bearer invalidtoken')
      .expect(403, done);
  });
});