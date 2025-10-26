const newFunction = require('../src/newFunction');

test('should return uppercase of valid input', () => {
  expect(newFunction('hello')).toBe('HELLO');
});

test('should throw error for non-string input', () => {
  expect(() => newFunction(123)).toThrow('Invalid input: Input should be a string');
});
