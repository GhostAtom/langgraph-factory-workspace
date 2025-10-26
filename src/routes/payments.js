const express = require('express');
const router = express.Router();

const { processStripePayment, processPayPalPayment } = require('../services/paymentService');

// Process payment route
router.post('/stripe', async (req, res) => {
  try {
    const result = await processStripePayment(req.body);
    res.status(200).json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

router.post('/paypal', async (req, res) => {
  try {
    const result = await processPayPalPayment(req.body);
    res.status(200).json(result);
  } catch (error) {
    res.status(500).json({ error: error.message });
  }
});

module.exports = router;
