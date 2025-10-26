const mongoose = require('mongoose');

const ConfigSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true
    },
    value: {
        type: String,
        required: true
    }
});

module.exports = mongoose.model('Config', ConfigSchema);
