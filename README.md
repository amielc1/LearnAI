# LearnAI

A .NET WPF application for AI learning and experimentation.

## Development Workflow

This repository follows a Git flow branching strategy with automated CI/CD pipeline:

### Branches

- **`main`** - Production-ready code. Protected branch with gated check-ins.
- **`develop`** - Integration branch for feature development.
- **Feature branches** - Created from `develop` for individual features.

### Branch Protection Rules

The `main` branch is protected with the following requirements:
- All commits must be made via pull requests
- Pull requests must pass CI/CD pipeline before merging
- At least one approval required for pull requests
- Branch must be up to date before merging

### CI/CD Pipeline

The automated pipeline runs on:
- Pushes to `main` and `develop` branches
- Pull requests targeting `main` branch

Pipeline steps:
1. **Build Validation** - Ensures the solution builds successfully
2. **Test Execution** - Runs all unit tests
3. **Artifact Publishing** - Stores build artifacts for successful builds

### Development Process

1. **Feature Development**:
   ```bash
   git checkout develop
   git pull origin develop
   git checkout -b feature/your-feature-name
   # Make your changes
   git commit -m "Your changes"
   git push origin feature/your-feature-name
   ```

2. **Create Pull Request**:
   - Create PR from feature branch to `develop`
   - Ensure CI/CD pipeline passes
   - Request code review

3. **Release to Main**:
   - Create PR from `develop` to `main`
   - Ensure all tests pass
   - Requires approval before merge

## Building and Testing

### Prerequisites
- .NET 8.0 SDK or later
- Windows environment (for WPF application)

### Build Commands
```bash
# Restore dependencies
dotnet restore LearnAI.sln

# Build solution
dotnet build LearnAI.sln --configuration Release

# Run tests
dotnet test LearnAI.sln --configuration Release
```

## Project Structure

- **AIClient** - Main WPF application
- **AIClient.Tests** - Unit tests for the application
- **.github/workflows** - CI/CD pipeline configurations