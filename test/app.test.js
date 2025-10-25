const request = require('supertest');
const app = require('../src/app');

describe('Test Endpoint', () => {
  it('GET /api/test should return application running message', (done) => {
    request(app)
      .get('/api/test')
      .expect('Content-Type', /json/)
      .expect(200)
      .expect({ message: 'Application is running' }, done);
  });

  it('POST /api/test should return method not allowed', (done) => {
    request(app)
      .post('/api/test')
      .expect(404, done);
  });

  it('GET /invalid should return 404', (done) => {
    request(app)
      .get('/invalid')
      .expect(404, done);
  });
});
