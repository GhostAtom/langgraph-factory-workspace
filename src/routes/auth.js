const express = require('express');
const passport = require('passport');
require('../strategies/google');
require('../strategies/facebook');
require('../strategies/github');

const router = express.Router();

// Google Auth Routes
router.get('/google', passport.authenticate('google', {
  scope: ['profile', 'email']
}));

router.get('/google/callback', passport.authenticate('google', {
  failureRedirect: '/login'
}), (req, res) => {
  res.redirect('/');
});

// Facebook Auth Routes
router.get('/facebook', passport.authenticate('facebook'));

router.get('/facebook/callback', passport.authenticate('facebook', {
  failureRedirect: '/login'
}), (req, res) => {
  res.redirect('/');
});

// GitHub Auth Routes
router.get('/github', passport.authenticate('github'));

router.get('/github/callback', passport.authenticate('github', {
  failureRedirect: '/login'
}), (req, res) => {
  res.redirect('/');
});

module.exports = router;
