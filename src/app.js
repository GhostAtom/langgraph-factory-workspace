const express = require('express');
const os = require('os');

const app = express();
const PORT = process.env.PORT || 3000;

// /status endpoint
app.get('/status', (req, res) => {
  const uptime = os.uptime();
  res.json({ uptime });
});

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
