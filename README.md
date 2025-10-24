# Branch Naming Convention Validation

## Overview
This project enforces a branch naming convention for consistent and clear collaboration across development teams.

## Branch Naming Guidelines
Branches must adhere to the following naming convention:
- Must start with one of the categories: `feature`, `bugfix`, `hotfix`, or `release`.
- Must be followed by a descriptive name separated by a slash, containing only lowercase letters, numbers, and hyphens.

### Examples:
- `feature/add-login`
- `bugfix/fix-login-button`

## Setup & Usage
1. Ensure you have Bash shell access.
2. Run the validation script by passing the branch name to check:
   ```bash
   ./src/branch_naming_validation.sh "feature/your-branch-name"
   ```

## Testing
Use the provided test script to verify the validity of branch names:
```bash
./test/test_branch_validation.sh
```

All valid and invalid naming patterns are tested in this script.

## Documentation
- See the [contribution guidelines](CONTRIBUTING.md) for setting up a development environment and contribution steps.
