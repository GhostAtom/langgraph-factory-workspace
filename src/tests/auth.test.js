const request = require('supertest');
const express = require('express');
const session = require('express-session');
const authRoutes = require('../authRoutes');

const app = express();

app.use(session({
  secret: 'test_secret',
  resave: false,
  saveUninitialized: true,
}));

app.use('/auth', authRoutes);

app.get('/dashboard', (req, res) => {
  if (req.isAuthenticated()) {
    res.send('Dashboard');
  } else {
    res.redirect('/');
  }
});

jest.mock('passport', () => ({
  authenticate: (strategy, options, callback) => (req, res, next) => {
    req.isAuthenticated = () => true;
    callback(null, {}, null);
  }
}));

describe('Auth Route Tests', () => {
  test('Should redirect to /dashboard on successful Google authentication', async () => {
    const response = await request(app).get('/auth/google/callback');
    expect(response.statusCode).toBe(302);
    expect(response.headers.location).toBe('/dashboard');
  });

  // Add tests for Facebook and GitHub similarly
});
