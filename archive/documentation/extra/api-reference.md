# Ultra-Generic System API Reference

This document provides practical examples of working API endpoints based on real-world testing of the Ultra-Generic System.

## Base URL
```
http://localhost:5000
```

## Authentication
Currently, the API does not require authentication for development purposes.

## Working Endpoints

### 1. Memory System

The Memory System provides persistent storage with vector embeddings and similarity search capabilities.

#### Get Memory Statistics
```http
GET /api/Memory/stats
```

**Response:**
```json
{
  "memoryEntries": 1,
  "whiteboardEntries": 0,
  "documents": 0,
  "documentChunks": 0,
  "cacheSize": 0,
  "whiteboardCacheSize": 0
}
```

#### Store Memory Entry
```http
POST /api/Memory/store
Content-Type: application/json
```

**Request Body:**
```json
{
  "key": "test-key",
  "content": "test-content",
  "description": "Test memory entry",
  "collection": "test"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Memory entry stored successfully",
  "affectedCount": 1,
  "metadata": null
}
```

#### Retrieve Memory Entry
```http
GET /api/Memory/get/{key}
```

**Response:**
```json
{
  "key": "test-key",
  "content": "test-content",
  "description": "Test memory entry",
  "source": "",
  "collection": "test",
  "embedding": [-0.016796006, -0.04402639, -0.032046754, -0.02517418],
  "similarityScore": null,
  "lastAccessed": "2025-06-23T03:11:12.000Z",
  "accessCount": 1,
  "id": "894212c8-7025-4719-ada5-2b72d2968918",
  "createdAt": "2025-06-23T03:11:07.000Z",
  "updatedAt": "2025-06-23T03:11:12.000Z",
  "createdBy": "",
  "isDeleted": false,
  "metadata": null,
  "entityType": "MemoryEntry",
  "version": 4,
  "tags": []
}
```

#### Search Memory
```http
POST /api/Memory/search
Content-Type: application/json
```

**Request Body:**
```json
{
  "query": "test content",
  "collection": "test",
  "maxResults": 10,
  "minSimilarity": 0.7,
  "includeMetadata": true
}
```

### 2. Ollama Integration

The Ollama Integration provides direct access to local LLM models for text generation and embeddings.

#### List Available Models
```http
GET /api/Ollama/models
```

**Response:**
```json
{
  "models": ["llama3:latest", "phi3:latest", "llama3.1:8b", "codellama:latest"],
  "count": 4,
  "timestamp": "2025-06-23T03:10:26.000Z"
}
```

#### Generate Text Response
```http
POST /api/Ollama/chat
Content-Type: application/json
```

**Request Body:**
```json
{
  "model": "llama3:latest",
  "prompt": "Hello, how are you?",
  "stream": false
}
```

**Response:**
```json
{
  "response": "I'm just an AI, so I don't have feelings or emotions like humans do. However, I'm functioning properly and ready to assist you with any questions or tasks you may have! It's great that you're reaching out and starting a conversation. How can I help you today?",
  "context": [128006, 882, 128007, 271],
  "done": true,
  "total_duration": 4088992500,
  "load_duration": 2544311100,
  "prompt_eval_count": 16,
  "prompt_eval_duration": 342136600,
  "eval_count": 59,
  "eval_duration": 1200518100,
  "timestamp": "2025-06-23T03:11:40.000Z",
  "requestId": "a0054bbe-e2b3-4e64-b647-caa2de782239"
}
```

#### Generate Embeddings
```http
POST /api/Ollama/embeddings
Content-Type: application/json
```

**Request Body:**
```json
{
  "text": "Sample text for embedding generation"
}
```

#### Check Model Availability
```http
GET /api/Ollama/models/{modelName}/available
```

### 3. Self-Evolution System

The Self-Evolution System provides code generation and template management capabilities.

#### List Code Templates
```http
GET /api/SelfEvolution/templates
```

**Response:**
```json
[
  {
    "name": "Entity",
    "templateType": "Entity",
    "templateContent": "using System.ComponentModel.DataAnnotations;\nusing UltraGenericSystem.Models;\n\nnamespace {{Namespace}}\n{\n    public class {{EntityName}} : BaseEntity\n    {\n        {{#each Properties}}\n        [MaxLength(255)]\n        public {{Value}} {{Key}} { get; set; }\n        {{/each}}\n    }\n}",
    "description": "Default template for generating C# entity classes",
    "isActive": true,
    "version": 1,
    "tags": ["entity", "model", "class"],
    "id": "f5288c4a-3902-4693-8d53-142d06ae4ee8",
    "createdAt": "2025-06-23T02:59:21.000Z",
    "updatedAt": "2025-06-23T02:59:21.000Z",
    "createdBy": "system",
    "isDeleted": false,
    "metadata": null,
    "entityType": "CodeTemplate"
  }
]
```

