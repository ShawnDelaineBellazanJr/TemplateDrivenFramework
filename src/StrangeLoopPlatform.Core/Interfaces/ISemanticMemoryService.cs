using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Service for managing semantic memory and context persistence
/// </summary>
public interface ISemanticMemoryService
{
    /// <summary>
    /// Stores a memory entry with semantic embedding
    /// </summary>
    /// <param name="key">Unique key for the memory entry</param>
    /// <param name="text">Text content to store</param>
    /// <param name="metadata">Additional metadata</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if stored successfully</returns>
    Task<bool> StoreMemoryAsync(string key, string text, Dictionary<string, object>? metadata = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves memory entries by semantic similarity
    /// </summary>
    /// <param name="query">Query text to search for</param>
    /// <param name="limit">Maximum number of results</param>
    /// <param name="minRelevanceScore">Minimum relevance score threshold</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of relevant memory entries</returns>
    Task<List<MemoryEntry>> RetrieveMemoryAsync(string query, int limit = 10, double minRelevanceScore = 0.7, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a specific memory entry by key
    /// </summary>
    /// <param name="key">Memory entry key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Memory entry if found</returns>
    Task<MemoryEntry?> GetMemoryAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing memory entry
    /// </summary>
    /// <param name="key">Memory entry key</param>
    /// <param name="text">Updated text content</param>
    /// <param name="metadata">Updated metadata</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if updated successfully</returns>
    Task<bool> UpdateMemoryAsync(string key, string text, Dictionary<string, object>? metadata = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a memory entry
    /// </summary>
    /// <param name="key">Memory entry key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if removed successfully</returns>
    Task<bool> RemoveMemoryAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores process context for later retrieval
    /// </summary>
    /// <param name="processId">Process identifier</param>
    /// <param name="context">Process context data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if stored successfully</returns>
    Task<bool> StoreProcessContextAsync(string processId, ProcessContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves process context by process ID
    /// </summary>
    /// <param name="processId">Process identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Process context if found</returns>
    Task<ProcessContext?> GetProcessContextAsync(string processId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores learning insights from completed processes
    /// </summary>
    /// <param name="insight">Learning insight to store</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if stored successfully</returns>
    Task<bool> StoreLearningInsightAsync(LearningInsight insight, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves learning insights relevant to a given context
    /// </summary>
    /// <param name="context">Context to search for relevant insights</param>
    /// <param name="limit">Maximum number of insights to return</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of relevant learning insights</returns>
    Task<List<LearningInsight>> GetLearningInsightsAsync(string context, int limit = 5, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets memory statistics and performance metrics
    /// </summary>
    /// <returns>Memory service metrics</returns>
    Task<MemoryMetrics> GetMemoryMetricsAsync();

    /// <summary>
    /// Cleans up old or irrelevant memory entries
    /// </summary>
    /// <param name="olderThan">Remove entries older than this time</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Number of entries removed</returns>
    Task<int> CleanupMemoryAsync(TimeSpan olderThan, CancellationToken cancellationToken = default);
}

/// <summary>
/// Represents a memory entry with semantic embedding
/// </summary>
public class MemoryEntry
{
    public string Key { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public Dictionary<string, object> Metadata { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public double RelevanceScore { get; set; }
    public string[] Tags { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Represents process context data
/// </summary>
public class ProcessContext
{
    public string ProcessId { get; set; } = string.Empty;
    public Dictionary<string, object> Data { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? LastAccessed { get; set; }
    public int AccessCount { get; set; }
}

/// <summary>
/// Represents a learning insight from completed processes
/// </summary>
public class LearningInsight
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string[] Tags { get; set; } = Array.Empty<string>();
    public DateTime CreatedAt { get; set; }
    public string SourceProcessId { get; set; } = string.Empty;
    public double Confidence { get; set; }
    public Dictionary<string, object> Evidence { get; set; } = new();
}

/// <summary>
/// Memory service performance metrics
/// </summary>
public class MemoryMetrics
{
    public int TotalEntries { get; set; }
    public int TotalProcessContexts { get; set; }
    public int TotalLearningInsights { get; set; }
    public TimeSpan AverageQueryTime { get; set; }
    public TimeSpan AverageStoreTime { get; set; }
    public long TotalStorageSize { get; set; }
    public Dictionary<string, int> QueryTypes { get; set; } = new();
    public Dictionary<string, int> ErrorTypes { get; set; } = new();
} 