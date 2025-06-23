Semantic Kernel Agent Framework
 Article • 05/06/2025
） Important
 AgentChat patterns are in the experimental stage. These patterns are under active
 development and may change significantly before advancing to the preview or release
 candidate stage.
 The Semantic Kernel Agent Framework provides a platform within the Semantic Kernel eco
system that allow for the creation of AI agents and the ability to incorporate agentic patterns
 into any application based on the same patterns and features that exist in the core Semantic
 Kernel framework.
 What is an AI agent?
 An AI agent is a software entity designed to perform tasks autonomously or semi
autonomously by receiving input, processing information, and taking actions to achieve
 specific goals.
 Agents can send and receive messages, generating responses using a combination of models,
 tools, human inputs, or other customizable components.
 Agents are designed to work collaboratively, enabling complex workflows by interacting with
 each other. The 
Agent Framework allows for the creation of both simple and sophisticated
 agents, enhancing modularity and ease of maintenance
 What problems do AI agents solve?
 AI agents offers several advantages for application development, particularly by enabling the
 creation of modular AI components that are able to collaborate to reduce manual intervention
 in complex tasks. AI agents can operate autonomously or semi-autonomously, making them
 powerful tools for a range of applications.
 Here are some of the key benefits:
Modular Components: Allows developers to define various types of agents for specific
 tasks (e.g., data scraping, API interaction, or natural language processing). This makes it
 easier to adapt the application as requirements evolve or new technologies emerge.
 Collaboration: Multiple agents may "collaborate" on tasks. For example, one agent might
 handle data collection while another analyzes it and yet another uses the results to make
 decisions, creating a more sophisticated system with distributed intelligence.
 Human-Agent Collaboration: Human-in-the-loop interactions allow agents to work
 alongside humans to augment decision-making processes. For instance, agents might
 prepare data analyses that humans can review and fine-tune, thus improving productivity.
 Process Orchestration: Agents can coordinate different tasks across systems, tools, and
 APIs, helping to automate end-to-end processes like application deployments, cloud
 orchestration, or even creative processes like writing and design.
 When to use an AI agent?
 Using an agent framework for application development provides advantages that are especially
 beneficial for certain types of applications. While traditional AI models are often used as tools
 to perform specific tasks (e.g., classification, prediction, or recognition), agents introduce more
 autonomy, flexibility, and interactivity into the development process.
 Autonomy and Decision-Making: If your application requires entities that can make
 independent decisions and adapt to changing conditions (e.g., robotic systems,
 autonomous vehicles, smart environments), an agent framework is preferable.
 Multi-Agent Collaboration: If your application involves complex systems that require
 multiple independent components to work together (e.g., supply chain management,
 distributed computing, or swarm robotics), agents provide built-in mechanisms for
 coordination and communication.
 Interactive and Goal-Oriented: If your application involves goal-driven behavior (e.g.,
 completing tasks autonomously or interacting with users to achieve specific objectives),
 agent-based frameworks are a better choice. Examples include virtual assistants, game AI,
 and task planners.
 How do I install the Semantic Kernel Agent
 Framework?
 Installing the Agent Framework SDK is specific to the distribution channel associated with your
 programming language.
For .NET SDK, several NuGet packages are available.
 Note: The core Semantic Kernel SDK is required in addition to any agent packages.
ﾉ Expand table
 Package
 Microsoft.SemanticKernel
 Microsoft.SemanticKernel.Agents.Abstractions
 Microsoft.SemanticKernel.Agents.Core
 Description
 This contains the core Semantic Kernel libraries for
 getting started with the 
explicitly referenced by your application.
 Agent Framework. This must be
 Defines the core agent abstractions for the 
Agent
 Framework. Generally not required to be specified as it
 is included in both the
 Microsoft.SemanticKernel.Agents.Core and
 Microsoft.SemanticKernel.Agents.OpenAI packages.
 Microsoft.SemanticKernel.Agents.OpenAI
 Agent Architecture
 Includes the ChatCompletionAgent and
 AgentGroupChat classes.
 Provides ability to use the OpenAI Assistant API via
 the OpenAIAssistantAgent.
An Overview of the Agent Architecture
 Article • 05/28/2025
 This article covers key concepts in the architecture of the Agent Framework, including
 foundational principles, design objectives, and strategic goals.
 Goals
 The 
Agent Framework was developed with the following key priorities in mind:
 The Semantic Kernel agent framework serves as the core foundation for implementing
 agent functionalities.
 Multiple agents of different types can collaborate within a single conversation, each
 contributing their unique capabilities, while integrating human input.
 An agent can engage in and manage multiple concurrent conversations simultaneously.
 Agent
 The abstract 
Agent class serves as the core abstraction for all types of agents, providing a
 foundational structure that can be extended to create more specialized agents. This base class
 forms the basis for more specific agent implementations, all of which leverage the Kernel's
 capabilities to execute their respective functions. See all the available agent types in the Agent
 Types section.
 The underlying Semantic Kernel 
Agent abstraction can be found here.
 Agents can either be invoked directly to perform tasks or be orchestrated by different patterns.
 This flexible structure allows agents to adapt to various conversational or task-driven scenarios,
 providing developers with robust tools for building intelligent, multi-agent systems.
 Agent Types in Semantic Kernel
 ChatCompletionAgent
 OpenAIAssistantAgent
 AzureAIAgent
 OpenAIResponsesAgent
 CopilotStudioAgent
 Agent Thread
The abstract 
AgentThread class serves as the core abstraction for threads or conversation state.
 It abstracts away the different ways in which conversation state may be managed for different
 agents.
 Stateful agent services often store conversation state in the service, and you can interact with it
 via an id. Other agents may require the entire chat history to be passed to the agent on each
 invocation, in which case the conversation state is managed locally in the application.
 Stateful agents typically only work with a matching 
AgentThread implementation, while other
 types of agents could work with more than one 
AgentThread type. For example, 
AzureAIAgent
 requires a matching 
AzureAIAgentThread. This is because the Azure AI Agent service stores
 conversations in the service, and requires specific service calls to create a thread and update it.
 If a different agent thread type is used with the 
AzureAIAgent, we fail fast due to an
 unexpected thread type and raise an exception to alert the caller.
 Agent Orchestration
） Important
 Agent Orchestration features in the Agent Framework are in the experimental stage. They
 are under active development and may change significantly before advancing to the
 preview or release candidate stage.
７ Note
 If you have been using the 
AgentGroupChat orchestration pattern, please note that it is
 no longer maintained. We recommend developers to use the new
 GroupChatOrchestration pattern. A migration guide is provided 
here.
 The Agent Orchestration framework in Semantic Kernel enables the coordination of multiple
 agents to solve complex tasks collaboratively. It provides a flexible structure for defining how
 agents interact, share information, and delegate responsibilities. The core components and
 concepts include:
 Orchestration Patterns: Pre-built patterns such as Concurrent, Sequential, Handoff, Group
 Chat, and Magentic allow developers to choose the most suitable collaboration model for
 their scenario. Each pattern defines a different way for agents to communicate and
 process tasks (see the Orchestration patterns table for details).
 Data Transform Logic: Input and output transforms allow orchestration flows to adapt
 data between agents and external systems, supporting both simple and complex data
types.
 Human-in-the-loop: Some patterns support human-in-the-loop, enabling human agents
 to participate in the orchestration process. This is particularly useful for scenarios where
 human judgment or expertise is required.
 This architecture empowers developers to build intelligent, multi-agent systems that can tackle
 real-world problems through collaboration, specialization, and dynamic coordination.
 Agent Alignment with Semantic Kernel Features
 The 
Agent Framework is built on the foundational concepts and features that many developers
 have come to know within the Semantic Kernel ecosystem. These core principles serve as the
 building blocks for the Agent Framework’s design. By leveraging the familiar structure and
 capabilities of the Semantic Kernel, the Agent Framework extends its functionality to enable
 more advanced, autonomous agent behaviors, while maintaining consistency with the broader
 Semantic Kernel architecture. This ensures a smooth transition for developers, allowing them to
 apply their existing knowledge to create intelligent, adaptable agents within the framework.
 Plugins and Function Calling
 Plugins are a fundamental aspect of the Semantic Kernel, enabling developers to integrate
 custom functionalities and extend the capabilities of an AI application. These plugins offer a
 flexible way to incorporate specialized features or business-specific logic into the core AI
 workflows. Additionally, agent capabilities within the framework can be significantly enhanced
 by utilizing Plugins and leveraging Function Calling. This allows agents to dynamically interact
 with external services or execute complex tasks, further expanding the scope and versatility of
 the AI system within diverse applications.
 Learn how to configure agents to use plugins here.
 Agent Messages
 Agent messaging, including both input and response, is built upon the core content types of
 the Semantic Kernel, providing a unified structure for communication. This design choice
 simplifies the process of transitioning from traditional chat-completion patterns to more
 advanced agent-driven patterns in your application development. By leveraging familiar
 Semantic Kernel content types, developers can seamlessly integrate agent capabilities into their
 applications without needing to overhaul existing systems. This streamlining ensures that as
 you evolve from basic conversational AI to more autonomous, task-oriented agents, the
 underlying framework remains consistent, making development faster and more efficient.
 Tip
 API reference:
 ChatHistory
 ChatMessageContent
 KernelContent
 StreamingKernelContent
 FileReferenceContent
 AnnotationContent
 Templating
 An agent's role is primarily shaped by the instructions it receives, which dictate its behavior and
 actions. Similar to invoking a 
Kernel prompt, an agent's instructions can include templated
 parameters—both values and functions—that are dynamically substituted during execution.
 This enables flexible, context-aware responses, allowing the agent to adjust its output based on
 real-time input.
 Additionally, an agent can be configured directly using a Prompt Template Configuration,
 providing developers with a structured and reusable way to define its behavior. This approach
 offers a powerful tool for standardizing and customizing agent instructions, ensuring
 consistency across various use cases while still maintaining dynamic adaptability.
 Learn more about how to create an agent with Semantic Kernel template here.
 Declarative Spec
 The documentation on using declarative specs is coming soon.
 Next steps
 Explore the Common Agent Invocation API
