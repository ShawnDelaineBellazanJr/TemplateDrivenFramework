# StrangeLoop Platform API Quick Reference

## Base URL: `http://localhost:5166`

## 1. StrangeLoop Controller (`/api/StrangeLoop`)

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/execute` | POST | Start strange loop process |
| `/status/{requestId}` | GET | Get process status |
| `/cancel/{requestId}` | POST | Cancel running process |
| `/active` | GET | List active processes |
| `/health` | GET | Health check |

### Example: Execute Strange Loop
```bash
curl -X POST "http://localhost:5166/api/StrangeLoop/execute" \
  -H "Content-Type: application/json" \
  -d '{
    "requirements": "Build a REST API for user management",
    "complexity": 1,
    "securityLevel": 1,
    "maxIterations": 3,
    "enableDynamicCodeGeneration": true,
    "enableSemanticMemory": true,
    "enableDynamicApiGeneration": true
  }'
```

## 2. Process Framework Controller (`/api/ProcessFramework`)

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/start` | POST | Start self-improvement process |
| `/{processId}/next` | POST | Execute next phase |
| `/execute` | POST | Execute complete process |
| `/{processId}` | GET | Get process status |
| `/{processId}/cancel` | POST | Cancel process |
| `/active` | GET | List active processes |
| `/compile` | POST | Compile and test code |
| `/memory/metrics` | GET | Get memory metrics |
| `/memory/search` | GET | Search semantic memory |
| `/compile/metrics` | GET | Get compilation metrics |

### Example: Dynamic Code Compilation
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

### Example: Memory Search
```bash
curl -X GET "http://localhost:5166/api/ProcessFramework/memory/search?query=API%20performance&limit=5"
```

## 3. Dynamic API Controller (`/api/DynamicApi`)

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/plugins` | GET | List available plugins |
| `/endpoints` | POST | Create dynamic endpoint |
| `/openapi` | GET | Generate OpenAPI spec |
| `/invoke/{plugin}/{function}` | POST | Invoke plugin function |
| `/endpoints/{endpointName}` | DELETE | Remove dynamic endpoint |
| `/reload-openapi` | POST | **Reload OpenAPI plugin** |

### Example: Plugin Function Invocation
```bash
curl -X POST "http://localhost:5166/api/DynamicApi/invoke/web_api/execute_strange_loop" \
  -H "Content-Type: application/json" \
  -d '{
    "requirements": "Create a microservice for order processing"
  }'
```

### Example: Reload OpenAPI Plugin
```bash
curl -X POST "http://localhost:5166/api/DynamicApi/reload-openapi"
```

## 4. Swagger Documentation

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/swagger/v1/swagger.json` | GET | OpenAPI specification |
| `/swagger` | GET | Swagger UI (dev mode) |

## Key Data Models

### StrangeLoopRequest
```json
{
  "requirements": "string",
  "complexity": 0-3,
  "securityLevel": 0-2,
  "maxIterations": 1-10,
  "enableDynamicCodeGeneration": true,
  "enableSemanticMemory": true,
  "enableDynamicApiGeneration": true
}
```

### CodeCompilationRequest
```json
{
  "sourceCode": "string",
  "functionName": "string",
  "parameters": [],
  "executeAfterCompile": true
}
```

## Enums

### ComplexityLevel
- `0`: Low
- `1`: Medium  
- `2`: High
- `3`: Critical

### SecurityLevel
- `0`: Basic
- `1`: Enterprise
- `2`: Critical

### StrangeLoopStatus
- `0`: Pending
- `1`: Planning
- `2`: Implementing
- `3`: Testing
- `4`: Reflecting
- `5`: Completed
- `6`: Failed
- `7`: Cancelled

## Quick Start Examples

### 1. Basic Process Execution
```bash
curl -X POST "http://localhost:5166/api/StrangeLoop/execute" \
  -H "Content-Type: application/json" \
  -d '{
    "requirements": "Build a simple calculator API",
    "complexity": 1,
    "securityLevel": 1
  }'
```

### 2. Check Health
```bash
curl -X GET "http://localhost:5166/api/StrangeLoop/health"
```

### 3. Get Memory Metrics
```bash
curl -X GET "http://localhost:5166/api/ProcessFramework/memory/metrics"
```

### 4. List Available Plugins
```bash
curl -X GET "http://localhost:5166/api/DynamicApi/plugins"
```

## Error Handling

### Common Response Codes
- `200`: Success
- `400`: Bad Request (invalid input)
- `404`: Not Found (process/endpoint not found)
- `500`: Internal Server Error

### Error Response Format
```json
{
  "error": "Error description",
  "details": "Detailed error message"
}
```

## Notes

- **Reload Endpoint**: Use `/api/DynamicApi/reload-openapi` to reload the OpenAPI plugin
- **Process Lifecycle**: Completed processes are removed from active lists
- **Memory**: Semantic memory persists across requests
- **Compilation**: Dynamic code compilation uses Roslyn with security validation
- **Agents**: 6 enterprise agents work collaboratively (Architect, BusinessAnalyst, SeniorDeveloper, QualityAssurance, Reflector, Orchestrator)

For detailed documentation, see the full API documentation file. 