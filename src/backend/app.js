const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
app.use(cors());
app.use(bodyParser.json());

let yos = [];

app.post('/sendYo', (req, res) => {
    const { friend } = req.body;
    yos.push({ friend, timestamp: new Date() });
    res.status(200).send({ message: `Yo sent to ${friend}` });
});

app.get('/getYos', (req, res) => {
    res.status(200).json(yos);
});

const port = process.env.PORT || 3001;
app.listen(port, () => {
    console.log(`Server running on port ${port}`);
});
