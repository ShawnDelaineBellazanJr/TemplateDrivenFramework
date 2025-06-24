# OpenAPI → Code → Test → Deploy Workflow

## Overview

The StrangeLoopPlatform implements a comprehensive "OpenAPI → Code → Test → Deploy" workflow that leverages AI agents and NSwag code generation to automatically create, test, and deploy code from OpenAPI specifications. This workflow is orchestrated through the Process Framework with full agent collaboration.

## Architecture Components

### Core Services
- **NSwagCodeGenerationService**: Generates C# client code from OpenAPI specs
- **ProcessFrameworkOrchestrator**: Manages the multi-phase improvement workflow
- **DynamicCodeService**: Compiles and executes generated code using Roslyn
- **SemanticMemoryService**: Stores and retrieves context and learnings
- **ApiPluginService**: Manages dynamic API endpoints

### Agent Roles
- **EnterpriseArchitect**: Designs system architecture and integration patterns
- **EnterpriseBusinessAnalyst**: Analyzes requirements and business context
- **EnterpriseSeniorDeveloper**: Implements production-grade code
- **EnterpriseQualityAssurance**: Validates code quality and security
- **EnterpriseReflector**: Assesses quality and identifies improvements
- **EnterpriseOrchestrator**: Coordinates multi-agent collaboration

## Workflow Phases

### Phase 1: Planning
- **Agent**: EnterpriseArchitect + EnterpriseBusinessAnalyst
- **Activities**:
  - Analyze OpenAPI specification
  - Design system architecture
  - Identify integration patterns
  - Plan implementation approach
- **Output**: Architecture plan with component specifications

### Phase 2: Coding
- **Agent**: EnterpriseSeniorDeveloper + NSwagCodeGenerationService
- **Activities**:
  - Generate C# client code from OpenAPI spec
  - Implement service interfaces
  - Create integration components
  - Apply enterprise patterns
- **Output**: Compiled and validated code

### Phase 3: Testing
- **Agent**: EnterpriseQualityAssurance + DynamicCodeService
- **Activities**:
  - Generate test cases automatically
  - Execute unit and integration tests
  - Validate security and performance
  - Verify Aspire component health
- **Output**: Test results and quality metrics

### Phase 4: Reflection
- **Agent**: EnterpriseReflector
- **Activities**:
  - Assess overall solution quality
  - Identify improvement opportunities
  - Store learnings in semantic memory
  - Plan next iteration if needed
- **Output**: Quality assessment and improvement recommendations

## API Endpoints

### Process Framework Endpoints

#### Start Self-Improvement Process
```http
POST /api/ProcessFramework/start
Content-Type: application/json

{
  "title": "Generate User Management API Client",
  "description": "Create a C# client for the User Management API using OpenAPI specification",
  "goals": [
    "Generate type-safe C# client",
    "Implement authentication",
    "Add comprehensive error handling",
    "Include unit tests"
  ],
  "constraints": [
    "Must use .NET 9",
    "Follow enterprise security standards",
    "Include 80% code coverage"
  ],
  "priority": "High",
  "timeLimit": "00:30:00",
  "context": {
    "openApiUrl": "https://api.example.com/swagger/v1/swagger.json",
    "targetFramework": "net9.0",
    "authenticationType": "Bearer"
  }
}
```

#### Execute Next Phase
```http
POST /api/ProcessFramework/{processId}/next
```

#### Get Process Status
```http
GET /api/ProcessFramework/{processId}
```

#### Execute Complete Process
```http
POST /api/ProcessFramework/execute
Content-Type: application/json

{
  "title": "Complete OpenAPI to Code Generation",
  "description": "Generate, test, and validate C# client from OpenAPI spec",
  "goals": ["Generate client", "Test functionality", "Validate quality"],
  "constraints": ["Enterprise standards", "Security compliance"],
  "priority": "High",
  "context": {
    "openApiUrl": "https://api.example.com/swagger/v1/swagger.json"
  }
}
```

#### Cancel Process
```http
POST /api/ProcessFramework/{processId}/cancel
```

