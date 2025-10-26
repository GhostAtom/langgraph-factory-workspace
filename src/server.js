const express = require('express');
const jwt = require('jsonwebtoken');
const mongoose = require('mongoose');
const Config = require('./models/Config');

const app = express();
app.use(express.json());

// Middleware to authenticate user using JWT
authenticateToken = (req, res, next) => {
    const authHeader = req.headers['authorization'];
    const token = authHeader && authHeader.split(' ')[1];
    if (token == null) return res.sendStatus(401);
    jwt.verify(token, process.env.ACCESS_TOKEN_SECRET, (err, user) => {
        if (err) return res.sendStatus(403);
        req.user = user;
        next();
    });
};

// Delete config endpoint
app.delete('/config/:id', authenticateToken, async (req, res) => {
    try {
        const config = await Config.findById(req.params.id);
        if (!config) return res.status(404).send('Configuration does not exist');
        await Config.findByIdAndRemove(req.params.id);
        res.send('Configuration deleted successfully');
    } catch (error) {
        res.status(500).send('Server error');
    }
});

// Connect to MongoDB
mongoose.connect('mongodb://localhost:27017/app', {
    useNewUrlParser: true,
    useUnifiedTopology: true
}).then(() => console.log('MongoDB connected')).catch(err => console.error(err));

app.listen(3000, () => {
    console.log('Server running on http://localhost:3000');
});
