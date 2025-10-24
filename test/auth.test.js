const request = require('supertest');
const express = require('express');
const session = require('express-session');
const passport = require('passport');
const GoogleStrategy = require('passport-google-oauth20').Strategy;

const app = express();
app.use(session({ secret: 'test_secret', resave: false, saveUninitialized: true }));
app.use(passport.initialize());
app.use(passport.session());

passport.use(new GoogleStrategy(
  {
    clientID: 'test-client-id',
    clientSecret: 'test-client-secret',
    callbackURL: '/auth/google/callback'
  },
  function(token, tokenSecret, profile, done) {
    return done(null, profile);
  }
));

passport.serializeUser((user, done) => {
  done(null, user);
});

passport.deserializeUser((obj, done) => {
  done(null, obj);
});

app.get('/auth/google', passport.authenticate('google', { scope: ['profile'] }));
app.get('/auth/google/callback', passport.authenticate('google', { failureRedirect: '/' }), (req, res) => {
  res.redirect('/dashboard');
});

app.get('/dashboard', (req, res) => {
  if (req.isAuthenticated()) {
    res.send('Welcome to the dashboard');
  } else {
    res.redirect('/');
  }
});

// Test case
describe('GET /auth/google', () => {
  it('should redirect to Google authentication', (done) => {
    request(app)
      .get('/auth/google')
      .expect(302)
      .expect('Location', /accounts.google.com/, done);
  });
});