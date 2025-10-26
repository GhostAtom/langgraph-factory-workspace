const request = require('supertest');
const app = require('../server');
const jwt = require('jsonwebtoken');

jest.mock('../services/configService', () => ({
    getConfig: jest.fn().mockResolvedValue({ key: 'value' })
}));

const validToken = jwt.sign({ id: 'testuser' }, process.env.JWT_SECRET);

describe('GET /api/config', () => {
    it('should return 200 and configuration settings for valid request', async () => {
        const res = await request(app)
            .get('/api/config')
            .set('Authorization', validToken);
        expect(res.statusCode).toBe(200);
        expect(res.body).toHaveProperty('key', 'value');
    });

    it('should return 401 for invalid authentication', async () => {
        const res = await request(app)
            .get('/api/config')
            .set('Authorization', 'invalidToken');
        expect(res.statusCode).toBe(401);
    });
});