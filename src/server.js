const express = require('express');
const testRoute = require('./routes/testRoute');
const app = express();
const port = process.env.PORT || 3000;

// Use the test route
app.use('/api/test', testRoute);

app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});
