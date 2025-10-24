const request = require('supertest');
const app = require('../src/server');

describe('Health Check Endpoint', () => {
  it('should return 200 OK when the application is healthy', async () => {
    const res = await request(app)
      .get('/health')
      .send();

    expect(res.statusCode).toEqual(200);
    expect(res.body.status).toEqual('OK');
  });

  it('should return 500 Internal Server Error when the application is unhealthy', async () => {
    // Simulate an unhealthy state
    const unhealthyApp = express();
    unhealthyApp.get('/health', (req, res) => {
      res.status(500).send({ status: 'ERROR', message: 'Application is unhealthy' });
    });

    const res = await request(unhealthyApp)
      .get('/health')
      .send();

    expect(res.statusCode).toEqual(500);
    expect(res.body.status).toEqual('ERROR');
  });
});
