const express = require('express');
const app = express();
const port = process.env.PORT || 3000;

// Health Endpoint
app.get('/health', (req, res) => {
  res.status(200).json({ status: 'healthy' });
});

app.listen(port, () => {
  console.log(`App is running on http://localhost:${port}`);
});
