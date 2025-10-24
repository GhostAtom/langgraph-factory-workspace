
const express = require('express');
const app = express();
const PORT = process.env.PORT || 3000;

// Health check endpoint
app.get('/health', (req, res) => {
    const healthStatus = checkHealth();
    if (healthStatus.isHealthy) {
        res.status(200).json({ status: 'OK', details: healthStatus.details });
    } else {
        res.status(500).json({ status: 'ERROR', details: healthStatus.details });
    }
});

// Health check logic
function checkHealth() {
    const details = { database: 'OK', serviceA: 'OK' };
    let isHealthy = true;
    // Simulate database check
    const dbHealthy = checkDatabaseConnection();
    if (!dbHealthy) {
        isHealthy = false;
        details.database = 'FAILED';
    }

    // Simulate service check
    const serviceAHealthy = checkServiceA();
    if (!serviceAHealthy) {
        isHealthy = false;
        details.serviceA = 'FAILED';
    }
    
    return { isHealthy, details };
}

function checkDatabaseConnection() {
    // Simulate a database check
    // Return true if the database connection is healthy, false otherwise
    return true;
}

function checkServiceA() {
    // Simulate a service availability check
    // Return true if the service is available, false otherwise
    return true;
}

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
