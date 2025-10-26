const express = require('express');
const app = express();
const port = 3000;

// Test endpoint
app.get('/api/test', (req, res) => {
  res.json({ message: 'Application is running' });
});

app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});
