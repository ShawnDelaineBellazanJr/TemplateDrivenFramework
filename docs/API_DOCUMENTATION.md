# StrangeLoop Platform API Documentation

## Overview

The StrangeLoop Platform is an advanced meta-programmable self-evolving AI architecture that enables AI-driven code generation, multi-agent orchestration, and continuous self-improvement loops. This API provides comprehensive endpoints for managing the entire lifecycle of AI-driven development processes.

## Base URL
```
http://localhost:5166
```

## Authentication
Currently, the API does not require authentication. All endpoints are publicly accessible.

## Response Format
All API responses follow a consistent JSON format:
- **Success**: HTTP 200 with JSON payload
- **Error**: HTTP 4xx/5xx with error details in JSON format

## API Endpoints

---

## 1. StrangeLoop Controller (`/api/StrangeLoop`)

The main controller for executing strange loop processes with multi-agent orchestration.

### 1.1 Execute Strange Loop Process
**POST** `/api/StrangeLoop/execute`

Starts a new strange loop process with AI agents working collaboratively.

**Request Body:**
```json
{
  "requirements": "Build a REST API for user management",
  "nonFunctionalRequirements": "High availability, 99.9% uptime",
  "businessContext": "E-commerce platform with 10M+ users",
  "constraints": "Must use .NET 9, Azure cloud",
  "complexity": 1,
  "securityLevel": 1,
  "maxIterations": 3,
  "enableDynamicCodeGeneration": true,
  "enableSemanticMemory": true,
  "enableDynamicApiGeneration": true
}
```

**Response:**
```json
{
  "id": "4aba113a-243c-4cce-acac-c6889be37cfd",
  "request": {
    "id": "d1736427-36f3-49f9-b4b5-2da3643990c1",
    "requirements": "Build a REST API for user management",
    "complexity": 1,
    "securityLevel": 1,
    "maxIterations": 3,
    "enableDynamicCodeGeneration": true,
    "enableSemanticMemory": true,
    "enableDynamicApiGeneration": true,
    "createdAt": "2025-06-24T02:13:04.000Z"
  },
  "status": 5,
  "currentIteration": 1,
  "agentResults": {
    "BusinessAnalyst": null,
    "Architect": null,
    "SeniorDeveloper": null,
    "QualityAssurance": null,
    "Reflector": null
  },
  "generatedArtifacts": [
    {
      "id": "e7d99ef6-3e1f-4c28-a48d-8eee107042ff",
      "type": 5,
      "name": "System Architecture Design",
      "content": "JSON architecture specification...",
      "isValid": true,
      "createdAt": "2025-06-24T02:13:45.000Z"
    }
  ],
  "qualityMetrics": {
    "codeCoverage": 0,
    "securityScore": 0,
    "performanceScore": 0,
    "overallScore": 85,
    "criticalIssues": 0,
    "highPriorityIssues": 0,
    "mediumPriorityIssues": 0,
    "lowPriorityIssues": 0,
    "allQualityGatesPassed": true
  },
  "errors": [],
  "warnings": [],
  "createdAt": "2025-06-24T02:13:04.000Z",
  "completedAt": "2025-06-24T02:14:23.000Z",
  "duration": "00:01:19.0528313"
}
```

### 1.2 Get Process Status
**GET** `/api/StrangeLoop/status/{requestId}`

Retrieves the current status of a running strange loop process.

**Response:** Same format as execute response, or 404 if process not found.

### 1.3 Cancel Process
**POST** `/api/StrangeLoop/cancel/{requestId}`

Cancels a running strange loop process.

**Response:**
```json
true
```

### 1.4 Get Active Processes
**GET** `/api/StrangeLoop/active`

Lists all currently active strange loop processes.

**Response:**
```json
[]
```

### 1.5 Health Check
**GET** `/api/StrangeLoop/health`

Returns the health status of the StrangeLoop service.

**Response:**
```json
{
  "status": "healthy",
  "timestamp": "2025-06-24T02:12:54.000Z",
  "service": "StrangeLoopPlatform",
  "version": "1.0.0"
}
```

---

## 2. Process Framework Controller (`/api/ProcessFramework`)

Advanced controller for managing self-improvement processes, dynamic code compilation, and semantic memory operations.

### 2.1 Start Self-Improvement Process
**POST** `/api/ProcessFramework/start`

Starts a new self-improvement process.

**Request Body:**
```json
{
  "title": "Optimize API Performance",
  "description": "Improve response times and throughput",
  "goals": ["Reduce latency by 50%", "Increase throughput by 200%"],
  "constraints": ["Must maintain backward compatibility"],
  "priority": "High",
  "timeLimit": "00:30:00"
}
```

### 2.2 Execute Next Phase
**POST** `/api/ProcessFramework/{processId}/next`

