# Branch Protection Setup Guide

This document outlines how to configure branch protection rules for the LearnAI repository to implement gated check-ins.

## Overview

The repository uses a two-branch strategy with automated CI/CD pipeline to ensure code quality:

- **main** - Protected production branch
- **develop** - Integration branch for ongoing development

## GitHub Branch Protection Configuration

### Setting up Protection for Main Branch

1. **Navigate to Repository Settings**:
   - Go to your repository on GitHub
   - Click on "Settings" tab
   - Select "Branches" from the left sidebar

2. **Add Branch Protection Rule**:
   - Click "Add rule"
   - Enter branch name pattern: `main`

3. **Configure Protection Settings**:
   - ✅ **Require a pull request before merging**
     - ✅ Require approvals: 1
     - ✅ Dismiss stale PR approvals when new commits are pushed
     - ✅ Require review from code owners (if CODEOWNERS file exists)
   
   - ✅ **Require status checks to pass before merging**
     - ✅ Require branches to be up to date before merging
     - Add required status checks:
       - `build-and-test` (from CI/CD workflow)
   
   - ✅ **Require conversation resolution before merging**
   
   - ✅ **Require signed commits** (optional, recommended for security)
   
   - ✅ **Include administrators** (applies rules to repo admins too)
   
   - ✅ **Allow force pushes** - DISABLED
   - ✅ **Allow deletions** - DISABLED

4. **Save Protection Rule**:
   - Click "Create" to apply the protection rules

### Setting up Protection for Develop Branch (Optional)

For additional protection on the develop branch:

1. Add another branch protection rule for `develop`
2. Configure similar settings but with relaxed requirements:
   - Require pull requests (optional)
   - Require status checks to pass
   - Allow force pushes for maintainers (optional)

## CI/CD Workflow Integration

The GitHub Actions workflow (`.github/workflows/ci.yml`) automatically:

1. **Triggers on**:
   - Push to `main` or `develop` branches
   - Pull requests targeting `main` branch

2. **Validates**:
   - Code builds successfully with .NET 8.0
   - All unit tests pass
   - Artifacts are generated correctly

3. **Reports Status**:
   - Success/failure status is reported back to GitHub
   - Status checks are used by branch protection rules
   - Pull requests are blocked if CI fails

## Workflow Process

### For Feature Development:
```
feature/branch → develop → main
       ↓           ↓        ↓
    Basic CI   →  Full CI → Protected CI
```

### For Hotfixes:
```
hotfix/branch → main
       ↓         ↓
    Basic CI → Protected CI
```

## Verification

After setup, verify the protection is working:

1. Try to push directly to `main` - should be blocked
2. Create a test PR with failing build - should be blocked from merging
3. Create a test PR with passing build - should allow merge after approval

## Troubleshooting

### Common Issues:

1. **Status check not found**:
   - Ensure the workflow has run at least once
   - Check that the job name matches exactly: `build-and-test`

2. **CI not triggering**:
   - Verify workflow file is in `.github/workflows/`
   - Check YAML syntax is valid
   - Ensure branch names match trigger conditions

3. **Windows runner issues**:
   - WPF applications require Windows runners
   - Ensure `runs-on: windows-latest` is specified

## Additional Security Measures

Consider implementing:

- **CODEOWNERS file** for automatic reviewer assignment
- **Signed commits** for authenticity verification
- **Dependabot** for dependency security updates
- **Secret scanning** for credential leak detection

## Maintenance

Regular tasks:
- Review and update CI/CD pipeline as needed
- Monitor build performance and optimize if necessary
- Update branch protection rules based on team growth
- Review and update documentation