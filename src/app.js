const express = require('express');
const app = express();

// Health check endpoint
app.get('/health', (req, res) => {
    // Example health check logic
    const isDbConnected = true;  // Simulate a database connection check
    const isServiceAvailable = true;  // Simulate a service availability check

    if (isDbConnected && isServiceAvailable) {
        res.status(200).json({ status: 'OK', message: 'Application is healthy' });
    } else {
        res.status(500).json({ status: 'ERROR', message: 'Application is unhealthy' });
    }
});

module.exports = app;