Executes the next phase of a running process.

### 2.3 Execute Complete Process
**POST** `/api/ProcessFramework/execute`

Executes a complete self-improvement process from start to finish.

### 2.4 Get Process Status
**GET** `/api/ProcessFramework/{processId}`

Retrieves the current status of a process.

### 2.5 Cancel Process
**POST** `/api/ProcessFramework/{processId}/cancel`

Cancels a running process.

### 2.6 Get Active Processes
**GET** `/api/ProcessFramework/active`

Lists all active process states.

### 2.7 Compile and Test Code
**POST** `/api/ProcessFramework/compile`

Dynamically compiles and executes C# code using Roslyn.

**Request Body:**
```json
{
  "sourceCode": "public static string Hello() { return \"Hello World!\"; }",
  "assemblyName": "DynamicAssembly",
  "typeName": "DynamicClass",
  "functionName": "Hello",
  "parameters": [],
  "executeAfterCompile": true
}
```

**Response:**
```json
{
  "execution": {
    "isSuccess": true,
    "output": "Hello World!",
    "executionTimeMs": 150,
    "memoryUsageBytes": 1024,
    "diagnostics": []
  }
}
```

### 2.8 Get Memory Metrics
**GET** `/api/ProcessFramework/memory/metrics`

Retrieves semantic memory service metrics.

**Response:**
```json
{
  "totalEntries": 150,
  "activeEntries": 145,
  "totalStorageBytes": 1048576,
  "averageEntrySizeBytes": 1024,
  "retrievalOperations": 500,
  "storageOperations": 200,
  "averageRetrievalTimeMs": 25.5,
  "averageStorageTimeMs": 15.2,
  "hitRate": 0.85,
  "topTags": {
    "architecture": 25,
    "performance": 20,
    "security": 15
  },
  "entriesByCategory": {
    "technical": 80,
    "business": 45,
    "process": 25
  },
  "lastUpdated": "2025-06-24T02:15:00.000Z"
}
```

### 2.9 Search Semantic Memory
**GET** `/api/ProcessFramework/memory/search?query={query}&limit={limit}`

Searches semantic memory for relevant information.

**Parameters:**
- `query` (string): Search query
- `limit` (int, optional): Maximum number of results (default: 10)

**Response:**
```json
[
  {
    "key": "api_performance_optimization",
    "content": "Performance optimization techniques for REST APIs...",
    "metadata": {
      "category": "technical",
      "tags": ["performance", "api", "optimization"]
    },
    "category": "technical",
    "tags": ["performance", "api", "optimization"],
    "createdAt": "2025-06-24T01:30:00.000Z",
    "lastAccessedAt": "2025-06-24T02:10:00.000Z",
    "lastUpdatedAt": "2025-06-24T01:30:00.000Z",
    "relevanceScore": 0.95,
    "accessCount": 5,
    "version": 1,
    "isActive": true
  }
]
```

### 2.10 Get Compilation Metrics
**GET** `/api/ProcessFramework/compile/metrics`

Retrieves compilation performance metrics.

**Response:**
```json
{
  "averageExecutionTimeMs": 125.5,
  "averageMemoryUsageBytes": 2048,
  "totalExecutions": 100,
  "successfulExecutions": 95,
  "failedExecutions": 5,
  "timeoutExecutions": 0,
  "trendData": [
    {
      "timestamp": "2025-06-24T02:00:00.000Z",
      "executionTimeMs": 120,
      "memoryUsageBytes": 2048,
      "wasSuccessful": true
    }
  ],
  "optimizationRecommendations": [
    "Consider caching compiled assemblies",
    "Optimize memory allocation patterns"
  ]
}
```

---

## 3. Dynamic API Controller (`/api/DynamicApi`)

Controller for managing Semantic Kernel plugins, dynamic endpoints, and OpenAPI integration.

### 3.1 Get Available Plugins
**GET** `/api/DynamicApi/plugins`

Lists all available Semantic Kernel plugins and their functions.

**Response:**
```json
[
  {
    "name": "web_api",
    "description": "Web API functions",
    "functions": [
      {
        "name": "execute_strange_loop",
        "description": "Execute a strange loop process",
        "parameters": [
          {
            "name": "requirements",
            "description": "Requirements for the process",
            "type": "string",
            "isRequired": true
          }
        ]
      }
    ]
  }
]
```

### 3.2 Create Dynamic Endpoint
**POST** `/api/DynamicApi/endpoints`

Creates a new dynamic API endpoint.

**Request Body:**
```json
{
  "name": "custom_user_endpoint",
  "path": "/api/users/custom",
  "httpMethod": "POST",
  "description": "Custom user management endpoint",
  "parameters": {
    "userId": "string",
    "action": "string"
  }
}
```

