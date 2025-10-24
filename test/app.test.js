const request = require('supertest');
const app = require('../src/app');

describe('GET /status', () => {
    it('should return system uptime in JSON format', async () => {
        const response = await request(app).get('/status');
        expect(response.statusCode).toBe(200);
        expect(response.body).toHaveProperty('uptime');
        expect(typeof response.body.uptime).toBe('number');
    });
});