const express = require('express');
const router = express.Router();

// Handle GET request for the test endpoint
router.get('/', (req, res) => {
  res.json({ message: 'Application is running' });
});

module.exports = router;