**Response:**
```json
{
  "message": "Dynamic endpoint created successfully",
  "endpoint": {
    "name": "custom_user_endpoint",
    "path": "/api/users/custom",
    "httpMethod": "POST",
    "description": "Custom user management endpoint",
    "functionName": "custom_user_endpoint_function"
  }
}
```

### 3.3 Generate OpenAPI Specification
**GET** `/api/DynamicApi/openapi`

Generates OpenAPI specification for all dynamic endpoints.

**Response:** OpenAPI 3.0.1 JSON specification.

### 3.4 Invoke Plugin Function
**POST** `/api/DynamicApi/invoke/{pluginName}/{functionName}`

Invokes a specific plugin function with arguments.

**Request Body:**
```json
{
  "requirements": "Build a simple calculator API",
  "complexity": 1,
  "securityLevel": 1
}
```

**Response:**
```json
{
  "plugin": "web_api",
  "function": "execute_strange_loop",
  "result": {
    "id": "process-id",
    "status": "completed"
  },
  "metadata": {
    "executionTime": "00:01:30",
    "tokensUsed": 1500
  }
}
```

### 3.5 Remove Dynamic Endpoint
**DELETE** `/api/DynamicApi/endpoints/{endpointName}`

Removes a dynamic endpoint.

**Response:**
```json
{
  "message": "Dynamic endpoint 'custom_user_endpoint' removed successfully"
}
```

### 3.6 Reload OpenAPI Plugin
**POST** `/api/DynamicApi/reload-openapi`

Reloads the live OpenAPI plugin for the Semantic Kernel.

**Response:**
```json
{
  "message": "Live OpenAPI plugin loaded successfully",
  "plugin": "live_api"
}
```

---

## 4. Swagger/OpenAPI Documentation

### 4.1 OpenAPI Specification
**GET** `/swagger/v1/swagger.json`

Returns the complete OpenAPI 3.0.1 specification for all endpoints.

### 4.2 Swagger UI
**GET** `/swagger`

Interactive API documentation interface (available in development mode).

---

## 5. Data Models

### 5.1 StrangeLoopRequest
```json
{
  "id": "string",
  "requirements": "string",
  "nonFunctionalRequirements": "string",
  "businessContext": "string",
  "constraints": "string",
  "complexity": 0,
  "securityLevel": 0,
  "maxIterations": 0,
  "enableDynamicCodeGeneration": true,
  "enableSemanticMemory": true,
  "enableDynamicApiGeneration": true,
  "createdAt": "2025-06-24T02:13:04.000Z"
}
```

### 5.2 StrangeLoopResponse
```json
{
  "id": "string",
  "request": {},
  "status": 0,
  "currentIteration": 0,
  "agentResults": {},
  "generatedArtifacts": [],
  "qualityMetrics": {},
  "errors": [],
  "warnings": [],
  "createdAt": "2025-06-24T02:13:04.000Z",
  "completedAt": "2025-06-24T02:14:23.000Z",
  "duration": "00:01:19.0528313"
}
```

### 5.3 ProcessState
```json
{
  "id": "string",
  "title": "string",
  "description": "string",
  "status": 0,
  "currentPhase": 0,
  "createdAt": "2025-06-24T02:13:04.000Z",
  "lastUpdatedAt": "2025-06-24T02:13:04.000Z",
  "completedAt": "2025-06-24T02:14:23.000Z",
  "progressPercentage": 0,
  "goals": [],
  "constraints": [],
  "priority": "string",
  "timeLimit": "00:30:00",
  "artifacts": {},
  "errors": [],
  "warnings": [],
  "context": {},
  "tags": [],
  "metrics": {},
  "isActive": true
}
```

### 5.4 MemoryEntry
```json
{
  "key": "string",
  "content": "string",
  "metadata": {},
  "category": "string",
  "tags": [],
  "createdAt": "2025-06-24T02:13:04.000Z",
  "lastAccessedAt": "2025-06-24T02:13:04.000Z",
  "lastUpdatedAt": "2025-06-24T02:13:04.000Z",
  "relevanceScore": 0.0,
  "accessCount": 0,
  "version": 0,
  "isActive": true
}
```

---

## 6. Enums

### 6.1 ComplexityLevel
- `0`: Low
- `1`: Medium
- `2`: High
- `3`: Critical

### 6.2 SecurityLevel
- `0`: Basic
- `1`: Enterprise
- `2`: Critical

### 6.3 StrangeLoopStatus
- `0`: Pending
- `1`: Planning
- `2`: Implementing
- `3`: Testing
- `4`: Reflecting
- `5`: Completed
- `6`: Failed
- `7`: Cancelled

