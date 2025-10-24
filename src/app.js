const express = require('express');
const app = express();
const port = process.env.PORT || 3000;

// Health check endpoint
app.get('/health', (req, res) => {
  const healthStatus = getHealthStatus();
  if (healthStatus.healthy) {
    return res.status(200).json({ status: 'healthy', message: 'Application is running smoothly.' });
  }
  return res.status(500).json({ status: 'unhealthy', message: 'Application is facing some issues.' });
});

// Dummy health checker function
function getHealthStatus() {
  // Implement actual health checks here (e.g., DB connectivity, external service status)
  return { healthy: true };
}

app.listen(port, () => {
  console.log(`Server is up and running on http://localhost:${port}`);
});
