# Examples Documentation

## Overview

This section provides comprehensive examples of how to use ProjectName for various real-world scenarios. Each example demonstrates the ultra-generic, template-driven, self-evolving capabilities of the system.

## Example Categories

### 1. **Reflection Journal System**
A positive reflection journal that uses the Strange Loop pattern to help users see their thoughts in a constructive light.

### 2. **Business Process Automation**
Automated business workflows that can adapt and improve over time.

### 3. **Social Media Analysis**
Real-time social media monitoring and sentiment analysis with automatic response generation.

### 4. **IoT Device Management**
Intelligent IoT device orchestration and automation.

### 5. **Research Assistant**
AI-powered research assistant that can discover, analyze, and synthesize information.

## Example 1: Reflection Journal System

### Overview
A system that helps users journal their thoughts and provides positive, constructive reflections using the Planner → Maker → Checker → Reflector pattern.

### System Architecture
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
      - ref: "reflection-workflow"
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
```

### Agent Templates

#### Planner Agent
```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: reflection-planner
  version: 1.0.0
  description: "Plans reflection analysis strategy"
spec:
  role: "ReflectionPlanner"
  model: "gpt-4"
  capabilities:
    - "emotion-analysis"
    - "pattern-recognition"
    - "goal-identification"
  prompts:
    system: |
      You are a compassionate reflection planner. Your role is to analyze user journal entries
      and create a plan for providing positive, constructive insights.
      
      Focus on:
      - Identifying underlying emotions and patterns
      - Recognizing growth opportunities
      - Planning constructive feedback
      - Maintaining a positive, supportive tone
    user: |
      User Journal Entry: {{userEntry}}
      Previous Reflections: {{previousReflections}}
      User Goals: {{userGoals}}
      
      Create a reflection plan that will help the user see their thoughts in a positive light.
  contextSchema:
    type: "object"
    properties:
      userEntry: { type: "string", required: true }
      previousReflections: { type: "array" }
      userGoals: { type: "array" }
      emotionalState: { type: "string" }
```

#### Maker Agent
```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: reflection-maker
  version: 1.0.0
  description: "Creates positive reflections"
spec:
  role: "ReflectionMaker"
  model: "gpt-4"
  capabilities:
    - "positive-framing"
    - "constructive-feedback"
    - "encouragement"
  prompts:
    system: |
      You are a supportive reflection maker. Your role is to create positive, constructive
      reflections based on the planner's analysis.
      
      Guidelines:
      - Always maintain a positive, encouraging tone
      - Focus on growth and learning opportunities
      - Acknowledge emotions without dwelling on negativity
      - Provide actionable insights and suggestions
      - Celebrate progress and achievements
    user: |
      Reflection Plan: {{reflectionPlan}}
      User Entry: {{userEntry}}
      
      Create a positive, constructive reflection that helps the user see their thoughts
      in a new, encouraging light.
```

#### Checker Agent
```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: reflection-checker
  version: 1.0.0
  description: "Validates reflection quality"
spec:
  role: "ReflectionChecker"
  model: "gpt-4"
  capabilities:
    - "quality-assessment"
    - "tone-analysis"
    - "safety-checking"
  prompts:
    system: |
      You are a reflection quality checker. Your role is to validate that reflections
      meet quality standards and are safe for users.
      
      Check for:
      - Positive, constructive tone
      - Appropriate level of encouragement
      - Safety and appropriateness
      - Clarity and helpfulness
      - Alignment with user goals
    user: |
      Reflection: {{reflection}}
      User Entry: {{userEntry}}
      Quality Standards: {{qualityStandards}}
      
      Validate the reflection and provide feedback on quality and safety.
```

#### Reflector Agent
```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: reflection-reflector
  version: 1.0.0
  description: "Evolves reflection system"
spec:
  role: "ReflectionReflector"
  model: "gpt-4"
  capabilities:
    - "pattern-recognition"
    - "system-optimization"
    - "learning-extraction"
  prompts:
    system: |
      You are a reflection system reflector. Your role is to analyze the reflection process
      and evolve the system for better user outcomes.
      
      Focus on:
      - Identifying effective reflection patterns
      - Recognizing areas for improvement
      - Learning from user feedback
      - Optimizing agent prompts and strategies
      - Enhancing user experience
    user: |
      Full Reflection Process: {{fullProcess}}
      User Feedback: {{userFeedback}}
      System Performance: {{performance}}
      
      Analyze the reflection process and suggest improvements for the system.
```

### Workflow Template
```yaml
apiVersion: ai.framework/v1
kind: Workflow
metadata:
  name: reflection-workflow
  version: 1.0.0
  description: "Complete reflection processing workflow"
