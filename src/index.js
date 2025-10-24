const express = require('express');
const app = express();
const port = process.env.PORT || 3000;

// Route to return system uptime
app.get('/status', (req, res) => {
    const uptime = process.uptime();
    res.json({ uptime });
});

// Server initialization
app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
});