#### Generate Code
```http
POST /api/SelfEvolution/generate
Content-Type: application/json
```

**Request Body:**
```json
{
  "entityName": "TestEntity",
  "namespace": "TestNamespace",
  "properties": {
    "Name": "string",
    "Age": "int",
    "Email": "string"
  },
  "templateType": "Entity",
  "templateName": "Entity"
}
```

**Response:**
```json
{
  "success": true,
  "generatedCode": "using System.ComponentModel.DataAnnotations;\nusing UltraGenericSystem.Models;\n\nnamespace {{Namespace}}\n{\n    public class {{EntityName}} : BaseEntity\n    {\n        {{#each Properties}}\n        [MaxLength(255)]\n        public {{Value}} {{Key}} { get; set; }\n        {{/each}}\n    }\n}",
  "outputPath": "",
  "warnings": {},
  "errors": {},
  "compilationResult": null,
  "generationTime": "00:00:00.0086580",
  "metadata": null
}
```

### 4. Available Template Types

The system includes the following code templates:

1. **Entity** - C# entity classes with validation attributes
2. **Controller** - REST API controllers with full CRUD operations
3. **Service** - Business logic services with repository pattern
4. **Repository** - Data access repositories with generic pattern

### 5. PowerShell Examples

Here are practical PowerShell examples for testing the API:

#### Test Memory System
```powershell
# Store memory entry
$memoryEntry = @{
    Key = "test-key"
    Content = "test-content"
    Description = "Test memory entry"
    Collection = "test"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/Memory/store" -Method POST -Body $memoryEntry -ContentType "application/json"

# Retrieve memory entry
Invoke-RestMethod -Uri "http://localhost:5000/api/Memory/get/test-key" -Method GET

# Get memory statistics
Invoke-RestMethod -Uri "http://localhost:5000/api/Memory/stats" -Method GET
```

#### Test Ollama Integration
```powershell
# List available models
Invoke-RestMethod -Uri "http://localhost:5000/api/Ollama/models" -Method GET

# Generate text response
$ollamaRequest = @{
    model = "llama3:latest"
    prompt = "Hello, how are you?"
    stream = $false
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/Ollama/chat" -Method POST -Body $ollamaRequest -ContentType "application/json"
```

#### Test Code Generation
```powershell
# List templates
Invoke-RestMethod -Uri "http://localhost:5000/api/SelfEvolution/templates" -Method GET

# Generate code
$codeRequest = @{
    entityName = "TestEntity"
    namespace = "TestNamespace"
    properties = @{
        Name = "string"
        Age = "int"
        Email = "string"
    }
    templateType = "Entity"
    templateName = "Entity"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/SelfEvolution/generate" -Method POST -Body $codeRequest -ContentType "application/json"
```

### 6. Error Handling

The API returns standard HTTP status codes:

- **200 OK** - Request successful
- **400 Bad Request** - Invalid request data
- **404 Not Found** - Endpoint or resource not found
- **500 Internal Server Error** - Server-side error

Error responses include detailed information:
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Content": ["The Content field is required."]
  },
  "traceId": "00-c0ff224a8fa5d8f421683bcf4c650e3c-fa85540ad3aa7ab1-00"
}
```

### 7. Known Issues

1. **Generic Entity Controllers** - The `/api/v1/{entityType}` endpoints are not currently accessible due to generic controller registration issues.
2. **Advanced Orchestration** - Complex orchestration endpoints may return 500 errors due to service initialization issues.
3. **MCP Integration** - MCP-related endpoints may return 500 errors due to configuration issues.

### 8. Development Notes

- The system uses SQLite for data persistence
- Vector embeddings are generated automatically for memory entries
- Code generation uses Handlebars templating
- All timestamps are in UTC format
- The system supports streaming responses for Ollama integration

## Next Steps

For production deployment:
1. Fix generic controller registration for entity CRUD operations
2. Configure authentication and authorization
3. Set up proper logging and monitoring
4. Configure HTTPS and security headers
5. Implement rate limiting and request validation 