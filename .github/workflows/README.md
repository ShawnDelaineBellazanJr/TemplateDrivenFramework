# GitHub Actions Workflow Documentation

This directory contains the CI/CD pipeline configuration for the TemplateDrivenFramework project.

## Overview

The workflow is designed to provide comprehensive automation for building, testing, and deploying the StrangeLoopPlatform multi-agent orchestration system.

## Workflow Structure

### Jobs

1. **build-and-test**: Builds the solution and runs tests
2. **security-analysis**: Performs security scanning and dependency checks
3. **code-quality**: Runs code formatting and complexity analysis
4. **docker-build**: Builds and pushes Docker images
5. **deploy-***: Deployment jobs for different environments
6. **notify**: Sends notifications about workflow results

### Triggers

- **Push**: Automatically triggers on pushes to `main`, `master`, or `develop` branches
- **Pull Request**: Runs on PRs to `main` or `master` branches
- **Manual**: Can be triggered manually with environment selection

## Environment Variables

The workflow uses the following environment variables:

- `DOTNET_VERSION`: .NET version (9.0.x)
- `SOLUTION_FILE`: Path to the solution file
- `API_PROJECT`: Path to the API project

## Secrets Required

To use the full workflow, you need to configure these secrets in your GitHub repository:

### Docker Hub
- `DOCKER_USERNAME`: Your Docker Hub username
- `DOCKER_PASSWORD`: Your Docker Hub password or access token

### Azure (Optional)
- `AZURE_CREDENTIALS`: Service principal credentials for Azure deployment
- `AZURE_SUBSCRIPTION_ID`: Azure subscription ID

### Notifications (Optional)
- `SLACK_WEBHOOK_URL`: Slack webhook URL for notifications
- `TEAMS_WEBHOOK_URL`: Microsoft Teams webhook URL

## Usage

### Automatic Triggers

The workflow runs automatically on:
- Code pushes to protected branches
- Pull requests to main branches

### Manual Deployment

To manually trigger a deployment:

1. Go to the Actions tab in your GitHub repository
2. Select the "CI/CD Pipeline" workflow
3. Click "Run workflow"
4. Select the target environment (development, staging, production)
5. Click "Run workflow"

### Environment Protection

Production deployments require:
- Manual approval (if configured)
- Successful completion of all previous jobs
- Proper environment configuration

## Customization

### Adding New Environments

To add a new environment:

1. Create a new job in the workflow file
2. Add the environment to the workflow_dispatch inputs
3. Configure environment-specific deployment steps

### Adding New Tools

To add new analysis tools:

1. Add a new step in the appropriate job
2. Install the tool using `dotnet tool install`
3. Run the analysis
4. Upload results as artifacts if needed

### Notification Integration

To add notification integrations:

1. Add your webhook URLs as secrets
2. Modify the notify job to include your notification service
3. Customize the notification messages

## Troubleshooting

### Common Issues

1. **Build Failures**: Check .NET version compatibility
2. **Test Failures**: Review test output and fix failing tests
3. **Security Issues**: Address vulnerabilities in dependencies
4. **Deployment Failures**: Verify environment configuration and credentials

### Debugging

- Check the Actions tab for detailed logs
- Review artifact uploads for additional information
- Use workflow_dispatch for manual testing

## Security Considerations

- Secrets are encrypted and only accessible during workflow execution
- Docker images are built with security best practices
- Non-root users are used in containers
- Security scanning is performed on all builds

## Performance Optimization

- Docker layer caching is enabled
- Build artifacts are cached between runs
- Parallel job execution where possible
- Resource limits are configured for containers

## Monitoring

The workflow includes:
- Build status monitoring
- Test coverage reporting
- Security vulnerability tracking
- Deployment status notifications

## Support

For issues with the workflow:
1. Check the Actions tab for error details
2. Review the workflow configuration
3. Verify secret configuration
4. Contact the development team 