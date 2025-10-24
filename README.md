# Branch Naming Convention Check

This project is designed to enforce branch naming conventions in a Git repository through automated checks.

## Branch Naming Conventions

Branches should follow the pattern:
- `feature/`
- `bugfix/`
- `hotfix/`
- `release/`
- `experiment/`

## Setup Instructions

1. Clone the repository.
2. Install the dependencies:
   ```bash
   npm install
   ```
3. Run the tests to ensure everything is set up correctly:
   ```bash
   npm test
   ```

## CI/CD Integration

The branch naming conventions are enforced via GitHub Actions. Upon creation or push to a branch, the action defined in `.github/workflows/branch-check.yml` will execute to validate the branch name.

## Test Cases

- **Test Case 1**: Create a branch with a valid name - Expected: Branch is accepted and created.
- **Test Case 2**: Create a branch with an invalid name - Expected: Branch creation is rejected with an error message.
- **Test Case 3**: Attempt to push a branch with an invalid name - Expected: Push is rejected with an error message.

## Contributors

Please read through the `CONTRIBUTING.md` before contributing to this project.
