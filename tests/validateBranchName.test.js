const { execSync } = require('child_process');

jest.mock('child_process');

const { isValidBranchName } = require('../scripts/validateBranchName');

test('Valid branch name is accepted', () => {
  const result = isValidBranchName('feature/new-feature');
  expect(result).toBe(true);
});

test('Invalid branch name is rejected', () => {
  const result = isValidBranchName('invalid/branch');
  expect(result).toBe(false);
});
