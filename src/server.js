const express = require('express');
const configService = require('./services/configService');
const jwtAuth = require('./middleware/jwtAuth');

const app = express();
app.use(express.json());

// Config Endpoint
app.get('/api/config', jwtAuth, async (req, res) => {
    try {
        const config = await configService.getConfig();
        res.status(200).json(config);
    } catch (error) {
        console.error('Error fetching configuration:', error);
        res.status(503).json({ message: 'Service Unavailable' });
    }
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});

module.exports = app; // for testing