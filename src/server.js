
const express = require('express');
const app = express();
const PORT = process.env.PORT || 3000;

// Function to format uptime in a human-readable way
function formatUptime(seconds) {
  const days = Math.floor(seconds / (3600 * 24));
  const hours = Math.floor((seconds % (3600 * 24)) / 3600);
  const minutes = Math.floor((seconds % 3600) / 60);
  return `${days} days, ${hours} hours, ${minutes} minutes`;
}

// /status endpoint
app.get('/status', (req, res) => {
  const uptime = process.uptime();
  const humanReadableUptime = formatUptime(uptime);
  res.json({ uptime: humanReadableUptime });
});

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
