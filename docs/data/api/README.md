# API Reference Documentation

## Overview

ProjectName provides comprehensive REST and gRPC APIs for interacting with the ultra-generic, template-driven, self-evolving AI system. The APIs enable template management, workflow execution, agent orchestration, and system monitoring.

## API Architecture

### 1. REST API
- **Base URL**: `https://api.projectname.com/v1`
- **Authentication**: Bearer token or API key
- **Content-Type**: `application/json`
- **Rate Limiting**: 1000 requests per minute per API key

### 2. gRPC API
- **Endpoint**: `grpc.projectname.com:50051`
- **Protocol**: HTTP/2 with gRPC
- **Authentication**: JWT tokens
- **Streaming**: Bidirectional streaming support

## REST API Endpoints

### Template Management

#### Get All Templates
```http
GET /templates
```

**Query Parameters:**
- `kind` (string): Filter by template kind (agent, workflow, skill, integration)
- `version` (string): Filter by template version
- `limit` (integer): Maximum number of templates to return (default: 50)
- `offset` (integer): Number of templates to skip (default: 0)

**Response:**
```json
{
  "templates": [
    {
      "apiVersion": "ai.framework/v1",
      "kind": "Agent",
      "metadata": {
        "name": "planner-agent",
        "version": "1.0.0",
        "description": "Strategic planning agent"
      },
      "spec": {
        "role": "Planner",
        "model": "gpt-4",
        "capabilities": ["strategic-thinking", "goal-decomposition"]
      }
    }
  ],
  "pagination": {
    "total": 100,
    "limit": 50,
    "offset": 0,
    "hasMore": true
  }
}
```

#### Get Template by Name
```http
GET /templates/{name}
```

**Path Parameters:**
- `name` (string): Template name

**Response:**
```json
{
  "apiVersion": "ai.framework/v1",
  "kind": "Agent",
  "metadata": {
    "name": "planner-agent",
    "version": "1.0.0",
    "description": "Strategic planning agent"
  },
  "spec": {
    "role": "Planner",
    "model": "gpt-4",
    "capabilities": ["strategic-thinking", "goal-decomposition"],
    "prompts": {
      "system": "You are a strategic planner...",
      "user": "Context: {{context}}\nGoal: {{goal}}"
    }
  },
  "runtime": {
    "provider": "semantic-kernel",
    "configuration": {
      "temperature": 0.7,
      "maxTokens": 2000
    }
  }
}
```

#### Create Template
```http
POST /templates
```

**Request Body:**
```json
{
  "apiVersion": "ai.framework/v1",
  "kind": "Agent",
  "metadata": {
    "name": "new-agent",
    "version": "1.0.0",
    "description": "New agent template"
  },
  "spec": {
    "role": "Specialist",
    "model": "gpt-4",
    "capabilities": ["specialized-knowledge"]
  }
}
```

**Response:**
```json
{
  "success": true,
  "template": {
    "apiVersion": "ai.framework/v1",
    "kind": "Agent",
    "metadata": {
      "name": "new-agent",
      "version": "1.0.0",
      "description": "New agent template"
    }
  },
  "validation": {
    "valid": true,
    "warnings": []
  }
}
```

#### Update Template
```http
PUT /templates/{name}
```

**Path Parameters:**
- `name` (string): Template name

**Request Body:** Template object

**Response:** Updated template object

#### Delete Template
```http
DELETE /templates/{name}
```

**Path Parameters:**
- `name` (string): Template name

**Response:**
```json
{
  "success": true,
  "message": "Template deleted successfully"
}
```

### Workflow Execution

#### Execute Workflow
```http
POST /workflows/execute
```

**Request Body:**
```json
{
  "workflowName": "context-evolution-workflow",
  "input": {
    "context": "User wants to build a web application",
    "goal": "Create a modern, responsive web app",
    "constraints": ["budget", "timeline"]
  },
  "parameters": {
    "maxIterations": 10,
    "timeout": "30m"
  }
}
```

**Response:**
```json
{
  "executionId": "exec-12345",
  "status": "running",
  "workflow": {
    "name": "context-evolution-workflow",
    "version": "1.0.0"
  },
  "startTime": "2024-01-15T10:30:00Z",
  "estimatedCompletion": "2024-01-15T11:00:00Z"
}
```

#### Get Workflow Status
```http
GET /workflows/{executionId}
```

**Path Parameters:**
- `executionId` (string): Workflow execution ID

