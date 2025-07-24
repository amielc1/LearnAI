# Quick Setup Checklist

This checklist helps you complete the DevOps setup for the LearnAI repository with gated check-ins.

## ✅ Completed Automatically

- [x] Created CI/CD pipeline (`.github/workflows/ci.yml`)
- [x] Created validation pipeline (`.github/workflows/validate.yml`)
- [x] Updated README.md with development workflow
- [x] Created branch protection documentation (`docs/BRANCH_PROTECTION.md`)
- [x] Added CODEOWNERS file for automatic reviewer assignment
- [x] Fixed .NET target framework compatibility (net8.0)
- [x] Local branch structure prepared (main, develop)

## 🔧 Manual Setup Required

### 1. Push Branches to Remote
Since git push requires authentication, you'll need to:
```bash
git checkout main
git push origin main
git checkout develop  
git push origin develop
```

### 2. Set Up Branch Protection Rules
Navigate to GitHub repository settings and configure branch protection:

**For `main` branch:**
1. Go to Settings → Branches → Add rule
2. Branch name pattern: `main`
3. Enable:
   - ✅ Require a pull request before merging
   - ✅ Require approvals (1)
   - ✅ Require status checks to pass before merging
   - ✅ Require branches to be up to date before merging
   - ✅ Include administrators
4. Add required status checks:
   - `build-and-test` (from ci.yml workflow)
   - `validate-structure` (from validate.yml workflow)

**For `develop` branch (optional):**
- Similar settings but with relaxed requirements

### 3. Set Default Branch
1. Go to Settings → General → Default branch
2. Change default branch to `main`
3. Update and confirm

### 4. Test the Setup
1. Create a test feature branch
2. Make a small change and push
3. Create PR to main branch
4. Verify CI runs and status checks appear
5. Verify merge is blocked until checks pass
6. Test approval workflow

## 🎯 Verification Steps

After setup, verify:
- [ ] Direct pushes to `main` are blocked
- [ ] PRs trigger CI/CD pipeline
- [ ] Failed builds block PR merging
- [ ] Successful builds allow merging after approval
- [ ] Branch protection rules are enforced

## 📋 Next Steps

1. **Team Onboarding**: Share the updated README.md and development workflow
2. **Monitoring**: Set up notifications for build failures
3. **Security**: Consider enabling signed commits and Dependabot
4. **Documentation**: Keep branch protection docs updated as team grows

## 🔧 Troubleshooting

### Common Issues:
- **Workflow not triggering**: Ensure `.github/workflows/` files are in the right location
- **Status checks not found**: Run the workflow at least once before setting up protection
- **Windows build issues**: Ensure `runs-on: windows-latest` for WPF applications

### Support:
- See detailed setup guide: `docs/BRANCH_PROTECTION.md`
- Check workflow status in Actions tab
- Review build logs for specific errors

---
**Note**: The CI/CD pipeline uses Windows runners specifically for the WPF application. Linux runners won't work for this project type.