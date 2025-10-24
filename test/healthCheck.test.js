const request = require('supertest');
const express = require('express');

const app = require('../src/index');

describe('GET /health', () => {
    it('should return 200 OK with all services healthy', async () => {
        const res = await request(app).get('/health');
        expect(res.statusCode).toEqual(200);
        expect(res.body.status).toEqual('OK');
    });

    it('should return 500 Internal Server Error when a service is down', async () => {
        // Mock a service failure
        jest.mock('../src/index', () => ({
            ...jest.requireActual('../src/index'),
            checkServiceA: jest.fn(() => false)
        }));

        const res = await request(app).get('/health');
        expect(res.statusCode).toEqual(500);
        expect(res.body.status).toEqual('ERROR');
    });
});
