const express = require('express');
const router = express.Router();
const jwt = require('jsonwebtoken');

// Mock configuration data, replace with actual implementation
const config = {
  server: 'localhost',
  port: 3000,
  // sensitive data should not be exposed
  databasePassword: '<hidden>',
  apiKey: '<hidden>'
};

// Middleware to check JWT
function authenticateToken(req, res, next) {
  const authHeader = req.headers['authorization'];
  const token = authHeader && authHeader.split(' ')[1];
  if (token == null) return res.sendStatus(401);

  jwt.verify(token, process.env.ACCESS_TOKEN_SECRET, (err, user) => {
    if (err) return res.sendStatus(403);
    req.user = user;
    next();
  });
}

// GET /config-check endpoint
router.get('/config-check', authenticateToken, (req, res) => {
  const safeConfig = {
    server: config.server,
    port: config.port
  };
  res.json(safeConfig);
});

module.exports = router;