### 6.4 ProcessStatus
- `0`: NotStarted
- `1`: Running
- `2`: Paused
- `3`: Completed
- `4`: Failed
- `5`: Cancelled
- `6`: Suspended

### 6.5 ProcessPhase
- `0`: Planning
- `1`: Coding
- `2`: Testing
- `3`: Reflecting

### 6.6 AgentRole
- `0`: Architect
- `1`: BusinessAnalyst
- `2`: Orchestrator
- `3`: QualityAssurance
- `4`: Reflector
- `5`: SeniorDeveloper

### 6.7 ArtifactType
- `0`: Code
- `1`: Api
- `2`: Documentation
- `3`: Test
- `4`: Configuration
- `5`: Architecture
- `6`: Other

---

## 7. Usage Examples

### 7.1 Basic Strange Loop Execution
```bash
curl -X POST "http://localhost:5166/api/StrangeLoop/execute" \
  -H "Content-Type: application/json" \
  -d '{
    "requirements": "Build a simple REST API for user management",
    "complexity": 1,
    "securityLevel": 1,
    "maxIterations": 3,
    "enableDynamicCodeGeneration": true,
    "enableSemanticMemory": true,
    "enableDynamicApiGeneration": true
  }'
```

### 7.2 Dynamic Code Compilation
```bash
curl -X POST "http://localhost:5166/api/ProcessFramework/compile" \
  -H "Content-Type: application/json" \
  -d '{
    "sourceCode": "public static int Add(int a, int b) { return a + b; }",
    "functionName": "Add",
    "parameters": [5, 3],
    "executeAfterCompile": true
  }'
```

### 7.3 Semantic Memory Search
```bash
curl -X GET "http://localhost:5166/api/ProcessFramework/memory/search?query=API%20performance%20optimization&limit=5"
```

### 7.4 Plugin Function Invocation
```bash
curl -X POST "http://localhost:5166/api/DynamicApi/invoke/web_api/execute_strange_loop" \
  -H "Content-Type: application/json" \
  -d '{
    "requirements": "Create a microservice for order processing"
  }'
```

### 7.5 Reload OpenAPI Plugin
```bash
curl -X POST "http://localhost:5166/api/DynamicApi/reload-openapi"
```

---

## 8. Error Handling

### 8.1 Common Error Responses

**400 Bad Request:**
```json
{
  "error": "Requirements cannot be empty"
}
```

**404 Not Found:**
```json
{
  "error": "No process found with ID: process-id"
}
```

**500 Internal Server Error:**
```json
{
  "error": "An error occurred while executing the strange loop process",
  "details": "Detailed error message"
}
```

### 8.2 Compilation Errors
```json
{
  "error": "Compilation/execution failed",
  "diagnostics": [
    "CS1002: ; expected",
    "CS1513: } expected"
  ],
  "message": "Compilation failed with 2 errors"
}
```

---

## 9. Best Practices

### 9.1 Request Optimization
- Use appropriate complexity levels to optimize processing time
- Enable only necessary features (dynamic code generation, semantic memory, etc.)
- Set reasonable maxIterations to prevent infinite loops

### 9.2 Error Handling
- Always check response status codes
- Handle compilation errors gracefully
- Implement retry logic for transient failures

### 9.3 Memory Management
- Regularly clean up old memory entries
- Monitor memory metrics for performance optimization
- Use appropriate tags and categories for better organization

### 9.4 Security Considerations
- Validate all input parameters
- Sanitize code before compilation
- Monitor execution times and resource usage

---

## 10. Monitoring and Observability

### 10.1 Health Checks
- Use `/api/StrangeLoop/health` for basic health monitoring
- Monitor process completion rates and durations
- Track compilation success rates

### 10.2 Metrics
- Memory usage and hit rates
- Compilation performance metrics
- Process execution times and success rates

### 10.3 Logging
- All endpoints include comprehensive logging
- Check application logs for detailed execution traces
- Monitor agent execution times and results

---

## 11. Troubleshooting

### 11.1 Common Issues

**Process Not Found:**
- Check if the process ID is correct
- Verify the process hasn't completed (completed processes are removed from active list)

**Compilation Failures:**
- Ensure C# syntax is correct
- Check for missing dependencies
- Verify function signatures match expected parameters

**Memory Search Issues:**
- Use specific, relevant search terms
- Check if memory entries exist for the query
- Verify memory service is properly initialized

**Plugin Loading Issues:**
- Use the reload endpoint to refresh plugins
- Check if the API is fully started before loading plugins
- Verify plugin configurations are correct

### 11.2 Performance Optimization
- Use appropriate complexity levels
- Enable caching where possible
- Monitor and optimize memory usage
- Implement proper error handling and retry logic

---

This documentation provides a comprehensive guide to using the StrangeLoop Platform API. For additional support or questions, refer to the project documentation or contact the development team. 