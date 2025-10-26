const mongoose = require('mongoose');

const transactionSchema = new mongoose.Schema({
  transactionId: String,
  amount: Number,
  paymentMethod: String,
  timestamp: { type: Date, default: Date.now },
  status: String
});

module.exports = mongoose.model('Transaction', transactionSchema);
