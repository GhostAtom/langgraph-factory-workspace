const request = require('supertest');
const express = require('express');
const session = require('express-session');
const passport = require('passport');

const app = express();
app.use(session({ secret: 'test', resave: false, saveUninitialized: true }));
app.use(passport.initialize());
app.use(passport.session());

// Mock Google authentication
app.get('/auth/google', (req, res) => res.send('Google auth here'));

app.get('/dashboard', (req, res) => {
  if (!req.isAuthenticated()) {
    return res.redirect('/');
  }
  res.send(`Hello User!`);
});

describe('GET /auth/google', () => {
  it('should initiate google authentication', (done) => {
    request(app)
      .get('/auth/google')
      .expect(200)
      .expect('Google auth here', done);
  });
});
