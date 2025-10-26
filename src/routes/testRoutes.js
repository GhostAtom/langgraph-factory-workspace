const express = require('express');
const { getTests } = require('../controllers/testController');
const { authenticate } = require('../middleware/authMiddleware');

const router = express.Router();

// Define the /api/tests endpoint
router.get('/', authenticate, getTests);

module.exports = router;
