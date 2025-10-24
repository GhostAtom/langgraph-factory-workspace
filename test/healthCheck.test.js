
const request = require('supertest');
const mongoose = require('mongoose');
const app = require('../src/server');

jest.mock('mongoose');

describe('Health Check Endpoint', () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it('should return 200 and Healthy status when the application is healthy', async () => {
    mongoose.connection.db.admin.mockImplementation(() => ({ ping: jest.fn().mockResolvedValue(true) }));

    const response = await request(app).get('/health');
    expect(response.status).toEqual(200);
    expect(response.body).toEqual({ status: 'Healthy', message: 'Application is running' });
  });

  it('should return 500 and Unhealthy status when the database connection fails', async () => {
    mongoose.connection.db.admin.mockImplementation(() => ({ ping: jest.fn().mockRejectedValue(new Error('Database connection failed')) }));

    const response = await request(app).get('/health');
    expect(response.status).toEqual(500);
    expect(response.body).toEqual({ status: 'Unhealthy', message: 'Database connection failed' });
  });
});
