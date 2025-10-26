const request = require('supertest');
const app = require('../app');

describe('Yo API', () => {
    it('should send a Yo', async () => {
        const res = await request(app)
            .post('/sendYo')
            .send({ friend: 'John' });
        expect(res.statusCode).toEqual(200);
        expect(res.body.message).toBe('Yo sent to John');
    });

    it('should get all Yos', async () => {
        const res = await request(app).get('/getYos');
        expect(res.statusCode).toEqual(200);
        expect(res.body).toBeInstanceOf(Array);
    });
});
