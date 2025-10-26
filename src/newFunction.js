function newFunction(input) {
  if (typeof input !== 'string') {
    throw new Error('Invalid input: Input should be a string');
  }
  return input.toUpperCase();
}

module.exports = newFunction;
