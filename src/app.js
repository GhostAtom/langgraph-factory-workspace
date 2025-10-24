
const express = require('express');
const os = require('os');

const app = express();

app.get('/status', (req, res) => {
  const uptime = os.uptime();
  res.json({ uptime });
});

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});
