const express = require('express');
const app = express();

// Import routes
const configCheckRoute = require('./config-check');

// Use routes
app.use('/api', configCheckRoute);

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});