#!/bin/bash

# Test cases for the branch naming validation script

source ../src/branch_naming_validation.sh

test_valid_branch_names() {
  echo "Running valid branch name tests..."
  validate_branch_name "feature/add-new-feature" && echo "Pass"
  validate_branch_name "bugfix/fix-bug" && echo "Pass"
  validate_branch_name "hotfix/critical-fix" && echo "Pass"
}

test_invalid_branch_names() {
  echo "Running invalid branch name tests..."
  ! validate_branch_name "feature_underscore" && echo "Pass"
  ! validate_branch_name "feature/no-slash" && echo "Pass"
  ! validate_branch_name "feature/invalid-char!" && echo "Pass"
}

run_tests() {
  test_valid_branch_names
  test_invalid_branch_names
}

run_tests
