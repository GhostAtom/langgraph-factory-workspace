const mongoose = require('mongoose');

const testSchema = new mongoose.Schema({
  name: { type: String, required: true },
  subject: String,
  date: Date,
});

const Test = mongoose.model('Test', testSchema);

module.exports = Test;
