const request = require('supertest');
const express = require('express');
const app = express();

app.get('/status', (req, res) => {
    const uptime = process.uptime();
    res.json({ uptime });
});

describe('GET /status', () => {
    it('should return uptime in seconds', async () => {
        const response = await request(app).get('/status');
        expect(response.statusCode).toBe(200);
        expect(response.body).toHaveProperty('uptime');
        expect(typeof response.body.uptime).toBe('number');
    });
});
