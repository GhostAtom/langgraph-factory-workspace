const newFunction = require('./newFunction');

try {
  console.log(newFunction('hello world'));
} catch (error) {
  console.error(error.message);
}
