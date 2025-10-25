const express = require('express');
const app = express();
const port = process.env.PORT || 3000;

app.get('/api/test', (req, res) => {
  res.status(200).json({ message: 'Application is running' });
});

app.use((req, res, next) => {
  res.status(404).send('Not Found');
});

app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});

module.exports = app;