**Response:**
```json
{
  "executionId": "exec-12345",
  "status": "completed",
  "workflow": {
    "name": "context-evolution-workflow",
    "version": "1.0.0"
  },
  "startTime": "2024-01-15T10:30:00Z",
  "endTime": "2024-01-15T10:45:00Z",
  "duration": "15m",
  "steps": [
    {
      "name": "planner-step",
      "status": "completed",
      "startTime": "2024-01-15T10:30:00Z",
      "endTime": "2024-01-15T10:32:00Z",
      "duration": "2m"
    },
    {
      "name": "maker-step",
      "status": "completed",
      "startTime": "2024-01-15T10:32:00Z",
      "endTime": "2024-01-15T10:40:00Z",
      "duration": "8m"
    }
  ],
  "result": {
    "evolvedContext": "Enhanced context with detailed plan and implementation",
    "confidence": 0.95,
    "improvements": ["better-planning", "optimized-execution"]
  }
}
```

#### Cancel Workflow
```http
POST /workflows/{executionId}/cancel
```

**Path Parameters:**
- `executionId` (string): Workflow execution ID

**Response:**
```json
{
  "success": true,
  "message": "Workflow cancelled successfully"
}
```

### Agent Orchestration

#### Create Agent Session
```http
POST /agents/sessions
```

**Request Body:**
```json
{
  "pattern": "sequential",
  "agents": [
    {
      "name": "planner",
      "template": "planner-agent"
    },
    {
      "name": "maker",
      "template": "maker-agent"
    },
    {
      "name": "checker",
      "template": "checker-agent"
    },
    {
      "name": "reflector",
      "template": "reflector-agent"
    }
  ],
  "context": {
    "input": "User request for analysis",
    "goal": "Provide comprehensive analysis"
  }
}
```

**Response:**
```json
{
  "sessionId": "session-12345",
  "status": "created",
  "agents": ["planner", "maker", "checker", "reflector"],
  "pattern": "sequential",
  "createdAt": "2024-01-15T10:30:00Z"
}
```

#### Execute Agent Session
```http
POST /agents/sessions/{sessionId}/execute
```

**Path Parameters:**
- `sessionId` (string): Agent session ID

**Response:**
```json
{
  "sessionId": "session-12345",
  "status": "running",
  "currentAgent": "planner",
  "progress": 0.25,
  "context": {
    "input": "User request for analysis",
    "goal": "Provide comprehensive analysis",
    "plan": "Detailed execution plan"
  }
}
```

#### Get Agent Session Status
```http
GET /agents/sessions/{sessionId}
```

**Path Parameters:**
- `sessionId` (string): Agent session ID

**Response:**
```json
{
  "sessionId": "session-12345",
  "status": "completed",
  "agents": [
    {
      "name": "planner",
      "status": "completed",
      "result": "Strategic plan created"
    },
    {
      "name": "maker",
      "status": "completed",
      "result": "Plan executed successfully"
    },
    {
      "name": "checker",
      "status": "completed",
      "result": "Results validated"
    },
    {
      "name": "reflector",
      "status": "completed",
      "result": "Context evolved"
    }
  ],
  "finalContext": {
    "evolvedContext": "Enhanced context with insights",
    "improvements": ["better-planning", "optimized-execution"],
    "learnings": ["pattern-recognition", "efficiency-gains"]
  }
}
```

### Evolution Management

#### Get Evolution Status
```http
GET /evolution/status
```

**Response:**
```json
{
  "evolutionEnabled": true,
  "lastEvolution": "2024-01-15T09:00:00Z",
  "evolutionCount": 15,
  "successRate": 0.93,
  "currentCapabilities": 45,
  "pendingEvolutions": 2,
  "recentEvolutions": [
    {
      "id": "evol-123",
      "type": "template-optimization",
      "timestamp": "2024-01-15T09:00:00Z",
      "status": "completed",
      "impact": "positive"
    }
  ]
}
```

#### Trigger Evolution
```http
POST /evolution/trigger
```

**Request Body:**
```json
{
  "type": "capability-generation",
  "trigger": "missing-function",
  "context": "System needs weather API integration",
  "priority": "high"
}
```

**Response:**
```json
{
  "evolutionId": "evol-124",
  "type": "capability-generation",
  "status": "triggered",
  "estimatedDuration": "10m",
  "triggeredAt": "2024-01-15T10:30:00Z"
}
```

#### Get Evolution History
```http
GET /evolution/history
```

**Query Parameters:**
- `type` (string): Filter by evolution type
- `status` (string): Filter by evolution status
- `limit` (integer): Maximum number of evolutions to return
- `offset` (integer): Number of evolutions to skip

**Response:**
```json
{
  "evolutions": [
    {
      "id": "evol-123",
      "type": "template-optimization",
      "trigger": "performance-threshold",
      "timestamp": "2024-01-15T09:00:00Z",
      "status": "completed",
      "duration": "5m",
      "impact": "positive",
      "changes": [
        {
          "template": "planner-agent",
          "change": "prompt-optimization",
          "improvement": "15%"
        }
      ]
    }
  ],
  "pagination": {
    "total": 50,
    "limit": 20,
    "offset": 0,
    "hasMore": true
  }
}
```