spec:
  pattern: "sequential"
  steps:
    - name: "plan-reflection"
      type: "agent-step"
      agent: "reflection-planner"
      input: "{{userEntry}}"
      output: "reflectionPlan"
    
    - name: "create-reflection"
      type: "agent-step"
      agent: "reflection-maker"
      input: "{{reflectionPlan}}"
      output: "reflection"
    
    - name: "validate-reflection"
      type: "agent-step"
      agent: "reflection-checker"
      input: "{{reflection}}"
      output: "validation"
    
    - name: "evolve-system"
      type: "agent-step"
      agent: "reflection-reflector"
      input: "{{validation}}"
      output: "evolution"
  
  contextFlow:
    - from: "input"
      to: "plan-reflection"
    - from: "plan-reflection"
      to: "create-reflection"
    - from: "create-reflection"
      to: "validate-reflection"
    - from: "validate-reflection"
      to: "evolve-system"
    - from: "evolve-system"
      to: "output"
```

### Usage Example
```bash
# Submit a journal entry
curl -X POST https://api.projectname.com/v1/reflection/journal \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer your-token" \
  -d '{
    "entry": "I feel overwhelmed with work today. I made several mistakes and my boss seemed disappointed. I don\'t know if I\'m cut out for this role.",
    "userGoals": ["build confidence", "improve performance"],
    "previousReflections": []
  }'

# Response
{
  "reflectionId": "ref-12345",
  "reflection": "I can see you're having a challenging day at work, and it's completely normal to feel overwhelmed when things don't go as planned. The fact that you're aware of the mistakes shows your attention to detail and commitment to doing well. Your boss's reaction might be more about their own stress than your performance. Remember, every professional faces setbacks - they're opportunities to learn and grow. What specific aspects of your role would you like to strengthen?",
  "confidence": 0.92,
  "insights": ["self-awareness", "growth-mindset", "resilience"],
  "suggestions": ["practice self-compassion", "identify learning opportunities", "seek feedback"]
}
```

## Example 2: Business Process Automation

### Overview
An intelligent business process automation system that can adapt workflows based on changing business needs and performance data.

### System Components

#### Business Process Agent
```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: business-process-agent
  version: 1.0.0
  description: "Business process automation specialist"
spec:
  role: "BusinessProcessSpecialist"
  model: "gpt-4"
  capabilities:
    - "process-analysis"
    - "workflow-design"
    - "optimization"
    - "compliance-checking"
  prompts:
    system: |
      You are a business process automation specialist. Your role is to analyze business
      processes and create optimized, automated workflows.
      
      Focus on:
      - Process efficiency and optimization
      - Compliance and risk management
      - Cost reduction and time savings
      - Scalability and maintainability
      - Integration with existing systems
    user: |
      Business Process: {{processDescription}}
      Current Performance: {{currentMetrics}}
      Business Goals: {{businessGoals}}
      Constraints: {{constraints}}
      
      Analyze the process and create an optimized automation plan.
```

#### Workflow Template
```yaml
apiVersion: ai.framework/v1
kind: Workflow
metadata:
  name: invoice-processing-workflow
  version: 1.0.0
  description: "Automated invoice processing workflow"
spec:
  pattern: "conditional"
  steps:
    - name: "receive-invoice"
      type: "integration-step"
      integration: "email-system"
      input: "{{invoiceEmail}}"
      output: "invoiceData"
    
    - name: "extract-data"
      type: "agent-step"
      agent: "data-extraction-agent"
      input: "{{invoiceData}}"
      output: "extractedData"
    
    - name: "validate-invoice"
      type: "agent-step"
      agent: "validation-agent"
      input: "{{extractedData}}"
      output: "validationResult"
    
    - name: "route-invoice"
      type: "conditional-step"
      conditions:
        - condition: "{{validationResult.valid}} == true"
          nextStep: "approve-invoice"
        - condition: "{{validationResult.valid}} == false"
          nextStep: "flag-for-review"
    
    - name: "approve-invoice"
      type: "integration-step"
      integration: "accounting-system"
      input: "{{extractedData}}"
      output: "approvalResult"
    
    - name: "flag-for-review"
      type: "integration-step"
      integration: "notification-system"
      input: "{{validationResult.issues}}"
      output: "notificationResult"
```

### Usage Example
```bash
# Submit invoice for processing
curl -X POST https://api.projectname.com/v1/workflows/execute \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer your-token" \
  -d '{
    "workflowName": "invoice-processing-workflow",
    "input": {
      "invoiceEmail": "base64-encoded-email-content",
      "sender": "vendor@company.com",
      "subject": "Invoice #INV-2024-001"
    }
  }'

