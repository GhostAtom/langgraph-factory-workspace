
const express = require('express');
const mongoose = require('mongoose');

const app = express();
const PORT = process.env.PORT || 3000;

// MongoDB connection setup
db = mongoose.connection;
db.on('error', () => console.error('Database connection error:'));
db.once('open', () => console.log('Database connected successfully'));

// Health check endpoint
app.get('/health', async (req, res) => {
  try {
    await mongoose.connection.db.admin().ping(); // Verify database connection
    res.status(200).json({ status: 'Healthy', message: 'Application is running' });
  } catch (error) {
    res.status(500).json({ status: 'Unhealthy', message: error.message });
  }
});

// Start the server
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
