# Template System Documentation

## Overview

The template system is the core of ProjectName's ultra-generic, self-evolving architecture. Everything in the system—agents, workflows, skills, integrations, and even the system behavior itself—is defined through declarative templates.

## Core Concepts

### 1. Universal Template Schema
All templates follow a single, universal schema that ensures consistency and enables dynamic composition:

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "type": "object",
  "properties": {
    "apiVersion": { "type": "string", "pattern": "^ai\\.framework/v\\d+$" },
    "kind": { "type": "string" },
    "metadata": {
      "type": "object",
      "properties": {
        "name": { "type": "string" },
        "version": { "type": "string" },
        "description": { "type": "string" },
        "labels": { "type": "object" },
        "annotations": { "type": "object" }
      },
      "required": ["name"]
    },
    "spec": {
      "type": "object",
      "description": "Template-specific configuration"
    },
    "runtime": {
      "type": "object",
      "properties": {
        "provider": { "type": "string" },
        "configuration": { "type": "object" },
        "dependencies": { "type": "array" },
        "lifecycle": { "type": "object" }
      }
    },
    "templates": {
      "type": "object",
      "description": "Nested template definitions"
    }
  },
  "required": ["apiVersion", "kind", "metadata", "spec"]
}
```

### 2. Template Types

#### Agent Templates
Define AI agents with specific roles, capabilities, and behaviors:

```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: planner-agent
  version: 1.0.0
  description: "Plans and strategizes based on context"
spec:
  role: "Planner"
  model: "gpt-4"
  capabilities:
    - "strategic-thinking"
    - "goal-decomposition"
    - "resource-planning"
  prompts:
    system: |
      You are a strategic planner. Analyze the context and create a detailed plan.
    user: |
      Context: {{context}}
      Goal: {{goal}}
      Create a strategic plan.
  contextSchema:
    type: object
    properties:
      context: { type: string }
      goal: { type: string }
      constraints: { type: array }
runtime:
  provider: "semantic-kernel"
  configuration:
    temperature: 0.7
    maxTokens: 2000
  lifecycle:
    create: "initialize-agent"
    execute: "run-planning"
    evolve: "optimize-strategy"
```

#### Workflow Templates
Define multi-step processes and orchestration patterns:

```yaml
apiVersion: ai.framework/v1
kind: Workflow
metadata:
  name: context-evolution-loop
  version: 1.0.0
  description: "Planner → Maker → Checker → Reflector loop"
spec:
  pattern: "sequential"
  steps:
    - name: "planner"
      agent: "planner-agent"
      input: "{{context}}"
      output: "plan"
    - name: "maker"
      agent: "maker-agent"
      input: "{{plan}}"
      output: "result"
    - name: "checker"
      agent: "checker-agent"
      input: "{{result}}"
      output: "validation"
    - name: "reflector"
      agent: "reflector-agent"
      input: "{{validation}}"
      output: "evolved-context"
  contextFlow:
    - from: "input"
      to: "planner"
    - from: "planner"
      to: "maker"
    - from: "maker"
      to: "checker"
    - from: "checker"
      to: "reflector"
    - from: "reflector"
      to: "output"
runtime:
  provider: "process-framework"
  configuration:
    maxIterations: 10
    timeout: "30m"
```

#### Skill Templates
Define reusable capabilities and functions:

```yaml
apiVersion: ai.framework/v1
kind: Skill
metadata:
  name: data-analysis
  version: 1.0.0
  description: "Analyze data and extract insights"
spec:
  category: "analysis"
  inputSchema:
    type: object
    properties:
      data: { type: array }
      analysisType: { type: string }
    required: ["data"]
  outputSchema:
    type: object
    properties:
      insights: { type: array }
      summary: { type: string }
      confidence: { type: number }
  implementation:
    type: "semantic-function"
    prompt: |
      Analyze the following data: {{data}}
      Analysis type: {{analysisType}}
      Provide insights and summary.
runtime:
  provider: "semantic-kernel"
  configuration:
    model: "gpt-4"
    temperature: 0.3
```

#### Integration Templates
Define external system connections:

```yaml
apiVersion: ai.framework/v1
kind: Integration
metadata:
  name: facebook-api
  version: 1.0.0
  description: "Facebook API integration"
spec:
  type: "rest-api"
  baseUrl: "https://graph.facebook.com/v18.0"
  authentication:
    type: "oauth2"
    config:
      clientId: "{{env.FACEBOOK_CLIENT_ID}}"
      clientSecret: "{{env.FACEBOOK_CLIENT_SECRET}}"
  endpoints:
    - name: "get-user-posts"
      method: "GET"
      path: "/me/posts"
      parameters:
        - name: "access_token"
          type: "string"
          required: true
        - name: "limit"
          type: "number"
          default: 10
  eventHandlers:
    - event: "user-post"
      action: "analyze-sentiment"
      agent: "sentiment-analyzer"
runtime:
  provider: "http-client"
  configuration:
    timeout: "30s"
    retryAttempts: 3
