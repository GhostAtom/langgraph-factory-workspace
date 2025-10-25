const express = require('express');
const passport = require('passport');
require('./passportSetup');

const router = express.Router();

router.get('/google', passport.authenticate('google', {
  scope: ['profile', 'email']
}));

router.get('/google/callback', passport.authenticate('google', {
  failureRedirect: '/'
}), (req, res) => {
  res.redirect('/dashboard');
});

router.get('/facebook', passport.authenticate('facebook'));

router.get('/facebook/callback', passport.authenticate('facebook', {
  failureRedirect: '/'
}), (req, res) => {
  res.redirect('/dashboard');
});

router.get('/github', passport.authenticate('github', {
  scope: ['user:email']
}));

router.get('/github/callback', passport.authenticate('github', {
  failureRedirect: '/'
}), (req, res) => {
  res.redirect('/dashboard');
});

module.exports = router;