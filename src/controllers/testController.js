const Test = require('../models/Test');

// Controller: Get Tests
delete

exports.getTests = async (req, res) => {
  try {
    // Assuming query parameters for filtering and pagination
    const tests = await Test.find(req.query);
    res.status(200).json(tests);
  } catch (error) {
    res.status(500).json({ message: 'Server Error' });
  }
};