### System Monitoring

#### Get System Health
```http
GET /health
```

**Response:**
```json
{
  "status": "healthy",
  "timestamp": "2024-01-15T10:30:00Z",
  "components": {
    "template-engine": {
      "status": "healthy",
      "uptime": "24h",
      "load": 0.3
    },
    "agent-orchestrator": {
      "status": "healthy",
      "uptime": "24h",
      "active-sessions": 5
    },
    "process-engine": {
      "status": "healthy",
      "uptime": "24h",
      "active-workflows": 3
    },
    "evolution-engine": {
      "status": "healthy",
      "uptime": "24h",
      "evolutions-today": 2
    }
  },
  "metrics": {
    "total-requests": 1500,
    "success-rate": 0.98,
    "average-response-time": "250ms"
  }
}
```

#### Get System Metrics
```http
GET /metrics
```

**Query Parameters:**
- `period` (string): Time period (1h, 24h, 7d, 30d)
- `metric` (string): Specific metric to retrieve

**Response:**
```json
{
  "period": "24h",
  "metrics": {
    "requests-per-minute": [120, 135, 110, 125],
    "response-times": [250, 230, 280, 240],
    "error-rates": [0.02, 0.01, 0.03, 0.01],
    "active-sessions": [5, 7, 3, 6],
    "active-workflows": [3, 4, 2, 3],
    "evolution-rate": [0.1, 0.15, 0.08, 0.12]
  },
  "timestamps": [
    "2024-01-14T10:00:00Z",
    "2024-01-14T16:00:00Z",
    "2024-01-15T00:00:00Z",
    "2024-01-15T06:00:00Z"
  ]
}
```

## gRPC API

### Service Definitions

#### Template Service
```protobuf
service TemplateService {
  rpc GetTemplate(GetTemplateRequest) returns (Template);
  rpc ListTemplates(ListTemplatesRequest) returns (ListTemplatesResponse);
  rpc CreateTemplate(CreateTemplateRequest) returns (Template);
  rpc UpdateTemplate(UpdateTemplateRequest) returns (Template);
  rpc DeleteTemplate(DeleteTemplateRequest) returns (DeleteTemplateResponse);
  rpc ValidateTemplate(ValidateTemplateRequest) returns (ValidateTemplateResponse);
}
```

#### Workflow Service
```protobuf
service WorkflowService {
  rpc ExecuteWorkflow(ExecuteWorkflowRequest) returns (WorkflowExecution);
  rpc GetWorkflowStatus(GetWorkflowStatusRequest) returns (WorkflowExecution);
  rpc CancelWorkflow(CancelWorkflowRequest) returns (CancelWorkflowResponse);
  rpc StreamWorkflowEvents(StreamWorkflowEventsRequest) returns (stream WorkflowEvent);
}
```

#### Agent Service
```protobuf
service AgentService {
  rpc CreateSession(CreateSessionRequest) returns (AgentSession);
  rpc ExecuteSession(ExecuteSessionRequest) returns (AgentSession);
  rpc GetSessionStatus(GetSessionStatusRequest) returns (AgentSession);
  rpc StreamSessionEvents(StreamSessionEventsRequest) returns (stream SessionEvent);
}
```

#### Evolution Service
```protobuf
service EvolutionService {
  rpc GetEvolutionStatus(GetEvolutionStatusRequest) returns (EvolutionStatus);
  rpc TriggerEvolution(TriggerEvolutionRequest) returns (Evolution);
  rpc GetEvolutionHistory(GetEvolutionHistoryRequest) returns (GetEvolutionHistoryResponse);
  rpc StreamEvolutionEvents(StreamEvolutionEventsRequest) returns (stream EvolutionEvent);
}
```

### Message Types

#### Template Messages
```protobuf
message Template {
  string api_version = 1;
  string kind = 2;
  TemplateMetadata metadata = 3;
  google.protobuf.Struct spec = 4;
  RuntimeConfiguration runtime = 5;
}

message TemplateMetadata {
  string name = 1;
  string version = 2;
  string description = 3;
  map<string, string> labels = 4;
  map<string, string> annotations = 5;
}

message RuntimeConfiguration {
  string provider = 1;
  google.protobuf.Struct configuration = 2;
  repeated string dependencies = 3;
  map<string, string> lifecycle = 4;
}
```

#### Workflow Messages
```protobuf
message WorkflowExecution {
  string execution_id = 1;
  string status = 2;
  string workflow_name = 3;
  google.protobuf.Timestamp start_time = 4;
  google.protobuf.Timestamp end_time = 5;
  repeated WorkflowStep steps = 6;
  google.protobuf.Struct result = 7;
}

message WorkflowStep {
  string name = 1;
  string status = 2;
  google.protobuf.Timestamp start_time = 3;
  google.protobuf.Timestamp end_time = 4;
  google.protobuf.Struct input = 5;
  google.protobuf.Struct output = 6;
  string error = 7;
}
```

