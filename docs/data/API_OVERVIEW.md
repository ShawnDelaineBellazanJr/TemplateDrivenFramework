# 🚀 Ultra-Generic System API Overview

## What You're Looking At

The **Swagger UI** at `http://localhost:5000/swagger` is your **interactive API documentation and testing interface**. It's like a "control panel" for your entire AI system.

## 🎯 What This System Actually Does

### **1. Multi-Agent AI Orchestration**
- **Planner Agent**: Breaks down complex tasks into steps
- **Maker Agent**: Executes the planned steps
- **Checker Agent**: Validates results and finds issues
- **Reflector Agent**: Learns from experiences and creates new capabilities

### **2. Self-Evolving Code Generation**
- **Dynamic C# Code Generation**: Creates new classes, controllers, services
- **Roslyn Compilation**: Compiles generated code at runtime
- **Plugin System**: Loads and executes new capabilities dynamically
- **Self-Modification**: Can evolve its own codebase (with safety controls)

### **3. Knowledge Management & RAG**
- **Vector Storage**: Stores and retrieves knowledge semantically
- **Document Processing**: Ingests and chunks documents for RAG
- **Memory System**: Persistent storage with metadata
- **Whiteboard**: Collaborative session management

### **4. AI Service Integration**
- **Azure OpenAI**: GPT-4, embeddings, function calling
- **Ollama**: Local LLM hosting (llama3, etc.)
- **MCP (Model Context Protocol)**: External tool integration
- **GitHub Integration**: Repository analysis and code generation

## 🔧 API Categories You Can Test

### **🧠 Memory & Knowledge**
```
POST /api/Memory/store          # Store information
GET  /api/Memory/get/{key}      # Retrieve information
GET  /api/Memory/search         # Semantic search
POST /api/Memory/rag/query      # RAG-powered queries
```

### **🤖 AI Chat & Generation**
```
POST /api/Ollama/chat           # Chat with local LLM
POST /api/AzureAIAgent/generate # Generate with Azure AI
POST /api/AzureAIAgent/function # Function calling
```

### **⚡ Self-Evolution**
```
POST /api/SelfEvolution/generate        # Generate new code
POST /api/SelfEvolution/generate/entity # Create new entities
POST /api/SelfEvolution/compile         # Compile generated code
GET  /api/SelfEvolution/plugins         # List available plugins
```

### **🔄 Advanced Orchestration**
```
POST /api/AdvancedOrchestration/workflow    # Multi-step workflows
POST /api/AdvancedOrchestration/structured  # Structured reasoning
POST /api/AdvancedOrchestration/patterns    # Pattern extraction
```

### **🔗 External Integrations**
```
GET  /api/MCP/servers                    # Available MCP servers
GET  /api/MCP/github/repository/{owner}/{repo}  # GitHub analysis
POST /api/SwaggerToSK/generate-plugin    # Generate SK plugins
```

## 🎮 How to Use the Swagger UI

1. **Browse Endpoints**: Click on any endpoint to expand it
2. **Try It Out**: Click "Try it out" button
3. **Fill Parameters**: Enter required data
4. **Execute**: Click "Execute" to test the API
5. **See Results**: View the response and status code

## 🧪 Quick Test Examples

### **Test 1: Store Knowledge**
```json
POST /api/Memory/store
{
  "key": "my-knowledge",
  "content": "This is important information",
  "metadata": {
    "source": "manual",
    "category": "test"
  }
}
```

### **Test 2: Chat with Local AI**
```json
POST /api/Ollama/chat
{
  "message": "Hello, what can you do?",
  "model": "llama3:latest"
}
```

### **Test 3: Generate Code**
```json
POST /api/SelfEvolution/generate
{
  "prompt": "Create a C# service for user management",
  "context": "Building a web application"
}
```

## 🎯 What Makes This "Ultra-Generic"

1. **Template-Driven**: Everything defined in YAML/JSON templates
2. **Self-Evolving**: Can modify its own capabilities
3. **Multi-Modal**: Supports text, code, documents, APIs
4. **Enterprise-Ready**: Proper error handling, validation, security
5. **Strange Loop**: 4-level meta-cognitive architecture

## 🚀 Next Steps

1. **Explore the Swagger UI** - Click around and see what's available
2. **Try the Memory System** - Store and retrieve some test data
3. **Test Code Generation** - Generate a simple C# class
4. **Experiment with Chat** - Talk to the local AI
5. **Build Workflows** - Create multi-step orchestration

This is a **production-grade AI system** that can literally build and evolve itself! 