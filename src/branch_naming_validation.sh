#!/bin/bash

# This script validates branch names according to the defined convention.

validate_branch_name() {
  local branch_name="$1"
  local branch_regex="^(feature|bugfix|hotfix|release)/[a-z0-9\-]+$"
  if [[ $branch_name =~ $branch_regex ]]; then
    echo "Branch name is valid."
    return 0
  else
    echo "Error: Branch name '"$branch_name"' is invalid. It must match the pattern 'feature|bugfix|hotfix|release' followed by a slash and a descriptive name."
    return 1
  fi
}

if [[ "$1" == "" ]]; then
  echo "Usage: $0 <branch_name>"
  exit 1
fi

validate_branch_name "$1"