```

### 3. Template Composition

Templates can be composed and nested to create complex systems:

```yaml
apiVersion: ai.framework/v1
kind: System
metadata:
  name: reflection-journal-system
  version: 1.0.0
  description: "Positive reflection journal system"
spec:
  components:
    agents:
      - ref: "planner-agent"
      - ref: "maker-agent"
      - ref: "checker-agent"
      - ref: "reflector-agent"
    workflows:
      - ref: "context-evolution-loop"
    skills:
      - ref: "sentiment-analysis"
      - ref: "positive-reflection"
    integrations:
      - ref: "database-storage"
      - ref: "notification-service"
  configuration:
    reflectionMode: "positive-only"
    autoOptimization: true
    userFeedback: true
templates:
  positive-reflection:
    apiVersion: ai.framework/v1
    kind: Skill
    metadata:
      name: positive-reflection
    spec:
      prompt: |
        Transform the user's reflection into a positive, constructive version.
        Remove negative self-criticism while preserving insights.
        Input: {{userReflection}}
        Output: Positive, constructive reflection.
```

## Template Evolution

### 1. Self-Modification
Templates can modify themselves based on performance and feedback:

```yaml
apiVersion: ai.framework/v1
kind: EvolutionStrategy
metadata:
  name: template-optimization
spec:
  triggers:
    - type: "performance-threshold"
      metric: "accuracy"
      threshold: 0.8
    - type: "user-feedback"
      sentiment: "negative"
  actions:
    - type: "prompt-optimization"
      agent: "prompt-optimizer"
      input: "{{currentTemplate}}"
      output: "{{optimizedTemplate}}"
    - type: "parameter-tuning"
      agent: "parameter-optimizer"
      parameters: ["temperature", "maxTokens"]
runtime:
  provider: "evolution-engine"
  configuration:
    maxChanges: 5
    validationRequired: true
```

### 2. Capability Detection
The system can detect missing capabilities and generate new templates:

```yaml
apiVersion: ai.framework/v1
kind: CapabilityDetector
metadata:
  name: gap-analysis
spec:
  detectionMethods:
    - type: "error-analysis"
      patterns:
        - "function not found"
        - "capability missing"
        - "unsupported operation"
    - type: "performance-analysis"
      metrics:
        - "execution-time"
        - "success-rate"
        - "user-satisfaction"
  generationStrategy:
    type: "llm-generation"
    agent: "template-generator"
    prompt: |
      Based on the error: {{error}}
      Generate a new skill template to handle this capability.
      Context: {{context}}
      Requirements: {{requirements}}
runtime:
  provider: "evolution-engine"
  configuration:
    autoGenerate: true
    validationRequired: true
```

## Template Registry

### 1. Discovery and Loading
Templates are discovered and loaded dynamically:

```yaml
# template-registry.yaml
apiVersion: ai.framework/v1
kind: TemplateRegistry
metadata:
  name: system-registry
spec:
  sources:
    - type: "file-system"
      path: "./templates"
      watch: true
    - type: "http"
      url: "https://api.template-store.com/templates"
      authentication:
        type: "api-key"
        key: "{{env.TEMPLATE_API_KEY}}"
    - type: "database"
      connectionString: "{{env.DB_CONNECTION}}"
      table: "templates"
  validation:
    schemaValidation: true
    securityScan: true
    performanceCheck: true
  caching:
    enabled: true
    ttl: "1h"
    maxSize: 1000
```

### 2. Version Management
Templates support versioning and evolution tracking:

```yaml
apiVersion: ai.framework/v1
kind: TemplateVersion
metadata:
  name: planner-agent-v2
  version: "2.0.0"
  previousVersion: "1.0.0"
spec:
  changes:
    - type: "prompt-improvement"
      description: "Enhanced planning prompt for better strategy generation"
      diff: |
        - You are a strategic planner.
        + You are an expert strategic planner with deep domain knowledge.
    - type: "capability-addition"
      description: "Added resource optimization capability"
      capabilities:
        - "resource-optimization"
  migration:
    type: "automatic"
    strategy: "gradual-rollout"
    validation:
      type: "a-b-testing"
      duration: "24h"
```

## Best Practices

### 1. Template Design
- **Single Responsibility**: Each template should have one clear purpose
- **Composability**: Design templates to be easily combined and reused
- **Parameterization**: Use variables and placeholders for flexibility
- **Documentation**: Include clear descriptions and examples

### 2. Evolution Strategy
- **Incremental Changes**: Make small, testable improvements
- **Validation**: Always validate evolved templates before deployment
- **Rollback**: Maintain ability to revert to previous versions
- **Monitoring**: Track performance and user feedback

### 3. Security Considerations
- **Input Validation**: Validate all template inputs and parameters
- **Access Control**: Restrict template modification permissions
- **Audit Logging**: Log all template changes and executions
- **Sandboxing**: Execute templates in isolated environments

## Examples

### Complete System Example
See the [examples](./examples/) directory for complete, working template systems including:
- Reflection Journal System
- Business Process Automation
- Social Media Analysis
- IoT Device Management
- Research Assistant

### Template Development
See the [development guide](./development.md) for:
- Template creation workflow
- Testing strategies
- Debugging techniques
- Performance optimization 