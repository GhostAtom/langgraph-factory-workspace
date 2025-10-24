const execSync = require('child_process').execSync;

function getCurrentBranchName() {
  const branchName = execSync('git rev-parse --abbrev-ref HEAD').toString().trim();
  return branchName;
}

function isValidBranchName(branchName) {
  const validPatterns = ['feature/', 'bugfix/', 'hotfix/', 'release/', 'experiment/'];
  return validPatterns.some(pattern => branchName.startsWith(pattern));
}

const branchName = getCurrentBranchName();

if (!isValidBranchName(branchName)) {
  console.error(`Error: Invalid branch name "${branchName}". Branch names must start with one of: feature/, bugfix/, hotfix/, release/, experiment/`);
  process.exit(1);
}

console.log(`Branch name "${branchName}" is valid.`);