#### Get Active Processes
```http
GET /api/ProcessFramework/active
```

### Dynamic Code Generation Endpoints

#### Compile and Test Code
```http
POST /api/ProcessFramework/compile
Content-Type: application/json

{
  "sourceCode": "public class Calculator { public int Add(int a, int b) => a + b; }",
  "functionName": "Add",
  "parameters": [5, 3],
  "executeAfterCompile": true
}
```

#### Get Compilation Metrics
```http
GET /api/ProcessFramework/compile/metrics
```

### Memory Management Endpoints

#### Get Memory Metrics
```http
GET /api/ProcessFramework/memory/metrics
```

#### Search Memory
```http
GET /api/ProcessFramework/memory/search?query=OpenAPI generation&limit=10
```

### Dynamic API Endpoints

#### Get Available Plugins
```http
GET /api/DynamicApi/plugins
```

#### Create Dynamic Endpoint
```http
POST /api/DynamicApi/endpoints
Content-Type: application/json

{
  "name": "UserService",
  "methods": [
    {
      "name": "GetUsers",
      "httpMethod": "GET",
      "route": "/users",
      "parameters": []
    }
  ]
}
```

#### Reload OpenAPI Plugin
```http
POST /api/DynamicApi/reload-openapi
```

## Step-by-Step Workflow

### 1. Start the Workflow

```bash
# Start the API server
cd src
dotnet run --project StrangeLoopPlatform.Api

# In another terminal, start the workflow
curl -X POST http://localhost:5000/api/ProcessFramework/start \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Generate User Management API Client",
    "description": "Create a C# client for the User Management API",
    "goals": ["Generate type-safe C# client", "Implement authentication"],
    "constraints": ["Must use .NET 9", "Follow enterprise standards"],
    "priority": "High",
    "context": {
      "openApiUrl": "https://api.example.com/swagger/v1/swagger.json"
    }
  }'
```

### 2. Monitor Process Progress

```bash
# Get the process ID from the response and monitor progress
curl http://localhost:5000/api/ProcessFramework/{processId}

# Execute the next phase
curl -X POST http://localhost:5000/api/ProcessFramework/{processId}/next
```

### 3. Review Generated Code

The generated code will be available in the process artifacts:

```json
{
  "id": "process-id",
  "currentPhase": "Testing",
  "artifacts": {
    "coding_output": "Generated C# client code...",
    "test_cases": "Generated test cases...",
    "reflection_analysis": "Quality assessment..."
  }
}
```

### 4. Test Generated Code

```bash
# Compile and test the generated code
curl -X POST http://localhost:5000/api/ProcessFramework/compile \
  -H "Content-Type: application/json" \
  -d '{
    "sourceCode": "Generated code here...",
    "functionName": "Main",
    "executeAfterCompile": true
  }'
```

## Configuration

### NSwag Configuration

The NSwag code generation service supports the following configuration:

```json
{
  "NSwagCodeGeneration": {
    "DefaultNamespace": "Generated",
    "GenerateClientClasses": true,
    "GenerateDataContracts": true,
    "GenerateExceptionClasses": true,
    "GenerateResponseClasses": true,
    "WrapDtoExceptions": true,
    "UseHttpClientCreationMethod": true,
    "UseHttpRequestMessageCreationMethod": true,
    "GenerateOptionalParameters": true,
    "GenerateDataAnnotations": true,
    "GenerateDefaultValues": true,
    "GenerateJsonMethods": true,
    "GenerateCloneMethod": false,
    "GenerateCompareToMethod": false,
    "GenerateToStringMethod": false,
    "GenerateEqualMethod": false,
    "GenerateGetHashCodeMethod": false
  }
}
```

### Agent Configuration

Each agent can be configured with specific parameters:

```json
{
  "Agents": {
    "EnterpriseArchitect": {
      "temperature": 0.2,
      "maxTokens": 4000
    },
    "EnterpriseSeniorDeveloper": {
      "temperature": 0.2,
      "maxTokens": 4000
    },
    "EnterpriseQualityAssurance": {
      "temperature": 0.1,
      "maxTokens": 4000
    }
  }
}
```