# Monitor workflow execution
curl -X GET https://api.projectname.com/v1/workflows/exec-12345 \
  -H "Authorization: Bearer your-token"
```

## Example 3: Social Media Analysis

### Overview
A real-time social media monitoring system that analyzes posts, detects trends, and generates appropriate responses.

### System Components

#### Social Media Monitor Agent
```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: social-media-monitor
  version: 1.0.0
  description: "Social media monitoring and analysis"
spec:
  role: "SocialMediaAnalyst"
  model: "gpt-4"
  capabilities:
    - "sentiment-analysis"
    - "trend-detection"
    - "content-classification"
    - "response-generation"
  prompts:
    system: |
      You are a social media monitoring specialist. Your role is to analyze social media
      posts and identify trends, sentiment, and opportunities for engagement.
      
      Focus on:
      - Sentiment analysis and emotional tone
      - Trend identification and prediction
      - Content classification and categorization
      - Engagement opportunities and risks
      - Brand mention monitoring
    user: |
      Social Media Post: {{postContent}}
      Platform: {{platform}}
      Author: {{author}}
      Context: {{context}}
      
      Analyze the post and provide insights for engagement strategy.
```

#### Integration Template
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
      agent: "social-media-monitor"
```

### Usage Example
```bash
# Monitor Facebook posts
curl -X POST https://api.projectname.com/v1/integrations/facebook/monitor \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer your-token" \
  -d '{
    "action": "start-monitoring",
    "keywords": ["product", "service", "feedback"],
    "sentimentThreshold": 0.7
  }'

# Get analysis results
curl -X GET https://api.projectname.com/v1/social-media/analysis \
  -H "Authorization: Bearer your-token"
```

## Example 4: IoT Device Management

### Overview
An intelligent IoT device management system that can orchestrate devices, optimize performance, and respond to environmental changes.

### System Components

#### IoT Orchestrator Agent
```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: iot-orchestrator
  version: 1.0.0
  description: "IoT device orchestration specialist"
spec:
  role: "IoTOrchestrator"
  model: "gpt-4"
  capabilities:
    - "device-management"
    - "automation-design"
    - "performance-optimization"
    - "energy-management"
  prompts:
    system: |
      You are an IoT device orchestration specialist. Your role is to manage and optimize
      IoT device networks for maximum efficiency and performance.
      
      Focus on:
      - Device coordination and automation
      - Energy efficiency and optimization
      - Performance monitoring and improvement
      - Predictive maintenance
      - Security and safety
    user: |
      Device Network: {{deviceNetwork}}
      Environmental Data: {{environmentalData}}
      Performance Goals: {{performanceGoals}}
      
      Analyze the IoT network and create an optimization plan.
```

#### Device Management Workflow
```yaml
apiVersion: ai.framework/v1
kind: Workflow
metadata:
  name: smart-home-automation
  version: 1.0.0
  description: "Smart home device automation workflow"
spec:
  pattern: "event-driven"
  triggers:
    - event: "motion-detected"
      source: "motion-sensor"
    - event: "temperature-change"
      source: "thermostat"
    - event: "light-level-change"
      source: "light-sensor"
  
  steps:
    - name: "analyze-environment"
      type: "agent-step"
      agent: "iot-orchestrator"
      input: "{{sensorData}}"
      output: "environmentAnalysis"
    
    - name: "optimize-devices"
      type: "agent-step"
      agent: "device-optimizer"
      input: "{{environmentAnalysis}}"
      output: "optimizationPlan"
    
    - name: "execute-actions"
      type: "parallel-step"
      steps:
        - name: "adjust-lighting"
          type: "integration-step"
          integration: "smart-lights"
        - name: "adjust-climate"
          type: "integration-step"
          integration: "smart-thermostat"
        - name: "adjust-security"
          type: "integration-step"
          integration: "security-system"
```

### Usage Example
```bash
# Register IoT devices
curl -X POST https://api.projectname.com/v1/iot/devices \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer your-token" \
  -d '{
    "deviceType": "motion-sensor",
    "location": "living-room",
    "capabilities": ["motion-detection", "temperature-sensing"],
    "automationRules": ["turn-on-lights", "adjust-thermostat"]
  }'

# Monitor device performance
curl -X GET https://api.projectname.com/v1/iot/devices/performance \
  -H "Authorization: Bearer your-token"
```

## Example 5: Research Assistant

### Overview
An AI-powered research assistant that can discover, analyze, and synthesize information from multiple sources.

### System Components

