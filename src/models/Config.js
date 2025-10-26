const mongoose = require('mongoose');

const configSchema = new mongoose.Schema({
  name: String,
  value: String,
  createdAt: {
    type: Date,
    default: Date.now
  }
});

module.exports = mongoose.model('Config', configSchema);