## Quality Gates

The workflow includes comprehensive quality gates:

### Code Quality
- **Code Coverage**: ≥ 80%
- **Cyclomatic Complexity**: ≤ 10 per method
- **Maintainability Index**: ≥ 65
- **Security Scan**: No critical vulnerabilities

### Performance
- **Response Time**: < 200ms for 95th percentile
- **Memory Usage**: Within allocated limits
- **Compilation Time**: < 30 seconds

### Security
- **Static Analysis**: No critical or high severity issues
- **Dynamic Analysis**: No exploitable vulnerabilities
- **Secret Scanning**: No hardcoded secrets

### Aspire Integration
- **Component Health**: All components healthy
- **Service Discovery**: All services registered
- **Configuration**: Valid configuration

## Error Handling

### Common Issues and Solutions

#### OpenAPI Spec Not Accessible
```json
{
  "error": "Failed to load OpenAPI specification",
  "solution": "Verify the OpenAPI URL is accessible and returns valid JSON"
}
```

#### Code Generation Failure
```json
{
  "error": "NSwag code generation failed",
  "solution": "Check OpenAPI spec validity and NSwag configuration"
}
```

#### Compilation Errors
```json
{
  "error": "Generated code compilation failed",
  "solution": "Review generated code and fix syntax issues"
}
```

#### Test Failures
```json
{
  "error": "Generated tests are failing",
  "solution": "Review test cases and fix implementation issues"
}
```

## Monitoring and Observability

### Process Metrics
- **Execution Time**: Per phase and total
- **Success Rate**: Percentage of successful processes
- **Error Rate**: Types and frequency of errors
- **Resource Usage**: Memory and CPU consumption

### Agent Performance
- **Response Time**: Per agent execution
- **Quality Score**: Output quality assessment
- **Learning Rate**: Improvement over time

### Code Generation Metrics
- **Generation Time**: Time to generate code
- **Code Quality**: Automated quality metrics
- **Test Coverage**: Generated test coverage

## Best Practices

### OpenAPI Specification
- Ensure the OpenAPI spec is valid and complete
- Include comprehensive examples and descriptions
- Use proper HTTP status codes and error responses
- Document authentication requirements

### Process Configuration
- Set appropriate time limits for each phase
- Define clear goals and constraints
- Include relevant context information
- Monitor process execution and intervene if needed

### Code Quality
- Review generated code before deployment
- Run additional security scans
- Validate performance characteristics
- Test integration with existing systems

## Troubleshooting

### Debug Process Issues
```bash
# Get detailed process information
curl http://localhost:5000/api/ProcessFramework/{processId}

# Check process logs
# Look for errors in the process artifacts
```

### Debug Code Generation
```bash
# Test NSwag service directly
curl -X POST http://localhost:5000/api/ProcessFramework/compile \
  -H "Content-Type: application/json" \
  -d '{"sourceCode": "test code", "functionName": "Test"}'
```

### Debug Memory Issues
```bash
# Check memory metrics
curl http://localhost:5000/api/ProcessFramework/memory/metrics

# Search for relevant context
curl "http://localhost:5000/api/ProcessFramework/memory/search?query=OpenAPI"
```

## Future Enhancements

### Planned Features
- **Multi-language Support**: Generate clients in multiple languages
- **Advanced Testing**: AI-generated integration tests
- **Deployment Automation**: Automatic deployment to cloud platforms
- **Performance Optimization**: AI-driven code optimization
- **Security Hardening**: Advanced security analysis and hardening

### Integration Opportunities
- **CI/CD Pipelines**: Integration with Azure DevOps and GitHub Actions
- **Cloud Platforms**: Azure, AWS, and Google Cloud integration
- **Monitoring**: Application Insights and other monitoring tools
- **Security**: Azure Key Vault and other security services

---

This workflow enables rapid, high-quality code generation from OpenAPI specifications with full AI agent orchestration, ensuring enterprise-grade quality and security standards. 