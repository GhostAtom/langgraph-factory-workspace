
const request = require('supertest');
const express = require('express');
const app = require('../src/server');

describe('GET /status', () => {
  it('should return uptime in a human-readable format', async () => {
    const response = await request(app).get('/status');
    expect(response.status).toBe(200);
    expect(response.body).toHaveProperty('uptime');
    const uptimePattern = /\d+ days, \d+ hours, \d+ minutes/;
    expect(response.body.uptime).toMatch(uptimePattern);
  });
});
