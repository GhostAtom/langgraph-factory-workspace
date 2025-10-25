const express = require('express');
const app = express();
const port = 3000;

app.use(express.json());
app.use(express.static('src'));

app.post('/api/login', (req, res) => {
    const { username, password } = req.body;

    if (username === 'user' && password === 'pass') {
        res.json({ success: true, message: 'Login successful' });
    } else {
        res.json({ success: false, message: 'Invalid username or password' });
    }
});

app.listen(port, () => {
    console.log(`Server is running at http://localhost:${port}`);
});
