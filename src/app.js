const express = require('express');

const app = express();
const PORT = process.env.PORT || 3000;

// Health Check Endpoint
app.get('/health', (req, res) => {
  // Here you would typically include checks to your database or any external services

  // If all checks pass, send 200
  return res.status(200).json({ status: 'healthy' });

  // If there's an issue with any critical service, you might return 503
  // return res.status(503).json({ status: 'unhealthy' });
});

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
