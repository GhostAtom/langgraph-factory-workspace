const express = require('express');
const mongoose = require('mongoose');
const jwt = require('jsonwebtoken');
const config = require('./config');
const Config = require('./models/Config');

const app = express();

// Middleware to verify JWT
authMiddleware = (req, res, next) => {
  const token = req.headers['authorization'];
  if (!token) return res.status(403).send('Access denied. No token provided.');
  jwt.verify(token, config.secret, (err, decoded) => {
    if (err) return res.status(403).send('Failed to authenticate token.');
    req.userId = decoded.id;
    next();
  });
};

// DELETE config endpoint
app.delete('/api/configs/:id', authMiddleware, async (req, res) => {
  try {
    const configId = req.params.id;
    const result = await Config.findByIdAndDelete(configId);
    if (!result) return res.status(404).send('Configuration not found.');
    res.status(200).send('Configuration deleted successfully.');
  } catch (err) {
    res.status(500).send('Internal server error');
  }
});

mongoose.connect(config.mongoURI, { useNewUrlParser: true, useUnifiedTopology: true })
  .then(() => app.listen(3000, () => console.log('Listening on port 3000...')))
  .catch(err => console.error('Could not connect to MongoDB...', err));
