const request = require('supertest');
const express = require('express');
const testRoutes = require('../src/routes/testRoutes');

const app = express();
app.use('/api/tests', testRoutes);

// Mock auth middleware to allow tests
jest.mock('../src/middleware/authMiddleware', () => ({
  authenticate: (req, res, next) => next(),
}));

// Test API
describe('GET /api/tests', () => {
  it('should respond with status 200 and an array of tests', async () => {
    const res = await request(app).get('/api/tests');
    expect(res.statusCode).toEqual(200);
    // Further assertions depending on mock data
  });

  it('should respond with 400 on bad request', async () => {
    const res = await request(app).get('/api/tests?invalidParam=true');
    expect(res.statusCode).toEqual(400);
  });

  it('should respond with 401 when unauthorized', async () => {
    jest.mock('../src/middleware/authMiddleware', () => ({
      authenticate: (req, res, next) => res.sendStatus(401),
    }));
    const res = await request(app).get('/api/tests');
    expect(res.statusCode).toEqual(401);
  });
});