#### Agent Messages
```protobuf
message AgentSession {
  string session_id = 1;
  string status = 2;
  string pattern = 3;
  repeated AgentInfo agents = 4;
  google.protobuf.Struct context = 5;
  google.protobuf.Timestamp created_at = 6;
  google.protobuf.Timestamp updated_at = 7;
}

message AgentInfo {
  string name = 1;
  string template = 2;
  string status = 3;
  google.protobuf.Struct result = 4;
}
```

## Authentication

### REST API Authentication
```http
Authorization: Bearer <jwt-token>
```

or

```http
X-API-Key: <api-key>
```

### gRPC Authentication
```protobuf
message AuthMetadata {
  string authorization = 1;
}
```

## Error Handling

### Error Response Format
```json
{
  "error": {
    "code": "TEMPLATE_NOT_FOUND",
    "message": "Template 'unknown-template' not found",
    "details": {
      "templateName": "unknown-template",
      "availableTemplates": ["planner-agent", "maker-agent"]
    },
    "timestamp": "2024-01-15T10:30:00Z",
    "requestId": "req-12345"
  }
}
```

### Common Error Codes
- `TEMPLATE_NOT_FOUND`: Template does not exist
- `TEMPLATE_VALIDATION_FAILED`: Template validation failed
- `WORKFLOW_EXECUTION_FAILED`: Workflow execution failed
- `AGENT_SESSION_FAILED`: Agent session failed
- `EVOLUTION_FAILED`: Evolution process failed
- `RATE_LIMIT_EXCEEDED`: API rate limit exceeded
- `AUTHENTICATION_FAILED`: Authentication failed
- `AUTHORIZATION_FAILED`: Authorization failed

## Rate Limiting

### Limits
- **REST API**: 1000 requests per minute per API key
- **gRPC API**: 2000 requests per minute per JWT token
- **WebSocket**: 100 connections per minute per API key

### Rate Limit Headers
```http
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 950
X-RateLimit-Reset: 1642248600
```

## SDK Examples

### C# SDK
```csharp
using ProjectName.Client;

var client = new ProjectNameClient("https://api.projectname.com/v1", "your-api-key");

// Get template
var template = await client.Templates.GetAsync("planner-agent");

// Execute workflow
var execution = await client.Workflows.ExecuteAsync(new ExecuteWorkflowRequest
{
    WorkflowName = "context-evolution-workflow",
    Input = new { context = "User request", goal = "Analysis" }
});

// Monitor execution
var status = await client.Workflows.GetStatusAsync(execution.ExecutionId);
```

### Python SDK
```python
from projectname import ProjectNameClient

client = ProjectNameClient("https://api.projectname.com/v1", "your-api-key")

# Get template
template = client.templates.get("planner-agent")

# Execute workflow
execution = client.workflows.execute(
    workflow_name="context-evolution-workflow",
    input={"context": "User request", "goal": "Analysis"}
)

# Monitor execution
status = client.workflows.get_status(execution.execution_id)
```

### JavaScript SDK
```javascript
import { ProjectNameClient } from '@projectname/client';

const client = new ProjectNameClient('https://api.projectname.com/v1', 'your-api-key');

// Get template
const template = await client.templates.get('planner-agent');

// Execute workflow
const execution = await client.workflows.execute({
  workflowName: 'context-evolution-workflow',
  input: { context: 'User request', goal: 'Analysis' }
});

// Monitor execution
const status = await client.workflows.getStatus(execution.executionId);
```

## WebSocket API

### Real-time Events
```javascript
const ws = new WebSocket('wss://api.projectname.com/v1/events');

ws.onmessage = (event) => {
  const data = JSON.parse(event.data);
  
  switch (data.type) {
    case 'workflow.started':
      console.log('Workflow started:', data.executionId);
      break;
    case 'workflow.completed':
      console.log('Workflow completed:', data.result);
      break;
    case 'agent.session.updated':
      console.log('Agent session updated:', data.session);
      break;
    case 'evolution.triggered':
      console.log('Evolution triggered:', data.evolution);
      break;
  }
};
```

## Best Practices

### 1. API Usage
- **Use pagination**: Always use pagination for list endpoints
- **Handle errors**: Implement proper error handling
- **Rate limiting**: Respect rate limits and implement backoff
- **Caching**: Cache frequently accessed data

### 2. Security
- **Secure tokens**: Keep API keys and JWT tokens secure
- **HTTPS only**: Always use HTTPS in production
- **Input validation**: Validate all input data
- **Audit logging**: Log all API interactions

### 3. Performance
- **Connection pooling**: Reuse HTTP connections
- **Compression**: Enable gzip compression
- **Async operations**: Use async/await for better performance
- **Monitoring**: Monitor API performance and errors 