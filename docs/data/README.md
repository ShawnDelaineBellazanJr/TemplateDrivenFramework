# ProjectName: Ultra-Generic Template-Driven Self-Evolving AI System

## Overview

ProjectName is a production-grade, ultra-generic, template-driven, self-evolving AI system built on Microsoft Semantic Kernel and .NET Aspire. The system implements a "context-in-the-loop" architecture with Strange Loop patterns for continuous self-improvement.

## Core Philosophy

- **Everything is a Template**: Agents, skills, workflows, and system behavior are defined in declarative templates (YAML/Prompty)
- **Ultra-Generic Design**: The same framework handles any domain or use case without code changes
- **Strange Loop Architecture**: Self-referential, self-evolving patterns where each output becomes input for the next cycle
- **Context-in-the-Loop**: Continuous evolution driven by context rather than human intervention

## Key Components

### 1. Template Engine
- **Universal Template Schema**: JSON Schema for all system components
- **Dynamic Loading**: Runtime template instantiation and modification
- **Self-Modifying Templates**: AI can evolve its own configuration

### 2. Agent Orchestration
- **Planner → Maker → Checker → Reflector** pattern
- **Multi-Agent Patterns**: Sequential, concurrent, handoff, group chat
- **Semantic Kernel Integration**: Built on SK Agent Framework

### 3. Process Framework
- **Event-Driven Workflows**: Steps communicate via events
- **Dynamic Branching**: Conditional flows and parallel execution
- **Stateful Execution**: Context preservation across steps

### 4. Self-Evolution
- **Template Evolution**: AI modifies its own templates
- **Capability Detection**: Automatic plugin generation for missing capabilities
- **Performance Optimization**: Continuous improvement based on feedback

## Architecture

```
ProjectName/
├── ProjectName.AppHost/           # Aspire orchestration host
├── ProjectName.API/               # REST/gRPC API
├── ProjectName.TemplateEngine/    # Core template system
├── ProjectName.Agents/            # Agent orchestration
├── ProjectName.Skills/            # Dynamic skill system
├── ProjectName.Workflows/         # Process framework
├── ProjectName.Evolution/         # Self-evolution engine
├── ProjectName.ServiceDefaults/   # Shared configuration
└── templates/                     # YAML/Prompty templates
```

## Quick Start

1. **Clone and Build**
   ```bash
   git clone <repository>
   cd ProjectName
   dotnet build
   ```

2. **Run with Aspire**
   ```bash
   cd ProjectName.AppHost
   dotnet run
   ```

3. **Access Documentation**
   - API Documentation: http://localhost:5000/swagger
   - Aspire Dashboard: http://localhost:5000

## Documentation Structure

- **[Architecture](./architecture/README.md)**: System design and patterns
- **[Templates](./templates/README.md)**: Template system and schemas
- **[Agents](./agents/README.md)**: Agent orchestration patterns
- **[Workflows](./workflows/README.md)**: Process framework usage
- **[Evolution](./evolution/README.md)**: Self-evolution mechanisms
- **[API Reference](./api/README.md)**: REST/gRPC API documentation
- **[Examples](./examples/README.md)**: Use cases and implementations

## Contributing

This system is designed for self-evolution. Templates can be modified at runtime, and the system will adapt accordingly. See the [Evolution Guide](./evolution/README.md) for details on how the system improves itself.

## License

[License information] 