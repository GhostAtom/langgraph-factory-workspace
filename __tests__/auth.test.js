const request = require('supertest');
const express = require('express');
const passport = require('passport');
const authRoutes = require('../src/routes/auth');

const app = express();
app.use(passport.initialize());
app.use('/auth', authRoutes);

// Mock strategies
passport.use(new (require('passport-strategy'))());


describe('OAuth2 Authentication Routes', () => {
  it('should redirect to Google authentication page', (done) => {
    request(app)
      .get('/auth/google')
      .expect(302)
      .expect('Location', new RegExp('accounts\.google\.com'))
      .end(done);
  });

  it('should redirect to Facebook authentication page', (done) => {
    request(app)
      .get('/auth/facebook')
      .expect(302)
      .expect('Location', new RegExp('www\.facebook\.com'))
      .end(done);
  });

  it('should redirect to GitHub authentication page', (done) => {
    request(app)
      .get('/auth/github')
      .expect(302)
      .expect('Location', new RegExp('github\.com'))
      .end(done);
  });
});