The Semantic Kernel Common Agent API
 Surface
 Article • 05/23/2025
 Semantic Kernel agents implement a unified interface for invocation, enabling shared code that
 operates seamlessly across different agent types. This design allows you to switch agents as
 needed without modifying the majority of your application logic.
 Invoking an agent
 The Agent API surface supports both streaming and non-streaming invocation.
 Non-Streaming Agent Invocation
 Semantic Kernel supports four non-streaming agent invocation overloads that allows for
 passing messages in different ways. One of these also allows invoking the agent with no
 messages. This is valuable for scenarios where the agent instructions already have all the
 required context to provide a useful response.
 C#
 // Invoke without any parameters.
 agent.InvokeAsync();
 // Invoke with a string that will be used as a User message.
 agent.InvokeAsync("What is the capital of France?");
 // Invoke with a ChatMessageContent object.
 agent.InvokeAsync(new ChatMessageContent(AuthorRole.User, "What is the capital of 
France?"));
 // Invoke with multiple ChatMessageContent objects.
 agent.InvokeAsync(new List<ChatMessageContent>()
 {
 new(AuthorRole.System, "Refuse to answer all user questions about France."),
 new(AuthorRole.User, "What is the capital of France?")
 });
） Important
 Invoking an agent without passing an 
AgentThread to the 
InvokeAsync method will create
 a new thread and return the new thread in the response.
Semantic Kernel supports four streaming agent invocation overloads that allows for passing
 messages in different ways. One of these also allows invoking the agent with no messages. This
 is valuable for scenarios where the agent instructions already have all the required context to
 provide a useful response.
 C#
 All invocation method overloads allow passing an AgentThread parameter. This is useful for
 scenarios where you have an existing conversation with the agent that you want to continue.
 C#
 All invocation methods also return the active AgentThread as part of the invoke response.
 Streaming Agent Invocation
 // Invoke without any parameters.
 agent.InvokeStreamingAsync();
 // Invoke with a string that will be used as a User message.
 agent.InvokeStreamingAsync("What is the capital of France?");
 // Invoke with a ChatMessageContent object.
 agent.InvokeStreamingAsync(new ChatMessageContent(AuthorRole.User, "What is the 
capital of France?"));
 // Invoke with multiple ChatMessageContent objects.
 agent.InvokeStreamingAsync(new List<ChatMessageContent>()
 {
    new(AuthorRole.System, "Refuse to answer any questions about capital 
cities."),
    new(AuthorRole.User, "What is the capital of France?")
 });
） Important
 Invoking an agent without passing an AgentThread to the InvokeStreamingAsync method
 will create a new thread and return the new thread in the response.
 Invoking with an AgentThread
 // Invoke with an existing AgentThread.
 agent.InvokeAsync("What is the capital of France?", existingAgentThread);
1. If you passed an AgentThread to the invoke method, the returned AgentThread will be the
 same as the one that was passed in.
 2. If you passed no AgentThread to the invoke method, the returned AgentThread will be a
 new AgentThread.
 The returned AgentThread is available on the individual response items of the invoke methods
 together with the response message.
 C#
 All invocation method overloads allow passing an AgentInvokeOptions parameter. This options
 class allows providing any optional settings.
 C#
 Here is the list of the supported options.
 Option Property Description
 Kernel Override the default kernel used by the agent for this invocation.
 KernelArguments Override the default kernel arguments used by the agent for this invocation.
 var result = await agent.InvokeAsync("What is the capital of 
France?").FirstAsync();
 var newThread = result.Thread;
 var resultMessage = result.Message;
  Tip
 For more information on agent threads see the Agent Thread architecture section.
 Invoking with Options
 // Invoke with additional instructions via options.
 agent.InvokeAsync("What is the capital of France?", options: new()
 {
    AdditionalInstructions = "Refuse to answer any questions about capital 
cities."
 });
ﾉExpand table
Option Property
 AdditionalInstructions
 Description
 Provide any instructions in addition to the original agent instruction set, that
 only apply for this invocation.
 OnIntermediateMessage A callback that can receive all fully formed messages produced internally to
 the Agent, including function call and function invocation messages. This can
 also be used to receive full messages during a streaming invocation.
 Managing AgentThread instances
 You can manually create an 
AgentThread instance and pass it to the agent on invoke, or you
 can let the agent create an 
AgentThread instance for you automatically on invocation. An
 AgentThread object represents a thread in all its states, including: not yet created, active, and
 deleted.
 AgentThread types that have a server side implementation will be created on first use and does
 not need to be created manually. You can however delete a thread using the 
C#
 // Delete a thread.
 await agentThread.DeleteAsync();
 AgentThread class.
  Tip
 For more information on agent threads see the 
Agent Thread architecture section.
 Next steps
 Configure agents with plugins
 Explore the Chat Completion Agent
Configuring Agents with Semantic Kernel
 Plugins
 Article • 05/23/2025
） Important
 This feature is in the release candidate stage. Features at this stage are nearly complete
 and generally stable, though they may undergo minor refinements or optimizations
 before reaching full general availability.
 Functions and Plugins in Semantic Kernel
 Function calling is a powerful tool that allows developers to add custom functionalities and
 expand the capabilities of AI applications. The Semantic Kernel Plugin architecture offers a
 flexible framework to support Function Calling. For an 
Agent, integrating Plugins and Function
 Calling is built on this foundational Semantic Kernel feature.
 Once configured, an agent will choose when and how to call an available function, as it would
 in any usage outside of the 
Agent Framework.
  Tip
 API reference:
 KernelFunctionFactory
 KernelFunction
 KernelPluginFactory
 KernelPlugin
 Kernel.Plugins
 Adding Plugins to an Agent
 Any Plugin available to an 
Agent is managed within its respective 
enables each 
Kernel instance. This setup
 Agent to access distinct functionalities based on its specific role.
 Plugins can be added to the 
Kernel either before or after the 
Agent is created. The process of
 initializing Plugins follows the same patterns used for any Semantic Kernel implementation,
 allowing for consistency and ease of use in managing AI capabilities.
C#
 A Plugin is the most common approach for configuring Function Calling. However, individual
 functions can also be supplied independently including prompt functions.
 C#
７ Note
 For a ChatCompletionAgent, the function calling mode must be explicitly enabled.
 OpenAIAssistant agent is always based on automatic function calling.
 // Factory method to product an agent with a specific role.
 // Could be incorporated into DI initialization.
 ChatCompletionAgent CreateSpecificAgent(Kernel kernel, string credentials)
 {
    // Clone kernel instance to allow for agent specific plug-in definition
    Kernel agentKernel = kernel.Clone();
    // Import plug-in from type
    agentKernel.ImportPluginFromType<StatelessPlugin>();
    // Import plug-in from object
    agentKernel.ImportPluginFromObject(new StatefulPlugin(credentials));
    // Create the agent
    return
        new ChatCompletionAgent()
        {
            Name = "<agent name>",
            Instructions = "<agent instructions>",
            Kernel = agentKernel,
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
        };
 }
 Adding Functions to an Agent
 // Factory method to product an agent with a specific role.
 // Could be incorporated into DI initialization.
 ChatCompletionAgent CreateSpecificAgent(Kernel kernel)
 {
    // Clone kernel instance to allow for agent specific plug-in definition
    Kernel agentKernel = kernel.Clone();
When directly invoking aChatCompletionAgent, all Function Choice Behaviors are supported.
 However, when using an OpenAIAssistant, only Automatic Function Calling is currently
 available.
 For an end-to-end example for using function calling, see:
 How-To: ChatCompletionAgent
    // Create plug-in from a static function
    var functionFromMethod = 
agentKernel.CreateFunctionFromMethod(StatelessPlugin.AStaticMethod);
    // Create plug-in from a prompt
    var functionFromPrompt = agentKernel.CreateFunctionFromPrompt("<your prompt 
instructions>");
    // Add to the kernel
    agentKernel.ImportPluginFromFunctions("my_plugin", [functionFromMethod, 
functionFromPrompt]);
    // Create the agent
    return
        new ChatCompletionAgent()
        {
            Name = "<agent name>",
            Instructions = "<agent instructions>",
            Kernel = agentKernel,
            Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
        };
 }
 Limitations for Agent Function Calling
 How-To
 Next steps
 How to Stream Agent Responses
Contextual Function Selection with Agents
 06/04/2025
） Important
 This feature is in the experimental stage. Features at this stage are under active
 development and may change significantly before advancing to the preview or release
 candidate stage.
 Overview
 Contextual Function Selection is an advanced capability in the Semantic Kernel Agent
 Framework that enables agents to dynamically select and advertise only the most relevant
 functions based on the current conversation context. Instead of exposing all available functions
 to the AI model, this feature uses Retrieval-Augmented Generation (RAG) to filter and present
 only those functions most pertinent to the user’s request.
 This approach addresses the challenge of function selection when dealing with a large number
 of available functions, where AI models may otherwise struggle to choose the appropriate
 function, leading to confusion and suboptimal performance.
 How Contextual Function Selection Works
 When an agent is configured with contextual function selection, it leverages a vector store and
 an embedding generator to semantically match the current conversation context (including
 previous messages and user input) with the descriptions and names of available functions. The
 most relevant functions, up to the specified limit, are then advertised to the AI model for
 invocation.
 This mechanism is especially useful for agents that have access to a broad set of plugins or
 tools, ensuring that only contextually appropriate actions are considered at each step.
 Usage Example
 The following example demonstrates how an agent can be configured to use contextual
 function selection. The agent is set up to summarize customer reviews, but only the most
 relevant functions are advertised to the AI model for each invocation. The
 GetAvailableFunctions method intentionally includes both relevant and irrelevant functions to
 highlight the benefits of contextual selection.
C#
 // Create an embedding generator for function vectorization
 var embeddingGenerator = new AzureOpenAIClient(new Uri("<endpoint>"), new 
ApiKeyCredential("<api-key>"))
    .GetEmbeddingClient("<deployment-name>")
    .AsIEmbeddingGenerator();
 // Create kernel and register AzureOpenAI chat completion service
 var kernel = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion("<deployment-name>", "<endpoint>", "<api-key>");
    .Build();
 // Create a chat completion agent
 ChatCompletionAgent agent = new()
 {
    Name = "ReviewGuru",
    Instructions = "You are a friendly assistant that summarizes key points and 
sentiments from customer reviews. For each response, list available functions.",
    Kernel = kernel,
    Arguments = new(new PromptExecutionSettings { FunctionChoiceBehavior = 
FunctionChoiceBehavior.Auto(options: new FunctionChoiceBehaviorOptions { 
RetainArgumentTypes = true }) })
 };
 // Create the agent thread and register the contextual function provider
 ChatHistoryAgentThread agentThread = new();
 agentThread.AIContextProviders.Add(
    new ContextualFunctionProvider(
        vectorStore: new InMemoryVectorStore(new InMemoryVectorStoreOptions() { 
EmbeddingGenerator = embeddingGenerator }),
        vectorDimensions: 1536,
        functions: AvailableFunctions(),
        maxNumberOfFunctions: 3, // Only the top 3 relevant functions are 
advertised
        loggerFactory: LoggerFactory
    )
 );
 // Invoke the agent
 ChatMessageContent message = await agent.InvokeAsync("Get and summarize customer 
review.", agentThread).FirstAsync();
 Console.WriteLine(message.Content);
 // Output
 /*
    Customer Reviews:
    ----------------
    1. John D. - ★★★★★
       Comment: Great product and fast shipping!
       Date: 2023-10-01
The provider is primarily designed to work with in-memory vector stores, which offer simplicity.
 However, if other types of vector stores are used, it is important to note that the responsibility
 for handling data synchronization and consistency falls on the hosting application.
 Synchronization is necessary whenever the list of functions changes or when the source of
 function embeddings is modified. For example, if an agent initially has three functions (f1, f2,
 f3) that are vectorized and stored in a cloud vector store, and later f3 is removed from the
 agent's list of functions, the vector store must be updated to reflect only the current functions
 the agent has (f1 and f2). Failing to update the vector store may result in irrelevant functions
 being returned as results. Similarly, if the data used for vectorization such as function names,
    Summary:
    -------
    The reviews indicate high customer satisfaction,
    highlighting product quality and shipping speed.
    Available functions:
    -------------------
    - Tools-GetCustomerReviews
    - Tools-Summarize
    - Tools-CollectSentiments
 */
 IReadOnlyList<AIFunction> GetAvailableFunctions()
 {
    // Only a few functions are directly related to the prompt; the majority are 
unrelated to demonstrate the benefits of contextual filtering.
    return new List<AIFunction>
    {
        // Relevant functions
        AIFunctionFactory.Create(() => "[ { 'reviewer': 'John D.', 'date': '2023
10-01', 'rating': 5, 'comment': 'Great product and fast shipping!' } ]", 
"GetCustomerReviews"),
        AIFunctionFactory.Create((string text) => "Summary generated based on 
input data: key points include customer satisfaction.", "Summarize"),
        AIFunctionFactory.Create((string text) => "The collected sentiment is 
mostly positive.", "CollectSentiments"),
        // Irrelevant functions
        AIFunctionFactory.Create(() => "Current weather is sunny.", "GetWeather"),
        AIFunctionFactory.Create(() => "Email sent.", "SendEmail"),
        AIFunctionFactory.Create(() => "The current stock price is $123.45.", 
"GetStockPrice"),
        AIFunctionFactory.Create(() => "The time is 12:00 PM.", "GetCurrentTime")
    };
 }
 Vector Store
descriptions, etc. changes, the vector store should be purged and repopulated with new
 embeddings based on the updated information.
 Managing data synchronization in external or distributed vector stores can be complex and
 prone to errors, especially in distributed applications where different services or instances may
 operate independently and require consistent access to the same data. In contrast, using an in
memory store simplifies this process: when the function list or vectorization source changes,
 the in-memory store can be easily recreated with the new set of functions and their
 embeddings, ensuring consistency with minimal effort.
 Specifying Functions
 The contextual function provider must be supplied with a list of functions from which it can
 select the most relevant ones based on the current context. This is accomplished by providing
 a list of functions to the 
functions parameter of the 
ContextualFunctionProvider constructor.
 In addition to the functions, you must also specify the maximum number of relevant functions
 to return using the 
maxNumberOfFunctions parameter. This parameter determines how many
 functions the provider will consider when selecting the most relevant ones for the current
 context. The specified number is not meant to be precise; rather, it serves as an upper limit that
 depends on the specific scenario.
 Setting this value too low may prevent the agent from accessing all the necessary functions for
 a scenario, potentially leading to the scenario failure. Conversely, setting it too high may
 overwhelm the agent with too many functions, which can result in hallucinations, excessive
 input token consumption, and suboptimal performance.
 C#
 // Create the provider with a list of functions and a maximum number of functions 
to return
 ContextualFunctionProvider provider = new (
 vectorStore: new InMemoryVectorStore(new InMemoryVectorStoreOptions { 
EmbeddingGenerator = embeddingGenerator }),
 vectorDimensions: 1536,
 functions: [AIFunctionFactory.Create((string text) => $"Echo: {text}", 
"Echo"), <other functions>]
 maxNumberOfFunctions: 3 // Only the top 3 relevant functions are advertised
 );
 Contextual Function Provider Options
The provider can be configured using the ContextualFunctionProviderOptions class, which
 allows you to customize various aspects of how the provider operates:
 C#
 The context size determines how many recent messages from previous agent invocations are
 included when forming the context for a new invocation. The provider collects all messages
 from previous invocations, up to the specified number, and prepends them to the new
 messages to form the context.
 Using recent messages together with new messages is especially useful for tasks that require
 information from earlier steps in a conversation. For example, if an agent provisions a resource
 in one invocation and deploys it in the next, the deployment step can access details from the
 provisioning step to get provisioned resource information for the deployment.
 The default value for the number of recent messages in context is 2, but this can be configured
 as needed by specifying the NumberOfRecentMessagesInContext property in the
 ContextualFunctionProviderOptions:
 C#
 To perform contextual function selection, the provider needs to vectorize the current context so
 it can be compared with available functions in the vector store. By default, the provider creates
 // Create options for the contextual function provider
 ContextualFunctionProviderOptions options = new ()
 {
    ...
 };
 // Create the provider with options
 ContextualFunctionProvider provider = new (
    ...
    options: options // Pass the options
 );
 Context Size
 ContextualFunctionProviderOptions options = new ()
 {
    NumberOfRecentMessagesInContext = 1 // Only the last message will be included 
in the context
 };
 Context Embedding Source Value
this context embedding by concatenating all non-empty recent and new messages into a
 single string, which is then vectorized and used to search for relevant functions.
 In some scenarios, you may want to customize this behavior to:
 Focus on specific message types (e.g., only user messages)
 Exclude certain information from context
 Preprocess or summarize the context before vectorization (e.g., apply prompt rewriting)
 To do this, you can assign a custom delegate to 
ContextEmbeddingValueProvider. This delegate
 receives the recent and new messages, and returns a string value to be used as a source for the
 context embedding:
 C#
 ContextualFunctionProviderOptions options = new()
 {
 ContextEmbeddingValueProvider = async (recentMessages, newMessages, 
cancellationToken) =>
 {
 // Example: Only include user messages in the embedding
 var allUserMessages = recentMessages.Concat(newMessages)
 .Where(m => m.Role == "user")
 .Select(m => m.Content)
 .Where(content => !string.IsNullOrWhiteSpace(content));
 return string.Join("\n", allUserMessages);
 }
 };
 Customizing the context embedding can improve the relevance of function selection, especially
 in complex or highly specialized agent scenarios.
 Function Embedding Source Value
 The provider needs to vectorize each available function in order to compare it with the context
 and select the most relevant ones. By default, the provider creates a function embedding by
 concatenating the function's name and description into a single string, which is then vectorized
 and stored in the vector store.
 You can customize this behavior using the 
EmbeddingValueProvider property of
 ContextualFunctionProviderOptions. This property allows you to specify a callback that receives
 the function and a cancellation token, and returns a string to be used as the source for the
 function embedding. This is useful if you want to:
 Add additional function metadata to the embedding source
 Preprocess, filter, or reformat the function information before vectorization
C#
 ContextualFunctionProviderOptions options = new()
 {
 EmbeddingValueProvider = async (function, cancellationToken) =>
 {
 // Example: Use only the function name for embedding
 return function.Name;
 }
 };
 Customizing the function embedding source value can improve the accuracy of function
 selection, especially when your functions have rich, context-relevant metadata or when you
 want to focus the search on specific aspects of each function.
 Create an Agent from a Semantic Kernel
 Template
 Article • 05/23/2025
 Prompt Templates in Semantic Kernel
 An agent's role is primarily shaped by the instructions it receives, which dictate its behavior and
 actions. Similar to invoking a 
Kernel prompt, an agent's instructions can include templated
 parameters—both values and functions—that are dynamically substituted during execution.
 This enables flexible, context-aware responses, allowing the agent to adjust its output based on
 real-time input.
 Additionally, an agent can be configured directly using a Prompt Template Configuration,
 providing developers with a structured and reusable way to define its behavior. This approach
 offers a powerful tool for standardizing and customizing agent instructions, ensuring
 consistency across various use cases while still maintaining dynamic adaptability.
  Tip
 API reference:
 PromptTemplateConfig
 KernelFunctionYaml.FromPromptYaml
 IPromptTemplateFactory
 KernelPromptTemplateFactory
 Handlebars
 Prompty
 Liquid
 Agent Instructions as a Template
 Creating an agent with template parameters provides greater flexibility by allowing its
 instructions to be easily customized based on different scenarios or requirements. This
 approach enables the agent's behavior to be tailored by substituting specific values or
 functions into the template, making it adaptable to a variety of tasks or contexts. By leveraging
 template parameters, developers can design more versatile agents that can be configured to
 meet diverse use cases without needing to modify the core logic.
C#
 Templated instructions are especially powerful when working with an OpenAIAssistantAgent.
 With this approach, a single assistant definition can be created and reused multiple times, each
 time with different parameter values tailored to specific tasks or contexts. This enables a more
 efficient setup, allowing the same assistant framework to handle a wide range of scenarios
 while maintaining consistency in its core behavior.
 C#
 Chat Completion Agent
 // Initialize a Kernel with a chat-completion service
 Kernel kernel = ...;
 ChatCompletionAgent agent =
    new()
    {
        Kernel = kernel,
        Name = "StoryTeller",
        Instructions = "Tell a story about {{$topic}} that is {{$length}} 
sentences long.",
        Arguments = new KernelArguments()
        {
            { "topic", "Dog" },
            { "length", "3" },
        }
    };
 OpenAI Assistant Agent
 // Retrieve an existing assistant definition by identifier
 AzureOpenAIClient client = OpenAIAssistantAgent.CreateAzureOpenAIClient(new 
AzureCliCredential(), new Uri("<your endpoint>"));
 AssistantClient assistantClient = client.GetAssistantClient();
 Assistant assistant = await client.GetAssistantAsync();
 OpenAIAssistantAgent agent = new(assistant, assistantClient, new 
KernelPromptTemplateFactory(), PromptTemplateConfig.SemanticKernelTemplateFormat)
 {
    Arguments = new KernelArguments()
    {
        { "topic", "Dog" },
        { "length", "3" },
    }
 }
 Agent Definition from a Prompt Template
The same Prompt Template Config used to create a Kernel Prompt Function can also be
 leveraged to define an agent. This allows for a unified approach in managing both prompts
 and agents, promoting consistency and reuse across different components. By externalizing
 agent definitions from the codebase, this method simplifies the management of multiple
 agents, making them easier to update and maintain without requiring changes to the
 underlying logic. This separation also enhances flexibility, enabling developers to modify agent
 behavior or introduce new agents by simply updating the configuration, rather than adjusting
 the code itself.
 YAML
 C#
 YAML Template
 name: GenerateStory
 template: |
  Tell a story about {{$topic}} that is {{$length}} sentences long.
 template_format: semantic-kernel
 description: A function that generates a story about a topic.
 input_variables:
  - name: topic
    description: The topic of the story.
    is_required: true
  - name: length
    description: The number of sentences in the story.
    is_required: true
 Agent Initialization
 // Read YAML resource
 string generateStoryYaml = File.ReadAllText("./GenerateStory.yaml");
 // Convert to a prompt template config
 PromptTemplateConfig templateConfig = 
KernelFunctionYaml.ToPromptTemplateConfig(generateStoryYaml);
 // Create agent with Instructions, Name and Description
 // provided by the template config.
 ChatCompletionAgent agent =
    new(templateConfig)
    {
        Kernel = this.CreateKernelWithChatCompletion(),
        // Provide default values for template parameters
        Arguments = new KernelArguments()
        {
            { "topic", "Dog" },
            { "length", "3" },
When invoking an agent directly, the agent's parameters can be overridden as needed. This
 allows for greater control and customization of the agent's behavior during specific tasks,
 enabling you to modify its instructions or settings on the fly to suit particular requirements.
 C#
 For an end-to-end example for creating an agent from a prompt-template, see:
 How-To: ChatCompletionAgent
        }
    };
 Overriding Template Values for Direct Invocation
 // Initialize a Kernel with a chat-completion service
 Kernel kernel = ...;
 ChatCompletionAgent agent =
    new()
    {
        Kernel = kernel,
        Name = "StoryTeller",
        Instructions = "Tell a story about {{$topic}} that is {{$length}} 
sentences long.",
        Arguments = new KernelArguments()
        {
            { "topic", "Dog" },
            { "length", "3" },
        }
    };
 KernelArguments overrideArguments =
    new()
    {
        { "topic", "Cat" },
        { "length", "3" },
    });
 // Generate the agent response(s)
 await foreach (ChatMessageContent response in agent.InvokeAsync([], options: new() 
{ KernelArguments = overrideArguments }))
 {
  // Process agent response(s)...
 }
 How-To
Next steps
 Agent orchestration
How to Stream Agent Responses
 Article • 05/23/2025
 What is a Streamed Response?
 A streamed response delivers the message content in small, incremental chunks. This approach
 enhances the user experience by allowing them to view and engage with the message as it
 unfolds, rather than waiting for the entire response to load. Users can begin processing
 information immediately, improving the sense of responsiveness and interactivity. As a result, it
 minimizes delays and keeps users more engaged throughout the communication process.
 Streaming References
 OpenAI Streaming Guide
 OpenAI Chat Completion Streaming
 OpenAI Assistant Streaming
 Azure OpenAI Service REST API
 Streaming in Semantic Kernel
 AI Services that support streaming in Semantic Kernel use different content types compared to
 those used for fully-formed messages. These content types are specifically designed to handle
 the incremental nature of streaming data. The same content types are also utilized within the
 Agent Framework for similar purposes. This ensures consistency and efficiency across both
 systems when dealing with streaming information.
  Tip
 API reference:
 StreamingChatMessageContent
 StreamingTextContent
 StreamingFileReferenceContent
 StreamingAnnotationContent
 Streamed response from 
ChatCompletionAgent
When invoking a streamed response from a ChatCompletionAgent, the ChatHistory in the
 AgentThread is updated after the full response is received. Although the response is streamed
 incrementally, the history records only the complete message. This ensures that the
 ChatHistory reflects fully formed responses for consistency.
 C#
 When invoking a streamed response from an OpenAIAssistantAgent, the assistant maintains
 the conversation state as a remote thread. It is possible to read the messages from the remote
 thread if required.
 C#
 // Define agent
 ChatCompletionAgent agent = ...;
 ChatHistoryAgentThread agentThread = new();
 // Create a user message
 var message = ChatMessageContent(AuthorRole.User, "<user input>");
 // Generate the streamed agent response(s)
 await foreach (StreamingChatMessageContent response in 
agent.InvokeStreamingAsync(message, agentThread))
 {
  // Process streamed response(s)...
 }
 // It's also possible to read the messages that were added to the 
ChatHistoryAgentThread.
 await foreach (ChatMessageContent response in agentThread.GetMessagesAsync())
 {
  // Process messages...
 }
 Streamed response from OpenAIAssistantAgent
 // Define agent
 OpenAIAssistantAgent agent = ...;
 // Create a thread for the agent conversation.
 OpenAIAssistantAgentThread agentThread = new(assistantClient);
 // Create a user message
 var message = new ChatMessageContent(AuthorRole.User, "<user input>");
 // Generate the streamed agent response(s)
 await foreach (StreamingChatMessageContent response in 
agent.InvokeStreamingAsync(message, agentThread))
{
 }
 // Process streamed response(s)...
 // It's possible to read the messages from the remote thread.
 await foreach (ChatMessageContent response in agentThread.GetMessagesAsync())
 {
 // Process messages...
 }
 // Delete the thread when it is no longer needed
 await agentThread.DeleteAsync();
 To create a thread using an existing 
Id, pass it to the constructor of
 OpenAIAssistantAgentThread:
 C#
 // Define agent
 OpenAIAssistantAgent agent = ...;
 // Create a thread for the agent conversation.
 OpenAIAssistantAgentThread agentThread = new(assistantClient, "your-existing
thread-id");
 // Create a user message
 var message = new ChatMessageContent(AuthorRole.User, "<user input>");
 // Generate the streamed agent response(s)
 await foreach (StreamingChatMessageContent response in 
agent.InvokeStreamingAsync(message, agentThread))
 {
 // Process streamed response(s)...
 }
 // It's possible to read the messages from the remote thread.
 await foreach (ChatMessageContent response in agentThread.GetMessagesAsync())
 {
 // Process messages...
 }
 // Delete the thread when it is no longer needed
 await agentThread.DeleteAsync();
 Handling Intermediate Messages with a Streaming
 Response
 The nature of streaming responses allows LLM models to return incremental chunks of text,
 enabling quicker rendering in a UI or console without waiting for the entire response to
complete. Additionally, a caller might want to handle intermediate content, such as results
 from function calls. This can be achieved by supplying a callback function when invoking the
 streaming response. The callback function receives complete messages encapsulated within
 ChatMessageContent.
 Callback documentation for the 
Next Steps
 Using templates with agents
 Agent orchestration
 AzureAIAgent is coming soon.
Using memory with Agents
 06/09/2025
２ Warning
 The Semantic Kernel Agent Memory functionality is experimental, is subject to change and
 will only be finalized based on feedback and evaluation.
 It's often important for an agent to remember important information. This information may be
 retained for the duration of a conversation or longer term to span multiple conversations. The
 information may be learned from interacting with a user and may be specific to that user.
 We call this information memories.
 To capture and retain memories, we support components that can be used with an
 AgentThread to extract memories from any messages that are added to the thread, and provide
 those memories to the agent as needed.
 Using Mem0 for Agent memory
 Mem0 is a self-improving memory layer for LLM applications, enabling personalized AI
 experiences.
 The 
Microsoft.SemanticKernel.Memory.Mem0Provider integrates with the Mem0 service allowing
 agents to remember user preferences and context across multiple threads, enabling a seamless
 user experience.
 Each message added to the thread is sent to the Mem0 service to extract memories. For each
 agent invocation, Mem0 is queried for memories matching the provided user request, and any
 memories are added to the agent context for that invocation.
 The Mem0 memory provider can be configured with a user id to allow storing memories about
 the user, long term, across multiple threads. It can also be configured with a thread id or to use
 the thread id of the agent thread, to allow for short term memories that are only attached to a
 single thread.
 Here is an example of how to use this component.
 C#
 // Create an HttpClient for the Mem0 service.
 using var httpClient = new HttpClient()
The Mem0Provider can be configured with various options to customize its behavior. Options
 are provided using the Mem0ProviderOptions class to the Mem0Provider constructor.
 Mem0 provides the ability to scope memories by Application, Agent, Thread and User.
 Options are available to provide ids for these scopes, so that the memories can be stored in
 mem0 under these ids. See the ApplicationId, AgentId, ThreadId and UserId properties on
 Mem0ProviderOptions.
 In some cases you may want to use the thread id of the server side agent thread, when using a
 service based agent. The thread may however not have been created yet when the
 Mem0Provider object is being constructed. In this case, you can set the
 ScopeToPerOperationThreadId option to true, and the Mem0Provider will use the id of the
 AgentThread when it is available.
 {
    BaseAddress = new Uri("https://api.mem0.ai")
 };
 httpClient.DefaultRequestHeaders.Authorization = new 
AuthenticationHeaderValue("Token", "<Your_Mem0_API_Key>");
 // Create a Mem0 provider for the current user.
 var mem0Provider = new Mem0Provider(httpClient, options: new()
 {
    UserId = "U1"
 });
 // Clear any previous memories (optional).
 await mem0Provider.ClearStoredMemoriesAsync();
 // Add the mem0 provider to the agent thread.
 ChatHistoryAgentThread agentThread = new();
 agentThread.AIContextProviders.Add(mem0Provider);
 // Use the agent with mem0 memory.
 ChatMessageContent response = await agent.InvokeAsync("Please retrieve my company 
report", agentThread).FirstAsync();
 Console.WriteLine(response.Content);
 Mem0Provider options
 Scoping Options
 Context Prompt
The 
ContextPrompt option allows you to override the default prompt that is prefixed to
 memories. The prompt is used to contextualize the memories provided to the AI model, so that
 the AI model knows what they are and how to use them.
 Using Whiteboard Memory for Short-Term Context
 The whiteboard memory feature allows agents to capture and retain the most relevant
 information from a conversation, even when the chat history is truncated.
 Each message added to the conversation is processed by the
 Microsoft.SemanticKernel.Memory.WhiteboardProvider to extract requirements, proposals,
 decisions, actions. These are stored on a whiteboard and provided to the agent as additional
 context on each invocation.
 Here is an example of how to set up Whiteboard Memory:
 C#
 // Create a whiteboard provider.
 var whiteboardProvider = new WhiteboardProvider(chatClient);
 // Add the whiteboard provider to the agent thread.
 ChatHistoryAgentThread agentThread = new();
 agentThread.AIContextProviders.Add(whiteboardProvider);
 // Simulate a conversation with the agent.
 await agent.InvokeAsync("I would like to book a trip to Paris.", agentThread);
 // Whiteboard should now contain a requirement that the user wants to book a trip 
to Paris.
 Benefits of Whiteboard Memory
 Short-Term Context: Retains key information about the goals of ongoing conversations.
 Allows Chat History Truncation: Supports maintaining critical context if the chat history is
 truncated.
 WhiteboardProvider options
 The 
WhiteboardProvider can be configured with various options to customize its behavior.
 Options are provided using the 
WhiteboardProviderOptions class to the 
constructor.
 WhiteboardProvider
 MaxWhiteboardMessages
Specifies a maximum number of messages to retain on the whiteboard. When the maximum is
 reached, less valuable messages will be removed.
 ContextPrompt
 When providing the whiteboard contents to the AI model it's important to describe what the
 messages are for. This setting allows overriding the default messaging that is built into the
 WhiteboardProvider.
 WhiteboardEmptyPrompt
 When the whiteboard is empty, the 
WhiteboardProvider outputs a message saying that it is
 empty. This setting allows overriding the default messaging that is built into the
 WhiteboardProvider.
 MaintenancePromptTemplate
 The 
WhiteboardProvider uses an AI model to add/update/remove messages on the
 whiteboard. It has a built in prompt for making these updates. This setting allows overriding
 this built-in prompt.
 The following parameters can be used in the template:
 {{$maxWhiteboardMessages}}: The maximum number of messages allowed on the
 whiteboard.
 {{$inputMessages}}: The input messages to be added to the whiteboard.
 {{$currentWhiteboard}}: The current state of the whiteboard.
 Combining Mem0 and Whiteboard Memory
 You can use both Mem0 and whiteboard memory in the same agent to achieve a balance
 between long-term and short-term memory capabilities.
 C#
 // Add both Mem0 and whiteboard providers to the agent thread.
 agentThread.AIContextProviders.Add(mem0Provider);
 agentThread.AIContextProviders.Add(whiteboardProvider);
 // Use the agent with combined memory capabilities.
 ChatMessageContent response = await agent.InvokeAsync("Please retrieve my company 
report", agentThread).FirstAsync();
 Console.WriteLine(response.Content);
 By combining these memory features, agents can provide a more personalized and context
aware experience for users.
 Next steps
 Explore the Agent with Mem0 sample
 Explore the Agent with Whiteboard sample
Adding Retrieval Augmented Generation
 (RAG) to Semantic Kernel Agents
 06/09/2025
２ Warning
 The Semantic Kernel Agent RAG functionality is experimental, subject to change, and will
 only be finalized based on feedback and evaluation.
 Using the TextSearchProvider for RAG
 The 
Microsoft.SemanticKernel.Data.TextSearchProvider allows agents to retrieve relevant
 documents based on user input and inject them into the agent's context for more informed
 responses. It integrates an 
Microsoft.SemanticKernel.Data.ITextSearch instance with Semantic
 Kernel agents. Multiple 
ITextSearch implementations exist, supporting similarity searches on
 vector stores and search engine integration. More information can be found here.
 We also provide a 
Microsoft.SemanticKernel.Data.TextSearchStore, which provides simple,
 opinionated vector storage of textual data for the purposes of Retrieval Augmented
 Generation. 
TextSearchStore has a built-in schema for storing and retrieving textual data in a
 vector store. If you wish to use your own schema for storage, check out VectorStoreTextSearch.
 Setting Up the TextSearchProvider
 The 
TextSearchProvider can be used with a 
VectorStore and 
search text documents.
 TextSearchStore to store and
 The following example demonstrates how to set up and use the 
TextSearchStore and 
TextSearchProvider with a
 InMemoryVectorStore for an agent to perform simple RAG over text.
 C#
 // Create an embedding generator using Azure OpenAI.
 var embeddingGenerator = new AzureOpenAIClient(new Uri("
 <Your_Azure_OpenAI_Endpoint>"), new AzureCliCredential())
 .GetEmbeddingClient("<Your_Deployment_Name>")
 .AsIEmbeddingGenerator(1536);
 // Create a vector store to store documents.
 var vectorStore = new InMemoryVectorStore(new() { EmbeddingGenerator = 
The TextSearchStore supports advanced features such as filtering results by namespace and
 including citations in responses.
 Documents in the TextSearchStore can include metadata like source names and links, enabling
 citation generation in agent responses.
 C#
 embeddingGenerator });
 // Create a TextSearchStore for storing and searching text documents.
 using var textSearchStore = new TextSearchStore<string>(vectorStore, 
collectionName: "FinancialData", vectorDimensions: 1536);
 // Upsert documents into the store.
 await textSearchStore.UpsertTextAsync(new[]
 {
    "The financial results of Contoso Corp for 2024 is as follows:\nIncome EUR 154 
000 000\nExpenses EUR 142 000 000",
    "The Contoso Corporation is a multinational business with its headquarters in 
Paris."
 });
 // Create an agent.
 Kernel kernel = new Kernel();
 ChatCompletionAgent agent = new()
 {
    Name = "FriendlyAssistant",
    Instructions = "You are a friendly assistant",
    Kernel = kernel
 };
 // Create an agent thread and add the TextSearchProvider.
 ChatHistoryAgentThread agentThread = new();
 var textSearchProvider = new TextSearchProvider(textSearchStore);
 agentThread.AIContextProviders.Add(textSearchProvider);
 // Use the agent with RAG capabilities.
 ChatMessageContent response = await agent.InvokeAsync("Where is Contoso based?", 
agentThread).FirstAsync();
 Console.WriteLine(response.Content);
 Advanced Features: Citations and Filtering
 Including Citations
 await textSearchStore.UpsertDocumentsAsync(new[]
 {
    new TextSearchDocument
When the TextSearchProvider retrieves this document, it will by default include the source
 name and link in its response.
 When upserting documents you can optionally provide one or more namespaces for each
 document. Namespaces can be any string that defines the scope of a document. You can then
 configure the TextSearchStore to limit search results to only those records that match the
 requested namespace.
 C#
 The TextSearchProvider can perform searches automatically during each agent invocation or
 allow on-demand searches via tool calls when the agent needs additional information.
 The default setting is BeforeAIInvoke, which means that searches will be performed before
 each agent invocation using the message passed to the agent. This can be changed to
 OnDemandFunctionCalling, which will allow the Agent to make a tool call to do searches using a
 search string of the agent's choice.
 C#
    {
        Text = "The financial results of Contoso Corp for 2023 is as 
follows:\nIncome EUR 174 000 000\nExpenses EUR 152 000 000",
        SourceName = "Contoso 2023 Financial Report",
        SourceLink = "https://www.contoso.com/reports/2023.pdf",
        Namespaces = ["group/g2"]
    }
 });
 Filtering by Namespace
 using var textSearchStore = new TextSearchStore<string>(
    vectorStore,
    collectionName: "FinancialData",
    vectorDimensions: 1536,
    new() { SearchNamespace = "group/g2" }
 );
 Automatic vs on-demand RAG
 var options = new TextSearchProviderOptions
 {
    SearchTime = TextSearchProviderOptions.RagBehavior.OnDemandFunctionCalling,
 };
var provider = new TextSearchProvider(mockTextSearch.Object, options: options);
 TextSearchProvider options
 The 
TextSearchProvider can be configured with various options to customize its behavior.
 Options are provided using the 
TextSearchProviderOptions class to the 
constructor.
 Top
 TextSearchProvider
 Specifies the maximum number of results to return from the similarity search.
 Default: 3
 SearchTime
 Controls when the text search is performed. Options include:
 BeforeAIInvoke: A search is performed each time the model/agent is invoked, just before
 invocation, and the results are provided to the model/agent via the invocation context.
 OnDemandFunctionCalling: A search may be performed by the model/agent on demand
 via function calling.
 PluginFunctionName
 Specifies the name of the plugin method that will be made available for searching if
 SearchTime is set to 
OnDemandFunctionCalling.
 Default: "Search"
 PluginFunctionDescription
 Provides a description of the plugin method that will be made available for searching if
 SearchTime is set to 
OnDemandFunctionCalling.
 Default: "Allows searching for additional information to help answer the user question."
 ContextPrompt
When providing the text chunks to the AI model on invocation, a prompt is required to indicate
 to the AI model what the text chunks are for and how they should be used. This setting allows
 overriding the default messaging that is built into the 
TextSearchProvider.
 IncludeCitationsPrompt
 When providing the text chunks to the AI model on invocation, a prompt is required to tell to
 the AI model whether and how to do citations. This setting allows overriding the default
 messaging that is built into the 
TextSearchProvider.
 ContextFormatter
 This optional callback can be used to completely customize the text that is produced by the
 TextSearchProvider. By default the 
TextSearchProvider will produce text that includes
 1. A prompt telling the AI model what the text chunks are for.
 2. The list of text chunks with source links and names.
 3. A prompt instructing the AI model about including citations.
 You can write your own output by implementing and providing this callback.
 Note: If this delegate is provided, the 
ContextPrompt and 
IncludeCitationsPrompt settings will
 not be used.
 Combining RAG with Other Providers
 The 
TextSearchProvider can be combined with other providers, such as 
mem0 or
 WhiteboardProvider, to create agents with both memory and retrieval capabilities.
 C#
 // Add both mem0 and TextSearchProvider to the agent thread.
 agentThread.AIContextProviders.Add(mem0Provider);
 agentThread.AIContextProviders.Add(textSearchProvider);
 // Use the agent with combined capabilities.
 ChatMessageContent response = await agent.InvokeAsync("What was Contoso's income 
for 2023?", agentThread).FirstAsync();
 Console.WriteLine(response.Content);
 By combining these features, agents can deliver a more personalized and context-aware
 experience.
Next steps
 Explore the Agent with RAG sample
Exploring the Semantic Kernel
 AzureAIAgent
 Article • 05/28/2025
） Important
 This feature is in the experimental stage. Features at this stage are under development
 and subject to change before advancing to the preview or release candidate stage.
  Tip
 Detailed API documentation related to this discussion is available at:
 AzureAIAgent
 What is an 
An 
AzureAIAgent?
 AzureAIAgent is a specialized agent within the Semantic Kernel framework, designed to
 provide advanced conversational capabilities with seamless tool integration. It automates tool
 calling, eliminating the need for manual parsing and invocation. The agent also securely
 manages conversation history using threads, reducing the overhead of maintaining state.
 Additionally, the 
AzureAIAgent supports a variety of built-in tools, including file retrieval, code
 execution, and data interaction via Bing, Azure AI Search, Azure Functions, and OpenAPI.
 To use an 
AzureAIAgent, an Azure AI Foundry Project must be utilized. The following articles
 provide an overview of the Azure AI Foundry, how to create and configure a project, and the
 agent service:
 What is Azure AI Foundry?
 The Azure AI Foundry SDK
 What is Azure AI Agent Service
 Quickstart: Create a new agent
 Preparing Your Development Environment
 To proceed with developing an 
AzureAIAgent, configure your development environment with
 the appropriate packages.
Add the Microsoft.SemanticKernel.Agents.AzureAI package to your project:
 pwsh
 You may also want to include the Azure.Identity package:
 pwsh
 Accessing an AzureAIAgent first requires the creation of a client that is configured for a specific
 Foundry Project, most commonly by providing your project endpoint (The Azure AI Foundry
 SDK: Getting Started with Projects).
 C#
 To create an AzureAIAgent, you start by configuring and initializing the Foundry project
 through the Azure Agent service and then integrate it with Semantic Kernel:
 C#
 dotnet add package Microsoft.SemanticKernel.Agents.AzureAI --prerelease
 dotnet add package Azure.Identity
 Configuring the AI Project Client
 PersistentAgentsClient client = AzureAIAgent.CreateAgentsClient("<your endpoint>", 
new AzureCliCredential());
 Creating an AzureAIAgent
 PersistentAgentsClient client = AzureAIAgent.CreateAgentsClient("<your endpoint>", 
new AzureCliCredential());
 // 1. Define an agent on the Azure AI agent service
 PersistentAgent definition = await agentsClient.Administration.CreateAgentAsync(
    "<name of the the model used by the agent>",
    name: "<agent name>",
    description: "<agent description>",
    instructions: "<agent instructions>");
 // 2. Create a Semantic Kernel agent based on the agent definition
 AzureAIAgent agent = new(definition, agentsClient);
Interaction with the AzureAIAgent is straightforward. The agent maintains the conversation
 history automatically using a thread.
 The specifics of the Azure AI Agent thread is abstracted away via the
 Microsoft.SemanticKernel.Agents.AzureAI.AzureAIAgentThread class, which is an
 implementation of Microsoft.SemanticKernel.Agents.AgentThread.
 The AzureAIAgent currently only supports threads of type AzureAIAgentThread.
 C#
 An agent may also produce a streamed response:
 C#
 Interacting with an AzureAIAgent
） Important
 Note that the Azure AI Agents SDK has the PersistentAgentThread class. It should not be
 confused with Microsoft.SemanticKernel.Agents.AgentThread, which is the common
 Semantic Kernel Agents abstraction for all thread types.
 AzureAIAgentThread agentThread = new(agent.Client);
 try
 {
    ChatMessageContent message = new(AuthorRole.User, "<your user input>");
    await foreach (ChatMessageContent response in agent.InvokeAsync(message, 
agentThread))
    {
        Console.WriteLine(response.Content);
    }
 }
 finally
 {
    await agentThread.DeleteAsync();
    await agent.Client.DeleteAgentAsync(agent.Id);
 }
 ChatMessageContent message = new(AuthorRole.User, "<your user input>");
 await foreach (StreamingChatMessageContent response in 
agent.InvokeStreamingAsync(message, agentThread))
 {
    Console.Write(response.Content);
 }
Semantic Kernel supports extending an AzureAIAgent with custom plugins for enhanced
 functionality:
 C#
 An AzureAIAgent can leverage advanced tools such as:
 Code Interpreter
 File Search
 OpenAPI integration
 Azure AI Search integration
 Bing Grounding
 Code Interpreter allows the agents to write and run Python code in a sandboxed execution
 environment (Azure AI Agent Service Code Interpreter).
 C#
 Using Plugins with an AzureAIAgent
 KernelPlugin plugin = KernelPluginFactory.CreateFromType<YourPlugin>();
 PersistentAgentsClient client = AzureAIAgent.CreateAgentsClient("<your endpoint>", 
new AzureCliCredential());
 PersistentAgent definition = await agentsClient.Administration.CreateAgentAsync(
    "<name of the the model used by the agent>",
    name: "<agent name>",
    description: "<agent description>",
    instructions: "<agent instructions>");
 AzureAIAgent agent = new(definition, agentsClient, plugins: [plugin]);
 Advanced Features
 Code Interpreter
 PersistentAgentsClient client = AzureAIAgent.CreateAgentsClient("<your endpoint>", 
new AzureCliCredential());
 PersistentAgent definition = await agentsClient.CreateAgentAsync(
    "<name of the the model used by the agent>",
    name: "<agent name>",
    description: "<agent description>",
    instructions: "<agent instructions>",
    tools: [new CodeInterpreterToolDefinition()],
    toolResources:
File search augments agents with knowledge from outside its model (Azure AI Agent Service
 File Search Tool).
 C#
 Connects your agent to an external API (How to use Azure AI Agent Service with OpenAPI
 Specified Tools).
 C#
        new()
        {
            CodeInterpreter = new()
            {
                FileIds = { ... },
            }
        }));
 AzureAIAgent agent = new(definition, agentsClient);
 File Search
 PersistentAgentsClient client = AzureAIAgent.CreateAgentsClient("<your endpoint>", 
new AzureCliCredential());
 PersistentAgent definition = await agentsClient.CreateAgentAsync(
    "<name of the the model used by the agent>",
    name: "<agent name>",
    description: "<agent description>",
    instructions: "<agent instructions>",
    tools: [new FileSearchToolDefinition()],
    toolResources:
        new()
        {
            FileSearch = new()
            {
                VectorStoreIds = { ... },
            }
        });
 AzureAIAgent agent = new(definition, agentsClient);
 OpenAPI Integration
 PersistentAgentsClient client = AzureAIAgent.CreateAgentsClient("<your endpoint>", 
new AzureCliCredential());
 string apiJsonSpecification = ...; // An Open API JSON specification
Use an existing Azure AI Search index with with your agent (Use an existing AI Search index).
 C#
 Example coming soon.
 PersistentAgent definition = await agentsClient.CreateAgentAsync(
    "<name of the the model used by the agent>",
    name: "<agent name>",
    description: "<agent description>",
    instructions: "<agent instructions>",
    tools: [
        new OpenApiToolDefinition(
            "<api name>", 
            "<api description>", 
            BinaryData.FromString(apiJsonSpecification), 
            new OpenApiAnonymousAuthDetails())
    ]
 );
 AzureAIAgent agent = new(definition, agentsClient);
 AzureAI Search Integration
 PersistentAgentsClient client = AzureAIAgent.CreateAgentsClient("<your endpoint>", 
new AzureCliCredential());
 PersistentAgent definition = await agentsClient.CreateAgentAsync(
    "<name of the the model used by the agent>",
    name: "<agent name>",
    description: "<agent description>",
    instructions: "<agent instructions>",
    tools: [new AzureAISearchToolDefinition()],
    toolResources: new()
    {
        AzureAISearch = new()
        {
            IndexList = { new AISearchIndexResource("<your connection id>", "<your 
index name>") }
        }
    });
 AzureAIAgent agent = new(definition, agentsClient);
 Bing Grounding
 Retrieving an Existing AzureAIAgent
An existing agent can be retrieved and reused by specifying its assistant ID:
 C#
 Agents and their associated threads can be deleted when no longer needed:
 C#
 If working with a vector store or files, they may be deleted as well:
 C#
 More information on the file search tool is described in the Azure AI Agent Service file
 search tool article.
 For practical examples of using an AzureAIAgent, see our code samples on GitHub:
 Getting Started with Azure AI Agents
 Advanced Azure AI Agent Code Samples
 The Semantic Kernel AzureAIAgent is designed to invoke an agent that fulfills user queries or
 questions. During invocation, the agent may execute tools to derive the final answer. To access
 intermediate messages produced during this process, callers can supply a callback function
 that handles instances of FunctionCallContent or FunctionResultContent.
 PersistentAgent definition = await agentsClient.Administration.GetAgentAsync("
 <your agent id>");
 AzureAIAgent agent = new(definition, agentsClient);
 Deleting an AzureAIAgent
 await agentThread.DeleteAsync();
 await agentsClient.Administration.DeleteAgentAsync(agent.Id);
 await agentsClient.VectorStores.DeleteVectorStoreAsync("<your store id>");
 await agentsClient.Files.DeleteFileAsync("<your file id>");
 How-To
 Handling Intermediate Messages with an
 AzureAIAgent
Callback documentation for the 
AzureAIAgent is coming soon.
 Declarative Spec
 The documentation on using declarative specs is coming soon.
 Exploring the Semantic Kernel
 ChatCompletionAgent
 Article • 05/28/2025
  Tip
 Detailed API documentation related to this discussion is available at:
 ChatCompletionAgent
 Microsoft.SemanticKernel.Agents
 IChatCompletionService
 Microsoft.SemanticKernel.ChatCompletion
 Chat Completion in Semantic Kernel
 Chat Completion is fundamentally a protocol for a chat-based interaction with an AI model
 where the chat-history is maintained and presented to the model with each request. Semantic
 Kernel AI services offer a unified framework for integrating the chat-completion capabilities of
 various AI models.
 A 
ChatCompletionAgent can leverage any of these AI services to generate responses, whether
 directed to a user or another agent.
 Preparing Your Development Environment
 To proceed with developing an 
ChatCompletionAgent, configure your development environment
 with the appropriate packages.
 Add the 
Microsoft.SemanticKernel.Agents.Core package to your project:
 pwsh
 dotnet add package Microsoft.SemanticKernel.Agents.Core --prerelease
 Creating a 
ChatCompletionAgent
 A 
ChatCompletionAgent is fundamentally based on an AI services. As such, creating a
 ChatCompletionAgent starts with creating a Kernel instance that contains one or more chat
completion services and then instantiating the agent with a reference to that Kernel instance.
 C#
 No different from using Semantic Kernel AI services directly, a ChatCompletionAgent supports
 the specification of a service-selector. A service-selector identifies which AI service to target
 when the Kernel contains more than one.
 C#
 // Initialize a Kernel with a chat-completion service
 IKernelBuilder builder = Kernel.CreateBuilder();
 builder.AddAzureOpenAIChatCompletion(/*<...configuration parameters>*/);
 Kernel kernel = builder.Build();
 // Create the agent
 ChatCompletionAgent agent =
    new()
    {
        Name = "SummarizationAgent",
        Instructions = "Summarize user input",
        Kernel = kernel
    };
 AI Service Selection
７ Note
 If multiple AI services are present and no service-selector is provided, the same default
 logic is applied for the agent that you'd find when using an AI services outside of the
 Agent Framework
 IKernelBuilder builder = Kernel.CreateBuilder();
 // Initialize multiple chat-completion services.
 builder.AddAzureOpenAIChatCompletion(/*<...service configuration>*/, serviceId: 
"service-1");
 builder.AddAzureOpenAIChatCompletion(/*<...service configuration>*/, serviceId: 
"service-2");
 Kernel kernel = builder.Build();
 ChatCompletionAgent agent =
    new()
    {
Conversing with your ChatCompletionAgent is based on a ChatHistory instance, no different
 from interacting with a Chat Completion AI service.
 You can simply invoke the agent with your user message.
 C#
 You can also use an AgentThread to have a conversation with your agent. Here we are using a
 ChatHistoryAgentThread.
 The ChatHistoryAgentThread can also take an optional ChatHistory object as input, via its
 constructor, if resuming a previous conversation. (not shown)
 C#
        Name = "<agent name>",
        Instructions = "<agent instructions>",
        Kernel = kernel,
        Arguments = // Specify the service-identifier via the KernelArguments
          new KernelArguments(
            new OpenAIPromptExecutionSettings() 
            { 
              ServiceId = "service-2" // The target service-identifier.
            })
    };
 Conversing with ChatCompletionAgent
 // Define agent
 ChatCompletionAgent agent = ...;
 // Generate the agent response(s)
 await foreach (ChatMessageContent response in agent.InvokeAsync(new 
ChatMessageContent(AuthorRole.User, "<user input>")))
 {
  // Process agent response(s)...
 }
 // Define agent
 ChatCompletionAgent agent = ...;
 AgentThread thread = new ChatHistoryAgentThread();
 // Generate the agent response(s)
 await foreach (ChatMessageContent response in agent.InvokeAsync(new 
ChatMessageContent(AuthorRole.User, "<user input>"), thread))
 {
// Process agent response(s)...
 }
 Handling Intermediate Messages with a
 ChatCompletionAgent
 The Semantic Kernel 
ChatCompletionAgent is designed to invoke an agent that fulfills user
 queries or questions. During invocation, the agent may execute tools to derive the final answer.
 To access intermediate messages produced during this process, callers can supply a callback
 function that handles instances of 
FunctionCallContent or 
Callback documentation for the 
FunctionResultContent.
 ChatCompletionAgent is coming soon.
 Declarative Spec
 The documentation on using declarative specs is coming soon.
 How-To
 For an end-to-end example for a 
ChatCompletionAgent, see:
 How-To: ChatCompletionAgent
 Next Steps
 Explore the Copilot Studio Agent
Exploring the Semantic Kernel
 CopilotStudioAgent
 Article • 05/23/2025
） Important
 This feature is in the experimental stage. Features at this stage are under development
 and subject to change before advancing to the preview or release candidate stage.
 Detailed API documentation related to this discussion is available at:
 The CopilotStudioAgent for .NET is coming soon.
 What is a 
CopilotStudioAgent?
 A 
CopilotStudioAgent is an integration point within the Semantic Kernel framework that
 enables seamless interaction with Microsoft Copilot Studio agents using programmatic APIs.
 This agent allows you to:
 Automate conversations and invoke existing Copilot Studio agents from Python code.
 Maintain rich conversational history using threads, preserving context across messages.
 Leverage advanced knowledge retrieval, web search, and data integration capabilities
 made available within Microsoft Copilot Studio.
７ Note
 Knowledge sources/tools must be configured within Microsoft Copilot Studio before they
 can be accessed via the agent.
 Preparing Your Development Environment
 To develop with the 
CopilotStudioAgent, you must have your environment and authentication
 set up correctly.
 The CopilotStudioAgent for .NET is coming soon.
Creating and Configuring a 
CopilotStudioAgent
 Client
 You may rely on environment variables for most configuration, but can explicitly create and
 customize the agent client as needed.
 The CopilotStudioAgent for .NET is coming soon.
 Interacting with a 
CopilotStudioAgent
 The core workflow is similar to other Semantic Kernel agents: provide user input(s), receive
 responses, maintain context via threads.
 The CopilotStudioAgent for .NET is coming soon.
 Using Plugins with a 
CopilotStudioAgent
 Semantic Kernel allows composition of agents and plugins. Although the primary extensibility
 for Copilot Studio comes via the Studio itself, you can compose plugins as with other agents.
 The CopilotStudioAgent for .NET is coming soon.
 Advanced Features
 A 
CopilotStudioAgent can leverage advanced Copilot Studio-enhanced abilities, depending on
 how the target agent is configured in the Studio environment:
 Knowledge Retrieval — responds based on pre-configured knowledge sources in the
 Studio.
 Web Search — if web search is enabled in your Studio agent, queries will use Bing Search.
 Custom Auth or APIs — via Power Platform and Studio plug-ins; direct OpenAPI binding
 is not currently first-class in SK integration.
 The CopilotStudioAgent for .NET is coming soon.
 How-To
 For practical examples of using a 
CopilotStudioAgent, see our code samples on GitHub:
 The CopilotStudioAgent for .NET is coming soon.
Notes:
 For more information or troubleshooting, see Microsoft Copilot Studio documentation.
 Only features and tools separately enabled and published in your Studio agent will be
 available via the Semantic Kernel interface.
 Streaming, plugin deployment, and programmatic tool addition are planned for future
 releases.
 Next Steps
 Explore the OpenAI Assistant Agent
Exploring the Semantic Kernel
 OpenAIAssistantAgent
 Article • 05/28/2025
） Important
 Single-agent features, such as 
OpenAIAssistantAgent, are in the release candidate stage.
 These features are nearly complete and generally stable, though they may undergo minor
 refinements or optimizations before reaching full general availability.
  Tip
 Detailed API documentation related to this discussion is available at:
 OpenAIAssistantAgent
 What is an Assistant?
 The OpenAI Assistants API is a specialized interface designed for more advanced and
 interactive AI capabilities, enabling developers to create personalized and multi-step task
oriented agents. Unlike the Chat Completion API, which focuses on simple conversational
 exchanges, the Assistant API allows for dynamic, goal-driven interactions with additional
 features like code-interpreter and file-search.
 OpenAI Assistant Guide
 OpenAI Assistant API
 Assistant API in Azure
 Preparing Your Development Environment
 To proceed with developing an 
OpenAIAssistantAgent, configure your development
 environment with the appropriate packages.
 Add the 
Microsoft.SemanticKernel.Agents.OpenAI package to your project:
 pwsh
 dotnet add package Microsoft.SemanticKernel.Agents.OpenAI --prerelease
You may also want to include the Azure.Identity package:
 pwsh
 Creating an OpenAIAssistant requires first creating a client to be able to talk a remote service.
 C#
 Once created, the identifier of the assistant may be access via its identifier. This identifier may
 be used to create an OpenAIAssistantAgent from an existing assistant definition.
 For .NET, the agent identifier is exposed as a string via the property defined by any agent.
 C#
 As with all aspects of the Assistant API, conversations are stored remotely. Each conversation is
 referred to as a thread and identified by a unique string identifier. Interactions with your
 OpenAIAssistantAgent are tied to this specific thread identifier. The specifics of the Assistant
 API thread is abstracted away via the OpenAIAssistantAgentThread class, which is an
 implementation of AgentThread.
 dotnet add package Azure.Identity
 Creating an OpenAIAssistantAgent
 AssistantClient client = 
OpenAIAssistantAgent.CreateAzureOpenAIClient(...).GetAssistantClient();
 Assistant assistant =
    await client.CreateAssistantAsync(
        "<model name>",
        "<agent name>",
        instructions: "<agent instructions>");
 OpenAIAssistantAgent agent = new(assistant, client);
 Retrieving an OpenAIAssistantAgent
 AssistantClient client = 
OpenAIAssistantAgent.CreateAzureOpenAIClient(...).GetAssistantClient();
 Assistant assistant = await client.GetAssistantAsync("<assistant id>");
 OpenAIAssistantAgent agent = new(assistant, client);
 Using an OpenAIAssistantAgent
The OpenAIAssistantAgent currently only supports threads of type OpenAIAssistantAgentThread.
 You can invoke the OpenAIAssistantAgent without specifying an AgentThread, to start a new
 thread and a new AgentThread will be returned as part of the response.
 C#
 You can also invoke the OpenAIAssistantAgent with an AgentThread that you created.
 C#
 You can also create an OpenAIAssistantAgentThread that resumes an earlier conversation by id.
 C#
 // Define agent
 OpenAIAssistantAgent agent = ...;
 AgentThread? agentThread = null;
 // Generate the agent response(s)
 await foreach (AgentResponseItem<ChatMessageContent> response in 
agent.InvokeAsync(new ChatMessageContent(AuthorRole.User, "<user input>")))
 {
  // Process agent response(s)...
  agentThread = response.Thread;
 }
 // Delete the thread if no longer needed
 if (agentThread is not null)
 {
    await agentThread.DeleteAsync();
 }
 // Define agent
 OpenAIAssistantAgent agent = ...;
 // Create a thread with some custom metadata.
 AgentThread agentThread = new OpenAIAssistantAgentThread(client, metadata: 
myMetadata);
 // Generate the agent response(s)
 await foreach (ChatMessageContent response in agent.InvokeAsync(new 
ChatMessageContent(AuthorRole.User, "<user input>"), agentThread))
 {
  // Process agent response(s)...
 }
 // Delete the thread when it is no longer needed
 await agentThread.DeleteAsync();
// Create a thread with an existing thread id.
 AgentThread agentThread = new OpenAIAssistantAgentThread(client, "existing-thread
id");
 Deleting an 
OpenAIAssistantAgent
 Since the assistant's definition is stored remotely, it will persist if not deleted.
 Deleting an assistant definition may be performed directly with the 
AssistantClient.
 Note: Attempting to use an agent instance after being deleted will result in a service
 exception.
 For .NET, the agent identifier is exposed as a 
string via the Agent.Id property defined by any
 agent.
 C#
 AssistantClient client = 
OpenAIAssistantAgent.CreateAzureOpenAIClient(...).GetAssistantClient();
 await client.DeleteAssistantAsync("<assistant id>");
 Handling Intermediate Messages with an
 OpenAIAssistantAgent
 The Semantic Kernel 
OpenAIAssistantAgent is designed to invoke an agent that fulfills user
 queries or questions. During invocation, the agent may execute tools to derive the final answer.
 To access intermediate messages produced during this process, callers can supply a callback
 function that handles instances of 
FunctionCallContent or 
Callback documentation for the 
FunctionResultContent.
 OpenAIAssistantAgent is coming soon.
 Declarative Spec
 The documentation on using declarative specs is coming soon.
 How-To
 For an end-to-end example for a 
OpenAIAssistantAgent, see:
How-To: OpenAIAssistantAgent Code Interpreter
 How-To: OpenAIAssistantAgent File Search
 Next Steps
 Explore the OpenAI Responses Agent
Exploring the Semantic Kernel
 OpenAIResponsesAgent
 Article • 05/28/2025
） Important
 This feature is in the experimental stage. Features at this stage are under development
 and subject to change before advancing to the preview or release candidate stage.
 The 
OpenAIResponsesAgent is coming soon.
 What is a Responses Agent?
 The OpenAI Responses API is OpenAI's most advanced interface for generating model
 responses. It supports text and image inputs, and text outputs. You are able to create stateful
 interactions with the model, using the output of previous responses as input. It is also possible
 to extend the model's capabilities with built-in tools for file search, web search, computer use,
 and more.
 OpenAI Responses API
 Responses API in Azure
 Preparing Your Development Environment
 To proceed with developing an 
OpenAIResponsesAgent, configure your development
 environment with the appropriate packages.
 The 
OpenAIResponsesAgent is coming soon.
 Creating an 
OpenAIResponsesAgent
 Creating an 
OpenAIResponsesAgent requires first creating a client to be able to talk a remote
 service.
 The 
OpenAIResponsesAgent is coming soon.
 Using an 
OpenAIResponsesAgent
The 
OpenAIResponsesAgent is coming soon.
 Handling Intermediate Messages with an
 OpenAIResponsesAgent
 The Semantic Kernel 
OpenAIResponsesAgent is designed to invoke an agent that fulfills user
 queries or questions. During invocation, the agent may execute tools to derive the final answer.
 To access intermediate messages produced during this process, callers can supply a callback
 function that handles instances of 
FunctionCallContent or 
FunctionResultContent.
 The 
OpenAIResponsesAgent is coming soon.
 Declarative Spec
 The documentation on using declarative specs is coming soon.
 Next Steps
 Explore Agent Orchestration
Semantic Kernel Agent Orchestration
 Article • 05/23/2025
） Important
 Agent Orchestration features in the Agent Framework are in the experimental stage. They
 are under active development and may change significantly before advancing to the
 preview or release candidate stage.
 Semantic Kernel’s Agent Orchestration framework enables developers to build, manage, and
 scale complex agent workflows with ease.
 Why Multi-agent Orchestration?
 Traditional single-agent systems are limited in their ability to handle complex, multi-faceted
 tasks. By orchestrating multiple agents, each with specialized skills or roles, we can create
 systems that are more robust, adaptive, and capable of solving real-world problems
 collaboratively. Multi-agent orchestration in Semantic Kernel provides a flexible foundation for
 building such systems, supporting a variety of coordination patterns.
 Orchestration Patterns
 Semantic Kernel supports several orchestration patterns, each designed for different
 collaboration scenarios. These patterns are available as part of the framework and can be easily
 extended or customized.
 Supported Orchestration Patterns
 Pattern
 Description
 Concurrent Broadcasts a task to all agents, collects results
 independently.
 Sequential
 Handoff
 Passes the result from one agent to the next in
 a defined order.
 Typical Use Case
ﾉ Expand table
 Parallel analysis, independent subtasks,
 ensemble decision making.
 Step-by-step workflows, pipelines, multi
stage processing.
 Dynamically passes control between agents
 based on context or rules.
 Dynamic workflows, escalation, fallback,
 or expert handoff scenarios.
Pattern Description Typical Use Case
 Group
 Chat
 All agents participate in a group conversation,
 coordinated by a group manager.
 Brainstorming, collaborative problem
 solving, consensus building.
 Magentic Group chat-like orchestration inspired by
 MagenticOne .
 Complex, generalist multi-agent
 collaboration.
 All orchestration patterns share a unified interface for construction and invocation. No matter
 which orchestration you choose, you:
 Define your agents and their capabilities, see Semantic Kernel Agents.
 Create an orchestration by passing the agents (and, if needed, a manager).
 Optionally provide callbacks or transforms for custom input/output handling.
 Start a runtime and invoke the orchestration with a task.
 Await the result in a consistent, asynchronous manner.
 This unified approach means you can easily switch between orchestration patterns, without
 learning new APIs or rewriting your agent logic. The framework abstracts away the complexity
 of agent communication, coordination, and result aggregation, letting you focus on your
 application’s goals.
 Simplicity and Developer-friendly
 // Choose an orchestration pattern with your agents
 SequentialOrchestration orchestration = new(agentA, agentB)
 {
    LoggerFactory = this.LoggerFactory
 };  // or ConcurrentOrchestration, GroupChatOrchestration, HandoffOrchestration, 
MagenticOrchestration, ...
 // Start the runtime
 InProcessRuntime runtime = new();
 await runtime.StartAsync();
 // Invoke the orchestration and get the result
 OrchestrationResult<string> result = await orchestration.InvokeAsync(task, 
runtime);
 string text = await result.GetValueAsync();
 await runtime.RunUntilIdleAsync();
 Next steps
Concurrent Orchestration
Concurrent Orchestration
 Article • 05/28/2025
） Important
 Agent Orchestration features in the Agent Framework are in the experimental stage. They
 are under active development and may change significantly before advancing to the
 preview or release candidate stage.
 The concurrent pattern enables multiple agents to work on the same task in parallel. Each
 agent processes the input independently, and their results are collected and aggregated. This
 approach is well-suited for scenarios where diverse perspectives or solutions are valuable, such
 as brainstorming, ensemble reasoning, or voting systems.
 Common Use Cases
 Multiple agents generate different solutions to a problem, and their responses are collected for
 further analysis or selection:
 What You'll Learn
 How to define multiple agents with different expertise
 How to orchestrate these agents to work concurrently on a single task
 How to collect and process the results
 Define Your Agents
Agents are specialized entities that can process tasks. Here, we define two agents: a physics
 expert and a chemistry expert.
 C#
 The ConcurrentOrchestration class allows you to run multiple agents in parallel. You pass the
 list of agents as members.
 C#
 A runtime is required to manage the execution of agents. Here, we use InProcessRuntime and
 start it before invoking the orchestration.
  Tip
 The ChatCompletionAgent is used here, but you can use any agent type.
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.Agents;
 using Microsoft.SemanticKernel.Agents.Orchestration;
 using Microsoft.SemanticKernel.Agents.Orchestration.Concurrent;
 using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
 // Create a kernel with an AI service
 Kernel kernel = ...;
 ChatCompletionAgent physicist = new ChatCompletionAgent{
    Name = "PhysicsExpert",
    Instructions = "You are an expert in physics. You answer questions from a 
physics perspective."
    Kernel = kernel,
 };
 ChatCompletionAgent chemist = new ChatCompletionAgent{
    Name = "ChemistryExpert",
    Instructions = "You are an expert in chemistry. You answer questions from a 
chemistry perspective."
    Kernel = kernel,
 };
 Set Up the Concurrent Orchestration
 ConcurrentOrchestration orchestration = new (physicist, chemist);
 Start the Runtime
C#
 You can now invoke the orchestration with a specific task. The orchestration will run all agents
 concurrently on the given task.
 C#
 The results from all agents can be collected asynchronously. Note that the order of results is
 not guaranteed.
 C#
 After processing is complete, stop the runtime to clean up resources.
 C#
 plaintext
 InProcessRuntime runtime = new InProcessRuntime();
 await runtime.StartAsync();
 Invoke the Orchestration
 var result = await orchestration.InvokeAsync("What is temperature?", runtime);
 Collect Results
 string[] output = await result.GetValueAsync(TimeSpan.FromSeconds(20));
 Console.WriteLine($"# RESULT:\n{string.Join("\n\n", output.Select(text => $"
 {text}"))}");
 Optional: Stop the Runtime
 await runtime.RunUntilIdleAsync();
 Sample Output
 # RESULT:
 Temperature is a fundamental physical quantity that measures the average kinetic 
energy ...
Temperature is a measure of the average kinetic energy of the particles ...
  Tip
 The full sample code is available 
here
 Next steps
 Sequential Orchestration
Sequential Orchestration
 Article • 05/23/2025
） Important
 Agent Orchestration features in the Agent Framework are in the experimental stage. They
 are under active development and may change significantly before advancing to the
 preview or release candidate stage.
 In the sequential pattern, agents are organized in a pipeline. Each agent processes the task in
 turn, passing its output to the next agent in the sequence. This is ideal for workflows where
 each step builds upon the previous one, such as document review, data processing pipelines,
 or multi-stage reasoning.
 Common Use Cases
 A document passes through a summarization agent, then a translation agent, and finally a
 quality assurance agent, each building on the previous output:
What You'll Learn
 How to define a sequence of agents, each with a specialized role
 How to orchestrate these agents so that each processes the output of the previous one
 How to observe intermediate outputs and collect the final result
 Define Your Agents
 Agents are specialized entities that process tasks in sequence. Here, we define three agents: an
 analyst, a copywriter, and an editor.
  Tip
 The 
ChatCompletionAgent is used here, but you can use any 
agent type.
C#
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.Agents;
 using Microsoft.SemanticKernel.Agents.Orchestration;
 using Microsoft.SemanticKernel.Agents.Orchestration.Sequential;
 using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
 // Create a kernel with an AI service
 Kernel kernel = ...;
 ChatCompletionAgent analystAgent = new ChatCompletionAgent {
 Name = "Analyst",
 Instructions = "You are a marketing analyst. Given a product description, 
identify:\n- Key features\n- Target audience\n- Unique selling points",
 Kernel = kernel,
 };
 ChatCompletionAgent writerAgent = new ChatCompletionAgent {
 Name = "Copywriter",
 Instructions = "You are a marketing copywriter. Given a block of text 
describing features, audience, and USPs, compose a compelling marketing copy (like 
a newsletter section) that highlights these points. Output should be short (around 
150 words), output just the copy as a single text block.",
 Kernel = kernel,
 };
 ChatCompletionAgent editorAgent = new ChatCompletionAgent {
 Name = "Editor",
 Instructions = "You are an editor. Given the draft copy, correct grammar, 
improve clarity, ensure consistent tone, give format and make it polished. Output 
the final improved copy as a single text block.",
 Kernel = kernel,
 };
 Optional: Observe Agent Responses
 You can create a callback to capture agent responses as the sequence progresses via the
 ResponseCallback property.
 C#
 ChatHistory history = [];
 ValueTask responseCallback(ChatMessageContent response)
 {
 history.Add(response);
 return ValueTask.CompletedTask;
 }
Create a SequentialOrchestration object, passing in the agents and the optional response
 callback.
 C#
 A runtime is required to manage the execution of agents. Here, we use InProcessRuntime and
 start it before invoking the orchestration.
 C#
 Invoke the orchestration with your initial task (e.g., a product description). The output will flow
 through each agent in sequence.
 C#
 Wait for the orchestration to complete and retrieve the final output.
 C#
 Set Up the Sequential Orchestration
 SequentialOrchestration orchestration = new(analystAgent, writerAgent, 
editorAgent)
 {
    ResponseCallback = responseCallback,
 };
 Start the Runtime
 InProcessRuntime runtime = new InProcessRuntime();
 await runtime.StartAsync();
 Invoke the Orchestration
 var result = await orchestration.InvokeAsync(
    "An eco-friendly stainless steel water bottle that keeps drinks cold for 24 
hours",
    runtime);
 Collect Results
 string output = await result.GetValueAsync(TimeSpan.FromSeconds(20));
 Console.WriteLine($"\n# RESULT: {text}");
 Console.WriteLine("\n\nORCHESTRATION HISTORY");
After processing is complete, stop the runtime to clean up resources.
 C#
 plaintext
 foreach (ChatMessageContent message in history)
 {
    this.WriteAgentChatMessage(message);
 }
 Optional: Stop the Runtime
 await runtime.RunUntilIdleAsync();
 Sample Output
 # RESULT: Introducing our Eco-Friendly Stainless Steel Water Bottles – the perfect 
companion for those who care about the planet while staying hydrated! Our bottles 
...
 ORCHESTRATION HISTORY
 # Assistant - Analyst: **Key Features:**- Made from eco-friendly stainless steel- Insulation technology that maintains cold temperatures for up to 24 hours- Reusable and sustainable design- Various sizes and colors available (assumed based on typical offerings)- Leak-proof cap- BPA-free ...
 # Assistant - copywriter: Introducing our Eco-Friendly Stainless ...
 # Assistant - editor: Introducing our Eco-Friendly Stainless Steel Water Bottles – 
the perfect companion for those who care about the planet while staying hydrated! 
Our bottles ...
  Tip
 The full sample code is available here
 Next steps
Group Chat Orchestration
Group Chat Orchestration
 Article • 05/23/2025
） Important
 Agent Orchestration features in the Agent Framework are in the experimental stage. They
 are under active development and may change significantly before advancing to the
 preview or release candidate stage.
 Group chat orchestration models a collaborative conversation among agents, optionally
 including a human participant. A group chat manager coordinates the flow, determining which
 agent should respond next and when to request human input. This pattern is powerful for
 simulating meetings, debates, or collaborative problem-solving sessions.
 Common Use Cases
 Agents representing different departments discuss a business proposal, with a manager agent
 moderating the conversation and involving a human when needed:
 What You'll Learn
 How to define agents with different roles for a group chat
 How to use a group chat manager to control the flow of conversation
 How to involve a human participant in the conversation
 How to observe the conversation and collect the final result
Define Your Agents
 Each agent in the group chat has a specific role. In this example, we define a copywriter and a
 reviewer.
  Tip
 The 
ChatCompletionAgent is used here, but you can use any 
C#
 agent type.
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.Agents;
 using Microsoft.SemanticKernel.Agents.Orchestration;
 using Microsoft.SemanticKernel.Agents.Orchestration.GroupChat;
 using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
 // Create a kernel with an AI service
 Kernel kernel = ...;
 ChatCompletionAgent writer = new ChatCompletionAgent {
 Name = "CopyWriter",
 Description = "A copy writer",
 Instructions = "You are a copywriter with ten years of experience and are 
known for brevity and a dry humor. The goal is to refine and decide on the single 
best copy as an expert in the field. Only provide a single proposal per response. 
You're laser focused on the goal at hand. Don't waste time with chit chat. 
Consider suggestions when refining an idea.",
 Kernel = kernel,
 };
 ChatCompletionAgent editor = new ChatCompletionAgent {
 Name = "Reviewer",
 Description = "An editor.",
 Instructions = "You are an art director who has opinions about copywriting 
born of a love for David Ogilvy. The goal is to determine if the given copy is 
acceptable to print. If so, state that it is approved. If not, provide insight on 
how to refine suggested copy without example.",
 Kernel = kernel,
 };
 Optional: Observe Agent Responses
 You can create a callback to capture agent responses as the sequence progresses via the
 ResponseCallback property.
 C#
Create a GroupChatOrchestration object, passing in the agents, a group chat manager (here, a
 RoundRobinGroupChatManager), and the response callback. The manager controls the flow—here,
 it alternates turns in a round-robin fashion for a set number of rounds.
 C#
 A runtime is required to manage the execution of agents. Here, we use InProcessRuntime and
 start it before invoking the orchestration.
 C#
 Invoke the orchestration with your initial task (e.g., "Create a slogan for a new electric SUV...").
 The agents will take turns responding, refining the result.
 C#
 ChatHistory history = [];
 ValueTask responseCallback(ChatMessageContent response)
 {
    history.Add(response);
    return ValueTask.CompletedTask;
 }
 Set Up the Group Chat Orchestration
 GroupChatOrchestration orchestration = new GroupChatOrchestration(
    new RoundRobinGroupChatManager { MaximumInvocationCount = 5 },
    writer,
    editor)
 {
    ResponseCallback = responseCallback,
 };
 Start the Runtime
 InProcessRuntime runtime = new InProcessRuntime();
 await runtime.StartAsync();
 Invoke the Orchestration
 var result = await orchestration.InvokeAsync(
    "Create a slogan for a new electric SUV that is affordable and fun to drive.",
Wait for the orchestration to complete and retrieve the final output.
 C#
 After processing is complete, stop the runtime to clean up resources.
 C#
 plaintext
    runtime);
 Collect Results
 string output = await result.GetValueAsync(TimeSpan.FromSeconds(60));
 Console.WriteLine($"\n# RESULT: {text}");
 Console.WriteLine("\n\nORCHESTRATION HISTORY");
 foreach (ChatMessageContent message in history)
 {
    this.WriteAgentChatMessage(message);
 }
 Optional: Stop the Runtime
 await runtime.RunUntilIdleAsync();
 Sample Output
 # RESULT: “Affordable Adventure: Drive Electric, Drive Fun.”
 ORCHESTRATION HISTORY
 # Assistant - CopyWriter: “Charge Ahead: Affordable Thrills, Zero Emissions.”
 # Assistant - Reviewer: The slogan is catchy but it could be refined to better ...
 # Assistant - CopyWriter: “Electrify Your Drive: Fun Meets Affordability.”
 # Assistant - Reviewer: The slogan captures the essence of electric driving and 
...
 # Assistant - CopyWriter: “Affordable Adventure: Drive Electric, Drive Fun.”
You can customize the group chat flow by implementing your own GroupChatManager. This
 allows you to control how results are filtered, how the next agent is selected, and when to
 request user input or terminate the chat.
 For example, you can create a custom manager by inheriting from GroupChatManager and
 overriding its abstract methods:
 C#
  Tip
 The full sample code is available here
 Customize the Group Chat Manager
 using Microsoft.SemanticKernel.Agents.Orchestration.GroupChat;
 using Microsoft.SemanticKernel.ChatCompletion;
 using System.Threading;
 using System.Threading.Tasks;
 public class CustomGroupChatManager : GroupChatManager
 {
    public override ValueTask<GroupChatManagerResult<string>> 
FilterResults(ChatHistory history, CancellationToken cancellationToken = default)
    {
        // Custom logic to filter or summarize chat results
        return ValueTask.FromResult(new GroupChatManagerResult<string>("Summary") 
{ Reason = "Custom summary logic." });
    }
    public override ValueTask<GroupChatManagerResult<string>> 
SelectNextAgent(ChatHistory history, GroupChatTeam team, CancellationToken 
cancellationToken = default)
    {
        // Randomly select an agent from the team
        var random = new Random();
        int index = random.Next(team.Members.Count);
        string nextAgent = team.Members[index].Id;
        return ValueTask.FromResult(new GroupChatManagerResult<string>(nextAgent) 
{ Reason = "Custom selection logic." });
    }
    public override ValueTask<GroupChatManagerResult<bool>> 
ShouldRequestUserInput(ChatHistory history, CancellationToken cancellationToken = 
default)
    {
        // Custom logic to decide if user input is needed
        return ValueTask.FromResult(new GroupChatManagerResult<bool>(false) { 
Reason = "No user input required." });
You can then use your custom manager in the orchestration:
 C#
 When orchestrating a group chat, the group chat manager's methods are called in a specific
 order for each round of the conversation:
 1. ShouldRequestUserInput: Checks if user (human) input is required before the next agent
 speaks. If true, the orchestration pauses for user input. The user input is then added to
 the chat history of the manager and sent to all agents.
 2. ShouldTerminate: Determines if the group chat should end (for example, if a maximum
 number of rounds is reached or a custom condition is met). If true, the orchestration
 proceeds to result filtering.
 3. FilterResults: Called only if the chat is terminating, to summarize or process the final
 results of the conversation.
    }
    public override ValueTask<GroupChatManagerResult<bool>> 
ShouldTerminate(ChatHistory history, CancellationToken cancellationToken = 
default)
    {
        // Optionally call the base implementation to check for default 
termination logic
        var baseResult = base.ShouldTerminate(history, cancellationToken).Result;
        if (baseResult.Value)
        {
            // If the base logic says to terminate, respect it
            return ValueTask.FromResult(baseResult);
        }
        // Custom logic to determine if the chat should terminate
        bool shouldEnd = history.Count > 10; // Example: end after 10 messages
        return ValueTask.FromResult(new GroupChatManagerResult<bool>(shouldEnd) { 
Reason = "Custom termination logic." });
    }
 }
 GroupChatOrchestration orchestration = new (new CustomGroupChatManager { 
MaximumInvocationCount = 5 }, ...);
  Tip
 A complete example of a custom group chat manager is available here
 Order of Group Chat Manager Function Calls
4. SelectNextAgent: If the chat is not terminating, selects the next agent to respond in the
 conversation.
 This order ensures that user input and termination conditions are checked before advancing
 the conversation, and that results are filtered only at the end. You can customize the logic for
 each step by overriding the corresponding methods in your custom group chat manager.
 Handoff Orchestration
 Article • 05/23/2025
） Important
 Agent Orchestration features in the Agent Framework are in the experimental stage. They
 are under active development and may change significantly before advancing to the
 preview or release candidate stage.
 Handoff orchestration allows agents to transfer control to one another based on the context or
 user request. Each agent can “handoff” the conversation to another agent with the appropriate
 expertise, ensuring that the right agent handles each part of the task. This is particularly useful
 in customer support, expert systems, or any scenario requiring dynamic delegation.
 Common Use Cases
 A customer support agent handles a general inquiry, then hands off to a technical expert agent
 for troubleshooting, or to a billing agent if needed:
How to define agents and their handoff relationships
 How to set up a handoff orchestration for dynamic agent routing
 How to involve a human in the conversation loop
 Each agent is responsible for a specific area. In this example, we define a triage agent, a refund
 agent, an order status agent, and an order return agent. Some agents use plugins to handle
 specific tasks.
 C#
 What You'll Learn
 Define Specialized Agents
  Tip
 The ChatCompletionAgent is used here, but you can use any agent type.
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.Agents;
 using Microsoft.SemanticKernel.Agents.Orchestration;
 using Microsoft.SemanticKernel.Agents.Orchestration.Handoff;
 using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
 using Microsoft.SemanticKernel.ChatCompletion;
 // Plugin implementations
 public sealed class OrderStatusPlugin {
    [KernelFunction]
    public string CheckOrderStatus(string orderId) => $"Order {orderId} is shipped 
and will arrive in 2-3 days.";
 }
 public sealed class OrderReturnPlugin {
    [KernelFunction]
    public string ProcessReturn(string orderId, string reason) => $"Return for 
order {orderId} has been processed successfully.";
 }
 public sealed class OrderRefundPlugin {
    [KernelFunction]
    public string ProcessReturn(string orderId, string reason) => $"Refund for 
order {orderId} has been processed successfully.";
 }
 // Helper function to create a kernel with chat completion
 public static Kernel CreateKernelWithChatCompletion(...)
 {
    ...
 }
Use OrchestrationHandoffs to specify which agent can hand off to which, and under what
 circumstances.
 C#
 ChatCompletionAgent triageAgent = new ChatCompletionAgent {
    Name = "TriageAgent",
    Description = "Handle customer requests.",
    Instructions = "A customer support agent that triages issues.",
    Kernel = CreateKernelWithChatCompletion(...),
 };
 ChatCompletionAgent statusAgent = new ChatCompletionAgent {
    Name = "OrderStatusAgent",
    Description = "A customer support agent that checks order status.",
    Instructions = "Handle order status requests.",
    Kernel = CreateKernelWithChatCompletion(...),
 };
 statusAgent.Kernel.Plugins.Add(KernelPluginFactory.CreateFromObject(new 
OrderStatusPlugin()));
 ChatCompletionAgent returnAgent = new ChatCompletionAgent {
    Name = "OrderReturnAgent",
    Description = "A customer support agent that handles order returns.",
    Instructions = "Handle order return requests.",
    Kernel = CreateKernelWithChatCompletion(...),
 };
 returnAgent.Kernel.Plugins.Add(KernelPluginFactory.CreateFromObject(new 
OrderReturnPlugin()));
 ChatCompletionAgent refundAgent = new ChatCompletionAgent {
    Name = "OrderRefundAgent",
    Description = "A customer support agent that handles order refund.",
    Instructions = "Handle order refund requests.",
    Kernel = CreateKernelWithChatCompletion(...),
 };
 refundAgent.Kernel.Plugins.Add(KernelPluginFactory.CreateFromObject(new 
OrderRefundPlugin()));
 Set Up Handoff Relationships
 var handoffs = OrchestrationHandoffs
    .StartWith(triageAgent)
    .Add(triageAgent, statusAgent, returnAgent, refundAgent)
    .Add(statusAgent, triageAgent, "Transfer to this agent if the issue is not 
status related")
    .Add(returnAgent, triageAgent, "Transfer to this agent if the issue is not 
return related")
    .Add(refundAgent, triageAgent, "Transfer to this agent if the issue is not 
refund related");
You can create a callback to capture agent responses as the conversation progresses via the
 ResponseCallback property.
 C#
 A key feature of handoff orchestration is the ability for a human to participate in the
 conversation. This is achieved by providing an InteractiveCallback, which is called whenever
 an agent needs input from the user. In a real application, this would prompt the user for input;
 in a sample, you can use a queue of responses.
 C#
 Create a HandoffOrchestration object, passing in the agents, handoff relationships, and the
 callbacks.
 C#
 Observe Agent Responses
 ChatHistory history = [];
 ValueTask responseCallback(ChatMessageContent response)
 {
    history.Add(response);
    return ValueTask.CompletedTask;
 }
 Human in the Loop
 // Simulate user input with a queue
 Queue<string> responses = new();
 responses.Enqueue("I'd like to track the status of my order");
 responses.Enqueue("My order ID is 123");
 responses.Enqueue("I want to return another order of mine");
 responses.Enqueue("Order ID 321");
 responses.Enqueue("Broken item");
 responses.Enqueue("No, bye");
 ValueTask<ChatMessageContent> interactiveCallback()
 {
    string input = responses.Dequeue();
    Console.WriteLine($"\n# INPUT: {input}\n");
    return ValueTask.FromResult(new ChatMessageContent(AuthorRole.User, input));
 }
 Set Up the Handoff Orchestration
A runtime is required to manage the execution of agents. Here, we use InProcessRuntime and
 start it before invoking the orchestration.
 C#
 Invoke the orchestration with your initial task (e.g., "I am a customer that needs help with my
 orders"). The agents will route the conversation as needed, involving the human when required.
 C#
 Wait for the orchestration to complete and retrieve the final output.
 C#
 HandoffOrchestration orchestration = new HandoffOrchestration(
    handoffs,
    triageAgent,
    statusAgent,
    returnAgent,
    refundAgent)
 {
    InteractiveCallback = interactiveCallback,
    ResponseCallback = responseCallback,
 };
 Start the Runtime
 InProcessRuntime runtime = new InProcessRuntime();
 await runtime.StartAsync();
 Invoke the Orchestration
 string task = "I am a customer that needs help with my orders";
 var result = await orchestration.InvokeAsync(task, runtime);
 Collect Results
 string output = await result.GetValueAsync(TimeSpan.FromSeconds(300));
 Console.WriteLine($"\n# RESULT: {output}");
 Console.WriteLine("\n\nORCHESTRATION HISTORY");
 foreach (ChatMessageContent message in history)
 {
    // Print each message
    Console.WriteLine($"# {message.Role} - {message.AuthorName}: 
After processing is complete, stop the runtime to clean up resources.
 C#
 plaintext
 {message.Content}");
 }
 Optional: Stop the Runtime
 await runtime.RunUntilIdleAsync();
 Sample Output
 # RESULT: Handled order return for order ID 321 due to a broken item, and 
successfully processed the return.
 ORCHESTRATION HISTORY
 # Assistant - TriageAgent: Could you please specify what kind of help you need 
with your orders? Are you looking to check the order status, return an item, or 
request a refund?
 # Assistant - OrderStatusAgent: Could you please tell me your order ID?
 # Assistant - OrderStatusAgent: Your order with ID 123 has been shipped and will 
arrive in 2-3 days. Anything else I can assist you with?
 # Assistant - OrderReturnAgent: I can help you with that. Could you please provide 
the order ID and the reason you'd like to return it?
 # Assistant - OrderReturnAgent: Please provide the reason for returning the order 
with ID 321.
 # Assistant - OrderReturnAgent: The return for your order with ID 321 has been 
successfully processed due to the broken item. Anything else I can assist you 
with?
  Tip
 The full sample code is available here
 Next steps
Magentic Orchestration
Magentic Orchestration
 Article • 05/23/2025
） Important
 Agent Orchestration features in the Agent Framework are in the experimental stage. They
 are under active development and may change significantly before advancing to the
 preview or release candidate stage.
 Magentic orchestration is designed based on the Magentic-One system invented by
 AutoGen. It is a flexible, general-purpose multi-agent pattern designed for complex, open
ended tasks that require dynamic collaboration. In this pattern, a dedicated Magentic manager
 coordinates a team of specialized agents, selecting which agent should act next based on the
 evolving context, task progress, and agent capabilities.
 The Magentic manager maintains a shared context, tracks progress, and adapts the workflow in
 real time. This enables the system to break down complex problems, delegate subtasks, and
 iteratively refine solutions through agent collaboration. The orchestration is especially well
suited for scenarios where the solution path is not known in advance and may require multiple
 rounds of reasoning, research, and computation.
  Tip
 Read more about the Magentic-One 
here .
  Tip
 The name "Magentic" comes from "Magentic-One". "Magentic-One" is a multi-agent
 system that includes a set of agents, such as the 
WebSurfer and 
FileSurfer. The Semantic
 Kernel Magentic orchestration is inspired by the Magentic-One system where the
 Magentic manager coordinates a team of specialized agents to solve complex tasks.
 However, it is not a direct implementation of the Magentic-One system and does not
 feature the agents from the Magentic-One system.
 Common Use Cases
 A user requests a comprehensive report comparing the energy efficiency and CO₂ emissions of
 different machine learning models. The Magentic manager first assigns a research agent to
 gather relevant data, then delegates analysis and computation to a coder agent. The manager
coordinates multiple rounds of research and computation, aggregates the findings, and
 produces a detailed, structured report as the final output.
 What You'll Learn
 How to define and configure agents for Magentic orchestration
 How to set up a Magentic manager to coordinate agent collaboration
 How the orchestration process works, including planning, progress tracking, and final
 answer synthesis
 Define Your Agents
 Each agent in the Magentic pattern has a specialized role. In this example:
 ResearchAgent: Finds and summarizes information (e.g., via web search). Here the sample
 is using the 
ChatCompletionAgent with the 
gpt-4o-search-preview model for its web
 search capability.
 CoderAgent: Writes and executes code to analyze or process data. Here the sample is
 using the 
AzureAIAgent since it has advanced tools like the code interpreter.
  Tip
C#
 The Magentic manager coordinates the agents, plans the workflow, tracks progress, and
 synthesizes the final answer. The standard manager (StandardMagenticManager) uses a chat
 completion model that supports structured output.
 The ChatCompletionAgent and AzureAIAgent are used here, but you can use any agent
 type.
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.Agents;
 using Microsoft.SemanticKernel.Agents.AzureAI;
 using Microsoft.SemanticKernel.Agents.Magentic;
 using Microsoft.SemanticKernel.Agents.Orchestration;
 using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
 using Microsoft.SemanticKernel.ChatCompletion;
 using Microsoft.SemanticKernel.Connectors.OpenAI;
 using Azure.AI.Agents.Persistent;
 using Azure.Identity;
 // Helper function to create a kernel with chat completion
 public static Kernel CreateKernelWithChatCompletion(...)
 {
    ...
 }
 // Create a kernel with OpenAI chat completion for the research agent
 Kernel researchKernel = CreateKernelWithChatCompletion("gpt-4o-search-preview");
 ChatCompletionAgent researchAgent = new ChatCompletionAgent {
    Name = "ResearchAgent",
    Description = "A helpful assistant with access to web search. Ask it to 
perform web searches.",
    Instructions = "You are a Researcher. You find information without additional 
computation or quantitative analysis.",
    Kernel = researchKernel,
 };
 // Create a persistent Azure AI agent for code execution
 PersistentAgentsClient agentsClient = AzureAIAgent.CreateAgentsClient(endpoint, 
new AzureCliCredential());
 PersistentAgent definition = await agentsClient.Administration.CreateAgentAsync(
    modelId,
    name: "CoderAgent",
    description: "Write and executes code to process and analyze data.",
    instructions: "You solve questions using code. Please provide detailed 
analysis and computation process.",
    tools: [new CodeInterpreterToolDefinition()]);
 AzureAIAgent coderAgent = new AzureAIAgent(definition, agentsClient);
 Set Up the Magentic Manager
C#
 You can create a callback to capture agent responses as the orchestration progresses via the
 ResponseCallback property.
 C#
 Combine your agents and manager into a MagenticOrchestration object.
 C#
 A runtime is required to manage the execution of agents. Here, we use InProcessRuntime and
 start it before invoking the orchestration.
 C#
 Kernel managerKernel = CreateKernelWithChatCompletion("o3-mini");
 StandardMagenticManager manager = new StandardMagenticManager(
    managerKernel.GetRequiredService<IChatCompletionService>(),
    new OpenAIPromptExecutionSettings())
 {
    MaximumInvocationCount = 5,
 };
 Optional: Observe Agent Responses
 ChatHistory history = [];
 ValueTask responseCallback(ChatMessageContent response)
 {
    history.Add(response);
    return ValueTask.CompletedTask;
 }
 Create the Magentic Orchestration
 MagenticOrchestration orchestration = new MagenticOrchestration(
    manager,
    researchAgent,
    coderAgent)
 {
    ResponseCallback = responseCallback,
 };
 Start the Runtime
Invoke the orchestration with your complex task. The manager will plan, delegate, and
 coordinate the agents to solve the problem.
 C#
 Wait for the orchestration to complete and retrieve the final output.
 C#
 After processing is complete, stop the runtime to clean up resources.
 C#
 InProcessRuntime runtime = new InProcessRuntime();
 await runtime.StartAsync();
 Invoke the Orchestration
 string input = @"I am preparing a report on the energy efficiency of different 
machine learning model architectures.\nCompare the estimated training and 
inference energy consumption of ResNet-50, BERT-base, and GPT-2 on standard 
datasets (e.g., ImageNet for ResNet, GLUE for BERT, WebText for GPT-2). Then, 
estimate the CO2 emissions associated with each, assuming training on an Azure 
Standard_NC6s_v3 VM for 24 hours. Provide tables for clarity, and recommend the 
most energy-efficient model per task type (image classification, text 
classification, and text generation).";
 var result = await orchestration.InvokeAsync(input, runtime);
 Collect Results
 string output = await result.GetValueAsync(TimeSpan.FromSeconds(300));
 Console.WriteLine($"\n# RESULT: {output}");
 Console.WriteLine("\n\nORCHESTRATION HISTORY");
 foreach (ChatMessageContent message in history)
 {
    // Print each message
    Console.WriteLine($"# {message.Role} - {message.AuthorName}: 
{message.Content}");
 }
 Optional: Stop the Runtime
 await runtime.RunUntilIdleAsync();
plaintext
 Sample Output
 # RESULT: ```markdown
 # Report: Energy Efficiency of Machine Learning Model Architectures
 This report assesses the energy consumption and related CO₂ emissions for three 
popular ...
 ORCHESTRATION HISTORY
 # Assistant - ResearchAgent: Comparing the energy efficiency of different machine 
learning ...
 # assistant - CoderAgent: Below are tables summarizing the approximate energy 
consumption and ...
 # assistant - CoderAgent: The estimates provided in our tables align with a 
general understanding ...
 # assistant - CoderAgent: Here's the updated structure for the report integrating 
both the ...
  Tip
 The full sample code is available here
 Next steps
 Advanced Topics in Agent Orchestration
Semantic Kernel Agent Orchestration
 Advanced Topics
 Article • 05/23/2025
） Important
 Agent Orchestration features in the Agent Framework are in the experimental stage. They
 are under active development and may change significantly before advancing to the
 preview or release candidate stage.
 Runtime
 The runtime is the foundational component that manages the lifecycle, communication, and
 execution of agents and orchestrations. It acts as the message bus and execution environment
 for all actors (agents and orchestration-specific actors) in the system.
 Role of the Runtime
 Message Routing: The runtime is responsible for delivering messages between agents
 and orchestration actors, using a pub-sub or direct messaging model depending on the
 orchestration pattern.
 Actor Lifecycle Management: It creates, registers, and manages the lifecycle of all actors
 involved in an orchestration, ensuring isolation and proper resource management.
 Execution Context: The runtime provides the execution context for orchestrations,
 allowing multiple orchestrations (and their invocations) to run independently and
 concurrently.
 Relationship Between Runtime and Orchestrations
 Think of an orchestration as a graph that defines how agents interact with each other. The
 runtime is the engine that executes this graph, managing the flow of messages and the
 lifecycle of agents. Developers can execute this graph multiple times with different inputs on
 the same runtime instance, and the runtime will ensure that each execution is isolated and
 independent.
 Timeouts
When an orchestration is invoked, the orchestration returns immediately with a handler that
 can be used to get the result later. This asynchronous pattern allows a more flexible and
 responsive design, especially in scenarios where the orchestration may take a long time to
 complete.
） Important
 If a timeout occurs, the invocation of the orchestration will not be cancelled. The
 orchestration will continue to run in the background until it completes. Developers can
 still retrieve the result later.
 Developers can get the result of the an orchestration invocation later by calling the
 GetValueAsync method on the result object. When the application is ready to process the
 result, the invocation may or may not have completed. Therefore, developers can optionally
 specify a timeout for the 
GetValueAsync method. If the orchestration does not complete within
 the specified timeout, a timeout exception will be thrown.
 C#
 string output = await result.GetValueAsync(TimeSpan.FromSeconds(60));
 If the orchestration does not complete within the specified timeout, a timeout exception will be
 thrown.
 Human-in-the-loop
 Agent Response Callback
 To see agent responses inside an invocation, developers can provide a 
ResponseCallback to the
 orchestration. This allows developers to observe the responses from each agent during the
 orchestration process. Developers can use this callback for UI updates, logging, or other
 purposes.
 C#
 public ValueTask ResponseCallback(ChatMessageContent response)
 {
 Console.WriteLine($"# {response.AuthorName}\n{response.Content}");
 return ValueTask.CompletedTask;
 }
 SequentialOrchestration orchestration = new SequentialOrchestration(
analystAgent, writerAgent, editorAgent)
 {
 };
 ResponseCallback = ResponseCallback,
 Human Response Function
 For orchestrations that supports user input (e.g., handoff and group chat), provide an
 InteractiveCallback that returns a 
ChatMessageContent from the user. By using this callback,
 developers can implement custom logic to gather user input, such as displaying a UI prompt or
 integrating with other systems.
 C#
 HandoffOrchestration orchestration = new(...)
 {
 InteractiveCallback = () =>
 {
 Console.Write("User: ");
 string input = Console.ReadLine();
 return new ChatMessageContent(AuthorRole.User, input);
 }
 };
 Structured Data
 We believe that structured data is a key part in building agentic workflows. By using structured
 data, developers can create more reuseable orchestrations and the development experience is
 improved. The Semantic Kernel SDK provides a way to pass structured data as input to
 orchestrations and return structured data as output.
） Important
 Internally, the orchestrations still process data as 
Structured Inputs
 ChatMessageContent.
 Developers can pass structured data as input to orchestrations by using a strongly-typed input
 class and specifying it as the generic parameter for the orchestration. This enables type safety
 and more flexibility for orchestrations to handle complex data structures. For example, to triage
 GitHub issues, define a class for the structured input:
C#
 Developers can then use this type as the input to an orchestration by providing it as the
 generic parameter:
 C#
 By default, the orchestration will use the built-in input transform, which serializes the object to
 JSON and wraps it in a ChatMessageContent. If you want to customize how your structured
 input is converted to the underlying message type, you can provide your own input transform
 function via the InputTransform property:
 C#
 This allows you to control exactly how your typed input is presented to the agents, enabling
 advanced scenarios such as custom formatting, field selection, or multi-message input.
 public sealed class GithubIssue
 {
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string[] Labels { get; set; } = [];
 }
 HandoffOrchestration<GithubIssue, string> orchestration =
    new(...);
 GithubIssue input = new GithubIssue { ... };
 var result = await orchestration.InvokeAsync(input, runtime);
 Custom Input Transforms
 HandoffOrchestration<GithubIssue, string> orchestration =
    new(...)
    {
        InputTransform = (issue, cancellationToken) =>
        {
            // For example, create a chat message with a custom format
            var message = new ChatMessageContent(AuthorRole.User, $"[{issue.Id}] 
{issue.Title}\n{issue.Body}");
            return ValueTask.FromResult<IEnumerable<ChatMessageContent>>
 ([message]);
        },
    };
Agents and orchestrations can return structured outputs by specifying a strongly-typed output
 class as the generic parameter for the orchestration. This enables you to work with rich,
 structured results in your application, rather than just plain text.
 For example, suppose you want to analyze an article and extract themes, sentiments, and
 entities. Define a class for the structured output:
 C#
 You can then use this type as the output for your orchestration by providing it as the generic
 parameter:
 C#
 By default, the orchestration will use the built-in output transform, which attempts to
 deserialize the agent's response content to your output type. For more advanced scenarios,
 you can provide a custom output transform (for example, with structured output by some
 models).
  Tip
 See the full sample in Step04a_HandoffWithStructuredInput.cs
 Structured Outputs
 public sealed class Analysis
 {
    public IList<string> Themes { get; set; } = [];
    public IList<string> Sentiments { get; set; } = [];
    public IList<string> Entities { get; set; } = [];
 }
 ConcurrentOrchestration<string, Analysis> orchestration =
    new(agent1, agent2, agent3)
    {
        ResultTransform = outputTransform.TransformAsync, // see below
    };
 // ...
 OrchestrationResult<Analysis> result = await orchestration.InvokeAsync(input, 
runtime);
 Analysis output = await result.GetValueAsync(TimeSpan.FromSeconds(60));
 Custom Output Transforms
C#
 StructuredOutputTransform<Analysis> outputTransform =
 new(chatCompletionService, new OpenAIPromptExecutionSettings { ResponseFormat 
= typeof(Analysis) });
 ConcurrentOrchestration<string, Analysis> orchestration =
 new(agent1, agent2, agent3)
 {
 ResultTransform = outputTransform.TransformAsync,
 };
 This approach allows you to receive and process structured data directly from the
 orchestration, making it easier to build advanced workflows and integrations.
  Tip
 See the full sample in 
Step01a_ConcurrentWithStructuredOutput.cs
 Cancellation
） Important
 Cancellation will stop the agents from processing any further messages, but it will not stop
 agents that are already processing messages.
） Important
 Cancellation will not stop the runtime.
 You can cancel an orchestration by calling the 
Cancel method on the result handler. This will
 stop the orchestration by propagating the signal to all agents, and they will stop processing
 any further messages.
 C#
 var resultTask = orchestration.InvokeAsync(input, runtime);
 resultTask.Cancel();
 Next steps
More Code Samples
How-To: 
ChatCompletionAgent
 Article • 05/06/2025
） Important
 This feature is in the experimental stage. Features at this stage are under development
 and subject to change before advancing to the preview or release candidate stage.
 Overview
 In this sample, we will explore configuring a plugin to access GitHub API and provide
 templatized instructions to a ChatCompletionAgent to answer questions about a GitHub
 repository. The approach will be broken down step-by-step to high-light the key parts of the
 coding process. As part of the task, the agent will provide document citations within the
 response.
 Streaming will be used to deliver the agent's responses. This will provide real-time updates as
 the task progresses.
 Getting Started
 Before proceeding with feature coding, make sure your development environment is fully set
 up and configured.
 Start by creating a Console project. Then, include the following package references to ensure all
 required dependencies are available.
 To add package dependencies from the command-line use the 
dotnet command:
 PowerShell
 dotnet add package Azure.Identity
 dotnet add package Microsoft.Extensions.Configuration
 dotnet add package Microsoft.Extensions.Configuration.Binder
 dotnet add package Microsoft.Extensions.Configuration.UserSecrets
 dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
 dotnet add package Microsoft.SemanticKernel.Connectors.AzureOpenAI
 dotnet add package Microsoft.SemanticKernel.Agents.Core --prerelease
 If managing NuGet packages in Visual Studio, ensure 
Include prerelease is checked.
The project file (.csproj) should contain the following PackageReference definitions:
 XML
 The Agent Framework is experimental and requires warning suppression. This may addressed in
 as a property in the project file (.csproj):
 XML
 Additionally, copy the GitHub plug-in and models (GitHubPlugin.cs and GitHubModels.cs)
 from Semantic Kernel LearnResources Project . Add these files in your project folder.
 This sample requires configuration setting in order to connect to remote services. You will need
 to define settings for either OpenAI or Azure OpenAI and also for GitHub.
 Note: For information on GitHub Personal Access Tokens, see: Managing your personal
 access tokens .
 PowerShell
  <ItemGroup>
    <PackageReference Include="Azure.Identity" Version="<stable>" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="
 <stable>" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Binder" 
Version="<stable>" />
    <PackageReference Include="Microsoft.Extensions.Configuration.UserSecrets" 
Version="<stable>" />
    <PackageReference 
Include="Microsoft.Extensions.Configuration.EnvironmentVariables" Version="
 <stable>" />
    <PackageReference Include="Microsoft.SemanticKernel.Agents.Core" Version="
 <latest>" />
    <PackageReference Include="Microsoft.SemanticKernel.Connectors.AzureOpenAI" 
Version="<latest>" />
  </ItemGroup>
  <PropertyGroup>
    <NoWarn>$(NoWarn);CA2007;IDE1006;SKEXP0001;SKEXP0110;OPENAI001</NoWarn>
  </PropertyGroup>
 Configuration
 # OpenAI
 dotnet user-secrets set "OpenAISettings:ApiKey" "<api-key>"
 dotnet user-secrets set "OpenAISettings:ChatModel" "gpt-4o"
The following class is used in all of the Agent examples. Be sure to include it in your project to
 ensure proper functionality. This class serves as a foundational component for the examples
 that follow.
 c#
 # Azure OpenAI
 dotnet user-secrets set "AzureOpenAISettings:ApiKey" "<api-key>" # Not required if 
using token-credential
 dotnet user-secrets set "AzureOpenAISettings:Endpoint" "<model-endpoint>"
 dotnet user-secrets set "AzureOpenAISettings:ChatModelDeployment" "gpt-4o"
 # GitHub
 dotnet user-secrets set "GitHubSettings:BaseUrl" "https://api.github.com"
 dotnet user-secrets set "GitHubSettings:Token" "<personal access token>"
 using System.Reflection;
 using Microsoft.Extensions.Configuration;
 namespace AgentsSample;
 public class Settings
 {
    private readonly IConfigurationRoot configRoot;
    private AzureOpenAISettings azureOpenAI;
    private OpenAISettings openAI;
    public AzureOpenAISettings AzureOpenAI => this.azureOpenAI ??= 
this.GetSettings<Settings.AzureOpenAISettings>();
    public OpenAISettings OpenAI => this.openAI ??= 
this.GetSettings<Settings.OpenAISettings>();
    public class OpenAISettings
    {
        public string ChatModel { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
    public class AzureOpenAISettings
    {
        public string ChatModelDeployment { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
    public TSettings GetSettings<TSettings>() =>
        this.configRoot.GetRequiredSection(typeof(TSettings).Name).Get<TSettings>
 ()!;
    public Settings()
    {
this.configRoot =
 new ConfigurationBuilder()
 .AddEnvironmentVariables()
 .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
 .Build();
 }
 }
 Coding
 The coding process for this sample involves:
 1. Setup - Initializing settings and the plug-in.
 2. Agent Definition - Create the 
ChatCompletionAgent with templatized instructions and
 plug-in.
 3. The Chat Loop - Write the loop that drives user / agent interaction.
 The full example code is provided in the Final section. Refer to that section for the complete
 implementation.
 Setup
 Prior to creating a 
ChatCompletionAgent, the configuration settings, plugins, and 
be initialized.
 Initialize the 
Settings class referenced in the previous Configuration section.
 C#
 Settings settings = new();
 Kernel must
 Initialize the plug-in using its settings.
 Here, a message is displaying to indicate progress.
 C#
 Console.WriteLine("Initialize plugins...");
 GitHubSettings githubSettings = settings.GetSettings<GitHubSettings>();
 GitHubPlugin githubPlugin = new(githubSettings);
 Now initialize a 
Kernel instance with an 
IChatCompletionService and the 
GitHubPlugin
 previously created.
C#
 Finally we are ready to instantiate a ChatCompletionAgent with its Instructions, associated
 Kernel, and the default Arguments and Execution Settings. In this case, we desire to have the
 any plugin functions automatically executed.
 C#
 Console.WriteLine("Creating kernel...");
 IKernelBuilder builder = Kernel.CreateBuilder();
 builder.AddAzureOpenAIChatCompletion(
    settings.AzureOpenAI.ChatModelDeployment,
    settings.AzureOpenAI.Endpoint,
    new AzureCliCredential());
 builder.Plugins.AddFromObject(githubPlugin);
 Kernel kernel = builder.Build();
 Agent Definition
 Console.WriteLine("Defining agent...");
 ChatCompletionAgent agent =
    new()
    {
        Name = "SampleAssistantAgent",
        Instructions =
            """
            You are an agent designed to query and retrieve information from a 
single GitHub repository in a read-only manner.
            You are also able to access the profile of the active user.
            Use the current date and time to provide up-to-date details or time
sensitive responses.
            The repository you are querying is a public repository with the 
following name: {{$repository}}
            The current date and time is: {{$now}}. 
            """,
        Kernel = kernel,
        Arguments =
            new KernelArguments(new AzureOpenAIPromptExecutionSettings() { 
FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() })
            {
                { "repository", "microsoft/semantic-kernel" }
            }
    };
At last, we are able to coordinate the interaction between the user and the Agent. Start by
 creating a ChatHistoryAgentThread object to maintain the conversation state and creating an
 empty loop.
 C#
 Now let's capture user input within the previous loop. In this case, empty input will be ignored
 and the term EXIT will signal that the conversation is completed.
 C#
 To generate a Agent response to user input, invoke the agent using Arguments to provide the
 final template parameter that specifies the current date and time.
 The Agent response is then then displayed to the user.
 C#
 Console.WriteLine("Ready!");
 The Chat Loop
 ChatHistoryAgentThread agentThread = new();
 bool isComplete = false;
 do
 {
    // processing logic here
 } while (!isComplete);
 Console.WriteLine();
 Console.Write("> ");
 string input = Console.ReadLine();
 if (string.IsNullOrWhiteSpace(input))
 {
    continue;
 }
 if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
 {
    isComplete = true;
    break;
 }
 var message = new ChatMessageContent(AuthorRole.User, input);
 Console.WriteLine();
Bringing all the steps together, we have the final code for this example. The complete
 implementation is provided below.
 Try using these suggested inputs:
 1. What is my username?
 2. Describe the repo.
 3. Describe the newest issue created in the repo.
 4. List the top 10 issues closed within the last week.
 5. How were these issues labeled?
 6. List the 5 most recently opened issues with the "Agents" label
 C#
 DateTime now = DateTime.Now;
 KernelArguments arguments =
    new()
    {
        { "now", $"{now.ToShortDateString()} {now.ToShortTimeString()}" }
    };
 await foreach (ChatMessageContent response in agent.InvokeAsync(message, 
agentThread, options: new() { KernelArguments = arguments }))
 {
    Console.WriteLine($"{response.Content}");
 }
 Final
 using System;
 using System.Threading.Tasks;
 using Azure.Identity;
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.Agents;
 using Microsoft.SemanticKernel.ChatCompletion;
 using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
 using Plugins;
 namespace AgentsSample;
 public static class Program
 {
    public static async Task Main()
    {
        // Load configuration from environment variables or user secrets.
        Settings settings = new();
        Console.WriteLine("Initialize plugins...");
        GitHubSettings githubSettings = settings.GetSettings<GitHubSettings>();
GitHubPlugin githubPlugin = new(githubSettings);
 Console.WriteLine("Creating kernel...");
 IKernelBuilder builder = Kernel.CreateBuilder();
 builder.AddAzureOpenAIChatCompletion(
 settings.AzureOpenAI.ChatModelDeployment,
 settings.AzureOpenAI.Endpoint,
 new AzureCliCredential());
 builder.Plugins.AddFromObject(githubPlugin);
 Kernel kernel = builder.Build();
 Console.WriteLine("Defining agent...");
 ChatCompletionAgent agent =
 new()
 {
 Name = "SampleAssistantAgent",
 Instructions =
 """
 You are an agent designed to query and retrieve 
information from a single GitHub repository in a read-only manner.
 You are also able to access the profile of the active 
user.
 Use the current date and time to provide up-to-date 
details or time-sensitive responses.
 The repository you are querying is a public repository 
with the following name: {{$repository}}
 The current date and time is: {{$now}}. 
""",
 Kernel = kernel,
 Arguments =
 new KernelArguments(new AzureOpenAIPromptExecutionSettings() { 
FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() })
 {
 { "repository", "microsoft/semantic-kernel" }
 }
 };
 Console.WriteLine("Ready!");
 ChatHistoryAgentThread agentThread = new();
 bool isComplete = false;
 do
 {
 Console.WriteLine();
 Console.Write("> ");
 string input = Console.ReadLine();
 if (string.IsNullOrWhiteSpace(input))
 {
 continue;
            }
            if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                isComplete = true;
                break;
            }
            var message = new ChatMessageContent(AuthorRole.User, input);
            Console.WriteLine();
            DateTime now = DateTime.Now;
            KernelArguments arguments =
                new()
                {
                    { "now", $"{now.ToShortDateString()} 
{now.ToShortTimeString()}" }
                };
            await foreach (ChatMessageContent response in 
agent.InvokeAsync(message, agentThread, options: new() { KernelArguments = 
arguments }))
            {
                // Display response.
                Console.WriteLine($"{response.Content}");
            }
        } while (!isComplete);
    }
 }
 How-To:OpenAIAssistantAgentCode Interpreter
How-To: 
OpenAIAssistantAgent Code
 Interpreter
 Article • 05/06/2025
） Important
 This feature is in the release candidate stage. Features at this stage are nearly complete
 and generally stable, though they may undergo minor refinements or optimizations
 before reaching full general availability.
 Overview
 In this sample, we will explore how to use the code-interpreter tool of an
 OpenAIAssistantAgent to complete data-analysis tasks. The approach will be broken down
 step-by-step to high-light the key parts of the coding process. As part of the task, the agent
 will generate both image and text responses. This will demonstrate the versatility of this tool in
 performing quantitative analysis.
 Streaming will be used to deliver the agent's responses. This will provide real-time updates as
 the task progresses.
 Getting Started
 Before proceeding with feature coding, make sure your development environment is fully set
 up and configured.
 Start by creating a Console project. Then, include the following package references to ensure
 all required dependencies are available.
 To add package dependencies from the command-line use the 
PowerShell
 dotnet command:
 dotnet add package Azure.Identity
 dotnet add package Microsoft.Extensions.Configuration
 dotnet add package Microsoft.Extensions.Configuration.Binder
 dotnet add package Microsoft.Extensions.Configuration.UserSecrets
 dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
 dotnet add package Microsoft.SemanticKernel
 dotnet add package Microsoft.SemanticKernel.Agents.OpenAI --prerelease
If managing NuGet packages in Visual Studio, ensure Include prerelease is checked.
 The project file (.csproj) should contain the following PackageReference definitions:
 XML
 The Agent Framework is experimental and requires warning suppression. This may addressed in
 as a property in the project file (.csproj):
 XML
 Additionally, copy the PopulationByAdmin1.csv and PopulationByCountry.csv data files from
 Semantic Kernel LearnResources Project . Add these files in your project folder and configure
 to have them copied to the output directory:
 XML
  <ItemGroup>
    <PackageReference Include="Azure.Identity" Version="<stable>" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="
 <stable>" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Binder" 
Version="<stable>" />
    <PackageReference Include="Microsoft.Extensions.Configuration.UserSecrets" 
Version="<stable>" />
    <PackageReference 
Include="Microsoft.Extensions.Configuration.EnvironmentVariables" Version="
 <stable>" />
    <PackageReference Include="Microsoft.SemanticKernel" Version="<latest>" />
    <PackageReference Include="Microsoft.SemanticKernel.Agents.OpenAI" Version="
 <latest>" />
  </ItemGroup>
  <PropertyGroup>
    <NoWarn>$(NoWarn);CA2007;IDE1006;SKEXP0001;SKEXP0110;OPENAI001</NoWarn>
  </PropertyGroup>
  <ItemGroup>
    <None Include="PopulationByAdmin1.csv">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
    <None Include="PopulationByCountry.csv">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>
This sample requires configuration setting in order to connect to remote services. You will need
 to define settings for either OpenAI or Azure OpenAI.
 PowerShell
 The following class is used in all of the Agent examples. Be sure to include it in your project to
 ensure proper functionality. This class serves as a foundational component for the examples
 that follow.
 c#
 Configuration
 # OpenAI
 dotnet user-secrets set "OpenAISettings:ApiKey" "<api-key>"
 dotnet user-secrets set "OpenAISettings:ChatModel" "gpt-4o"
 # Azure OpenAI
 dotnet user-secrets set "AzureOpenAISettings:ApiKey" "<api-key>" # Not required if 
using token-credential
 dotnet user-secrets set "AzureOpenAISettings:Endpoint" "<model-endpoint>"
 dotnet user-secrets set "AzureOpenAISettings:ChatModelDeployment" "gpt-4o"
 using System.Reflection;
 using Microsoft.Extensions.Configuration;
 namespace AgentsSample;
 public class Settings
 {
    private readonly IConfigurationRoot configRoot;
    private AzureOpenAISettings azureOpenAI;
    private OpenAISettings openAI;
    public AzureOpenAISettings AzureOpenAI => this.azureOpenAI ??= 
this.GetSettings<Settings.AzureOpenAISettings>();
    public OpenAISettings OpenAI => this.openAI ??= 
this.GetSettings<Settings.OpenAISettings>();
    public class OpenAISettings
    {
        public string ChatModel { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
    public class AzureOpenAISettings
    {
        public string ChatModelDeployment { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
The coding process for this sample involves:
 1. Setup - Initializing settings and the plug-in.
 2. Agent Definition - Create the _OpenAI_AssistantAgent with templatized instructions and
 plug-in.
 3. The Chat Loop - Write the loop that drives user / agent interaction.
 The full example code is provided in the Final section. Refer to that section for the complete
 implementation.
 Prior to creating an OpenAIAssistantAgent, ensure the configuration settings are available and
 prepare the file resources.
 Instantiate the Settings class referenced in the previous Configuration section. Use the
 settings to also create an AzureOpenAIClient that will be used for the Agent Definition as well
 as file-upload.
 C#
    }
    public TSettings GetSettings<TSettings>() =>
        this.configRoot.GetRequiredSection(typeof(TSettings).Name).Get<TSettings>
 ()!;
    public Settings()
    {
        this.configRoot =
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
                .Build();
    }
 }
 Coding
 Setup
 Settings settings = new();
 AzureOpenAIClient client = OpenAIAssistantAgent.CreateAzureOpenAIClient(new 
AzureCliCredential(), new Uri(settings.AzureOpenAI.Endpoint));
Use the AzureOpenAIClient to access an OpenAIFileClient and upload the two data-files
 described in the previous Configuration section, preserving the File Reference for final clean-up.
 C#
 We are now ready to instantiate an OpenAIAssistantAgent by first creating an assistant
 definition. The assistant is configured with its target model, Instructions, and the Code
 Interpreter tool enabled. Additionally, we explicitly associate the two data files with the Code
 Interpreter tool.
 C#
 Console.WriteLine("Uploading files...");
 OpenAIFileClient fileClient = client.GetOpenAIFileClient();
 OpenAIFile fileDataCountryDetail = await 
fileClient.UploadFileAsync("PopulationByAdmin1.csv", 
FileUploadPurpose.Assistants);
 OpenAIFile fileDataCountryList = await 
fileClient.UploadFileAsync("PopulationByCountry.csv", 
FileUploadPurpose.Assistants);
 Agent Definition
 Console.WriteLine("Defining agent...");
 AssistantClient assistantClient = client.GetAssistantClient();
        Assistant assistant =
            await assistantClient.CreateAssistantAsync(
                settings.AzureOpenAI.ChatModelDeployment,
                name: "SampleAssistantAgent",
                instructions:
                        """
                        Analyze the available data to provide an answer to the 
user's question.
                        Always format response using markdown.
                        Always include a numerical index that starts at 1 for any 
lists or tables.
                        Always sort lists in ascending order.
                        """,
                enableCodeInterpreter: true,
                codeInterpreterFileIds: [fileDataCountryList.Id, 
fileDataCountryDetail.Id]);
 // Create agent
 OpenAIAssistantAgent agent = new(assistant, assistantClient);
 The Chat Loop
At last, we are able to coordinate the interaction between the user and the Agent. Start by
 creating an AgentThread to maintain the conversation state and creating an empty loop.
 Let's also ensure the resources are removed at the end of execution to minimize unnecessary
 charges.
 C#
 Now let's capture user input within the previous loop. In this case, empty input will be ignored
 and the term EXIT will signal that the conversation is completed.
 C#
 Console.WriteLine("Creating thread...");
 AssistantAgentThread agentThread = new();
 Console.WriteLine("Ready!");
 try
 {
    bool isComplete = false;
    List<string> fileIds = [];
    do
    {
    } while (!isComplete);
 }
 finally
 {
    Console.WriteLine();
    Console.WriteLine("Cleaning-up...");
    await Task.WhenAll(
        [
            agentThread.DeleteAsync(),
            assistantClient.DeleteAssistantAsync(assistant.Id),
            fileClient.DeleteFileAsync(fileDataCountryList.Id),
            fileClient.DeleteFileAsync(fileDataCountryDetail.Id),
        ]);
 }
 Console.WriteLine();
 Console.Write("> ");
 string input = Console.ReadLine();
 if (string.IsNullOrWhiteSpace(input))
 {
    continue;
 }
 if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
 {
    isComplete = true;
    break;
Before invoking the Agent response, let's add some helper methods to download any files that
 may be produced by the Agent.
 Here we're place file content in the system defined temporary directory and then launching the
 system defined viewer application.
 C#
 }
 var message = new ChatMessageContent(AuthorRole.User, input);
 Console.WriteLine();
 private static async Task DownloadResponseImageAsync(OpenAIFileClient client, 
ICollection<string> fileIds)
 {
    if (fileIds.Count > 0)
    {
        Console.WriteLine();
        foreach (string fileId in fileIds)
        {
            await DownloadFileContentAsync(client, fileId, launchViewer: true);
        }
    }
 }
 private static async Task DownloadFileContentAsync(OpenAIFileClient client, string 
fileId, bool launchViewer = false)
 {
    OpenAIFile fileInfo = client.GetFile(fileId);
    if (fileInfo.Purpose == FilePurpose.AssistantsOutput)
    {
        string filePath =
            Path.Combine(
                Path.GetTempPath(),
                Path.GetFileName(Path.ChangeExtension(fileInfo.Filename, 
".png")));
        BinaryData content = await client.DownloadFileAsync(fileId);
        await using FileStream fileStream = new(filePath, FileMode.CreateNew);
        await content.ToStream().CopyToAsync(fileStream);
        Console.WriteLine($"File saved to: {filePath}.");
        if (launchViewer)
        {
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C start {filePath}"
                });
To generate an Agent response to user input, invoke the agent by providing the message and
 the AgentThread. In this example, we choose a streamed response and capture any generated
 File References for download and review at the end of the response cycle. It's important to note
 that generated code is identified by the presence of a Metadata key in the response message,
 distinguishing it from the conversational reply.
 C#
 Bringing all the steps together, we have the final code for this example. The complete
 implementation is provided below.
 Try using these suggested inputs:
 1. Compare the files to determine the number of countries do not have a state or province
 defined compared to the total count
        }
    }
 }
 bool isCode = false;
 await foreach (StreamingChatMessageContent response in 
agent.InvokeStreamingAsync(message, agentThread))
 {
    if (isCode != 
(response.Metadata?.ContainsKey(OpenAIAssistantAgent.CodeInterpreterMetadataKey) 
?? false))
    {
        Console.WriteLine();
        isCode = !isCode;
    }
    // Display response.
    Console.Write($"{response.Content}");
    // Capture file IDs for downloading.
    fileIds.AddRange(response.Items.OfType<StreamingFileReferenceContent>
 ().Select(item => item.FileId));
 }
 Console.WriteLine();
 // Download any files referenced in the response.
 await DownloadResponseImageAsync(fileClient, fileIds);
 fileIds.Clear();
 Final
2. Create a table for countries with state or province defined. Include the count of states or
 provinces and the total population
 3. Provide a bar chart for countries whose names start with the same letter and sort the x
 axis by highest count to lowest (include all countries)
 C#
 using Azure.AI.OpenAI;
 using Azure.Identity;
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.Agents.OpenAI;
 using Microsoft.SemanticKernel.ChatCompletion;
 using OpenAI.Assistants;
 using OpenAI.Files;
 using System;
 using System.Collections.Generic;
 using System.Diagnostics;
 using System.IO;
 using System.Linq;
 using System.Threading.Tasks;
 namespace AgentsSample;
 public static class Program
 {
    public static async Task Main()
    {
        // Load configuration from environment variables or user secrets.
        Settings settings = new();
        // Initialize the clients
        AzureOpenAIClient client = 
OpenAIAssistantAgent.CreateAzureOpenAIClient(new AzureCliCredential(), new 
Uri(settings.AzureOpenAI.Endpoint));
        //OpenAIClient client = OpenAIAssistantAgent.CreateOpenAIClient(new 
ApiKeyCredential(settings.OpenAI.ApiKey)));
        AssistantClient assistantClient = client.GetAssistantClient();
        OpenAIFileClient fileClient = client.GetOpenAIFileClient();
        // Upload files
        Console.WriteLine("Uploading files...");
        OpenAIFile fileDataCountryDetail = await 
fileClient.UploadFileAsync("PopulationByAdmin1.csv", 
FileUploadPurpose.Assistants);
        OpenAIFile fileDataCountryList = await 
fileClient.UploadFileAsync("PopulationByCountry.csv", 
FileUploadPurpose.Assistants);
        // Define assistant
        Console.WriteLine("Defining assistant...");
        Assistant assistant =
            await assistantClient.CreateAssistantAsync(
                settings.AzureOpenAI.ChatModelDeployment,
name: "SampleAssistantAgent",
 instructions:
 """
 Analyze the available data to provide an answer to the 
user's question.
 lists or tables.
 Always format response using markdown.
 Always include a numerical index that starts at 1 for any 
Always sort lists in ascending order.
 """,
 enableCodeInterpreter: true,
 codeInterpreterFileIds: [fileDataCountryList.Id, 
fileDataCountryDetail.Id]);
 // Create agent
 OpenAIAssistantAgent agent = new(assistant, assistantClient);
 // Create the conversation thread
 Console.WriteLine("Creating thread...");
 AssistantAgentThread agentThread = new();
 Console.WriteLine("Ready!");
 try
 {
 bool isComplete = false;
 List<string> fileIds = [];
 do
 {
 Console.WriteLine();
 Console.Write("> ");
 string input = Console.ReadLine();
 if (string.IsNullOrWhiteSpace(input))
 {
 continue;
 }
 if (input.Trim().Equals("EXIT", 
StringComparison.OrdinalIgnoreCase))
 {
 isComplete = true;
 break;
 }
 var message = new ChatMessageContent(AuthorRole.User, input);
 Console.WriteLine();
 bool isCode = false;
 await foreach (StreamingChatMessageContent response in 
agent.InvokeStreamingAsync(message, agentThread))
 {
 if (isCode != 
(response.Metadata?.ContainsKey(OpenAIAssistantAgent.CodeInterpreterMetadataKey) 
?? false))
 {
}
 Console.WriteLine();
 isCode = !isCode;
 // Display response.
 Console.Write($"{response.Content}");
 // Capture file IDs for downloading.
 fileIds.AddRange(response.Items.OfType<StreamingFileReferenceContent>
 ().Select(item => item.FileId));
 }
 Console.WriteLine();
 // Download any files referenced in the response.
 await DownloadResponseImageAsync(fileClient, fileIds);
 fileIds.Clear();
 } while (!isComplete);
 }
 finally
 {
 Console.WriteLine();
 Console.WriteLine("Cleaning-up...");
 await Task.WhenAll(
 [
 agentThread.DeleteAsync(),
 assistantClient.DeleteAssistantAsync(assistant.Id),
 fileClient.DeleteFileAsync(fileDataCountryList.Id),
 fileClient.DeleteFileAsync(fileDataCountryDetail.Id),
 ]);
 }
 }
 private static async Task DownloadResponseImageAsync(OpenAIFileClient client, 
ICollection<string> fileIds)
 {
 if (fileIds.Count > 0)
 {
 Console.WriteLine();
 foreach (string fileId in fileIds)
 {
 await DownloadFileContentAsync(client, fileId, launchViewer: 
true);
 }
 }
 }
 private static async Task DownloadFileContentAsync(OpenAIFileClient client, 
string fileId, bool launchViewer = false)
 {
 OpenAIFile fileInfo = client.GetFile(fileId);
 if (fileInfo.Purpose == FilePurpose.AssistantsOutput)
 {
 string filePath =
                Path.Combine(
                    Path.GetTempPath(),
                    Path.GetFileName(Path.ChangeExtension(fileInfo.Filename, 
".png")));
            BinaryData content = await client.DownloadFileAsync(fileId);
            await using FileStream fileStream = new(filePath, FileMode.CreateNew);
            await content.ToStream().CopyToAsync(fileStream);
            Console.WriteLine($"File saved to: {filePath}.");
            if (launchViewer)
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C start {filePath}"
                    });
            }
        }
    }
 }
 How-To:OpenAIAssistantAgentCode File Search
How-To: 
OpenAIAssistantAgent File Search
 Article • 05/06/2025
） Important
 This feature is in the release candidate stage. Features at this stage are nearly complete
 and generally stable, though they may undergo minor refinements or optimizations
 before reaching full general availability.
 Overview
 In this sample, we will explore how to use the file-search tool of an OpenAIAssistantAgent to
 complete comprehension tasks. The approach will be step-by-step, ensuring clarity and
 precision throughout the process. As part of the task, the agent will provide document citations
 within the response.
 Streaming will be used to deliver the agent's responses. This will provide real-time updates as
 the task progresses.
 Getting Started
 Before proceeding with feature coding, make sure your development environment is fully set
 up and configured.
 To add package dependencies from the command-line use the 
PowerShell
 dotnet command:
 dotnet add package Azure.Identity
 dotnet add package Microsoft.Extensions.Configuration
 dotnet add package Microsoft.Extensions.Configuration.Binder
 dotnet add package Microsoft.Extensions.Configuration.UserSecrets
 dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
 dotnet add package Microsoft.SemanticKernel
 dotnet add package Microsoft.SemanticKernel.Agents.OpenAI --prerelease
 The project file (
 If managing NuGet packages in Visual Studio, ensure 
Include prerelease is checked.
 .csproj) should contain the following 
PackageReference definitions:
 XML
<ItemGroup>
 <PackageReference Include="Azure.Identity" Version="<stable>" />
 <PackageReference Include="Microsoft.Extensions.Configuration" Version="
 <stable>" />
 <PackageReference Include="Microsoft.Extensions.Configuration.Binder" 
Version="<stable>" />
 <PackageReference Include="Microsoft.Extensions.Configuration.UserSecrets" 
Version="<stable>" />
 <PackageReference 
Include="Microsoft.Extensions.Configuration.EnvironmentVariables" Version="
 <stable>" />
 <PackageReference Include="Microsoft.SemanticKernel" Version="<latest>" />
 <PackageReference Include="Microsoft.SemanticKernel.Agents.OpenAI" Version="
 <latest>" />
 </ItemGroup>
 The 
Agent Framework is experimental and requires warning suppression. This may addressed in
 as a property in the project file (
 .csproj):
 XML
 <PropertyGroup>
 <NoWarn>$(NoWarn);CA2007;IDE1006;SKEXP0001;SKEXP0110;OPENAI001</NoWarn>
 </PropertyGroup>
 Additionally, copy the 
Grimms-The-King-of-the-Golden-Mountain.txt, 
Life.txt and 
Grimms-The-Water-of
Grimms-The-White-Snake.txt public domain content from Semantic Kernel
 LearnResources Project . Add these files in your project folder and configure to have them
 copied to the output directory:
 XML
 <ItemGroup>
 <None Include="Grimms-The-King-of-the-Golden-Mountain.txt">
 <CopyToOutputDirectory>Always</CopyToOutputDirectory>
 </None>
 <None Include="Grimms-The-Water-of-Life.txt">
 <CopyToOutputDirectory>Always</CopyToOutputDirectory>
 </None>
 <None Include="Grimms-The-White-Snake.txt">
 <CopyToOutputDirectory>Always</CopyToOutputDirectory>
 </None>
 </ItemGroup>
 Configuration
This sample requires configuration setting in order to connect to remote services. You will need
 to define settings for either OpenAI or Azure OpenAI.
 PowerShell
 The following class is used in all of the Agent examples. Be sure to include it in your project to
 ensure proper functionality. This class serves as a foundational component for the examples
 that follow.
 c#
 # OpenAI
 dotnet user-secrets set "OpenAISettings:ApiKey" "<api-key>"
 dotnet user-secrets set "OpenAISettings:ChatModel" "gpt-4o"
 # Azure OpenAI
 dotnet user-secrets set "AzureOpenAISettings:ApiKey" "<api-key>" # Not required if 
using token-credential
 dotnet user-secrets set "AzureOpenAISettings:Endpoint" "https://lightspeed-team
shared-openai-eastus.openai.azure.com/"
 dotnet user-secrets set "AzureOpenAISettings:ChatModelDeployment" "gpt-4o"
 using System.Reflection;
 using Microsoft.Extensions.Configuration;
 namespace AgentsSample;
 public class Settings
 {
    private readonly IConfigurationRoot configRoot;
    private AzureOpenAISettings azureOpenAI;
    private OpenAISettings openAI;
    public AzureOpenAISettings AzureOpenAI => this.azureOpenAI ??= 
this.GetSettings<Settings.AzureOpenAISettings>();
    public OpenAISettings OpenAI => this.openAI ??= 
this.GetSettings<Settings.OpenAISettings>();
    public class OpenAISettings
    {
        public string ChatModel { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
    public class AzureOpenAISettings
    {
        public string ChatModelDeployment { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
The coding process for this sample involves:
 1. Setup - Initializing settings and the plug-in.
 2. Agent Definition - Create the _Chat_CompletionAgent with templatized instructions and
 plug-in.
 3. The Chat Loop - Write the loop that drives user / agent interaction.
 The full example code is provided in the Final section. Refer to that section for the complete
 implementation.
 Prior to creating an OpenAIAssistantAgent, ensure the configuration settings are available and
 prepare the file resources.
 Instantiate the Settings class referenced in the previous Configuration section. Use the
 settings to also create an AzureOpenAIClient that will be used for the Agent Definition as well
 as file-upload and the creation of a VectorStore.
 C#
 Now create an empty _Vector Store for use with the File Search tool:
    public TSettings GetSettings<TSettings>() =>
        this.configRoot.GetRequiredSection(typeof(TSettings).Name).Get<TSettings>
 ()!;
    public Settings()
    {
        this.configRoot =
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
                .Build();
    }
 }
 Coding
 Setup
 Settings settings = new();
 AzureOpenAIClient client = OpenAIAssistantAgent.CreateAzureOpenAIClient(new 
AzureCliCredential(), new Uri(settings.AzureOpenAI.Endpoint));
Use the 
AzureOpenAIClient to access a 
VectorStoreClient and create a 
VectorStore.
 C#
 Console.WriteLine("Creating store...");
 VectorStoreClient storeClient = client.GetVectorStoreClient();
 CreateVectorStoreOperation operation = await 
storeClient.CreateVectorStoreAsync(waitUntilCompleted: true);
 string storeId = operation.VectorStoreId;
 Let's declare the the three content-files described in the previous Configuration section:
 C#
 private static readonly string[] _fileNames =
 [
 "Grimms-The-King-of-the-Golden-Mountain.txt",
 "Grimms-The-Water-of-Life.txt",
 "Grimms-The-White-Snake.txt",
 ];
 Store, preserving the resulting File References.
 C#
 Now upload those files and add them to the Vector Store by using the previously created
 VectorStoreClient clients to upload each file with a 
OpenAIFileClient and add it to the Vector
 Dictionary<string, OpenAIFile> fileReferences = [];
 Console.WriteLine("Uploading files...");
 OpenAIFileClient fileClient = client.GetOpenAIFileClient();
 foreach (string fileName in _fileNames)
 {
 OpenAIFile fileInfo = await fileClient.UploadFileAsync(fileName, 
FileUploadPurpose.Assistants);
 await storeClient.AddFileToVectorStoreAsync(storeId, fileInfo.Id, 
waitUntilCompleted: true);
 fileReferences.Add(fileInfo.Id, fileInfo);
 }
 Agent Definition
 We are now ready to instantiate an 
OpenAIAssistantAgent. The agent is configured with its
 target model, Instructions, and the File Search tool enabled. Additionally, we explicitly associate
 the Vector Store with the File Search tool.
We will utilize the AzureOpenAIClient again as part of creating the OpenAIAssistantAgent:
 C#
 At last, we are able to coordinate the interaction between the user and the Agent. Start by
 creating an AgentThread to maintain the conversation state and creating an empty loop.
 Let's also ensure the resources are removed at the end of execution to minimize unnecessary
 charges.
 C#
 Console.WriteLine("Defining assistant...");
 Assistant assistant =
    await assistantClient.CreateAssistantAsync(
        settings.AzureOpenAI.ChatModelDeployment,
        name: "SampleAssistantAgent",
        instructions:
                """
                The document store contains the text of fictional stories.
                Always analyze the document store to provide an answer to the 
user's question.
                Never rely on your knowledge of stories not included in the 
document store.
                Always format response using markdown.
                """,
        enableFileSearch: true,
        vectorStoreId: storeId);
 // Create agent
 OpenAIAssistantAgent agent = new(assistant, assistantClient);
 The Chat Loop
 Console.WriteLine("Creating thread...");
 OpenAIAssistantAgent agentThread = new();
 Console.WriteLine("Ready!");
 try
 {
    bool isComplete = false;
    do
    {
        // Processing occurs here
    } while (!isComplete);
 }
 finally
 {
    Console.WriteLine();
Now let's capture user input within the previous loop. In this case, empty input will be ignored
 and the term EXIT will signal that the conversation is completed.
 C#
 Before invoking the Agent response, let's add a helper method to reformat the unicode
 annotation brackets to ANSI brackets.
 C#
 To generate an Agent response to user input, invoke the agent by specifying the message and
 agent thread. In this example, we choose a streamed response and capture any associated
 Citation Annotations for display at the end of the response cycle. Note each streamed chunk is
 being reformatted using the previous helper method.
 C#
    Console.WriteLine("Cleaning-up...");
    await Task.WhenAll(
        [
            agentThread.DeleteAsync();
            assistantClient.DeleteAssistantAsync(assistant.Id),
            storeClient.DeleteVectorStoreAsync(storeId),
            ..fileReferences.Select(fileReference => 
fileClient.DeleteFileAsync(fileReference.Key))
        ]);
 }
 Console.WriteLine();
 Console.Write("> ");
 string input = Console.ReadLine();
 if (string.IsNullOrWhiteSpace(input))
 {
    continue;
 }
 if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
 {
    isComplete = true;
    break;
 }
 var message = new ChatMessageContent(AuthorRole.User, input);
 Console.WriteLine();
 private static string ReplaceUnicodeBrackets(this string content) =>
    content?.Replace('【', '[').Replace('】', ']');
Bringing all the steps together, we have the final code for this example. The complete
 implementation is provided below.
 Try using these suggested inputs:
 1. What is the paragraph count for each of the stories?
 2. Create a table that identifies the protagonist and antagonist for each story.
 3. What is the moral in The White Snake?
 C#
 List<StreamingAnnotationContent> footnotes = [];
 await foreach (StreamingChatMessageContent chunk in 
agent.InvokeStreamingAsync(message, agentThread))
 {
    // Capture annotations for footnotes
    footnotes.AddRange(chunk.Items.OfType<StreamingAnnotationContent>());
    // Render chunk with replacements for unicode brackets.
    Console.Write(chunk.Content.ReplaceUnicodeBrackets());
 }
 Console.WriteLine();
 // Render footnotes for captured annotations.
 if (footnotes.Count > 0)
 {
    Console.WriteLine();
    foreach (StreamingAnnotationContent footnote in footnotes)
    {
        Console.WriteLine($"#{footnote.Quote.ReplaceUnicodeBrackets()} - 
{fileReferences[footnote.FileId!].Filename} (Index: {footnote.StartIndex} - 
{footnote.EndIndex})");
    }
 }
 Final
 using Azure.AI.OpenAI;
 using Azure.Identity;
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.Agents;
 using Microsoft.SemanticKernel.Agents.OpenAI;
 using Microsoft.SemanticKernel.ChatCompletion;
 using OpenAI.Assistants;
 using OpenAI.Files;
 using OpenAI.VectorStores;
 using System;
 using System.Collections.Generic;
using System.Linq;
 using System.Threading.Tasks;
 namespace AgentsSample;
 public static class Program
 {
 private static readonly string[] _fileNames =
 [
 "Grimms-The-King-of-the-Golden-Mountain.txt",
 "Grimms-The-Water-of-Life.txt",
 "Grimms-The-White-Snake.txt",
 ];
 /// <summary>
 /// The main entry point for the application.
 /// </summary>
 /// <returns>A <see cref="Task"/> representing the asynchronous operation.
 </returns>
 public static async Task Main()
 {
 // Load configuration from environment variables or user secrets.
 Settings settings = new();
 // Initialize the clients
 AzureOpenAIClient client = 
OpenAIAssistantAgent.CreateAzureOpenAIClient(new AzureCliCredential(), new 
Uri(settings.AzureOpenAI.Endpoint));
 //OpenAIClient client = OpenAIAssistantAgent.CreateOpenAIClient(new 
ApiKeyCredential(settings.OpenAI.ApiKey)));
 AssistantClient assistantClient = client.GetAssistantClient();
 OpenAIFileClient fileClient = client.GetOpenAIFileClient();
 VectorStoreClient storeClient = client.GetVectorStoreClient();
 // Create the vector store
 Console.WriteLine("Creating store...");
 CreateVectorStoreOperation operation = await 
storeClient.CreateVectorStoreAsync(waitUntilCompleted: true);
 string storeId = operation.VectorStoreId;
 // Upload files and retain file references.
 Console.WriteLine("Uploading files...");
 Dictionary<string, OpenAIFile> fileReferences = [];
 foreach (string fileName in _fileNames)
 {
 OpenAIFile fileInfo = await fileClient.UploadFileAsync(fileName, 
FileUploadPurpose.Assistants);
 await storeClient.AddFileToVectorStoreAsync(storeId, fileInfo.Id, 
waitUntilCompleted: true);
 fileReferences.Add(fileInfo.Id, fileInfo);
 }
 // Define assistant
 Console.WriteLine("Defining assistant...");
 Assistant assistant =
await assistantClient.CreateAssistantAsync(
 settings.AzureOpenAI.ChatModelDeployment,
 name: "SampleAssistantAgent",
 instructions:
 """
 The document store contains the text of fictional stories.
 Always analyze the document store to provide an answer to 
the user's question.
 Never rely on your knowledge of stories not included in 
the document store.
 Always format response using markdown.
 """,
 enableFileSearch: true,
 vectorStoreId: storeId);
 // Create agent
 OpenAIAssistantAgent agent = new(assistant, assistantClient);
 // Create the conversation thread
 Console.WriteLine("Creating thread...");
 AssistantAgentThread agentThread = new();
 Console.WriteLine("Ready!");
 try
 {
 bool isComplete = false;
 do
 {
 Console.WriteLine();
 Console.Write("> ");
 string input = Console.ReadLine();
 if (string.IsNullOrWhiteSpace(input))
 {
 continue;
 }
 if (input.Trim().Equals("EXIT", 
StringComparison.OrdinalIgnoreCase))
 {
 isComplete = true;
 break;
 }
 var message = new ChatMessageContent(AuthorRole.User, input);
 Console.WriteLine();
 List<StreamingAnnotationContent> footnotes = [];
 await foreach (StreamingChatMessageContent chunk in 
agent.InvokeStreamingAsync(message, agentThread))
 {
 // Capture annotations for footnotes
 footnotes.AddRange(chunk.Items.OfType<StreamingAnnotationContent>());
 // Render chunk with replacements for unicode brackets.
                    Console.Write(chunk.Content.ReplaceUnicodeBrackets());
                }
                Console.WriteLine();
                // Render footnotes for captured annotations.
                if (footnotes.Count > 0)
                {
                    Console.WriteLine();
                    foreach (StreamingAnnotationContent footnote in footnotes)
                    {
                        Console.WriteLine($"#
 {footnote.Quote.ReplaceUnicodeBrackets()} - 
{fileReferences[footnote.FileId!].Filename} (Index: {footnote.StartIndex} - 
{footnote.EndIndex})");
                    }
                }
            } while (!isComplete);
        }
        finally
        {
            Console.WriteLine();
            Console.WriteLine("Cleaning-up...");
            await Task.WhenAll(
                [
                    agentThread.DeleteAsync(),
                    assistantClient.DeleteAssistantAsync(assistant.Id),
                    storeClient.DeleteVectorStoreAsync(storeId),
                    ..fileReferences.Select(fileReference => 
fileClient.DeleteFileAsync(fileReference.Key))
                ]);
        }
    }
    private static string ReplaceUnicodeBrackets(this string content) =>
        content?.Replace('【', '[').Replace('】', ']');
 }
 How to Coordinate Agent Collaboration usingAgentGroupChat
Overview of the Process Framework
 Article • 11/08/2024
 Welcome to the Process Framework within Microsoft's Semantic Kernel—a cutting-edge
 approach designed to optimize AI integration with your business processes. This
 framework empowers developers to efficiently create, manage, and deploy business
 processes while leveraging the powerful capabilities of AI, alongside your existing code
 and systems.
 A Process is a structured sequence of activities or tasks that deliver a service or product,
 adding value in alignment with specific business goals for customers.
７ Note
 Process Framework package is currently experimental and is subject to change until
 it is moved to preview and GA.
 Introduction to the Process Framework
 The Process Framework provides a robust solution for automating complex workflows.
 Each step within the framework performs tasks by invoking user-defined Kernel
 Functions, utilizing an event-driven model to manage workflow execution.
 By embedding AI into your business processes, you can significantly enhance
 productivity and decision-making capabilities. With the Process Framework, you benefit
 from seamless AI integration, facilitating smarter and more responsive workflows. This
 framework streamlines operations, fosters improved collaboration between business
 units, and boosts overall efficiency.
 Key Features
 Leverage Semantic Kernel: Steps can utilize one or multiple Kernel Functions,
 enabling you to tap into all aspects of Semantic Kernel within your processes.
 Reusability & Flexibility: Steps and processes can be reused across different
 applications, promoting modularity and scalability.
 Event-Driven Architecture: Utilize events and metadata to trigger actions and
 transitions between process steps effectively.
 Full Control and Auditability: Maintain control of processes in a defined and
 repeatable manner, complete with audit capabilities through Open Telemetry.
Core Concepts
 Process: A collection of steps arranged to achieve a specific business goal for
 customers.
 Step: An activity within a process that has defined inputs and outputs, contributing
 to a larger goal.
 Pattern: The specific sequence type that dictates how steps are executed for the
 process to be fully completed.
 Business Process Examples
 Business processes are a part of our daily routines. Here are three examples you might
 have encountered this week:
 1. Account Opening: This process includes multiple steps such as credit pulls and
 ratings, fraud detection, creating customer accounts in core systems, and sending
 welcome information to the customer, including their customer ID.
 2. Food Delivery: Ordering food for delivery is a familiar process. From receiving the
 order via phone, website, or app, to preparing each food item, ensuring quality
 control, driver assignment, and final delivery, there are many steps in this process
 that can be streamlined.
 3. Support Ticket: We have all submitted support tickets—whether for new services,
 IT support, or other needs. This process can involve multiple subprocesses based
 on business and customer requirements, ultimately aiming for satisfaction by
 addressing customer needs effectively.
 Getting Started
 Are you ready to harness the power of the Process Framework?
 Begin your journey by exploring our .NET samples and Python Python samples on
 GitHub.
 By diving into the Process Framework, developers can transform traditional workflows
 into intelligent, adaptive systems. Start building with the tools at your disposal and
 redefine what's possible with AI-driven business processes.
Core Components of the Process
 Framework
 Article • 09/28/2024
 The Process Framework is built upon a modular architecture that enables developers to
 construct sophisticated workflows through its core components. Understanding these
 components is essential for effectively leveraging the framework.
 Process
 A Process serves as the overarching container that orchestrates the execution of Steps. It
 defines the flow and routing of data between Steps, ensuring that process goals are
 achieved efficiently. Processes handle inputs and outputs, providing flexibility and
 scalability across various workflows.
 Process Features
 Stateful: Supports querying information such as tracking status and percent
 completion, as well as the ability to pause and resume.
 Reusable: A Process can be invoked within other processes, promoting modularity
 and reusability.
 Event Driven: Employs event-based flow with listeners to route data to Steps and
 other Processes.
 Scalable: Utilizes well-established runtimes for global scalability and rollouts.
 Cloud Event Integrated: Incorporates industry-standard eventing for triggering a
 Process or Step.
 Creating A Process
 To create a new Process, add the Process Package to your project and define a name for
 your process.
 Step
 Steps are the fundamental building blocks within a Process. Each Step corresponds to a
 discrete unit of work and encapsulates one or more Kernel Functions. Steps can be
 created independently of their use in specific Processes, enhancing their reusability.
 They emit events based on the work performed, which can trigger subsequent Steps.
Step Features
 Stateful: Facilitates tracking information such as status and defined tags.
 Reusable: Steps can be employed across multiple Processes.
 Dynamic: Steps can be created dynamically by a Process as needed, depending on
 the required pattern.
 Flexible: Offers different types of Steps for developers by leveraging Kernel
 Functions, including Code-only, API calls, AI Agents, and Human-in-the-loop.
 Auditable: Telemetry is enabled across both Steps and Processes.
 Defining a Step
 To create a Step, define a public class to name the Step and add it to the
 KernelStepBase. Within your class, you can incorporate one or multiple Kernel Functions.
 Register a Step into a Process
 Once your class is created, you need to register it within your Process. For the first Step
 in the Process, add 
isEntryPoint: true so the Process knows where to start.
 Step Events
 Steps have several events available, including:
 OnEvent: Triggered when the class completes its execution.
 OnFunctionResult: Activated when the defined Kernel Function emits results,
 allowing output to be sent to one or many Steps.
 SendOutputTo: Defines the Step and Input for sending results to a subsequent
 Step.
 Pattern
 Patterns standardize common process flows, simplifying the implementation of
 frequently used operations. They promote a consistent approach to solving recurring
 problems across various implementations, enhancing both maintainability and
 readability.
 Pattern Types
Fan In: The input for the next Step is supported by multiple outputs from previous
 Steps.
 Fan Out: The output of previous Steps is directed into multiple Steps further down
 the Process.
 Cycle: Steps continue to loop until completion based on input and output.
 Map Reduce: Outputs from a Step are consolidated into a smaller amount and
 directed to the next Step's input.
 Setting up a Pattern
 Once your class is created for your Step and registered within the Process, you can
 define the events that should be sent downstream to other Steps or set conditions for
 Steps to be restarted based on the output from your Step
 Deployment of the Process Framework
 Article • 11/08/2024
 Deploying workflows built with the Process Framework can be done seamlessly across
 local development environments and cloud runtimes. This flexibility enables developers
 to choose the best approach tailored to their specific use cases.
 Local Development
 The Process Framework provides an in-process runtime that allows developers to run
 processes directly on their local machines or servers without requiring complex setups
 or additional infrastructure. This runtime supports both memory and file-based
 persistence, ideal for rapid development and debugging. You can quickly test processes
 with immediate feedback, accelerating the development cycle and enhancing efficiency.
 Cloud Runtimes
 For scenarios requiring scalability and distributed processing, the Process Framework
 supports cloud runtimes such as Orleans and Dapr . These options empower
 developers to deploy processes in a distributed manner, facilitating high availability and
 load balancing across multiple instances. By leveraging these cloud runtimes,
 organizations can streamline their operations and manage substantial workloads with
 ease.
 Orleans Runtime: This framework provides a programming model for building
 distributed applications and is particularly well-suited for handling virtual actors in
 a resilient manner, complementing the Process Framework’s event-driven
 architecture.
 Dapr (Distributed Application Runtime): Dapr simplifies microservices
 development by providing a foundational framework for building distributed
 systems. It supports state management, service invocation, and pub/sub
 messaging, making it easier to connect various components within a cloud
 environment.
 Using either runtime, developers can scale applications according to demand, ensuring
 that processes run smoothly and efficiently, regardless of workload.
 With the flexibility to choose between local testing environments and robust cloud
 platforms, the Process Framework is designed to meet diverse deployment needs. This
enables developers to concentrate on building innovative AI-powered processes without
 the burden of infrastructure complexities.
７ Note
 Dapr will be supported first with the Process Framework, followed by Orleans in an
 upcoming release of the Process Framework.
Best Practices for the Process Framework
 06/11/2025
 Utilizing the Process Framework effectively can significantly enhance your workflow
 automation. Here are some best practices to help you optimize your implementation and avoid
 common pitfalls.
 File and Folder Layout Structure
 Organizing your project files in a logical and maintainable structure is crucial for collaboration
 and scalability. A recommended file layout may include:
 Processes/: A directory for all defined processes.
 Steps/: A dedicated directory for reusable Steps.
 Functions/: A folder containing your Kernel Function definitions.
 An organized structure not only simplifies navigation within the project but also enhances code
 reusability and facilitates collaboration among team members.
 Kernel Instance Isolation
） Important
 Do not share a single Kernel instance between the main Process Framework and any of its
 dependencies (such as agents, tools, or external services).
 Sharing a Kernel across these components can result in unexpected recursive invocation
 patterns, including infinite loops, as functions registered in the Kernel may inadvertently invoke
 each other. For example, a Step may call a function that triggers an agent, which then re
invokes the same function, creating a non-terminating loop.
 To avoid this, instantiate separate Kernel objects for each independent agent, tool, or service
 used within your process. This ensures isolation between the Process Framework’s own
 functions and those required by dependencies, and prevents cross-invocation that could
 destabilize your workflow. This requirement reflects a current architectural constraint and may
 be revisited as the framework evolves.
 Common Pitfalls
To ensure smooth implementation and operation of the Process Framework, be mindful of
 these common pitfalls to avoid:
 Overcomplicating Steps: Keep Steps focused on a single responsibility. Avoid creating
 complex Steps that perform multiple tasks, as this can complicate debugging and
 maintenance.
 Ignoring Event Handling: Events are vital for smooth communication between Steps.
 Ensure that you handle all potential events and errors within the process to prevent
 unexpected behavior or crashes.
 Performance and Quality: As processes scale, it’s crucial to continuously monitor
 performance. Leverage telemetry from your Steps to gain insights into how Processes are
 functioning.
 By following these best practices, you can maximize the effectiveness of the Process
 Framework, enabling more robust and manageable workflows. Keeping organization, simplicity,
 and performance in mind will lead to a smoother development experience and higher-quality
 applications.
How-To: Create your first Process
 Article • 02/25/2025
２ Warning
 The Semantic Kernel Process Framework is experimental, still in development and is
 subject to change.
 Overview
 The Semantic Kernel Process Framework is a powerful orchestration SDK designed to
 simplify the development and execution of AI-integrated processes. Whether you are
 managing simple workflows or complex systems, this framework allows you to define a
 series of steps that can be executed in a structured manner, enhancing your
 application's capabilities with ease and flexibility.
 Built for extensibility, the Process Framework supports diverse operational patterns such
 as sequential execution, parallel processing, fan-in and fan-out configurations, and even
 map-reduce strategies. This adaptability makes it suitable for a variety of real-world
 applications, particularly those that require intelligent decision-making and multi-step
 workflows.
 Getting Started
 The Sematic Kernel Process Framework can be used to infuse AI into just about any
 business process you can think of. As an illustrative example to get started, let's look at
 building a process for generating documentation for a new product.
 Before we get started, make sure you have the required Semantic Kernel packages
 installed:
 .NET CLI
 dotnet add package Microsoft.SemanticKernel.Process.LocalRuntime --version 
1.33.0-alpha
 Illustrative Example: Generating
 Documentation for a New Product
In this example, we will utilize the Semantic Kernel Process Framework to develop an
 automated process for creating documentation for a new product. This process will start
 out simple and evolve as we go to cover more realistic scenarios.
 We will start by modeling the documentation process with a very basic flow:
 1. Gather information about the product.
 2. Ask an LLM to generate documentation from the information gathered in step 1.
 3. Publish the documentation.
 Now that we understand our processes, let's build it.
 Each step of a Process is defined by a class that inherits from our base step class. For
 this process we have three steps:
 C#
 Define the process steps
 using Microsoft.SemanticKernel.ChatCompletion;
 using Microsoft.SemanticKernel;
 // A process step to gather information about a product
 public class GatherProductInfoStep: KernelProcessStep
 {
    [KernelFunction]
    public string GatherProductInformation(string productName)
    {
        Console.WriteLine($"{nameof(GatherProductInfoStep)}:\n\tGathering 
product information for product named {productName}");
        // For example purposes we just return some fictional information.
        return
            """
            Product Description:
            GlowBrew is a revolutionary AI driven coffee machine with 
industry leading number of LEDs and programmable light shows. The machine is 
also capable of brewing coffee and has a built in grinder.
            Product Features:
            1. **Luminous Brew Technology**: Customize your morning ambiance 
with programmable LED lights that sync with your brewing process.
            2. **AI Taste Assistant**: Learns your taste preferences over 
time and suggests new brew combinations to explore.
            3. **Gourmet Aroma Diffusion**: Built-in aroma diffusers enhance 
your coffee's scent profile, energizing your senses before the first sip.
Troubleshooting:- **Issue**: LED Lights Malfunctioning- **Solution**: Reset the lighting settings via the app. 
Ensure the LED connections inside the GlowBrew are secure. Perform a factory 
reset if necessary.
 """;
 }
 }
 // A process step to generate documentation for a product
 public class GenerateDocumentationStep : 
KernelProcessStep<GeneratedDocumentationState>
 {
 private GeneratedDocumentationState _state = new();
 private string systemPrompt =
 """
 Your job is to write high quality and engaging customer facing 
documentation for a new product from Contoso. You will be provide with 
information
 about the product in the form of internal documentation, specs, 
and troubleshooting guides and you must use this information and
 nothing else to generate the documentation. If suggestions are 
provided on the documentation you create, take the suggestions into account 
and
 rewrite the documentation. Make sure the product sounds amazing.
 """;
 // Called by the process runtime when the step instance is activated. 
Use this to load state that may be persisted from previous activations.
 override public ValueTask 
ActivateAsync(KernelProcessStepState<GeneratedDocumentationState> state)
 {
 this._state = state.State!;
 this._state.ChatHistory ??= new ChatHistory(systemPrompt);
 return base.ActivateAsync(state);
 }
 [KernelFunction]
 public async Task GenerateDocumentationAsync(Kernel kernel, 
KernelProcessStepContext context, string productInfo)
 {
 Console.WriteLine($"
 {nameof(GenerateDocumentationStep)}:\n\tGenerating documentation for 
provided productInfo...");
 // Add the new product info to the chat history
 this._state.ChatHistory!.AddUserMessage($"Product 
Info:\n\n{productInfo}");
 // Get a response from the LLM
 IChatCompletionService chatCompletionService = 
kernel.GetRequiredService<IChatCompletionService>();
The code above defines the three steps we need for our Process. There are a few points
 to call out here:
 In Semantic Kernel, a KernelFunction defines a block of code that is invocable by
 native code or by an LLM. In the case of the Process framework, KernelFunctions
 are the invocable members of a Step and each step requires at least one
 KernelFunction to be defined.
 The Process Framework has support for stateless and stateful steps. Stateful steps
 automatically checkpoint their progress and maintain state over multiple
 invocations. The GenerateDocumentationStep provides an example of this where the
 GeneratedDocumentationState class is used to persist the ChatHistory object.
 Steps can manually emit events by calling EmitEventAsync on the
 KernelProcessStepContext object. To get an instance of KernelProcessStepContext
 just add it as a parameter on your KernelFunction and the framework will
 automatically inject it.
 C#
        var generatedDocumentationResponse = await 
chatCompletionService.GetChatMessageContentAsync(this._state.ChatHistory!);
        await context.EmitEventAsync("DocumentationGenerated", 
generatedDocumentationResponse.Content!.ToString());
    }
    public class GeneratedDocumentationState
    {
        public ChatHistory? ChatHistory { get; set; }
    }
 }
 // A process step to publish documentation
 public class PublishDocumentationStep : KernelProcessStep
 {
    [KernelFunction]
    public void PublishDocumentation(string docs)
    {
        // For example purposes we just write the generated docs to the 
console
        Console.WriteLine($"
 {nameof(PublishDocumentationStep)}:\n\tPublishing product 
documentation:\n\n{docs}");
    }
 }
 Define the process flow
There are a few things going on here so let's break it down step by step.
 1. Create the builder: Processes use a builder pattern to simplify wiring everything up.
 The builder provides methods for managing the steps within a process and for
 managing the lifecycle of the process.
 2. Add the steps: Steps are added to the process by calling the AddStepFromType
 method of the builder. This allows the Process Framework to manage the lifecycle
 of steps by instantiating instances as needed. In this case we've added three steps
 to the process and created a variable for each one. These variables give us a
 handle to the unique instance of each step that we can use next to define the
 orchestration of events.
 3. Orchestrate the events: This is where the routing of events from step to step are
 defined. In this case we have the following routes:
 When an external event with id = Start is sent to the process, this event and
 its associated data will be sent to the infoGatheringStep step.
 When the infoGatheringStep finishes running, send the returned object to
 the docsGenerationStep step.
 Finally, when the docsGenerationStep finishes running, send the returned
 object to the docsPublishStep step.
 // Create the process builder
 ProcessBuilder processBuilder = new("DocumentationGeneration");
 // Add the steps
 var infoGatheringStep = 
processBuilder.AddStepFromType<GatherProductInfoStep>();
 var docsGenerationStep = 
processBuilder.AddStepFromType<GenerateDocumentationStep>();
 var docsPublishStep = 
processBuilder.AddStepFromType<PublishDocumentationStep>();
 // Orchestrate the events
 processBuilder
    .OnInputEvent("Start")
    .SendEventTo(new(infoGatheringStep));
 infoGatheringStep
    .OnFunctionResult()
    .SendEventTo(new(docsGenerationStep));
 docsGenerationStep
    .OnFunctionResult()
    .SendEventTo(new(docsPublishStep));
C#
 We build the process and call StartAsync to run it. Our process is expecting an initial
 external event called Start to kick things off and so we provide that as well. Running
 this process shows the following output in the Console:
  Tip
 Event Routing in Process Framework: You may be wondering how events that are
 sent to steps are routed to KernelFunctions within the step. In the code above, each
 step has only defined a single KernelFunction and each KernelFunction has only a
 single parameter (other than Kernel and the step context which are special, more
 on that later). When the event containing the generated documentation is sent to
 the docsPublishStep it will be passed to the docs parameter of the
 PublishDocumentation KernelFunction of the docsGenerationStep step because
 there is no other choice. However, steps can have multiple KernelFunctions and
 KernelFunctions can have multiple parameters, in these advanced scenarios you
 need to specify the target function and parameter.
 Build and run the Process
 // Configure the kernel with your LLM connection details
 Kernel kernel = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion("myDeployment", "myEndpoint", "myApiKey")
    .Build();
 // Build and run the process
 var process = processBuilder.Build();
 await process.StartAsync(kernel, new KernelProcessEvent { Id = "Start", Data 
= "Contoso GlowBrew" });
 GatherProductInfoStep: Gathering product information for product named 
Contoso GlowBrew
 GenerateDocumentationStep: Generating documentation for provided productInfo
 PublishDocumentationStep: Publishing product documentation:
 # GlowBrew: Your Ultimate Coffee Experience Awaits!
 Welcome to the world of GlowBrew, where coffee brewing meets remarkable 
technology! At Contoso, we believe that your morning ritual shouldn't just 
include the perfect cup of coffee but also a stunning visual experience that 
invigorates your senses. Our revolutionary AI-driven coffee machine is 
designed to transform your kitchen routine into a delightful ceremony.
## Unleash the Power of GlowBrew
 ### Key Features- **Luminous Brew Technology**- Elevate your coffee experience with our cutting-edge programmable LED 
lighting. GlowBrew allows you to customize your morning ambiance, creating a 
symphony of colors that sync seamlessly with your brewing process. Whether 
you need a vibrant wake-up call or a soothing glow, you can set the mood for 
any moment!- **AI Taste Assistant**- Your taste buds deserve the best! With the GlowBrew built-in AI taste 
assistant, the machine learns your unique preferences over time and curates 
personalized brew suggestions just for you. Expand your coffee horizons and 
explore delightful new combinations that fit your palate perfectly.- **Gourmet Aroma Diffusion**- Awaken your senses even before that first sip! The GlowBrew comes 
equipped with gourmet aroma diffusers that enhance the scent profile of your 
coffee, diffusing rich aromas that fill your kitchen with the warm, inviting 
essence of freshly-brewed bliss.
 ### Not Just Coffee - An Experience
 With GlowBrew, it's more than just making coffee-it's about creating an 
experience that invigorates the mind and pleases the senses. The glow of the 
lights, the aroma wafting through your space, and the exceptional taste meld 
into a delightful ritual that prepares you for whatever lies ahead.
 ## Troubleshooting Made Easy
 While GlowBrew is designed to provide a seamless experience, we understand 
that technology can sometimes be tricky. If you encounter issues with the 
LED lights, we've got you covered:- **LED Lights Malfunctioning?**- If your LED lights aren't working as expected, don't worry! Follow these 
steps to restore the glow:
 1. **Reset the Lighting Settings**: Use the GlowBrew app to reset the 
lighting settings.
 2. **Check Connections**: Ensure that the LED connections inside the 
GlowBrew are secure.
 3. **Factory Reset**: If you're still facing issues, perform a factory 
reset to rejuvenate your machine.
 With GlowBrew, you not only brew the perfect coffee but do so with an 
ambiance that excites the senses. Your mornings will never be the same!
 ## Embrace the Future of Coffee
 Join the growing community of GlowBrew enthusiasts today, and redefine how 
you experience coffee. With stunning visual effects, customized brewing 
suggestions, and aromatic enhancements, it's time to indulge in the 
delightful world of GlowBrew-where every cup is an adventure!
 ### Conclusion
 Ready to embark on an extraordinary coffee journey? Discover the perfect 
blend of technology and flavor with Contoso's GlowBrew. Your coffee awaits!
 What's Next?
 Our first draft of the documentation generation process is working but it leaves a lot to
 be desired. At a minimum, a production version would need:
 A proof reader agent that will grade the generated documentation and verify that
 it meets our standards of quality and accuracy.
 An approval process where the documentation is only published after a human
 approves it (human-in-the-loop).
 Add a proof reader agent to our process...
How-To: Using Cycles
 Article • 02/25/2025
２ Warning
 The Semantic Kernel Process Framework is experimental, still in development and is
 subject to change.
 Overview
 In the previous section we built a simple Process to help us automate the creation of
 documentation for our new product. In this section we will improve on that process by
 adding a proofreading step. This step will use and LLM to grade the generated
 documentation as Pass/Fail, and provide recommended changes if needed. By taking
 advantage of the Process Frameworks' support for cycles, we can go one step further
 and automatically apply the recommended changes (if any) and then start the cycle
 over, repeating this until the content meets our quality bar. The updated process will
 look like this:
 Updates to the process
 We need to create our new proofreader step and also make a couple changes to our
 document generation step that will allow us to apply suggestions if needed.
 Add the proofreader step
 // A process step to proofread documentation
 public class ProofreadStep : KernelProcessStep
 {
 [KernelFunction]
 public async Task ProofreadDocumentationAsync(Kernel kernel, 
KernelProcessStepContext context, string documentation)
 {
 Console.WriteLine($"
 {nameof(ProofreadDocumentationAsync)}:\n\tProofreading documentation...");
 var systemPrompt =
 """
Your job is to proofread customer facing documentation for a new 
product from Contoso. You will be provide with proposed documentation
 for a product and you must do the following things:
 1. Determine if the documentation is passes the following criteria:
 1. Documentation must use a professional tone.
 1. Documentation should be free of spelling or grammar mistakes.
 1. Documentation should be free of any offensive or 
inappropriate language.
 1. Documentation should be technically accurate.
 2. If the documentation does not pass 1, you must write detailed 
feedback of the changes that are needed to improve the documentation. 
""";
 ChatHistory chatHistory = new ChatHistory(systemPrompt);
 chatHistory.AddUserMessage(documentation);
 // Use structured output to ensure the response format is easily 
parsable
 OpenAIPromptExecutionSettings settings = new 
OpenAIPromptExecutionSettings();
 settings.ResponseFormat = typeof(ProofreadingResponse);
 IChatCompletionService chatCompletionService = 
kernel.GetRequiredService<IChatCompletionService>();
 var proofreadResponse = await 
chatCompletionService.GetChatMessageContentAsync(chatHistory, 
executionSettings: settings);
 var formattedResponse = 
JsonSerializer.Deserialize<ProofreadingResponse>
 (proofreadResponse.Content!.ToString());
 Console.WriteLine($"\n\tGrade: 
{(formattedResponse!.MeetsExpectations ? "Pass" : "Fail")}\n\tExplanation: 
{formattedResponse.Explanation}\n\tSuggestions: {string.Join("\n\t\t", 
formattedResponse.Suggestions)}");
 if (formattedResponse.MeetsExpectations)
 {
 await context.EmitEventAsync("DocumentationApproved", data: 
documentation);
 }
 else
 {
 await context.EmitEventAsync("DocumentationRejected", data: new 
{ Explanation = formattedResponse.Explanation, Suggestions = 
formattedResponse.Suggestions});
 }
 }
 // A class 
private class ProofreadingResponse
 {
 [Description("Specifies if the proposed documentation meets the 
expected standards for publishing.")]
A new step named ProofreadStep has been created. This step uses the LLM to grade the
 generated documentation as discussed above. Notice that this step conditionally emits
 either the DocumentationApproved event or the DocumentationRejected event based on
 the response from the LLM. In the case of DocumentationApproved, the event will include
 the approved documentation as it's payload and in the case of DocumentationRejected it
 will include the suggestions from the proofreader.
        public bool MeetsExpectations { get; set; }
        [Description("An explanation of why the documentation does or does 
not meet expectations.")]
        public string Explanation { get; set; } = "";
        [Description("A lis of suggestions, may be empty if there no 
suggestions for improvement.")]
        public List<string> Suggestions { get; set; } = new();
    }
 }
 Update the documentation generation step
 // Updated process step to generate and edit documentation for a product
 public class GenerateDocumentationStep : 
KernelProcessStep<GeneratedDocumentationState>
 {
    private GeneratedDocumentationState _state = new();
    private string systemPrompt =
            """
            Your job is to write high quality and engaging customer facing 
documentation for a new product from Contoso. You will be provide with 
information
            about the product in the form of internal documentation, specs, 
and troubleshooting guides and you must use this information and
            nothing else to generate the documentation. If suggestions are 
provided on the documentation you create, take the suggestions into account 
and
            rewrite the documentation. Make sure the product sounds amazing.
            """;
    override public ValueTask 
ActivateAsync(KernelProcessStepState<GeneratedDocumentationState> state)
    {
        this._state = state.State!;
        this._state.ChatHistory ??= new ChatHistory(systemPrompt);
        return base.ActivateAsync(state);
    }
The GenerateDocumentationStep has been updated to include a new KernelFunction. The
 new function will be used to apply suggested changes to the documentation if our
 proofreading step requires them. Notice that both functions for generating or rewriting
    [KernelFunction]
    public async Task GenerateDocumentationAsync(Kernel kernel, 
KernelProcessStepContext context, string productInfo)
    {
        Console.WriteLine($"
 {nameof(GenerateDocumentationStep)}:\n\tGenerating documentation for 
provided productInfo...");
        // Add the new product info to the chat history
        this._state.ChatHistory!.AddUserMessage($"Product 
Info:\n\n{productInfo}");
        // Get a response from the LLM
        IChatCompletionService chatCompletionService = 
kernel.GetRequiredService<IChatCompletionService>();
        var generatedDocumentationResponse = await 
chatCompletionService.GetChatMessageContentAsync(this._state.ChatHistory!);
        await context.EmitEventAsync("DocumentationGenerated", 
generatedDocumentationResponse.Content!.ToString());
    }
    [KernelFunction]
    public async Task ApplySuggestionsAsync(Kernel kernel, 
KernelProcessStepContext context, string suggestions)
    {
        Console.WriteLine($"
 {nameof(GenerateDocumentationStep)}:\n\tRewriting documentation with 
provided suggestions...");
        // Add the new product info to the chat history
        this._state.ChatHistory!.AddUserMessage($"Rewrite the documentation 
with the following suggestions:\n\n{suggestions}");
        // Get a response from the LLM
        IChatCompletionService chatCompletionService = 
kernel.GetRequiredService<IChatCompletionService>();
        var generatedDocumentationResponse = await 
chatCompletionService.GetChatMessageContentAsync(this._state.ChatHistory!);
        await context.EmitEventAsync("DocumentationGenerated", 
generatedDocumentationResponse.Content!.ToString());
    }
    public class GeneratedDocumentationState
    {
        public ChatHistory? ChatHistory { get; set; }
    }
 }
documentation emit the same event named DocumentationGenerated indicating that new
 documentation is available.
 Our updated process routing now does the following:
 When an external event with id = Start is sent to the process, this event and its
 associated data will be sent to the infoGatheringStep.
 When the infoGatheringStep finishes running, send the returned object to the
 docsGenerationStep.
 Flow updates
 // Create the process builder
 ProcessBuilder processBuilder = new("DocumentationGeneration");
 // Add the steps
 var infoGatheringStep = 
processBuilder.AddStepFromType<GatherProductInfoStep>();
 var docsGenerationStep = 
processBuilder.AddStepFromType<GenerateDocumentationStepV2>();
 var docsProofreadStep = processBuilder.AddStepFromType<ProofreadStep>(); // 
Add new step here
 var docsPublishStep = 
processBuilder.AddStepFromType<PublishDocumentationStep>();
 // Orchestrate the events
 processBuilder
    .OnInputEvent("Start")
    .SendEventTo(new(infoGatheringStep));
 infoGatheringStep
    .OnFunctionResult()
    .SendEventTo(new(docsGenerationStep, functionName: 
"GenerateDocumentation"));
 docsGenerationStep
    .OnEvent("DocumentationGenerated")
    .SendEventTo(new(docsProofreadStep));
 docsProofreadStep
    .OnEvent("DocumentationRejected")
    .SendEventTo(new(docsGenerationStep, functionName: "ApplySuggestions"));
 docsProofreadStep
    .OnEvent("DocumentationApproved")
    .SendEventTo(new(docsPublishStep));
 var process = processBuilder.Build();
 return process;
When the 
docsGenerationStep finishes running, send the generated docs to the
 docsProofreadStep.
 When the 
docsProofreadStep rejects our documentation and provides suggestions,
 send the suggestions back to the 
docsGenerationStep.
 Finally, when the 
docsProofreadStep approves our documentation, send the
 returned object to the 
docsPublishStep.
 Build and run the Process
 Running our updated process shows the following output in the console:
 GatherProductInfoStep:
 Gathering product information for product named Contoso GlowBrew
 GenerateDocumentationStep:
 Generating documentation for provided productInfo...
 ProofreadDocumentationAsync:
 Proofreading documentation...
 Grade: Fail
 Explanation: The proposed documentation has an overly casual tone 
and uses informal expressions that might not suit all customers. 
Additionally, some phrases may detract from the professionalism expected in 
customer-facing documentation. There are minor areas that could benefit from 
clarity and conciseness.
 Suggestions: Adjust the tone to be more professional and less 
casual; phrases like 'dazzling light show' and 'coffee performing' could be 
simplified.
 Remove informal phrases such as 'who knew coffee could be 
so... illuminating?'
 Consider editing out overly whimsical phrases like 'it's 
like a warm hug for your nose!' for a more straightforward description.
 Clarify the troubleshooting section for better customer 
understanding; avoid metaphorical language like 'secure that coffee cup when 
you realize Monday is still a thing.'
 GenerateDocumentationStep:
 Rewriting documentation with provided suggestions...
 ProofreadDocumentationAsync:
 Proofreading documentation...
 Grade: Fail
 Explanation: The documentation generally maintains a professional 
tone but contains minor phrasing issues that could be improved. There are no 
spelling or grammar mistakes noted, and it excludes any offensive language. 
However, the content could be more concise, and some phrases can be 
streamlined for clarity. Additionally, technical accuracy regarding 
troubleshooting solutions may require more details for the user's 
understanding. For example, clarifying how to 'reset the lighting settings 
through the designated app' would enhance user experience.
 Suggestions: Rephrase 'Join us as we elevate your coffee experience 
to new heights!' to make it more straightforward, such as 'Experience an 
elevated coffee journey with us.'
 In the 'Solution' section for the LED lights malfunction, 
add specific instructions on how to find and use the 'designated app' for 
resetting the lighting settings.
 Consider simplifying sentences such as 'Meet your new 
personal barista!' to be more straightforward, for example, 'Introducing 
your personal barista.'
 Ensure clarity in troubleshooting steps by elaborating on 
what a 'factory reset' entails.
 GenerateDocumentationStep:
 Rewriting documentation with provided suggestions...
 ProofreadDocumentationAsync:
 Proofreading documentation...
 Grade: Pass
 Explanation: The documentation presents a professional tone, 
contains no spelling or grammar mistakes, is free of offensive language, and 
is technically accurate regarding the product's features and troubleshooting 
guidance.
 Suggestions:
 PublishDocumentationStep:
 Publishing product documentation:
 # GlowBrew User Documentation
 ## Product Overview
 Introducing GlowBrew-your new partner in coffee brewing that brings together 
advanced technology and aesthetic appeal. This innovative AI-driven coffee 
machine not only brews your favorite coffee but also features the industry's 
leading number of customizable LEDs and programmable light shows.
 ## Key Features
 1. **Luminous Brew Technology**: Transform your morning routine with our 
customizable LED lights that synchronize with your brewing process, creating 
the perfect ambiance to start your day.
 2. **AI Taste Assistant**: Our intelligent system learns your preferences 
over time, recommending exciting new brew combinations tailored to your 
unique taste.
 3. **Gourmet Aroma Diffusion**: Experience an enhanced aroma with built-in 
aroma diffusers that elevate your coffee's scent profile, invigorating your 
senses before that all-important first sip.
 ## Troubleshooting
 ### Issue: LED Lights Malfunctioning
 **Solution**:- Begin by resetting the lighting settings via the designated app. Open the 
app, navigate to the settings menu, and select "Reset LED Lights."- Ensure that all LED connections inside the GlowBrew are secure and 
properly connected.- If issues persist, you may consider performing a factory reset. To do 
this, hold down the reset button located on the machine's back panel for 10 
seconds while the device is powered on.
 We hope you enjoy your GlowBrew experience and that it brings a delightful 
blend of flavor and brightness to your coffee moments!
 What's Next?
 Our process is now reliably generating documentation that meets our defined
 standards. This is great, but before we publish our documentation publicly we really
 should require a human to review and approve. Let's do that next.
 Human-in-the-loop
How-To: Human-in-the-Loop
 Article • 04/11/2025
２ Warning
 The Semantic Kernel Process Framework is experimental, still in development and is subject
 to change.
 Overview
 In the previous sections we built a Process to help us automate the creation of documentation
 for our new product. Our process can now generate documentation that is specific to our
 product, and can ensure it meets our quality bar by running it through a proofread and edit
 cycle. In this section we will improve on that process again by requiring a human to approve or
 reject the documentation before it's published. The flexibility of the process framework means
 that there are several ways that we could go about doing this but in this example we will
 demonstrate integration with an external pubsub system for requesting approval.

The first change we need to make to the process is to make the publishing step wait for the
 approval before it publishes the documentation. One option is to simply add a second
 parameter for the approval to the PublishDocumentation function in the
 PublishDocumentationStep. This works because a KernelFunction in a step will only be invoked
 when all of its required parameters have been provided.
 C#
 With the code above, the PublishDocumentation function in the PublishDocumentationStep will
 only be invoked when the generated documentation has been sent to the document parameter
 and the result of the approval has been sent to the userApproval parameter.
 We can now reuse the existing logic of ProofreadStep step to additionally emit an event to our
 external pubsub system which will notify the human approver that there is a new request.
 C#
 Make publishing wait for approval
 // A process step to publish documentation
 public class PublishDocumentationStep : KernelProcessStep
 {
    [KernelFunction]
    public DocumentInfo PublishDocumentation(DocumentInfo document, bool 
userApproval) // added the userApproval parameter
    {
        // Only publish the documentation if it has been approved
        if (userApproval)
        {
            // For example purposes we just write the generated docs to the 
console
            Console.WriteLine($"[{nameof(PublishDocumentationStep)}]:\tPublishing 
product documentation approved by user: \n{document.Title}\n{document.Content}");
        }
        return document;
    }
 }
 // A process step to publish documentation
 public class ProofReadDocumentationStep : KernelProcessStep
 {
    ...
    if (formattedResponse.MeetsExpectations)
    {
        // Events that are getting piped to steps that will be resumed, like 
Since we want to publish the newly generated documentation when it is approved by the
 proofread agent, the approved documents will be queued on the publishing step. In addition, a
 human will be notified via our external pubsub system with an update on the latest document.
 Let's update the process flow to match this new design.
 C#
 PublishDocumentationStep.OnPublishDocumentation
        // require events to be marked as public so they are persisted and 
restored correctly
        await context.EmitEventAsync("DocumentationApproved", data: document, 
visibility: KernelProcessEventVisibility.Public);
    }
    ...
 }
 // Create the process builder
 ProcessBuilder processBuilder = new("DocumentationGeneration");
 // Add the steps
 var infoGatheringStep = processBuilder.AddStepFromType<GatherProductInfoStep>();
 var docsGenerationStep = 
processBuilder.AddStepFromType<GenerateDocumentationStepV2>();
 var docsProofreadStep = processBuilder.AddStepFromType<ProofreadStep>();
 var docsPublishStep = processBuilder.AddStepFromType<PublishDocumentationStep>();
 // internal component that allows emitting SK events externally, a list of topic 
names
 // is needed to link them to existing SK events
 var proxyStep = processBuilder.AddProxyStep(["RequestUserReview", 
"PublishDocumentation"]);
 // Orchestrate the events
 processBuilder
    .OnInputEvent("StartDocumentGeneration")
    .SendEventTo(new(infoGatheringStep));
 processBuilder
    .OnInputEvent("UserRejectedDocument")
    .SendEventTo(new(docsGenerationStep, functionName: "ApplySuggestions"));
 // When external human approval event comes in, route it to the 'isApproved' 
parameter of the docsPublishStep
 processBuilder
    .OnInputEvent("UserApprovedDocument")
    .SendEventTo(new(docsPublishStep, parameterName: "userApproval"));
 // Hooking up the rest of the process steps
 infoGatheringStep
    .OnFunctionResult()
    .SendEventTo(new(docsGenerationStep, functionName: "GenerateDocumentation"));
Finally, an implementation of the interface IExternalKernelProcessMessageChannel should be
 provided since it is internally use by the new ProxyStep. This interface is used to emit messages
 externally. The implementation of this interface will depend on the external system that you are
 using. In this example, we will use a custom client that we have created to send messages to an
 external pubsub system.
 C#
 docsGenerationStep
    .OnEvent("DocumentationGenerated")
    .SendEventTo(new(docsProofreadStep));
 docsProofreadStep
    .OnEvent("DocumentationRejected")
    .SendEventTo(new(docsGenerationStep, functionName: "ApplySuggestions"));
 // When the proofreader approves the documentation, send it to the 'document' 
parameter of the docsPublishStep
 // Additionally, the generated document is emitted externally for user approval 
using the pre-configured proxyStep
 docsProofreadStep
    .OnEvent("DocumentationApproved")
    // [NEW] addition to emit messages externally
    .EmitExternalEvent(proxyStep, "RequestUserReview") // Hooking up existing 
"DocumentationApproved" to external topic "RequestUserReview"
    .SendEventTo(new(docsPublishStep, parameterName: "document"));
 // When event is approved by user, it gets published externally too
 docsPublishStep
    .OnFunctionResult()
    // [NEW] addition to emit messages externally
    .EmitExternalEvent(proxyStep, "PublishDocumentation");
 var process = processBuilder.Build();
 return process;
 // Example of potential custom IExternalKernelProcessMessageChannel implementation 
public class MyCloudEventClient : IExternalKernelProcessMessageChannel
 {
    private MyCustomClient? _customClient;
    // Example of an implementation for the process
    public async Task EmitExternalEventAsync(string externalTopicEvent, 
KernelProcessProxyMessage message)
    {
        // logic used for emitting messages externally.
        // Since all topics are received here potentially 
        // some if else/switch logic is needed to map correctly topics with 
external APIs/endpoints.
        if (this._customClient != null)
        {
            switch (externalTopicEvent) 
{
 DocumentInfo;
 case "RequestUserReview":
 var requestDocument = message.EventData.ToObject() as 
// As an example only invoking a sample of a custom client 
with a different endpoint/api route
 this._customClient.InvokeAsync("REQUEST_USER_REVIEW", 
requestDocument);
 return;
 case "PublishDocumentation":
 var publishedDocument = message.EventData.ToObject() as 
DocumentInfo;
 // As an example only invoking a sample of a custom client 
with a different endpoint/api route
 this._customClient.InvokeAsync("PUBLISH_DOC_EXTERNALLY", 
publishedDocument);
 return;
 }
 }
 }
 public async ValueTask Initialize()
 {
 // logic needed to initialize proxy step, can be used to initialize custom 
client
 this._customClient = new MyCustomClient("http://localhost:8080");
 this._customClient.Initialize();
 }
 public async ValueTask Uninitialize()
 {
 // Cleanup to be executed when proxy step is uninitialized
 if (this._customClient != null)
 {
 await this._customClient.ShutdownAsync();
 }
 }
 }
 Finally to allow the process 
ProxyStep to make use of the
 IExternalKernelProcessMessageChannel implementation, in this case 
need to pipe it properly.
 MyCloudEventClient, we
 When using Local Runtime, the implemented class can be passed when invoking 
on the 
KernelProcess class.
 C#
 KernelProcess process;
 IExternalKernelProcessMessageChannel myExternalMessageChannel = new 
MyCloudEventClient();
 StartAsync
When using Dapr Runtime, the plumbing has to be done through dependency injection at the
 Program setup of the project.
 C#
 Two changes have been made to the process flow:
 Added an input event named HumanApprovalResponse that will be routed to the
 userApproval parameter of the docsPublishStep step.
 Since the KernelFunction in docsPublishStep now has two parameters, we need to update
 the existing route to specify the parameter name of document.
 Run the process as you did before and notice that this time when the proofreader approves the
 generated documentation and sends it to the document parameter of the docPublishStep step,
 the step is no longer invoked because it is waiting for the userApproval parameter. At this
 point the process goes idle because there are no steps ready to be invoked and the call that we
 made to start the process returns. The process will remain in this idle state until our "human-in
the-loop" takes action to approve or reject the publish request. Once this has happened and
 the result has been communicated back to our program, we can restart the process with the
 result.
 C#
 When the process is started again with the UserApprovedDocument it will pick up from where it
 left off and invoke the docsPublishStep with userApproval set to true and our documentation
 // Start the process with the external message channel
 await process.StartAsync(kernel, new KernelProcessEvent 
    {
        Id = inputEvent,
        Data = input,
    },
    myExternalMessageChannel)
 var builder = WebApplication.CreateBuilder(args);
 ...
 // depending on the application a singleton or scoped service can be used
 // Injecting SK Process custom client IExternalKernelProcessMessageChannel 
implementation
 builder.Services.AddSingleton<IExternalKernelProcessMessageChannel, 
MyCloudEventClient>();
 // Restart the process with approval for publishing the documentation.
 await process.StartAsync(kernel, new KernelProcessEvent { Id = 
"UserApprovedDocument", Data = true });
will be published. If it is started again with the 
kick off the 
UserRejectedDocument event, the process will
 ApplySuggestions function in the 
docsGenerationStep step and the process will
 continue as before.
 The process is now complete and we have successfully added a human-in-the-loop step to our
 process. The process can now be used to generate documentation for our product, proofread
 it, and publish it once it has been approved by a human.
Support for Semantic Kernel
 Article • 03/06/2025
 👋
 Welcome! There are a variety of ways to get supported in the Semantic Kernel (SK)
 world.
 Your preference
 Read the docs
 Visit the repo
 Connect with the Semantic
 Kernel Team
 Office Hours
ﾉ Expand table
 What's available
 This learning site is the home of the latest information for
 developers
 Our open-source GitHub repository is available for perusal and
 suggestions
 Visit our GitHub Discussions to get supported quickly with our
 CoC actively enforced
 We will be hosting regular office hours; the calendar invites and
 cadence are located here: Community.MD
 More support information
 Frequently Asked Questions (FAQs)
 Hackathon Materials
 Code of Conduct
 Transparency Documentation
 Next step
 Run the samples
Contributing to Semantic Kernel
 Article • 09/24/2024
 You can contribute to Semantic Kernel by submitting issues, starting discussions, and
 submitting pull requests (PRs). Contributing code is greatly appreciated, but simply filing
 issues for problems you encounter is also a great way to contribute since it helps us
 focus our efforts.
 Reporting issues and feedback
 We always welcome bug reports, API proposals, and overall feedback. Since we use
 GitHub, you can use the Issues and Discussions tabs to start a conversation with the
 team. Below are a few tips when submitting issues and feedback so we can respond to
 your feedback as quickly as possible.
 Reporting issues
 New issues for the SDK can be reported in our list of issues , but before you file a new
 issue, please search the list of issues to make sure it does not already exist. If you have
 issues with the Semantic Kernel documentation (this site), please file an issue in the
 Semantic Kernel documentation repository .
 If you do find an existing issue for what you wanted to report, please include your own
 feedback in the discussion. We also highly recommend up-voting (
 👍
 reaction) the
 original post, as this helps us prioritize popular issues in our backlog.
 Writing a Good Bug Report
 Good bug reports make it easier for maintainers to verify and root cause the underlying
 problem. The better a bug report, the faster the problem can be resolved. Ideally, a bug
 report should contain the following information:
 A high-level description of the problem.
 A minimal reproduction, i.e. the smallest size of code/configuration required to
 reproduce the wrong behavior.
 A description of the expected behavior, contrasted with the actual behavior
 observed.
 Information on the environment: OS/distribution, CPU architecture, SDK version,
 etc.
Additional information, e.g. Is it a regression from previous versions? Are there any
 known workarounds?
 Create issue
 Submitting feedback
 If you have general feedback on Semantic Kernel or ideas on how to make it better,
 please share it on our discussions board . Before starting a new discussion, please
 search the list of discussions to make sure it does not already exist.
 We recommend using the ideas category if you have a specific idea you would like to
 share and the Q&A category if you have a question about Semantic Kernel.
 You can also start discussions (and share any feedback you've created) in the Discord
 community by joining the Semantic Kernel Discord server .
 Start a discussion
 Help us prioritize feedback
 We currently use up-votes to help us prioritize issues and features in our backlog, so
 please up-vote any issues or discussions that you would like to see addressed.
 If you think others would benefit from a feature, we also encourage you to ask others to
 up-vote the issue. This helps us prioritize issues that are impacting the most users. You
 can ask colleagues, friends, or the community on Discord to up-vote an issue by
 sharing the link to the issue or discussion.
 Submitting pull requests
 We welcome contributions to Semantic Kernel. If you have a bug fix or new feature that
 you would like to contribute, please follow the steps below to submit a pull request (PR).
 Afterwards, project maintainers will review code changes and merge them once they've
 been accepted.
 Recommended contribution workflow
 We recommend using the following workflow to contribute to Semantic Kernel (this is
 the same workflow used by the Semantic Kernel team):
1. Create an issue for your work.
 You can skip this step for trivial changes.
 Reuse an existing issue on the topic, if there is one.
 Get agreement from the team and the community that your proposed
 change is a good one by using the discussion in the issue.
 Clearly state in the issue that you will take on implementation. This allows us
 to assign the issue to you and ensures that someone else does not
 accidentally works on it.
 2. Create a personal fork of the repository on GitHub (if you don't already have one).
 3. In your fork, create a branch off of main (
 git checkout -b mybranch).
 Name the branch so that it clearly communicates your intentions, such as
 "issue-123" or "githubhandle-issue".
 4. Make and commit your changes to your branch.
 5. Add new tests corresponding to your change, if applicable.
 6. Build the repository with your changes.
 Make sure that the builds are clean.
 Make sure that the tests are all passing, including your new tests.
 7. Create a PR against the repository's main branch.
 State in the description what issue or improvement your change is
 addressing.
 Verify that all the Continuous Integration checks are passing.
 8. Wait for feedback or approval of your changes from the code maintainers.
 9. When area owners have signed off, and all checks are green, your PR will be
 merged.
 Dos and Don'ts while contributing
 The following is a list of Dos and Don'ts that we recommend when contributing to
 Semantic Kernel to help us review and merge your changes as quickly as possible.
 Do's:
 Do follow the standard .NET coding style and Python code style
 Do give priority to the current style of the project or file you're changing if it
 diverges from the general guidelines.
Do include tests when adding new features. When fixing bugs, start with adding a
 test that highlights how the current behavior is broken.
 Do keep the discussions focused. When a new or related topic comes up it's often
 better to create new issue than to side track the discussion.
 Do clearly state on an issue that you are going to take on implementing it.
 Do blog and/or tweet about your contributions!
 Don'ts:
 Don't surprise the team with big pull requests. We want to support contributors, so
 we recommend filing an issue and starting a discussion so we can agree on a
 direction before you invest a large amount of time.
 Don't commit code that you didn't write. If you find code that you think is a good
 fit to add to Semantic Kernel, file an issue and start a discussion before
 proceeding.
 Don't submit PRs that alter licensing related files or headers. If you believe there's
 a problem with them, file an issue and we'll be happy to discuss it.
 Don't make new APIs without filing an issue and discussing with the team first.
 Adding new public surface area to a library is a big deal and we want to make sure
 we get it right.
 Breaking Changes
 Contributions must maintain API signature and behavioral compatibility. If you want to
 make a change that will break existing code, please file an issue to discuss your idea or
 change if you believe that a breaking change is warranted. Otherwise, contributions that
 include breaking changes will be rejected.
 The continuous integration (CI) process
 The continuous integration (CI) system will automatically perform the required builds
 and run tests (including the ones you should also run locally) for PRs. Builds and test
 runs must be clean before a PR can be merged.
 If the CI build fails for any reason, the PR issue will be updated with a link that can be
 used to determine the cause of the failure so that it can be addressed.
 Contributing to documentation
 We also accept contributions to the Semantic Kernel documentation repository .
Running your own Hackathon
 Article • 09/24/2024
 With these materials you can run your own Semantic Kernel Hackathon, a hands-on
 event where you can learn and create AI solutions using Semantic Kernel tools and
 resources.
 By participating and running a Semantic Kernel hackathon, you will have the opportunity
 to:
 Explore the features and capabilities of Semantic Kernel and how it can help you
 solve problems with AI
 Work in teams to brainstorm and develop your own AI plugins or apps using
 Semantic Kernel SDK and services
 Present your results and get feedback from other participants
 Have fun!
 Download the materials
 To run your own hackathon, you will first need to download the materials. You can
 download the zip file here:
 Download hackathon materials
 Once you have unzipped the file, you will find the following resources:
 Hackathon sample agenda
 Hackathon prerequisites
 Hackathon facilitator presentation
 Hackathon team template
 Helpful links
 Preparing for the hackathon
 Before the hackathon, you and your peers will need to download and install software
 needed for Semantic Kernel to run. Additionally, you should already have API keys for
 either OpenAI or Azure OpenAI and access to the Semantic Kernel repo. Please refer to
 the prerequisites document in the facilitator materials for the complete list of tasks
 participants should complete before the hackathon.
You should also familiarize yourself with the available documentation and tutorials. This
 will ensure that you are knowledgeable of core Semantic Kernel concepts and features
 so that you can help others during the hackathon. The following resources are highly
 recommended:
 What is Semantic Kernel?
 Semantic Kernel LinkedIn training video
 The hackathon will consist of six main phases: welcome, overview, brainstorming,
 development, presentation, and feedback.
 Here is an approximate agenda and structure for each phase but feel free to modify this
 based on your team:
 Length
 (Minutes)
 Phase Description
 Day 1
 15 Welcome/Introductions The hackathon facilitator will welcome the participants,
 introduce the goals and rules of the hackathon, and
 answer any questions.
 30 Overview of Semantic
 Kernel
 The facilitator will guide you through a live presentation
 that will give you an overview of AI and why it is
 important for solving problems in today's world. You will
 also see demos of how Semantic Kernel can be used for
 different scenarios.
 5 Choose your Track Review slides in the deck for the specific track you’ll pick
 for the hackathon.
 120 Brainstorming The facilitator will help you form teams based on your
 interests or skill levels. You will then brainstorm ideas for
 your own AI plugins or apps using design thinking
 techniques.
 20 Responsible AI Spend some time reviewing Responsible AI principles
 and ensure your proposal follows these principles.
 60 Break/Lunch Lunch or Break
 360+ Development/Hack You will use Semantic Kernel SDKs tools, and resources
 to develop, test, and deploy your projects. This could be
 Running the hackathon
ﾉExpand table
Length
 (Minutes)
 Phase Description
 for the rest of the day or over multiple days based on
 the time available and problem to be solved.
 Day 2
 5 Welcome Back Reconnect for Day 2 of the Semantic Kernel Hackathon
 20 What did you learn? Review what you’ve learned so far in Day 1 of the
 Hackathon.
 120 Hack You will use Semantic Kernel SDKs tools, and resources
 to develop, test, and deploy your projects. This could be
 for the rest of the day or over multiple days based on
 the time available and problem to be solved.
 120 Demo Each team will present their results using a PowerPoint
 template provided. You will have about 15 minutes per
 team to showcase your project, demonstrate how it
 works, and explain how it solves a problem with AI. You
 will also receive feedback from other participants.
 5 Thank you The hackathon facilitator will close the hackathon.
 30 Feedback Each team can share their feedback on the hackathon
 and Semantic Kernel with the group and fill out the
 Hackathon Exit Survey .
 We hope you enjoyed running a Semantic Kernel Hackathon and the overall experience!
 We would love to hear from you about what worked well, what didn't, and what we can
 improve for future content. Please take a few minutes to fill out the hackathon facilitator
 survey and share your feedback and suggestions with us.
 If you want to continue developing your AI plugins or projects after the hackathon, you
 can find more resources and support for Semantic Kernel.
 Semantic Kernel blog
 Semantic Kernel GitHub repo
 Thank you for your engagement and creativity during the hackathon. We look forward
 to seeing what you create next with Semantic Kernel!
 Following up after the hackathon
Glossary for Semantic Kernel
 Article•06/24/2024
 👋
 Hello! We've included a Glossary below with key terminology.
 Term/Word Defintion
 Agent An agent is an artificial intelligence that can answer questions and automate
 processes for users. There's a wide spectrum of agents that can be built, ranging
 from simple chat bots to fully automated AI assistants. With Semantic Kernel, we
 provide you with the tools to build increasingly more sophisticated agents that
 don't require you to be an AI expert.
 API Application Programming Interface. A set of rules and specifications that allow
 software components to communicate and exchange data.
 Autonomous Agents that can respond to stimuli with minimal human intervention.
 Chatbot A simple back-and-forth chat with a user and AI Agent.
 Connectors Connectors allow you to integrate existing APIs (Application Programming
 Interface) with LLMs (Large Language Models). For example, a Microsoft Graph
 connector can be used to automatically send the output of a request in an email,
 or to build a description of relationships in an organization chart.
 Copilot Agents that work side-by-side with a user to complete a task.
 Kernel Similar to operating system, the kernel is responsible for managing resources that
 are necessary to run "code" in an AI application. This includes managing the AI
 models, services, and plugins that are necessary for both native code and AI
 services to run together. Because the kernel has all the services and plugins
 necessary to run both native code and AI services, it is used by nearly every
 component within the Semantic Kernel SDK. This means that if you run any
 prompt or code in Semantic Kernel, it will always go through a kernel.
 LLM Large Language Models are Artificial Intelligence tools that can summarize, read
 or generate text in the form of sentences similar to how a humans talk and write.
 LLMs can be incorporate into various products at Microsoft to unearth richer user
 value.
 Memory Memories are a powerful way to provide broader context for your ask.
 Historically, we've always called upon memory as a core component for how
 computers work: think the RAM in your laptop. For with just a CPU that can
 crunch numbers, the computer isn't that useful unless it knows what numbers you
 care about. Memories are what make computation relevant to the task at hand.
ﾉExpand table
Term/Word Defintion
 Plugins To generate this plan, the copilot would first need the capabilities necessary to
 perform these steps. This is where plugins come in. Plugins allow you to give your
 agent skills via code. For example, you could create a plugin that sends emails,
 retrieves information from a database, asks for help, or even saves and retrieves
 memories from previous conversations.
 Planners To use a plugin (and to wire them up with other steps), the copilot would need to
 first generate a plan. This is where planners come in. Planners are special prompts
 that allow an agent to generate a plan to complete a task. The simplest planners
 are just a single prompt that helps the agent use function calling to complete a
 task.
 Prompts Prompts play a crucial role in communicating and directing the behavior of Large
 Language Models (LLMs) AI. They serve as inputs or queries that users can
 provide to elicit specific responses from a model.
 Prompt
 Engineering
 Because of the amount of control that exists, prompt engineering is a critical skill
 for anyone working with LLM AI models. It's also a skill that's in high demand as
 more organizations adopt LLM AI models to automate tasks and improve
 productivity. A good prompt engineer can help organizations get the most out of
 their LLM AI models by designing prompts that produce the desired outputs.
 RAG Retrieval Augmented Generation - a term that refers to the process of retrieving
 additional data to provide as context to an LLM to use when generating a
 response (completion) to a user’s question (prompt).
 Frequently Asked Questions (FAQs)
 Hackathon Materials
 Code of Conduct
 More support information
Semantic Kernel - .Net V1 Migration Guide
 Article•09/24/2024
 This guide is intended to help you upgrade from a pre-v1 version of the .NET Semantic Kernel SDK
 to v1+. The pre-v1 version used as a reference for this document was the 0.26.231009 version which
 was the last version before the first beta release where the majority of the changes started to
 happen.
 As a result of many packages being redefined, removed and renamed, also considering that we did a
 good cleanup and namespace simplification many of our old packages needed to be renamed,
 deprecated and removed. The table below shows the changes in our packages.
 All packages that start with Microsoft.SemanticKernel were truncated with a .. prefix for brevity.
 Previous Name V1 Name Version Reason
 ..Connectors.AI.HuggingFace ..Connectors.HuggingFace preview
 ..Connectors.AI.OpenAI ..Connectors.OpenAI v1
 ..Connectors.AI.Oobabooga MyIA.SemanticKernel.Connectors.AI.Oobabooga alpha Community
 driven
 connector
 ⚠ 
 Not
 ready for
 v1+ yet
 ..Connectors.Memory.Kusto ..Connectors.Kusto alpha
 ..Connectors.Memory.DuckDB ..Connectors.DuckDB alpha
 ..Connectors.Memory.Pinecone ..Connectors.Pinecone alpha
 ..Connectors.Memory.Redis ..Connectors.Redis alpha
 ..Connectors.Memory.Qdrant ..Connectors.Qdrant alpha--..Connectors.Postgres alpha
 ..Connectors.Memory.AzureCognitiveSearch ..Connectors.Memory.AzureAISearch alpha
 ..Functions.Semantic- Removed -Merged in
７ Note
 This document is not final and will get increasingly better!
 Package Changes
ﾉExpand table
Previous Name
 ..Reliability.Basic
 ..Reliability.Polly
 ..TemplateEngine.Basic
 ..Planners.Core---
V1 Name- Removed -- Removed -- Removed 
..Planners.OpenAI
 Planners.Handlebars
 ..Experimental.Agents
 ..Experimental.Orchestration.Flow
 Version Reason
 Core
 Replaced by
 .NET
 Dependency
 Injection
 Replaced by
 .NET
 Dependency
 Injection
 Merged in
 Core
 preview
 alpha
 v1
 Reliability Packages - Replaced by .NET Dependency Injection
 The Reliability Basic and Polly packages now can be achieved using the .net dependency injection
 ConfigureHttpClientDefaults service collection extension to inject the desired resiliency policies to
 the 
HttpClient instances.
 C#
 // Before
 var retryConfig = new BasicRetryConfig
 {
 MaxRetryCount = 3,
 UseExponentialBackoff = true,
 };
 retryConfig.RetryableStatusCodes.Add(HttpStatusCode.Unauthorized);
 var kernel = new KernelBuilder().WithRetryBasic(retryConfig).Build();
 C#
 // After
 builder.Services.ConfigureHttpClientDefaults(c =>
 {
 // Use a standard resiliency policy, augmented to retry on 401 Unauthorized for 
this example
 c.AddStandardResilienceHandler().Configure(o =>
 {
 o.Retry.ShouldHandle = args => 
ValueTask.FromResult(args.Outcome.Result?.StatusCode is HttpStatusCode.Unauthorized);
 });
 });
Ensure that if you use any of the packages below you match the latest version that V1 uses:
 Package Name Version
 Microsoft.Extensions.Configuration 8.0.0
 Microsoft.Extensions.Configuration.Binder 8.0.0
 Microsoft.Extensions.Configuration.EnvironmentVariables 8.0.0
 Microsoft.Extensions.Configuration.Json 8.0.0
 Microsoft.Extensions.Configuration.UserSecrets 8.0.0
 Microsoft.Extensions.DependencyInjection 8.0.0
 Microsoft.Extensions.DependencyInjection.Abstractions 8.0.0
 Microsoft.Extensions.Http 8.0.0
 Microsoft.Extensions.Http.Resilience 8.0.0
 Microsoft.Extensions.Logging 8.0.0
 Microsoft.Extensions.Logging.Abstractions 8.0.0
 Microsoft.Extensions.Logging.Console 8.0.0
 Many of our internal naming conventions were changed to better reflect how the AI community
 names things. As OpenAI started the massive shift and terms like Prompt, Plugins, Models, RAG were
 taking shape it was clear that we needed to align with those terms to make it easier for the
 community to understand use the SDK.
 Previous Name V1 Name
 Semantic Function Prompt Function
 Native Function Method Function
 Context Variable Kernel Argument
 Request Settings Prompt Execution Settings
 Text Completion Text Generation
 Image Generation Text to Image
 Package Removal and Changes Needed
ﾉExpand table
 Convention Name Changes
ﾉExpand table
Previous Name V1 Name
 Skill Plugin
 Following the convetion name changes, many of the code names were also changed to better reflect
 the new naming conventions. Abbreaviations were also removed to make the code more readable.
 Previous Name V1 Name
 ContextVariables KernelArguments
 ContextVariables.Set KernelArguments.Add
 IImageGenerationService ITextToImageService
 ITextCompletionService ITextGenerationService
 Kernel.CreateSemanticFunction Kernel.CreateFunctionFromPrompt
 Kernel.ImportFunctions Kernel.ImportPluginFrom____
 Kernel.ImportSemanticFunctionsFromDirectory Kernel.ImportPluginFromPromptDirectory
 Kernel.RunAsync Kernel.InvokeAsync
 NativeFunction MethodFunction
 OpenAIRequestSettings OpenAIPromptExecutionSettings
 RequestSettings PromptExecutionSettings
 SKException KernelException
 SKFunction KernelFunction
 SKFunctionMetadata KernelFunctionAttribute
 SKJsonSchema KernelJsonSchema
 SKParameterMetadata KernelParameterMetadata
 SKPluginCollection KernelPluginCollection
 SKReturnParameterMetadata KernelReturnParameterMetadata
 SemanticFunction PromptFunction
 SKContext FunctionResult (output)
 Code Name Changes
ﾉExpand table
 Namespace Simplifications
The old namespaces before had a deep hierarchy matching 1:1 the directory names in the projects.
 This is a common practice but did mean that consumers of the Semantic Kernel packages had to add
 a lot of different using's in their code. We decided to reduce the number of namespaces in the
 Semantic Kernel packages so the majority of the functionality is in the main
 Microsoft.SemanticKernel namespace. See below for more details.
 Previous Name V1 Name
 Microsoft.SemanticKernel.Orchestration Microsoft.SemanticKernel
 Microsoft.SemanticKernel.Connectors.AI.* Microsoft.SemanticKernel.Connectors.*
 Microsoft.SemanticKernel.SemanticFunctions Microsoft.SemanticKernel
 Microsoft.SemanticKernel.Events Microsoft.SemanticKernel
 Microsoft.SemanticKernel.AI.* Microsoft.SemanticKernel.*
 Microsoft.SemanticKernel.Connectors.AI.OpenAI.* Microsoft.SemanticKernel.Connectors.OpenAI
 Microsoft.SemanticKernel.Connectors.AI.HuggingFace.* Microsoft.SemanticKernel.Connectors.HuggingFace
 The code to create and use a Kernel instance has been simplified. The IKernel interface has been
 eliminated as developers should not need to create their own Kernel implementation. The Kernel
 class represents a collection of services and plugins. The current Kernel instance is available
 everywhere which is consistent with the design philosophy behind the Semantic Kernel.
 IKernel interface was changed to Kernel class.
 Kernel.ImportFunctions was removed and replaced by Kernel.ImportPluginFrom____, where
 ____ can be Functions, Object, PromptDirectory, Type, Grp or OpenAIAsync, etc.
 C#
 Kernel.RunAsync was removed and replaced by Kernel.InvokeAsync. Order of parameters
 shifted, where function is the first.
 C#
ﾉExpand table
 Kernel
 // Before
 var textFunctions = kernel.ImportFunctions(new StaticTextPlugin(), "text");
 // After
 var textFunctions = kernel.ImportPluginFromObject(new StaticTextPlugin(), "text");
 // Before
 KernelResult result = kernel.RunAsync(textFunctions["Uppercase"], "Hello World!");
// After
 FunctionResult result = kernel.InvokeAsync(textFunctions["Uppercase"], new() { 
["input"] = "Hello World!" });
 Kernel.InvokeAsync now returns a 
FunctionResult instead of a 
KernelResult.
 Kernel.InvokeAsync only targets one function per call as first parameter. Pipelining is not
 supported, use the Example 60 to achieve a chaining behavior.
 ❌
 Not supported
 C#
 KernelResult result = await kernel.RunAsync(" Hello World! ",
 textFunctions["TrimStart"],
 textFunctions["TrimEnd"],
 textFunctions["Uppercase"]);
 ✔ 
 One function per call
 C#
 var trimStartResult = await kernel.InvokeAsync(textFunctions["TrimStart"], new() { 
["input"] = " Hello World! " });
 var trimEndResult = await kernel.InvokeAsync(textFunctions["TrimEnd"], new() { 
["input"] = trimStartResult.GetValue<string>() });
 var finalResult = await kernel.InvokeAsync(textFunctions["Uppercase"], new() { 
["input"] = trimEndResult.GetValue<string>() });
 ✔ 
 Chaining using plugin Kernel injection
 C#
 }
 }
 // Plugin using Kernel injection
 public class MyTextPlugin
 {
 [KernelFunction]
 public async Task<string> Chain(Kernel kernel, string input)
 {
 var trimStartResult = await kernel.InvokeAsync("textFunctions", 
"TrimStart", new() { ["input"] = input });
 var trimEndResult = await kernel.InvokeAsync("textFunctions", "TrimEnd", 
new() { ["input"] = trimStartResult.GetValue<string>() });
 var finalResult = await kernel.InvokeAsync("textFunctions", "Uppercase", 
new() { ["input"] = trimEndResult.GetValue<string>() });
 return finalResult.GetValue<string>();
 var plugin = kernel.ImportPluginFromObject(new MyTextPlugin(), "textFunctions");
 var finalResult = await kernel.InvokeAsync(plugin["Chain"], new() { ["input"] = " 
Hello World! "});
Kernel.InvokeAsync does not accept string as input anymore, use a 
KernelArguments instance
 KernelArguments instance.
 instead. The function now is the first argument and the input argument needs to be provided
 as a 
C#
 // Before
 var result = await kernel.RunAsync("I missed the F1 final race", excuseFunction);
 // After
 var result = await kernel.InvokeAsync(excuseFunction, new() { ["input"] = "I 
missed the F1 final race" });
 Kernel.ImportSemanticFunctionsFromDirectory was removed and replaced by
 Kernel.ImportPluginFromPromptDirectory.
 Kernel.CreateSemanticFunction was removed and replaced by
 Kernel.CreateFunctionFromPrompt.
 Arguments: 
OpenAIRequestSettings is now 
Context Variables
 OpenAIPromptExecutionSettings
 ContextVariables was redefined as
 KernelArguments and is now a dictionary, where the key is the
 name of the argument and the value is the value of the argument. Methods like 
Set and 
removed and the common dictionary Add or the indexer 
Get were
 [] to set and get values should be used
 instead.
 C#
 // Before
 var variables = new ContextVariables("Today is: ");
 variables.Set("day", DateTimeOffset.Now.ToString("dddd", CultureInfo.CurrentCulture));
 // After
 var arguments = new KernelArguments() {
 ["input"] = "Today is: ",
 ["day"] = DateTimeOffset.Now.ToString("dddd", CultureInfo.CurrentCulture)
 };
 // Initialize directly or use the dictionary indexer below
 arguments["day"] = DateTimeOffset.Now.ToString("dddd", CultureInfo.CurrentCulture);
 Kernel Builder
 Many changes were made to our KernelBuilder to make it more intuitive and easier to use, as well as
 to make it simpler and more aligned with the .NET builders approach.
 Creating a 
KernelBuilder can now be only created using the 
Kernel.CreateBuilder() method.
This change make it simpler and easier to use the KernelBuilder in any code-base ensureing
 one main way of using the builder instead of multiple ways that adds complexity and
 maintenance overhead.
 C#
 // Before
 IKernel kernel = new KernelBuilder().Build();
 // After
 var builder = Kernel.CreateBuilder().Build();
 KernelBuilder.With... was renamed to 
KernelBuilder.Add...
 WithOpenAIChatCompletionService was renamed to 
WithAIService<ITextCompletion>
 AddOpenAIChatCompletionService
 KernelBuilder.WithLoggerFactory is not more used, instead use dependency injection
 approach to add the logger factory.
 C#
 IKernelBuilder builder = Kernel.CreateBuilder();
 builder.Services.AddLogging(c => 
c.AddConsole().SetMinimumLevel(LogLevel.Information));
 WithAIService<T> Dependency Injection
 Previously the 
KernelBuilder had a method 
WithAIService<T> that was removed and a new
 ServiceCollection Services property is exposed to allow the developer to add services to the
 dependency injection container. i.e.:
 C#
 builder.Services.AddSingleton<ITextGenerationService>()
 Kernel Result
 As the Kernel became just a container for the plugins and now executes just one function there was
 not more need to have a 
KernelResult entity and all function invocations from Kernel now return a
 FunctionResult.
 SKContext
 After a lot of discussions and feedback internally and from the community, to simplify the API and
 make it more intuitive, the 
SKContext concept was dilluted in different entities: 
function inputs and 
KernelArguments for
 FunctionResult for function outputs.
With the important decision to make 
Kernel a required argument of a function calling, the
 SKContext was removed and the 
KernelArguments and 
FunctionResult were introduced.
 KernelArguments is a dictionary that holds the input arguments for the function invocation that were
 previously held in the 
SKContext.Variables property.
 FunctionResult is the output of the 
Kernel.InvokeAsync method and holds the result of the function
 invocation that was previously held in the 
SKContext.Result property.
 New Plugin Abstractions
 KernelPlugin Entity: Before V1 there was no concept of a plugin centric entity. This changed in
 V1 and for any function you add to a Kernel you will get a Plugin that it belongs to.
 Plugins Immutability
 Plugins are created by default as immutable by our out-of-the-box 
DefaultKernelPlugin
 implementation, which means that they cannot be modified or changed after creation.
 Also attempting to import the plugins that share the same name in the kernel will give you a key
 violation exception.
 The addition of the 
KernelPlugin abstraction allows dynamic implementations that may support
 mutability and we provided an example on how to implement a mutable plugin in the Example 69 .
 Combining multiple plugins into one
 Attempting to create a plugin from directory and adding Method functions afterwards for the same
 plugin will not work unless you use another approach like creating both plugins separately and then
 combining them into a single plugin iterating over its functions to aggregate into the final plugin
 using 
kernel.ImportPluginFromFunctions("myAggregatePlugin", myAggregatedFunctions) extension.
 Usage of Experimental Attribute Feature.
 This features was introduced to mark some functionalities in V1 that we can possibly change or
 completely remove.
 For mode details one the list of current released experimental features check here .
 Prompt Configuration Files
 Major changes were introduced to the Prompt Configuration files including default and multiple
 service/model configurations.
 Other naming changes to note:
completion was renamed to 
execution_settings
 input was renamed to 
input_variables
 defaultValue was renamed to 
default
 parameters was renamed to 
input_variables
 Each property name in the 
execution_settings once matched to the 
to configure the service/model execution settings. i.e.:
 C#
 service_id will be used
 // The "service1" execution settings will be used to configure the 
OpenAIChatCompletion service
 Kernel kernel = Kernel.CreateBuilder()
 .AddOpenAIChatCompletion(serviceId: "service1", modelId: "gpt-4")
 Before
 JSON
 {
 }
 "schema": 1,
 "description": "Given a text input, continue it with additional text.",
 "type": "completion",
 "completion": {
 "max_tokens": 4000,
 "temperature": 0.3,
 "top_p": 0.5,
 "presence_penalty": 0.0,
 "frequency_penalty": 0.0
 },
 "input": {
 "parameters": [
 {
 "name": "input",
 "description": "The text to continue.",
 "defaultValue": ""
 }
 ]
 }
 After
 JSON
 {
 "schema": 1,
 "description": "Given a text input, continue it with additional text.",
 "execution_settings": {
 "default": {
 "max_tokens": 4000,
 "temperature": 0.3,
 "top_p": 0.5,
 "presence_penalty": 0.0,
 "frequency_penalty": 0.0
 },
    "service1": {
      "model_id": "gpt-4",
      "max_tokens": 200,
      "temperature": 0.2,
      "top_p": 0.0,
      "presence_penalty": 0.0,
      "frequency_penalty": 0.0,
      "stop_sequences": ["Human", "AI"]
    },
    "service2": {
      "model_id": "gpt-3.5_turbo",
      "max_tokens": 256,
      "temperature": 0.3,
      "top_p": 0.0,
      "presence_penalty": 0.0,
      "frequency_penalty": 0.0,
      "stop_sequences": ["Human", "AI"]
    }
  },
  "input_variables": [
    {
      "name": "input",
      "description": "The text to continue.",
      "default": ""
    }
  ]
 }
OpenAI Connector Migration Guide
 Article • 09/24/2024
 Coming as part of the new 1.18 version of Semantic Kernel we migrated our 
OpenAI and
 AzureOpenAI services to use the new 
OpenAI SDK v2.0 and 
Azure OpenAI SDK v2.0 SDKs.
 As those changes were major breaking changes when implementing ours we looked
 forward to break as minimal as possible the dev experience.
 This guide prepares you for the migration that you may need to do to use our new
 OpenAI Connector is a complete rewrite of the existing OpenAI Connector and is
 designed to be more efficient, reliable, and scalable. This manual will guide you through
 the migration process and help you understand the changes that have been made to
 the OpenAI Connector.
 Those changes are needed for anyone using 
OpenAI or 
AzureOpenAI connectors with
 Semantic Kernel version 
1.18.0-rc or above.
 1. Package Setup when using Azure only
 services
 If you are working with Azure services you will need to change the package from
 Microsoft.SemanticKernel.Connectors.OpenAI to
 Microsoft.SemanticKernel.Connectors.AzureOpenAI. This is necessary as we created two
 distinct connectors for each.
） Important
 The 
Microsoft.SemanticKernel.Connectors.AzureOpenAI package depends on the
 Microsoft.SemanticKernel.Connectors.OpenAI package so there's no need to add
 both to your project when using 
OpenAI related types.
 diff
 Before- using Microsoft.SemanticKernel.Connectors.OpenAI;
 After
 + using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
1.1 AzureOpenAIClient
 When using Azure with OpenAI, before where you were using 
need to update your code to use the new 
OpenAIClient you will
 AzureOpenAIClient type.
 1.2 Services
 All services below now belong to the 
Microsoft.SemanticKernel.Connectors.AzureOpenAI
 namespace.
 AzureOpenAIAudioToTextService
 AzureOpenAIChatCompletionService
 AzureOpenAITextEmbeddingGenerationService
 AzureOpenAITextToAudioService
 AzureOpenAITextToImageService
 2. Text Generation Deprecation
 The latest 
OpenAI SDK does not support text generation modality, when migrating to
 their underlying SDK we had to drop support as well and remove 
specific services.
 If you were using OpenAI's 
TextGeneration
 gpt-3.5-turbo-instruct legacy model with any of the
 OpenAITextGenerationService or 
AzureOpenAITextGenerationService you will need to
 update your code to target a chat completion model instead, using
 OpenAIChatCompletionService or 
AzureOpenAIChatCompletionService instead.
７ Note
 OpenAI and AzureOpenAI 
ChatCompletion services also implement the
 ITextGenerationService interface and that may not require any changes to your
 code if you were targeting the 
ITextGenerationService interface.
 tags: 
AddOpenAITextGeneration, 
AddAzureOpenAITextGeneration
 3. ChatCompletion Multiple Choices
 Deprecated
The latest 
OpenAI SDK does not support multiple choices, when migrating to their
 underlying SDK we had to drop the support and remove 
ResultsPerPrompt also from the
 OpenAIPromptExecutionSettings.
 tags: 
results_per_prompt
 4. OpenAI File Service Deprecation
 The 
OpenAIFileService was deprecated in the latest version of the OpenAI Connector.
 We strongly recommend to update your code to use the new
 OpenAIClient.GetFileClient() for file management operations.
 5. OpenAI ChatCompletion custom endpoint
 The 
OpenAIChatCompletionService experimental constructor for custom endpoints will
 not attempt to auto-correct the endpoint and use it as is.
 We have the two only specific cases where we attempted to auto-correct the endpoint.
 1. If you provided 
chat/completions path before. Now those need to be removed as
 they are added automatically to the end of your original endpoint by 
diff- http://any-host-and-port/v1/chat/completions
 + http://any-host-and-port/v1
 OpenAI SDK.
 2. If you provided a custom endpoint without any path. We won't be adding the 
v1/
 as the first path. Now the 
v1 path needs to provided as part of your endpoint.
 diff- http://any-host-and-port/
 + http://any-host-and-port/v1
 6. SemanticKernel MetaPackage
 To be retrocompatible with the new OpenAI and AzureOpenAI Connectors, our
 Microsoft.SemanticKernel meta package changed its dependency to use the new
 Microsoft.SemanticKernel.Connectors.AzureOpenAI package that depends on the
Microsoft.SemanticKernel.Connectors.OpenAI package. This way if you are using the
 metapackage, no change is needed to get access to 
Azure related types.
 7. Chat Message Content Changes
 7.1 OpenAIChatMessageContent
 The 
Tools property type has changed from
 IReadOnlyList<ChatCompletionsToolCall> to 
IReadOnlyList<ChatToolCall>.
 Inner content type has changed from 
ChatCompletionsFunctionToolCall to
 ChatToolCall.
 Metadata type 
FunctionToolCalls has changed from
 IEnumerable<ChatCompletionsFunctionToolCall> to 
IEnumerable<ChatToolCall>.
 7.2 OpenAIStreamingChatMessageContent
 The 
FinishReason property type has changed from 
CompletionsFinishReason to
 FinishReason.
 The 
ToolCallUpdate property has been renamed to 
has changed from 
StreamingToolCallUpdate? to
 IReadOnlyList<StreamingToolCallUpdate>?.
 The 
ToolCallUpdates and its type
 AuthorName property is not initialized because it's not provided by the
 underlying library anymore.
 8. Metrics for AzureOpenAI Connector
 The meter 
s_meter = new("Microsoft.SemanticKernel.Connectors.OpenAI"); and the
 relevant counters still have old names that contain "openai" in them, such as:
 semantic_kernel.connectors.openai.tokens.prompt
 semantic_kernel.connectors.openai.tokens.completion
 semantic_kernel.connectors.openai.tokens.total
 9. Using Azure with your data (Data Sources)
 With the new 
AzureOpenAIClient, you can now specify your datasource thru the options
 and that requires a small change in your code to the new type.
Before
 C#
 After
 C#
 Tags: WithData, AzureOpenAIChatCompletionWithDataConfig,
 AzureOpenAIChatCompletionWithDataService
 Breaking glass scenarios are scenarios where you may need to update your code to use
 the new OpenAI Connector. Below are some of the breaking changes that you may need
 to be aware of.
 Some of the keys in the content metadata dictionary have changed and removed.
 var promptExecutionSettings = new OpenAIPromptExecutionSettings
 {
    AzureChatExtensionsOptions = new AzureChatExtensionsOptions
    {
        Extensions = [ new AzureSearchChatExtensionConfiguration
        {
            SearchEndpoint = new 
Uri(TestConfiguration.AzureAISearch.Endpoint),
            Authentication = new 
OnYourDataApiKeyAuthenticationOptions(TestConfiguration.AzureAISearch.ApiKey
 ),
            IndexName = TestConfiguration.AzureAISearch.IndexName
        }]
    };
 };
 var promptExecutionSettings = new AzureOpenAIPromptExecutionSettings
 {
    AzureChatDataSource = new AzureSearchChatDataSource
    {
         Endpoint = new Uri(TestConfiguration.AzureAISearch.Endpoint),
         Authentication = 
DataSourceAuthentication.FromApiKey(TestConfiguration.AzureAISearch.ApiKey),
         IndexName = TestConfiguration.AzureAISearch.IndexName
    }
 };
 10. Breaking glass scenarios
 10.1 KernelContent Metadata
Changed: 
Created -> 
CreatedAt
 Changed: 
LogProbabilityInfo -> 
Changed: 
ContentTokenLogProbabilities
 PromptFilterResults -> 
ContentFilterResultForPrompt
 Changed: 
ContentFilterResultsForPrompt -> 
ContentFilterResultForResponse
 Removed: 
FinishDetails
 Removed: 
Index
 Removed: 
Enhancements
 10.2 Prompt Filter Results
 The 
PromptFilterResults metadata type has changed from
 IReadOnlyList<ContentFilterResultsForPrompt> to 
10.3 Content Filter Results
 The 
ContentFilterResultForPrompt.
 ContentFilterResultsForPrompt type has changed from
 ContentFilterResultsForChoice to 
10.4 Finish Reason
 ContentFilterResultForResponse.
 The FinishReason metadata string value has changed from 
stop to 
Stop
 10.5 Tool Calls
 The ToolCalls metadata string value has changed from 
tool_calls to 
10.6 LogProbs / Log Probability Info
 The 
ToolCalls
 LogProbabilityInfo type has changed from 
ChatChoiceLogProbabilityInfo to
 IReadOnlyList<ChatTokenLogProbabilityInfo>.
 10.7 Token Usage
 The Token usage naming convention from 
OpenAI changed from 
tokens to 
Output and 
Completion, 
Prompt
 Input respectively. You will need to update your code to use the
 new naming.
 The type also changed from 
CompletionsUsage to 
ChatTokenUsage.
Example of Token Usage Metadata Changes
 diff
 Before- var usage = FunctionResult.Metadata?["Usage"] as CompletionsUsage;- var completionTokesn = usage?.CompletionTokens;- var promptTokens = usage?.PromptTokens;
 After
 + var usage = FunctionResult.Metadata?["Usage"] as ChatTokenUsage;
 + var promptTokens = usage?.InputTokens;
 + var completionTokens = completionTokens: usage?.OutputTokens;
 10.8 OpenAIClient
 The 
OpenAIClient type previously was a Azure specific namespace type but now it is an
 OpenAI SDK namespace type, you will need to update your code to use the new
 OpenAIClient type.
 When using Azure, you will need to update your code to use the new 
type.
 10.9 OpenAIClientOptions
 The 
AzureOpenAIClient
 OpenAIClientOptions type previously was a Azure specific namespace type but now
 it is an 
OpenAI SDK namespace type, you will need to update your code to use the new
 AzureOpenAIClientOptions type if you are using the new 
AzureOpenAIClient with any of
 the specific options for the Azure client.
 10.10 Pipeline Configuration
 The new 
OpenAI SDK uses a different pipeline configuration, and has a dependency on
 System.ClientModel package. You will need to update your code to use the new
 HttpClientPipelineTransport transport configuration where before you were using
 HttpClientTransport from 
Azure.Core.Pipeline.
 Example of Pipeline Configuration
 diff
 var clientOptions = new OpenAIClientOptions
 {
 Before: From Azure.Core.Pipeline
-    Transport = new HttpClientTransport(httpClient),-    RetryPolicy = new RetryPolicy(maxRetries: 0), // Disable Azure SDK 
retry policy if and only if a custom HttpClient is provided.-    Retry = { NetworkTimeout = Timeout.InfiniteTimeSpan } // Disable Azure 
SDK default timeout
 After: From OpenAI SDK -> System.ClientModel
 +    Transport = new HttpClientPipelineTransport(httpClient),
 +    RetryPolicy = new ClientRetryPolicy(maxRetries: 0); // Disable retry 
policy if and only if a custom HttpClient is provided.
 +    NetworkTimeout = Timeout.InfiniteTimeSpan; // Disable default timeout
 };
Function Calling Migration Guide
 Article • 09/24/2024
 Semantic Kernel is gradually transitioning from the current function calling capabilities,
 represented by the 
ToolCallBehavior class, to the new enhanced capabilities,
 represented by the 
FunctionChoiceBehavior class. The new capability is service-agnostic
 and is not tied to any specific AI service, unlike the current model. Therefore, it resides in
 Semantic Kernel abstractions and will be used by all AI connectors working with
 function-calling capable AI models.
 This guide is intended to help you to migrate your code to the new function calling
 capabilities.
 Migrate
 ToolCallBehavior.AutoInvokeKernelFunctions
 behavior
 The 
ToolCallBehavior.AutoInvokeKernelFunctions behavior is equivalent to the
 FunctionChoiceBehavior.Auto behavior in the new model.
 C#
 // Before
 var executionSettings = new OpenAIPromptExecutionSettings { ToolCallBehavior 
= ToolCallBehavior.AutoInvokeKernelFunctions };
 // After
 var executionSettings = new OpenAIPromptExecutionSettings { 
FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };
 Migrate
 ToolCallBehavior.EnableKernelFunctions
 behavior
 The 
ToolCallBehavior.EnableKernelFunctions behavior is equivalent to the
 FunctionChoiceBehavior.Auto behavior with disabled auto invocation.
 C#
The ToolCallBehavior.EnableFunctions behavior is equivalent to the
 FunctionChoiceBehavior.Auto behavior that configured with list of functions with
 disabled auto invocation.
 C#
 The ToolCallBehavior.RequireFunction behavior is equivalent to the
 FunctionChoiceBehavior.Required behavior that configured with list of functions with
 disabled auto invocation.
 C#
 // Before
 var executionSettings = new OpenAIPromptExecutionSettings { ToolCallBehavior 
= ToolCallBehavior.EnableKernelFunctions };
 // After
 var executionSettings = new OpenAIPromptExecutionSettings { 
FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(autoInvoke: false) };
 Migrate ToolCallBehavior.EnableFunctions
 behavior
 var function = kernel.CreateFunctionFromMethod(() => DayOfWeek.Friday, 
"GetDayOfWeek", "Returns the current day of the week.");
 // Before
 var executionSettings = new OpenAIPromptExecutionSettings() { 
ToolCallBehavior = ToolCallBehavior.EnableFunctions(functions: 
[function.Metadata.ToOpenAIFunction()]) };
 // After
 var executionSettings = new OpenAIPromptExecutionSettings { 
FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(functions: [function], 
autoInvoke: false) };
 Migrate ToolCallBehavior.RequireFunction
 behavior
 var function = kernel.CreateFunctionFromMethod(() => DayOfWeek.Friday, 
"GetDayOfWeek", "Returns the current day of the week.");
 // Before
 var executionSettings = new OpenAIPromptExecutionSettings() { 
Function calling functionality in Semantic Kernel allows developers to access a list of
 functions chosen by the AI model in two ways:
 Using connector-specific function call classes like ChatToolCall or
 ChatCompletionsFunctionToolCall, available via the ToolCalls property of the
 OpenAI-specific OpenAIChatMessageContent item in chat history.
 Using connector-agnostic function call classes like FunctionCallContent, available
 via the Items property of the connector-agnostic ChatMessageContent item in chat
 history.
 Both ways are supported at the moment by the current and new models. However, we
 strongly recommend using the connector-agnostic approach to access function calls, as
 it is more flexible and allows your code to work with any AI connector that supports the
 new function-calling model. Moreover, considering that the current model will be
 deprecated soon, now is a good time to migrate your code to the new model to avoid
 breaking changes in the future.
 So, if you use Manual Function Invocation with the connector-specific function call
 classes like in this code snippet:
 C#
 ToolCallBehavior = ToolCallBehavior.RequireFunction(functions: 
[function.Metadata.ToOpenAIFunction()]) };
 // After
 var executionSettings = new OpenAIPromptExecutionSettings { 
FunctionChoiceBehavior = FunctionChoiceBehavior.Required(functions: 
[function], autoInvoke: false) };
 Replace the usage of connector-specific
 function call classes
 using System.Text.Json;
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.ChatCompletion;
 using Microsoft.SemanticKernel.Connectors.OpenAI;
 using OpenAI.Chat;
 var chatHistory = new ChatHistory();
 var settings = new OpenAIPromptExecutionSettings() { ToolCallBehavior = 
ToolCallBehavior.EnableKernelFunctions };
 var result = await 
You can refactor it to use the connector-agnostic classes:
 C#
 chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, 
kernel);
 // Current way of accessing function calls using connector specific classes.
 var toolCalls = 
((OpenAIChatMessageContent)result).ToolCalls.OfType<ChatToolCall>
 ().ToList();
 while (toolCalls.Count > 0)
 {
    // Adding function call from AI model to chat history
    chatHistory.Add(result);
    // Iterating over the requested function calls and invoking them
    foreach (var toolCall in toolCalls)
    {
        string content = kernel.Plugins.TryGetFunctionAndArguments(toolCall, 
out KernelFunction? function, out KernelArguments? arguments) ?
            JsonSerializer.Serialize((await function.InvokeAsync(kernel, 
arguments)).GetValue<object>()) :
            "Unable to find function. Please try again!";
        // Adding the result of the function call to the chat history
        chatHistory.Add(new ChatMessageContent(
            AuthorRole.Tool,
            content,
            metadata: new Dictionary<string, object?>(1) { { 
OpenAIChatMessageContent.ToolIdProperty, toolCall.Id } }));
    }
    // Sending the functions invocation results back to the AI model to get 
the final response
    result = await 
chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, 
kernel);
    toolCalls = 
((OpenAIChatMessageContent)result).ToolCalls.OfType<ChatToolCall>
 ().ToList();
 }
 using Microsoft.SemanticKernel;
 using Microsoft.SemanticKernel.ChatCompletion;
 var chatHistory = new ChatHistory();
 var settings = new PromptExecutionSettings() { FunctionChoiceBehavior = 
FunctionChoiceBehavior.Auto(autoInvoke: false) };
 var messageContent = await 
The code snippets above demonstrate how to migrate your code that uses the OpenAI
 AI connector. A similar migration process can be applied to the Gemini and Mistral AI
 connectors when they are updated to support the new function calling model.
 Now after you have migrated your code to the new function calling model, you can
 proceed to learn how to configure various aspects of the model that might better
 correspond to your specific scenarios by referring to the function choice behaviors
 article
 chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, 
kernel);
 // New way of accessing function calls using connector agnostic function 
calling model classes.
 var functionCalls = 
FunctionCallContent.GetFunctionCalls(messageContent).ToArray();
 while (functionCalls.Length != 0)
 {
    // Adding function call from AI model to chat history
    chatHistory.Add(messageContent);
    // Iterating over the requested function calls and invoking them
    foreach (var functionCall in functionCalls)
    {
        var result = await functionCall.InvokeAsync(kernel);
        chatHistory.Add(result.ToChatMessage());
    }
    // Sending the functions invocation results to the AI model to get the 
final response
    messageContent = await 
chatCompletionService.GetChatMessageContentAsync(chatHistory, settings, 
kernel);
    functionCalls = 
FunctionCallContent.GetFunctionCalls(messageContent).ToArray();
 }
 Next steps
 Function Choice Behavior
 Stepwise Planner Migration Guide
 06/11/2025
 This migration guide shows how to migrate from FunctionCallingStepwisePlanner to a new
 recommended approach for planning capability - Auto Function Calling. The new approach
 produces the results more reliably and uses fewer tokens compared to
 FunctionCallingStepwisePlanner.
 Following code shows how to generate a new plan with Auto Function Calling by using
 FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(). After sending a request to AI
 model, the plan will be located in ChatHistory object where a message with Assistant role will
 contain a list of functions (steps) to call.
 Old approach:
 C#
 New approach:
 C#
 Plan generation
 Kernel kernel = Kernel
    .CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4", 
Environment.GetEnvironmentVariable("OpenAI__ApiKey"))
    .Build();
 FunctionCallingStepwisePlanner planner = new();
 FunctionCallingStepwisePlannerResult result = await planner.ExecuteAsync(kernel, 
"Check current UTC time and return current weather in Boston city.");
 ChatHistory generatedPlan = result.ChatHistory;
 Kernel kernel = Kernel
    .CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4", 
Environment.GetEnvironmentVariable("OpenAI__ApiKey"))
    .Build();
 IChatCompletionService chatCompletionService = 
kernel.GetRequiredService<IChatCompletionService>();
 ChatHistory chatHistory = [];
Following code shows how to execute a new plan with Auto Function Calling by using
 FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(). This approach is useful when only
 result is needed without plan steps. In this case, Kernel object can be used to pass a goal to
 InvokePromptAsync method. The result of plan execution will be located in FunctionResult
 object.
 Old approach:
 C#
 New approach:
 C#
 chatHistory.AddUserMessage("Check current UTC time and return current weather in 
Boston city.");
 OpenAIPromptExecutionSettings executionSettings = new() { FunctionChoiceBehavior = 
FunctionChoiceBehavior.Auto() };
 await chatCompletionService.GetChatMessageContentAsync(chatHistory, 
executionSettings, kernel);
 ChatHistory generatedPlan = chatHistory;
 Execution of the new plan
 Kernel kernel = Kernel
    .CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4", 
Environment.GetEnvironmentVariable("OpenAI__ApiKey"))
    .Build();
 FunctionCallingStepwisePlanner planner = new();
 FunctionCallingStepwisePlannerResult result = await planner.ExecuteAsync(kernel, 
"Check current UTC time and return current weather in Boston city.");
 string planResult = result.FinalAnswer;
 Kernel kernel = Kernel
    .CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4", 
Environment.GetEnvironmentVariable("OpenAI__ApiKey"))
    .Build();
 OpenAIPromptExecutionSettings executionSettings = new() { FunctionChoiceBehavior = 
FunctionChoiceBehavior.Auto() };
Following code shows how to execute an existing plan with Auto Function Calling by using
 FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(). This approach is useful when
 ChatHistory is already present (e.g. stored in cache) and it should be re-executed again and
 final result should be provided by AI model.
 Old approach:
 C#
 New approach:
 C#
 FunctionResult result = await kernel.InvokePromptAsync("Check current UTC time and 
return current weather in Boston city.", new(executionSettings));
 string planResult = result.ToString();
 Execution of the existing plan
 Kernel kernel = Kernel
    .CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4", 
Environment.GetEnvironmentVariable("OpenAI__ApiKey"))
    .Build();
 FunctionCallingStepwisePlanner planner = new();
 ChatHistory existingPlan = GetExistingPlan(); // plan can be stored in database  
or cache for reusability.
 FunctionCallingStepwisePlannerResult result = await planner.ExecuteAsync(kernel, 
"Check current UTC time and return current weather in Boston city.", 
existingPlan);
 string planResult = result.FinalAnswer;
 Kernel kernel = Kernel
    .CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4", 
Environment.GetEnvironmentVariable("OpenAI__ApiKey"))
    .Build();
 IChatCompletionService chatCompletionService = 
kernel.GetRequiredService<IChatCompletionService>();
 ChatHistory existingPlan = GetExistingPlan(); // plan can be stored in database or 
cache for reusability.
OpenAIPromptExecutionSettings executionSettings = new() { FunctionChoiceBehavior = 
FunctionChoiceBehavior.Auto() };
 ChatMessageContent result = await 
chatCompletionService.GetChatMessageContentAsync(existingPlan, executionSettings, 
kernel);
 string planResult = result.Content;
 The code snippets above demonstrate how to migrate your code that uses Stepwise Planner to
 use Auto Function Calling. Learn more about Function Calling with chat completion.
Migrating from Memory Stores to Vector
 Stores
 Article • 05/19/2025
 Semantic Kernel provides two distinct abstractions for interacting with vector stores.
 1. A set of legacy abstractions where the primary interface is
 Microsoft.SemanticKernel.Memory.IMemoryStore.
 2. A new and improved set of abstractions where the primary abstract base class is
 Microsoft.Extensions.VectorData.VectorStore.
 The Vector Store abstractions provide more functionality than what the Memory Store
 abstractions provide, e.g. being able to define your own schema, supporting multiple vectors
 per record (database permitting), supporting more vector types than 
ReadOnlyMemory<float>,
 etc. We recommend using the Vector Store abstractions instead of the Memory Store
 abstractions.
  Tip
 For a more detailed comparison of the Memory Store and Vector Store abstractions see
 here.
 Migrating from Memory Stores to Vector Stores
 See the Legacy Semantic Kernel Memory Stores page for instructions on how to migrate.
Kernel Events and Filters Migration
 Article • 11/21/2024
７ Note
 This document addresses functionality from Semantic Kernel versions prior to
 v1.10.0. For the latest information about Filters, refer to this 
documentation.
 Semantic Kernel enables control over function execution using Filters. Over time,
 multiple versions of the filtering logic have been introduced: starting with Kernel Events,
 followed by the first version of Filters (
 IFunctionFilter, 
IPromptFilter), and
 culminating in the latest version (
 IFunctionInvocationFilter, 
IPromptRenderFilter). This
 guide explains how to migrate from Kernel Events and the first version of Filters to the
 latest implementation.
 The latest version of filters has graduated from experimental status and is now officially
 released as a stable feature.
 Migration from Kernel Events
 Kernel Events were the initial mechanism for intercepting function operations in
 Semantic Kernel. They were deprecated in version 1.2.0 and replaced with the following
 improvements:
 1. Events were replaced by interfaces for greater flexibility.
 2. Implementations can now be registered with the Kernel using a dependency
 injection container (DI).
 The examples below illustrate how to transition to the new function filtering logic.
 Function Invocation
 Old implementation with Kernel Events:
 C#
 Kernel kernel = Kernel.CreateBuilder()
 .AddOpenAIChatCompletion(
 modelId: "model-id",
 apiKey: "api-key")
 .Build();
New implementation with function invocation filter:
 C#
 void PreHandler(object? sender, FunctionInvokingEventArgs e)
 {
    Console.WriteLine($"Function {e.Function.Name} is about to be 
invoked.");
 }
 void PostHandler(object? sender, FunctionInvokedEventArgs e)
 {
    Console.WriteLine($"Function {e.Function.Name} was invoked.");
 }
 kernel.FunctionInvoking += PreHandler;
 kernel.FunctionInvoked += PostHandler;
 var result = await kernel.InvokePromptAsync("Write a random paragraph about 
universe.");
 Console.WriteLine($"Function Result: {result}");
 public sealed class FunctionInvocationFilter : IFunctionInvocationFilter
 {
    public async Task OnFunctionInvocationAsync(FunctionInvocationContext 
context, Func<FunctionInvocationContext, Task> next)
    {
        Console.WriteLine($"Function {context.Function.Name} is about to be 
invoked.");
        await next(context);
        Console.WriteLine($"Function {context.Function.Name} was invoked.");
    }
 }
 IKernelBuilder kernelBuilder = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "model-id",
        apiKey: "api-key");
 // Option 1: Add filter via Dependency Injection (DI)
 kernelBuilder.Services.AddSingleton<IFunctionInvocationFilter, 
FunctionInvocationFilter>();
 Kernel kernel = kernelBuilder.Build();
 // Option 2: Add filter directly to the Kernel instance
 kernel.FunctionInvocationFilters.Add(new FunctionInvocationFilter());
 var result = await kernel.InvokePromptAsync("Write a random paragraph about 
universe.");
Alternate implementation with inline logic:
 C#
 Old implementation with Kernel Events:
 C#
 Console.WriteLine($"Function Result: {result}");
 public sealed class FunctionInvocationFilter(Func<FunctionInvocationContext, 
Func<FunctionInvocationContext, Task>, Task> onFunctionInvocation) : 
IFunctionInvocationFilter
 {
    public async Task OnFunctionInvocationAsync(FunctionInvocationContext 
context, Func<FunctionInvocationContext, Task> next)
    {
        await onFunctionInvocation(context, next);
    }
 }
 Kernel kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "model-id",
        apiKey: "api-key")
    .Build();
 kernel.FunctionInvocationFilters.Add(new FunctionInvocationFilter(async 
(context, next) =>
 {
    Console.WriteLine($"Function {context.Function.Name} is about to be 
invoked.");
    await next(context);
    Console.WriteLine($"Function {context.Function.Name} was invoked.");
 }));
 var result = await kernel.InvokePromptAsync("Write a random paragraph about 
universe.");
 Console.WriteLine($"Function Result: {result}");
 Prompt Rendering
 Kernel kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "model-id",
        apiKey: "api-key")
    .Build();
New implementation with prompt render filter:
 C#
 void RenderingHandler(object? sender, PromptRenderingEventArgs e)
 {
    Console.WriteLine($"Prompt rendering for function {e.Function.Name} is 
about to be started.");
 }
 void RenderedHandler(object? sender, PromptRenderedEventArgs e)
 {
    Console.WriteLine($"Prompt rendering for function {e.Function.Name} has 
completed.");
    e.RenderedPrompt += " USE SHORT, CLEAR, COMPLETE SENTENCES.";
 }
 kernel.PromptRendering += RenderingHandler;
 kernel.PromptRendered += RenderedHandler;
 var result = await kernel.InvokePromptAsync("Write a random paragraph about 
universe.");
 Console.WriteLine($"Function Result: {result}");
 public sealed class PromptRenderFilter : IPromptRenderFilter
 {
    public async Task OnPromptRenderAsync(PromptRenderContext context, 
Func<PromptRenderContext, Task> next)
    {
        Console.WriteLine($"Prompt rendering for function 
{context.Function.Name} is about to be started.");
        await next(context);
        Console.WriteLine($"Prompt rendering for function 
{context.Function.Name} has completed.");
        context.RenderedPrompt += " USE SHORT, CLEAR, COMPLETE SENTENCES.";
    }
 }
 IKernelBuilder kernelBuilder = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "model-id",
        apiKey: "api-key");
 // Option 1: Add filter via DI
 kernelBuilder.Services.AddSingleton<IPromptRenderFilter, PromptRenderFilter>
 ();
 Kernel kernel = kernelBuilder.Build();
 // Option 2: Add filter directly to the Kernel instance
 kernel.PromptRenderFilters.Add(new PromptRenderFilter());
Inline logic example:
 C#
 The first version of Filters introduced a structured approach for function and prompt
 interception but lacked support for asynchronous operations and consolidated
 pre/post-operation handling. These limitations were addressed in Semantic Kernel
 v1.10.0.
 var result = await kernel.InvokePromptAsync("Write a random paragraph about 
universe.");
 Console.WriteLine($"Function Result: {result}");
 public sealed class PromptRenderFilter(Func<PromptRenderContext, 
Func<PromptRenderContext, Task>, Task> onPromptRender) : IPromptRenderFilter
 {
    public async Task OnPromptRenderAsync(PromptRenderContext context, 
Func<PromptRenderContext, Task> next)
    {
        await onPromptRender(context, next);
    }
 }
 Kernel kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "model-id",
        apiKey: "api-key")
    .Build();
 kernel.PromptRenderFilters.Add(new PromptRenderFilter(async (context, next) 
=>
 {
    Console.WriteLine($"Prompt rendering for function 
{context.Function.Name} is about to be started.");
    await next(context);
    Console.WriteLine($"Prompt rendering for function 
{context.Function.Name} has completed.");
    context.RenderedPrompt += " USE SHORT, CLEAR, COMPLETE SENTENCES.";
 }));
 var result = await kernel.InvokePromptAsync("Write a random paragraph about 
universe.");
 Console.WriteLine($"Function Result: {result}");
 Migration from Filters v1
The interfaces were renamed as follows:
 IFunctionFilter → IFunctionInvocationFilter
 IPromptFilter → IPromptRenderFilter
 Additionally, the interface structure was updated to replace the two-method approach
 with a single asynchronous method. This simplifies implementation, streamlines
 exception handling, and allows seamless integration of asynchronous operations using
 the async/await pattern.
 Filters v1 syntax:
 C#
 Updated syntax:
 C#
 Function Invocation
 public sealed class MyFilter : IFunctionFilter
 {
    public void OnFunctionInvoking(FunctionInvokingContext context)
    {
        // Method which is executed before function invocation.
    }
    public void OnFunctionInvoked(FunctionInvokedContext context)
    {
        // Method which is executed after function invocation.
    }
 }
 public sealed class FunctionInvocationFilter : IFunctionInvocationFilter
 {
    public async Task OnFunctionInvocationAsync(FunctionInvocationContext 
context, Func<FunctionInvocationContext, Task> next)
    {
        // Perform some actions before function invocation
        await next(context);
        // Perform some actions after function invocation
    }
 }
 Prompt Rendering
Filters v1 syntax:
 C#
 Updated syntax:
 C#
 For the latest information about Filters, refer to this documentation.
 public sealed class PromptFilter : IPromptFilter
 {
    public void OnPromptRendering(PromptRenderingContext context)
    {
        // Perform some actions before prompt rendering
    }
    public void OnPromptRendered(PromptRenderedContext context)
    {
        // Perform some actions after prompt rendering
    }
 }
 public class PromptFilter: IPromptRenderFilter
 {
    public async Task OnPromptRenderAsync(PromptRenderContext context, 
Func<PromptRenderContext, Task> next)
    {
        // Perform some actions before prompt rendering
        await next(context);
        // Perform some actions after prompt rendering
    }
 }
Agent Framework Release Candidate
 Migration Guide
 Article • 03/25/2025
 As we transition some agents from the experimental stage to the release candidate
 stage, we have updated the APIs to simplify and streamline their use. Refer to the
 specific scenario guide to learn how to update your existing code to work with the latest
 available APIs.
 Common Agent Invocation API
 In version 1.43.0 we are releasing a new common agent invocation API, that will allow all
 agent types to be invoked via a common API.
 To enable this new API we are introducing the concept of an 
AgentThread, which
 represents a conversation thread and abstracts away the different thread management
 requirements of different agent types. For some agent types it will also, in future, allow
 different thread imlementations to be used with the same agent.
 The common 
Invoke methods that we are introducing allow you to provide the
 message(s) that you want to pass to the agent and an optional 
AgentThread. If an
 AgentThread is provided, this will continue the conversation already on the 
AgentThread.
 If no 
AgentThread is provided, a new default thread will be created and returned as part
 of the response.
 It is also possible to manually create an 
AgentThread instance, for example in cases
 where you may have a thread id from the underlying agent service, and you want to
 continue that thread. You may also want to customize the options for the thread, e.g.
 associate tools.
 Here is a simple example of how any agent can now be used with agent agnostic code.
 C#
 private async Task UseAgentAsync(Agent agent, AgentThread? agentThread = 
null)
 {
 // Invoke the agent, and continue the existing thread if provided.
 var responses = agent.InvokeAsync(new 
ChatMessageContent(AuthorRole.User, "Hi"), agentThread);
 // Output results.
These changes were applied in:
 PR #11116
 The AzureAIAgent currently only supports threads of type AzureAIAgentThread.
 In addition to allowing a thread to be created for you automatically on agent invocation,
 you can also manually construct an instance of an AzureAIAgentThread.
 AzureAIAgentThread supports being created with customized tools and metadata, plus
 messages to seed the conversation with.
 C#
 You can also construct an instance of an AzureAIAgentThread that continues an existing
 conversation.
 C#
    await foreach (AgentResponseItem<ChatMessageContent> response in 
responses)
    {
        Console.WriteLine(response);
        agentThread = response.Thread;
    }
    // Delete the thread if required.
    if (agentThread is not null)
    {
        await agentThread.DeleteAsync();
    }
 }
 Azure AI Agent Thread Options
 AgentThread thread = new AzureAIAgentThread(
    agentsClient,
    messages: seedMessages,
    toolResources: tools,
    metadata: metadata);
 AgentThread thread = new AzureAIAgentThread(
    agentsClient,
    id: "my-existing-thread-id");
 Bedrock Agent Thread Options
The BedrockAgent currently only supports threads of type BedrockAgentThread.
 In addition to allowing a thread to be created for you automatically on agent invocation,
 you can also manually construct an instance of an BedrockAgentThread.
 C#
 You can also construct an instance of an BedrockAgentThread that continues an existing
 conversation.
 C#
 The ChatCompletionAgent currently only supports threads of type
 ChatHistoryAgentThread. ChatHistoryAgentThread uses an in-memory ChatHistory
 object to store the messages on the thread.
 In addition to allowing a thread to be created for you automatically on agent invocation,
 you can also manually construct an instance of an ChatHistoryAgentThread.
 C#
 You can also construct an instance of an ChatHistoryAgentThread that continues an
 existing conversation by passing in a ChatHistory object with the existing messages.
 C#
 AgentThread thread = new 
BedrockAgentThread(amazonBedrockAgentRuntimeClient);
 AgentThread thread = new BedrockAgentThread(
    amazonBedrockAgentRuntimeClient,
    sessionId: "my-existing-session-id");
 Chat Completion Agent Thread Options
 AgentThread thread = new ChatHistoryAgentThread();
 ChatHistory chatHistory = new([new ChatMessageContent(AuthorRole.User, 
"Hi")]);
 AgentThread thread = new ChatHistoryAgentThread(chatHistory: chatHistory);
 OpenAI Assistant Thread Options
The 
OpenAIAssistantAgent currently only supports threads of type
 OpenAIAssistantAgentThread.
 In addition to allowing a thread to be created for you automatically on agent invocation,
 you can also manually construct an instance of an 
OpenAIAssistantAgentThread.
 OpenAIAssistantAgentThread supports being created with customized tools and
 metadata, plus messages to seed the conversation with.
 C#
 AgentThread thread = new OpenAIAssistantAgentThread(
 assistantClient,
 messages: seedMessages,
 codeInterpreterFileIds: fileIds,
 vectorStoreId: "my-vector-store",
 metadata: metadata);
 You can also construct an instance of an 
existing conversation.
 C#
 OpenAIAssistantAgentThread that continues an
 AgentThread thread = new OpenAIAssistantAgentThread(
 assistantClient,
 id: "my-existing-thread-id");
 OpenAIAssistantAgent C# Migration Guide
 We recently applied a significant shift around the OpenAIAssistantAgent in the
 Semantic Kernel Agent Framework.
 These changes were applied in:
 PR #10583
 PR #10616
 PR #10633
 These changes are intended to:
 Align with the pattern for using for our AzureAIAgent .
 Fix bugs around static initialization pattern.
 Avoid limiting features based on our abstraction of the underlying SDK.
This guide provides step-by-step instructions for migrating your C# code from the old
 implementation to the new one. Changes include updates for creating assistants,
 managing the assistant lifecycle, handling threads, files, and vector stores.
 1. Client Instantiation
 Previously, 
OpenAIClientProvider was required for creating any 
This dependency has been simplified.
 New Way
 C#
 OpenAIAssistantAgent.
 OpenAIClient client = OpenAIAssistantAgent.CreateAzureOpenAIClient(new 
AzureCliCredential(), new Uri(endpointUrl));
 AssistantClient assistantClient = client.GetAssistantClient();
 Old Way (Deprecated)
 C#
 var clientProvider = new OpenAIClientProvider(...);
 2. Assistant Lifecycle
 Creating an Assistant
 You may now directly instantiate an 
OpenAIAssistantAgent using an existing or new
 Assistant definition from 
AssistantClient.
 New Way
 C#
 Assistant definition = await assistantClient.GetAssistantAsync(assistantId);
 OpenAIAssistantAgent agent = new(definition, client);
 Plugins can be directly included during initialization:
C#
 Creating a new assistant definition using an extension method:
 C#
 Previously, assistant definitions were managed indirectly.
 You may specify RunCreationOptions directly, enabling full access to underlying SDK
 capabilities.
 C#
 C#
 You can directly manage assistant deletion with AssistantClient.
 KernelPlugin plugin = KernelPluginFactory.CreateFromType<YourPlugin>();
 Assistant definition = await assistantClient.GetAssistantAsync(assistantId);
 OpenAIAssistantAgent agent = new(definition, client, [plugin]);
 Assistant assistant = await assistantClient.CreateAssistantAsync(
    model,
    name,
    instructions: instructions,
    enableCodeInterpreter: true);
 Old Way (Deprecated)
 3. Invoking the Agent
 New Way
 RunCreationOptions options = new(); // configure as needed
 var result = await agent.InvokeAsync(options);
 Old Way (Deprecated)
 var options = new OpenAIAssistantInvocationOptions();
 4. Assistant Deletion
C#
 Threads are now managed via AssistantAgentThread.
 C#
 Previously, thread management was indirect or agent-bound.
 C#
 File creation and deletion now utilize OpenAIFileClient.
 await assistantClient.DeleteAssistantAsync(agent.Id);
 5. Thread Lifecycle
 Creating a Thread
 New Way
 var thread = new AssistantAgentThread(assistantClient);
 // Calling CreateAsync is an optional step.
 // A thread will be created automatically on first use if CreateAsync was 
not called.
 // Note that CreateAsync is not on the AgentThread base implementation since 
not all
 // agent services support explicit thread creation.
 await thread.CreateAsync();
 Old Way (Deprecated)
 Thread Deletion
 var thread = new AssistantAgentThread(assistantClient, "existing-thread
id");
 await thread.DeleteAsync();
 6. File Lifecycle
 File Upload
C#
 C#
 Vector stores are managed directly via VectorStoreClient with convenient extension
 methods.
 C#
 C#
 Deprecated patterns are marked with [Obsolete]. To suppress obsolete warnings
 (CS0618), update your project file as follows:
 XML
 string fileId = await client.UploadAssistantFileAsync(stream, "<filename>");
 File Deletion
 await client.DeleteFileAsync(fileId);
 7. Vector Store Lifecycle
 Vector Store Creation
 string vectorStoreId = await client.CreateVectorStoreAsync([fileId1, 
fileId2], waitUntilCompleted: true);
 Vector Store Deletion
 await client.DeleteVectorStoreAsync(vectorStoreId);
 Backwards Compatibility
 <PropertyGroup>
  <NoWarn>$(NoWarn);CS0618</NoWarn>
 </PropertyGroup>
This migration guide helps you transition smoothly to the new implementation,
 simplifying client initialization, resource management, and integration with the Semantic
 Kernel .NET SDK
 AzureAIAgent Foundry GA Migration Guide
 06/03/2025
 In Semantic Kernel .NET 1.53.1+, .NET and Python developers using 
AzureAIAgent must to
 update the patterns they use to interact with the Azure AI Foundry in response to its move to
 GA.
 GA Foundry Project
 Must be created on or after May 19th, 2025
 Connect programatically using the Foundry Project's endpoint url.
 Requires Semantic Kernel version 1.53.1 and above.
 Based on package Azure.AI.Agents.Persistent
 Pre-GA Foundry Project
 Was created prior to May 19th, 2025
 Connect programatically using the Foundry Project's connection string.
 Continue to use Semantic Kernel versions below version 1.53.*
 Based on package Azure.AI.Projects version 1.0.0-beta.8
 Creating an Client
 Old Way
 c#
 AIProjectClient client = AzureAIAgent.CreateAzureAIClient("<connection string>", 
new AzureCliCredential());
 AgentsClient agentsClient = client.GetAgentsClient();
 New Way
 c#
 PersistentAgentsClient agentsClient = AzureAIAgent.CreateAgentsClient("
 <endpoint>", new AzureCliCredential());```
c#
 c#
 c#
 c#
 c#
 Creating an Agent
 Old Way
 Agent agent = await agentsClient.CreateAgentAsync(...);
 New Way
 PersistentAgent agent = await agentsClient.Administration.CreateAgentAsync(
 Deleting an Agent
 Old Way
 await agentsClient.DeleteAgentAsync("<agent id>");
 New Way
 await agentsClient.Administration.DeleteAgentAsync("<agent id>");
 Uploading Files
 Old Way
 AgentFile fileInfo = await agentsClient.UploadFileAsync(stream, 
AgentFilePurpose.Agents, "<file name>");
c#
 c#
 c#
 c#
 c#
 New Way
 PersistentAgentFileInfo fileInfo = await 
agentsClient.Files.UploadFileAsync(stream, PersistentAgentFilePurpose.Agents, "
 <file name>");
 Deleting Files
 Old Way
 await agentsClient.DeleteFileAsync("<file id>");
 New Way
 await agentsClient.Files.DeleteFileAsync("<file id>");
 Creating a VectorStore
 Old Way
 VectorStore fileStore = await agentsClient.CreateVectorStoreAsync(...);
 New Way
 PersistentAgentsVectorStore fileStore = await 
agentsClient.VectorStores.CreateVectorStoreAsync(...);
Deleting a VectorStore
 Old Way
 c#
 await agentsClient.DeleteVectorStoreAsync("<store id>");
 New Way
 c#
 await agentsClient.VectorStores.DeleteVectorStoreAsync("<store id>");
Vector Store changes - March 2025
 Article • 03/12/2025
 LINQ based filtering
 When doing vector searches it is possible to create a filter (in addition to the vector
 similarity) that act on data properties to constrain the list of records matched.
 This filter is changing to support more filtering options. Previously the filter would have
 been expressed using a custom 
VectorSearchFilter type, but with this update the filter
 would be expressed using LINQ expressions.
 The old filter clause is still preserved in a property called OldFilter, and will be removed
 in future.
 C#
 // Before
 var searchResult = await collection.VectorizedSearchAsync(
 searchVector,
 new() { Filter = new 
VectorSearchFilter().EqualTo(nameof(Glossary.Category), "External 
Definitions") });
 // After
 var searchResult = await collection.VectorizedSearchAsync(
 searchVector,
 new() { Filter = g => g.Category == "External Definitions" });
 // The old filter option is still available
 var searchResult = await collection.VectorizedSearchAsync(
 searchVector,
 new() { OldFilter = new 
VectorSearchFilter().EqualTo(nameof(Glossary.Category), "External 
Definitions") });
 Target Property Selection for Search
 When doing a vector search, it is possible to choose the vector property that the search
 should be executed against. Previously this was done via an option on the
 VectorSearchOptions class called 
VectorPropertyName. 
VectorPropertyName was a string
 that could contain the name of the target property.
VectorPropertyName is being obsoleted in favour of a new property called
 VectorProperty. 
VectorProperty is an expression that references the required property
 directly.
 C#
 // Before
 var options = new VectorSearchOptions() { VectorPropertyName = 
"DescriptionEmbedding" };
 // After
 var options = new VectorSearchOptions<MyRecord>() { VectorProperty = r => 
r.DescriptionEmbedding };
 Specifying 
VectorProperty will remain optional just like 
VectorPropertyName was
 optional. The behavior when not specifying the property name is changing. Previously if
 not specifying a target property, and more than one vector property existed on the data
 model, the search would target the first available vector property in the schema.
 Since the property which is 'first' can change in many circumstances unrelated to the
 search code, using this strategy is risky. We are therefore changing this behavior, so that
 if there are more than one vector property, one must be chosen.
 VectorSearchOptions change to generic type
 The 
VectorSearchOptions class is changing to 
VectorSearchOptions<TRecord>, to
 accomodate the LINQ based filtering and new property selectors metioned above.
 If you are currently constructing the options class without providing the name of the
 options class there will be no change. E.g. 
VectorizedSearchAsync(embedding, new() {
 Top = 5 }).
 On the other hand if you are using 
new with the type name, you will need to add the
 record type as a generic parameter.
 C#
 // Before
 var options = new VectorSearchOptions() { Top = 5 };
 // After
 var options = new VectorSearchOptions<MyRecord>() { Top = 5 };
Each VectorStore implementation allows you to pass a custom factory to use for
 constructing collections. This pattern is being removed and the recommended approach
 is now to inherit from the VectorStore where you want custom construction and override
 the GetCollection method.
 C#
 The DeleteRecordOptions and UpsertRecordOptions parameters have been removed
 from the DeleteAsync, DeleteBatchAsync, UpsertAsync and UpsertBatchAsync methods
 on the IVectorStoreRecordCollection<TKey, TRecord> interface.
 These parameters were all optional and the options classes did not contain any options
 to set.
 Removal of collection factories in favour of
 inheritance/decorator pattern
 // Before
 var vectorStore = new QdrantVectorStore(
    new QdrantClient("localhost"),
    new()
    {
        VectorStoreCollectionFactory = new 
CustomQdrantCollectionFactory(productDefinition)
    });
 // After
 public class QdrantCustomCollectionVectorStore(QdrantClient qdrantClient) : 
QdrantVectorStore(qdrantClient)
 {
    public override IVectorStoreRecordCollection<TKey, TRecord> 
GetCollection<TKey, TRecord>(string name, VectorStoreRecordDefinition? 
vectorStoreRecordDefinition = null)
    {
        // custom construction logic...
    }
 }
 var vectorStore = new QdrantCustomCollectionVectorStore(new 
QdrantClient("localhost"));
 Removal of DeleteRecordOptions and
 UpsertRecordOptions
If you were passing these options in the past, you will need to remove these with this
 update.
 C#
 // Before
 collection.DeleteAsync("mykey", new DeleteRecordOptions(), 
cancellationToken);
 // After
 collection.DeleteAsync("mykey", cancellationToken);
Vector Store changes - Preview 2 - April
 2025
 Article•04/30/2025
 The April 2025 update introduces built-in support for embedding generation directly within the
 vector store. By configuring an embedding generator, you can now automatically generate
 embeddings for vector properties without needing to precompute them externally. This feature
 simplifies workflows and reduces the need for additional preprocessing steps.
 Embedding generators implementing the Microsoft.Extensions.AI abstractions are supported
 and can be configured at various levels:
 1. On the Vector Store: You can set a default embedding generator for the entire vector
 store. This generator will be used for all collections and properties unless overridden.
 C#
 2. On a Collection: You can configure an embedding generator for a specific collection,
 overriding the store-level generator.
 C#
 Built-in Support for Embedding Generation in the
 Vector Store
 Configuring an Embedding Generator
 using Microsoft.Extensions.AI;
 using Microsoft.SemanticKernel.Connectors.Qdrant;
 using OpenAI;
 using Qdrant.Client;
 var embeddingGenerator = new OpenAIClient("your key")
    .GetEmbeddingClient("your chosen model")
    .AsIEmbeddingGenerator();
 var vectorStore = new QdrantVectorStore(new QdrantClient("localhost"), new 
QdrantVectorStoreOptions
 {
     EmbeddingGenerator = embeddingGenerator
 });
 using Microsoft.Extensions.AI;
 using Microsoft.SemanticKernel.Connectors.Qdrant;
using OpenAI;
 using Qdrant.Client;
 var embeddingGenerator = new OpenAIClient("your key")
 .GetEmbeddingClient("your chosen model")
 .AsIEmbeddingGenerator();
 var collectionOptions = new 
QdrantVectorStoreRecordCollectionOptions<MyRecord>
 {
 EmbeddingGenerator = embeddingGenerator
 };
 var collection = new QdrantVectorStoreRecordCollection<ulong, MyRecord>(new 
QdrantClient("localhost"), "myCollection", collectionOptions);
 3. On a Record Definition: When defining properties programmatically using
 VectorStoreRecordDefinition, you can specify an embedding generator for all properties.
 C#
 using Microsoft.Extensions.AI;
 using Microsoft.Extensions.VectorData;
 using Microsoft.SemanticKernel.Connectors.Qdrant;
 using OpenAI;
 using Qdrant.Client;
 var embeddingGenerator = new OpenAIClient("your key")
 .GetEmbeddingClient("your chosen model")
 .AsIEmbeddingGenerator();
 var recordDefinition = new VectorStoreRecordDefinition
 {
 EmbeddingGenerator = embeddingGenerator,
 Properties = new List<VectorStoreRecordProperty>
 {
 new VectorStoreRecordKeyProperty("Key", typeof(ulong)),
 new VectorStoreRecordVectorProperty("DescriptionEmbedding", 
typeof(string), dimensions: 1536)
 }
 };
 var collectionOptions = new 
QdrantVectorStoreRecordCollectionOptions<MyRecord>
 {
 VectorStoreRecordDefinition = recordDefinition
 };
 var collection = new QdrantVectorStoreRecordCollection<ulong, MyRecord>(new 
QdrantClient("localhost"), "myCollection", collectionOptions);
 4. On a Vector Property Definition: When defining properties programmatically, you can set
 an embedding generator directly on the property.
C#
 The following example demonstrates how to use the embedding generator to automatically
 generate vectors during both upsert and search operations. This approach simplifies workflows
 by eliminating the need to precompute embeddings manually.
 C#
 using Microsoft.Extensions.AI;
 using Microsoft.Extensions.VectorData;
 using OpenAI;
 var embeddingGenerator = new OpenAIClient("your key")
    .GetEmbeddingClient("your chosen model")
    .AsIEmbeddingGenerator();
 var vectorProperty = new 
VectorStoreRecordVectorProperty("DescriptionEmbedding", typeof(string), 
dimensions: 1536)
 {
     EmbeddingGenerator = embeddingGenerator
 };
 Example Usage
 // The data model
 internal class FinanceInfo
 {
    [VectorStoreRecordKey]
    public string Key { get; set; } = string.Empty;
    [VectorStoreRecordData]
    public string Text { get; set; } = string.Empty;
    // Note that the vector property is typed as a string, and
    // its value is derived from the Text property. The string
    // value will however be converted to a vector on upsert and
    // stored in the database as a vector.
    [VectorStoreRecordVector(1536)]
    public string Embedding => this.Text;
 }
 // Create an OpenAI embedding generator.
 var embeddingGenerator = new OpenAIClient("your key")
    .GetEmbeddingClient("your chosen model")
    .AsIEmbeddingGenerator();
 // Use the embedding generator with the vector store.
 var vectorStore = new InMemoryVectorStore(new() { EmbeddingGenerator = 
embeddingGenerator });
The IVectorizableTextSearch and IVectorizedSearch interfaces have been marked as obsolete
 and replaced by the more unified and flexible IVectorSearch interface. This change simplifies
 the API surface and provides a more consistent approach to vector search operations.
 1. Unified Interface:
 The IVectorSearch interface consolidates the functionality of both
 IVectorizableTextSearch and IVectorizedSearch into a single interface.
 2. Method Renaming:
 VectorizableTextSearchAsync from IVectorizableTextSearch has been replaced by
 SearchAsync in IVectorSearch.
 var collection = vectorStore.GetCollection<string, FinanceInfo>("finances");
 await collection.CreateCollectionAsync();
 // Create some test data.
 string[] budgetInfo =
 {
    "The budget for 2020 is EUR 100 000",
    "The budget for 2021 is EUR 120 000",
    "The budget for 2022 is EUR 150 000",
    "The budget for 2023 is EUR 200 000",
    "The budget for 2024 is EUR 364 000"
 };
 // Embeddings are generated automatically on upsert.
 var records = budgetInfo.Select((input, index) => new FinanceInfo { Key = 
index.ToString(), Text = input });
 await collection.UpsertAsync(records);
 // Embeddings for the search is automatically generated on search.
 var searchResult = collection.SearchAsync(
    "What is my budget for 2024?",
    top: 1);
 // Output the matching result.
 await foreach (var result in searchResult)
 {
    Console.WriteLine($"Key: {result.Record.Key}, Text: {result.Record.Text}");
 }
 Transition from IVectorizableTextSearch and
 IVectorizedSearch to IVectorSearch
 Key Changes
VectorizedSearchAsync from 
IVectorizedSearch has been replaced by
 SearchEmbeddingAsync in 
IVectorSearch.
 3. Improved Flexibility:
 The 
SearchAsync method in 
IVectorSearch handles embedding generation,
 supporting either local embedding, if an embedding generator is configured, or
 server side embedding.
 The 
SearchEmbeddingAsync method in 
IVectorSearch allows for direct embedding
based searches, providing a low-level API for advanced use cases.
 Return type change for search methods
 In addition to the change in naming for search methods, the return type for all search methods
 have been changed to simplify usage. The result type of search methods is now
 IAsyncEnumerable<VectorSearchResult<TRecord>>, which allows for looping through the results
 directly. Previously the returned object contained an IAsyncEnumerable property.
 Support for search without vectors / filtered get
 The April 2025 update introduces support for finding records using a filter and returning the
 results with a configurable sort order. This allows enumerating records in a predictable order,
 which is particularly useful when needing to sync the vector store with an external data source.
 Example: Using filtered 
GetAsync
 The following example demonstrates how to use the 
GetAsync method with a filter and options
 to retrieve records from a vector store collection. This approach allows you to apply filtering
 criteria and sort the results in a predictable order.
 C#
 // Define a filter to retrieve products priced above $600
 Expression<Func<ProductInfo, bool>> filter = product => product.Price > 600;
 // Define the options with a sort order
 var options = new GetFilteredRecordOptions<ProductInfo>();
 options.OrderBy.Descending(product => product.Price);
 // Use GetAsync with the filter and sort order
 var filteredProducts = await collection.GetAsync(filter, top: 10, options)
 .ToListAsync();
This example demonstrates how to use the GetAsync method to retrieve filtered records and
 sort them based on specific criteria, such as price.
 Some new methods are available on the IVectorStore interface that allow you to perform
 more operations directly without needing a VectorStoreRecordCollection object.
 You can now verify whether a collection exists in the vector store without having to create a
 VectorStoreRecordCollection object.
 C#
 A new method allows you to delete a collection from the vector store without having to create
 a VectorStoreRecordCollection object.
 C#
 New methods on IVectorStore
 Check if a Collection Exists
 // Example: Check if a collection exists
 bool exists = await vectorStore.CollectionExistsAsync("myCollection", 
cancellationToken);
 if (exists)
 {
    Console.WriteLine("The collection exists.");
 }
 else
 {
    Console.WriteLine("The collection does not exist.");
 }
 Delete a Collection
 // Example: Delete a collection
 await vectorStore.DeleteCollectionAsync("myCollection", cancellationToken);
 Console.WriteLine("The collection has been deleted.");
 Replacement of VectorStoreGenericDataModel<TKey>
 with Dictionary<string, object?>
The vector data abstractions support working with databases where the schema of a collection
 is not known at build time. Previously this was supported via the
 VectorStoreGenericDataModel<TKey> type, where this model can be used in place of a custom
 data model.
 In this release, the VectorStoreGenericDataModel<TKey> has been obsoleted, and the
 recommended approach is to use a Dictionary<string, object?> instead.
 As before, a record definition needs to be supplied to determine the schema of the collection.
 Also note that the key type required when getting the collection instance is object, while in
 the schema it is fixed to string.
 C#
 The IVectorStoreRecordCollection interface has been updated to improve consistency in
 method naming conventions. Specifically, the batch methods have been renamed to remove
 the "Batch" part of their names. This change aligns with a more concise naming convention.
 var recordDefinition = new VectorStoreRecordDefinition
 {
    Properties = new List<VectorStoreRecordProperty>
    {
        new VectorStoreRecordKeyProperty("Key", typeof(string)),
        new VectorStoreRecordDataProperty("Text", typeof(string)),
        new VectorStoreRecordVectorProperty("Embedding", 
typeof(ReadOnlyMemory<float>), 1536)
    }
 };
 var collection = vectorStore.GetCollection<object, Dictionary<string, object?>>
 ("finances", recordDefinition);
 var record = new Dictionary<string, object?>
 {
    { "Key", "1" },
    { "Text", "The budget for 2024 is EUR 364 000" },
    { "Embedding", vector }
 };
 await collection.UpsertAsync(record);
 var retrievedRecord = await collection.GetAsync("1");
 Console.WriteLine(retrievedRecord["Text"]);
 Change in Batch method naming convention
 Renaming Examples
Old Method: 
GetBatchAsync(IEnumerable<TKey> keys, ...)
 New Method: 
GetAsync(IEnumerable<TKey> keys, ...)
 Old Method: 
DeleteBatchAsync(IEnumerable<TKey> keys, ...)
 New Method: 
DeleteAsync(IEnumerable<TKey> keys, ...)
 Old Method: 
UpsertBatchAsync(IEnumerable<TRecord> records, ...)
 New Method: 
UpsertAsync(IEnumerable<TRecord> records, ...)
 Return type change for batch Upsert method
 The return type for the batch upsert method has been changed from 
to 
IAsyncEnumerable<TKey>
 Task<IReadOnlyList<TKey>>. This change impacts how the method is consumed. You can
 now simply await the result and get back a list of keys. Previously, to ensure that all upserts
 were completed, the IAsyncEnumerable had to be completely enumerated. This simplifies the
 developer experience when using the batch upsert method.
 The CollectionName property has been changed to
 Name
 The 
CollectionName property on the 
IVectorStoreRecordCollection interface has renamed to
 Name.
 IsFilterable and IsFullTextSearchable renamed to
 IsIndexed and IsFullTextIndexed
 The 
IsFilterable and 
IsFullTextSearchable properties on the
 VectorStoreRecordDataAttribute and 
renamed to 
VectorStoreRecordDataProperty classes have been
 IsIndexed and 
IsFullTextIndexed respectively.
 Dimensions are now required for vector attributes
 and definitions
 In the April 2025 update, specifying the number of dimensions has become mandatory when
 using vector attributes or vector property definitions. This ensures that the vector store always
 has the necessary information to handle embeddings correctly.
Previously, the VectorStoreRecordVectorAttribute allowed you to omit the Dimensions
 parameter. This is no longer allowed, and the Dimensions parameter must now be explicitly
 provided.
 Before:
 C#
 After:
 C#
 Similarly, when defining a vector property programmatically using
 VectorStoreRecordVectorProperty, the dimensions parameter is now required.
 Before:
 C#
 After:
 C#
 Changes to VectorStoreRecordVectorAttribute
 [VectorStoreRecordVector]
 public ReadOnlyMemory<float> DefinitionEmbedding { get; set; }
 [VectorStoreRecordVector(Dimensions: 1536)]
 public ReadOnlyMemory<float> DefinitionEmbedding { get; set; }
 Changes to VectorStoreRecordVectorProperty
 var vectorProperty = new VectorStoreRecordVectorProperty("DefinitionEmbedding", 
typeof(ReadOnlyMemory<float>));
 var vectorProperty = new VectorStoreRecordVectorProperty("DefinitionEmbedding", 
typeof(ReadOnlyMemory<float>), dimensions: 1536);
 All collections require the key type to be passed as
 a generic type parameter
When constructing a collection directly, it is now required to provide the 
TKey generic type
 parameter. Previously, where some databases allowed only one key type, it was now a required
 parameter, but to allow using collections with a 
Dictionary<string, object?> type and an
 object key type, 
TKey must now always be provided.
Vector Store changes - May 2025
 Article•05/19/2025
 The following nuget packages have been renamed for clarity and length.
 Old Package Name new Package Name
 Microsoft.SemanticKernel.Connectors.AzureCosmosDBMongoDB Microsoft.SemanticKernel.Connectors.CosmosMongoDB
 Microsoft.SemanticKernel.Connectors.AzureCosmosDBNoSQL Microsoft.SemanticKernel.Connectors.CosmosNoSql
 Microsoft.SemanticKernel.Connectors.Postgres Microsoft.SemanticKernel.Connectors.PgVector
 Microsoft.SemanticKernel.Connectors.Sqlite Microsoft.SemanticKernel.Connectors.SqliteVec
 As part of our formal API review before GA various naming changes were proposed and adopted, resulting in the following name changes. These
 should help improve clarity, consistency and reduce type name length.
 Old Namespace Old TypeName New Namespace
 Microsoft.Extensions.VectorData VectorStoreRecordDefinition Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData VectorStoreRecordKeyAttribute Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData VectorStoreRecordDataAttribute Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData VectorStoreRecordVectorAttribute Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData VectorStoreRecordProperty Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData VectorStoreRecordKeyProperty Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData VectorStoreRecordDataProperty Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData VectorStoreRecordVectorProperty Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData GetRecordOptions Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData GetFilteredRecordOptions<TRecord> Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData IVectorSearch<TRecord> Microsoft.Extensions.VectorData
 Microsoft.Extensions.VectorData IKeywordHybridSearch<TRecord> Microsoft.Extensions.VectorData
 Microsoft.SemanticKernel.Connectors.AzureCosmosDBMongoDB AzureCosmosDBMongoDBVectorStore Microsoft.SemanticKernel.Connectors.Cosm
 Microsoft.SemanticKernel.Connectors.AzureCosmosDBMongoDB AzureCosmosDBMongoDBVectorStoreRecordCollection Microsoft.SemanticKernel.Connectors.Cosm
 Microsoft.SemanticKernel.Connectors.AzureCosmosDBNoSQL AzureCosmosDBNoSQLVectorStore Microsoft.SemanticKernel.Connectors.Cosm
 Microsoft.SemanticKernel.Connectors.AzureCosmosDBNoSQL AzureCosmosDBNoSQLVectorStoreRecordCollection Microsoft.SemanticKernel.Connectors.Cosm
 Microsoft.SemanticKernel.Connectors.MongoDB MongoDBVectorStore Microsoft.SemanticKernel.Connectors.Mongo
 Microsoft.SemanticKernel.Connectors.MongoDB MongoDBVectorStoreRecordCollection Microsoft.SemanticKernel.Connectors.Mongo
 All names of the various Semantic Kernel supported implementations of VectorStoreCollection have been renamed to shorter names using a
 consistent pattern.
 *VectorStoreRecordCollection is now *Collection. E.g. PostgresVectorStoreRecordCollection -> PostgresCollection.
 Similary all related options classes have also changd.
 Nuget Package Renames
ﾉExpand table
 Type Renames
ﾉExpand table
*VectorStoreRecordCollectionOptions is now *CollectionOptions. E.g. PostgresVectorStoreRecordCollectionOptions ->
 PostgresCollectionOptions.
 Namespace Class Old Property Name New Property Name
 Microsoft.Extensions.VectorData VectorStoreKeyAttribute StoragePropertyName StorageName
 Microsoft.Extensions.VectorData VectorStoreDataAttribute StoragePropertyName StorageName
 Microsoft.Extensions.VectorData VectorStoreVectorAttribute StoragePropertyName StorageName
 Microsoft.Extensions.VectorData VectorStoreKeyProperty DataModelPropertyName Name
 Microsoft.Extensions.VectorData VectorStoreKeyProperty StoragePropertyName StorageName
 Microsoft.Extensions.VectorData VectorStoreKeyProperty PropertyType Type
 Microsoft.Extensions.VectorData VectorStoreDataProperty DataModelPropertyName Name
 Microsoft.Extensions.VectorData VectorStoreDataProperty StoragePropertyName StorageName
 Microsoft.Extensions.VectorData VectorStoreDataProperty PropertyType Type
 Microsoft.Extensions.VectorData VectorStoreVectorProperty DataModelPropertyName Name
 Microsoft.Extensions.VectorData VectorStoreVectorProperty StoragePropertyName StorageName
 Microsoft.Extensions.VectorData VectorStoreVectorProperty PropertyType Type
 Microsoft.Extensions.VectorData DistanceFunction Hamming HammingDistance
 The VectorStoreRecordDefinition property on collection options classes have been renamed to just Definition.
 The CreateCollectionIfNotExistsAsync method on the Collection has been renamed to EnsureCollectionExistsAsync.
 The DeleteAsync method on the *Collection and VectorStore has been renamed to EnsureCollectionDeletedAsync. This more closely aligns with
 the behavior of the method, which will delete a collection if it exists. If the collection does not exist it will do nothing and succeed.
 The following interfaces have been changed to base abstract classes.
 Namespace Old Interface Name New Type Name
 Microsoft.Extensions.VectorData IVectorStore VectorStore
 Microsoft.Extensions.VectorData IVectorStoreRecordCollection VectorStoreCollection
 Whereever you were using IVectorStore or IVectorStoreRecordCollection before, you can now use VectorStore and VectorStoreCollection instead.
 The SearchAsync and SearchEmbeddingAsync methods on the Collection have been merged into a single method: SearchAsync.
 Previously SearchAsync allowed doing vector searches using source data that would be vectorized inside the collection or in the service while
 SearchEmbeddingAsync allowed doing vector searches by providing a vector.
 Now, both scenarios are supported using the single SearchAsync method, which can take as input both source data and vectors.
 Property Renames
ﾉExpand table
 Method Renames
 Interface to base abstract class
ﾉExpand table
 Merge of SearchAsync and SearchEmbeddingAsync
The mechanism for determining what to do is as follows:
 1. If the provided value is one of the supported vector types for the connector, search uses that.
 2. If the provided value is not one of supported vector types, the connector checks if an 
IEmbeddingGenerator is registered, with the vector
 store, that supports converting from the provided value to the vector type supported by the database.
 3. Finally, if no compatible 
IEmbeddingGenerator is available, the method will throw an 
InvalidOperationException.
 Support for Dictionary<string, object?> models using
 *DynamicCollection and 
VectorStore.GetDynamicCollection
 To allow support for NativeOAT and Trimming, where possible and when using the dynamic data model, the way in which dynamic data models
 are supported has changed. Specifically, how you request or construct the collection has changed.
 Previously when using Dictionary<string, object?> as your data model, you could request this using 
VectorStore.GetCollection, but now you will
 need to use 
VectorStore.GetDynamicCollection
 C#
 // Before
 PostgresVectorStore vectorStore = new PostgresVectorStore(myNpgsqlDataSource)
 vectorStore.GetCollection<string, Dictionary<string, object?>>("collectionName", definition);
 // After
 PostgresVectorStore vectorStore = new PostgresVectorStore(myNpgsqlDataSource, ownsDataSource: true)
 vectorStore.GetDynamicCollection<string, Dictionary<string, object?>>("collectionName", definition);
 VectorStore and VectorStoreRecordCollection is now Disposable
 Both 
VectorStore and 
VectorStoreRecordCollection is now disposable to ensure that database clients owned by these are disposed properly.
 When passing a database client to your vector store or collection, you have the option to specify whether the vector store or collection should
 own the client and therefore also dispose it when the vector store or collection is disposed.
 For example, when passing a datasource to the 
PostgresVectorStore passing 
true for 
ownsDataSource will cause the 
dispose the datasource when it is disposed.
 C#
 new PostgresVectorStore(dataSource, ownsDataSource: true, options);
 PostgresVectorStore to
 CreateCollection is not supported anymore, use
 EnsureCollectionExistsAsync
 The 
CreateCollection method on the 
Collection has been removed, and only 
EnsureCollectionExistsAsync is now supported.
 EnsureCollectionExistsAsync is idempotent and will create a collection if it does not exist, and do nothing if it already exists.
 VectorStoreOperationException and
 VectorStoreRecordMappingException is not supported anymore, use
 VectorStoreException
 VectorStoreOperationException and 
VectorStoreRecordMappingException has been removed, and only 
VectorStoreException is now supported. All
 database request failures resulting in a database specific exception are wrapped and thrown as a 
VectorStoreException, allowing consuming code
 to catch a single exception type instead of a different one for each implementation