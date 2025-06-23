# API Quick Reference

## Base URL
```
http://localhost:5000
```

## Most Common Endpoints

### Memory System
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Memory/stats` | Get memory statistics |
| POST | `/api/Memory/store` | Store memory entry |
| GET | `/api/Memory/get/{key}` | Retrieve memory entry |
| POST | `/api/Memory/search` | Search memory entries |

### Ollama Integration
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Ollama/models` | List available models |
| POST | `/api/Ollama/chat` | Generate text response |
| POST | `/api/Ollama/embeddings` | Generate embeddings |

### Self-Evolution
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/SelfEvolution/templates` | List code templates |
| POST | `/api/SelfEvolution/generate` | Generate code from template |

## Quick PowerShell Tests

```powershell
# Test if system is running
Invoke-RestMethod -Uri "http://localhost:5000/api/Memory/stats" -Method GET

# Test Ollama integration
Invoke-RestMethod -Uri "http://localhost:5000/api/Ollama/models" -Method GET

# Test code generation
Invoke-RestMethod -Uri "http://localhost:5000/api/SelfEvolution/templates" -Method GET
```

## Common Request Bodies

### Store Memory
```json
{
  "key": "my-key",
  "content": "my content",
  "description": "description",
  "collection": "my-collection"
}
```

### Generate Text
```json
{
  "model": "llama3:latest",
  "prompt": "Your prompt here",
  "stream": false
}
```

### Generate Code
```json
{
  "entityName": "MyEntity",
  "namespace": "MyNamespace",
  "properties": {
    "Name": "string",
    "Value": "int"
  },
  "templateType": "Entity",
  "templateName": "Entity"
}
```

## Status Codes
- **200** - Success
- **400** - Bad Request (check request body)
- **404** - Not Found (check endpoint URL)
- **500** - Server Error (check server logs) 