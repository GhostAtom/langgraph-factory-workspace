const express = require('express');
const os = require('os');

const app = express();
const port = 3000;

app.get('/status', (req, res) => {
  const uptime = os.uptime();
  res.json({ uptime });
});

app.listen(port, () => {
  console.log(`Application running on http://localhost:${port}`);
});
