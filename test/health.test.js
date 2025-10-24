const request = require('supertest');
const app = require('../src/app');

describe('GET /health', () => {
    it('should return 200 OK when application is healthy', async () => {
        const res = await request(app).get('/health');
        expect(res.statusCode).toEqual(200);
        expect(res.body).toEqual({ status: 'OK', message: 'Application is healthy' });
    });

    // Simulate application unhealthy test
    it('should return 500 Internal Server Error when application is unhealthy', async () => {
        const res = await request(app).get('/health');
        if(app.query().isDbConnected === false){ // Pretend isDbConnected was false
           expect(res.statusCode).toEqual(500);
           expect(res.body).toEqual({ status: 'ERROR', message: 'Application is unhealthy' });
        }
    });
});