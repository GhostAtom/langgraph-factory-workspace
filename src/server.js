const express = require('express');
const app = express();
const PORT = process.env.PORT || 3000;

// Health check endpoint
app.get('/health', async (req, res) => {
  try {
    // Basic health check logic
    // Add any additional checks here (e.g., database connectivity)
    res.status(200).send({ status: 'OK', message: 'Application is healthy' });
  } catch (error) {
    res.status(500).send({ status: 'ERROR', message: 'Application is unhealthy', error: error.message });
  }
});

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});

module.exports = app;