#### Research Agent
```yaml
apiVersion: ai.framework/v1
kind: Agent
metadata:
  name: research-assistant
  version: 1.0.0
  description: "AI research assistant"
spec:
  role: "ResearchSpecialist"
  model: "gpt-4"
  capabilities:
    - "information-discovery"
    - "source-evaluation"
    - "synthesis"
    - "citation-management"
  prompts:
    system: |
      You are an AI research assistant. Your role is to discover, analyze, and synthesize
      information from multiple sources to provide comprehensive research insights.
      
      Focus on:
      - Comprehensive information gathering
      - Source credibility evaluation
      - Information synthesis and analysis
      - Clear, well-structured reporting
      - Proper citation and attribution
    user: |
      Research Topic: {{researchTopic}}
      Research Questions: {{researchQuestions}}
      Scope: {{scope}}
      Sources: {{preferredSources}}
      
      Conduct comprehensive research and provide detailed insights.
```

#### Research Workflow
```yaml
apiVersion: ai.framework/v1
kind: Workflow
metadata:
  name: research-workflow
  version: 1.0.0
  description: "Complete research process workflow"
spec:
  pattern: "sequential"
  steps:
    - name: "define-research"
      type: "agent-step"
      agent: "research-planner"
      input: "{{researchTopic}}"
      output: "researchPlan"
    
    - name: "gather-sources"
      type: "parallel-step"
      steps:
        - name: "search-academic"
          type: "integration-step"
          integration: "academic-databases"
        - name: "search-web"
          type: "integration-step"
          integration: "web-search"
        - name: "search-news"
          type: "integration-step"
          integration: "news-apis"
    
    - name: "evaluate-sources"
      type: "agent-step"
      agent: "source-evaluator"
      input: "{{gatheredSources}}"
      output: "evaluatedSources"
    
    - name: "analyze-information"
      type: "agent-step"
      agent: "research-assistant"
      input: "{{evaluatedSources}}"
      output: "analysis"
    
    - name: "synthesize-findings"
      type: "agent-step"
      agent: "synthesis-specialist"
      input: "{{analysis}}"
      output: "synthesis"
    
    - name: "generate-report"
      type: "agent-step"
      agent: "report-generator"
      input: "{{synthesis}}"
      output: "researchReport"
```

### Usage Example
```bash
# Submit research request
curl -X POST https://api.projectname.com/v1/research/request \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer your-token" \
  -d '{
    "topic": "Impact of AI on healthcare",
    "questions": [
      "How is AI being used in medical diagnosis?",
      "What are the benefits and risks?",
      "What is the future outlook?"
    ],
    "scope": "academic-and-industry",
    "depth": "comprehensive"
  }'

# Get research results
curl -X GET https://api.projectname.com/v1/research/results/research-12345 \
  -H "Authorization: Bearer your-token"
```

## Best Practices for Examples

### 1. Template Design
- **Modularity**: Design templates to be reusable and composable
- **Parameterization**: Use variables for flexibility and customization
- **Documentation**: Include clear descriptions and usage examples
- **Validation**: Implement proper schema validation

### 2. Agent Design
- **Single Responsibility**: Each agent should have one clear purpose
- **Context Awareness**: Agents should understand and adapt to context
- **Error Handling**: Implement robust error handling and recovery
- **Performance**: Optimize agent performance and resource usage

### 3. Workflow Design
- **Clarity**: Design workflows that are easy to understand and maintain
- **Flexibility**: Allow for dynamic branching and adaptation
- **Monitoring**: Implement comprehensive monitoring and logging
- **Testing**: Create thorough testing strategies for workflows

### 4. Integration Design
- **Security**: Implement proper authentication and authorization
- **Reliability**: Design for fault tolerance and recovery
- **Scalability**: Plan for growth and increased load
- **Monitoring**: Monitor integration health and performance

## Getting Started with Examples

### 1. Choose an Example
Select an example that matches your use case and requirements.

### 2. Review Templates
Study the template definitions to understand the system architecture.

### 3. Customize for Your Needs
Modify templates to fit your specific requirements and constraints.

### 4. Test and Iterate
Test the system thoroughly and iterate based on feedback and performance.

### 5. Monitor and Evolve
Monitor system performance and let it evolve based on usage patterns.

## Contributing Examples

### Guidelines
- **Clear Documentation**: Provide comprehensive documentation
- **Working Code**: Ensure examples are functional and tested
- **Best Practices**: Follow established patterns and conventions
- **Real-World Relevance**: Focus on practical, useful examples

### Submission Process
1. Create example templates and documentation
2. Test thoroughly with different scenarios
3. Document usage patterns and best practices
4. Submit for review and integration

---

These examples demonstrate the power and flexibility of the ProjectName system. Each example can be customized, extended, and combined to create sophisticated AI-powered applications that evolve and improve over time. 