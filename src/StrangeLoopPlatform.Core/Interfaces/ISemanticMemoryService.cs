using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Service for managing semantic memory and context persistence in the self-evolving AI system.
/// This interface provides capabilities for storing, retrieving, and managing contextual
/// information that enables the system to learn from past experiences and maintain
/// continuity across improvement cycles.
/// 
/// Key responsibilities:
/// - Store and retrieve contextual information and learning outcomes
/// - Manage semantic search and similarity matching
/// - Provide memory persistence and lifecycle management
/// - Enable context-aware decision making and learning
/// 
/// This service is essential for the self-evolving architecture as it enables
/// the system to build upon past experiences and maintain knowledge continuity.
/// </summary>
public interface ISemanticMemoryService
{
    /// <summary>
    /// Stores a memory entry with semantic content and metadata.
    /// This method provides persistent storage for contextual information,
    /// learning outcomes, and system experiences.
    /// </summary>
    /// <param name="content">The semantic content to store</param>
    /// <param name="metadata">Additional metadata for the memory entry</param>
    /// <param name="tags">Optional tags for categorization and retrieval</param>
    /// <returns>The unique identifier of the stored memory entry</returns>
    /// <exception cref="ArgumentNullException">Thrown when content is null or empty</exception>
    Task<string> StoreAsync(string content, Dictionary<string, object>? metadata = null, List<string>? tags = null);

    /// <summary>
    /// Retrieves memory entries based on semantic similarity to a query.
    /// This method enables context-aware retrieval by finding memories that
    /// are semantically related to the provided query.
    /// </summary>
    /// <param name="query">The semantic query to search for</param>
    /// <param name="maxResults">Maximum number of results to return</param>
    /// <param name="similarityThreshold">Minimum similarity score for results</param>
    /// <returns>An enumerable of relevant MemoryEntry objects</returns>
    /// <exception cref="ArgumentNullException">Thrown when query is null or empty</exception>
    /// <exception cref="ArgumentException">Thrown when maxResults is less than 1</exception>
    Task<IEnumerable<MemoryEntry>> RetrieveAsync(string query, int maxResults = 10, double similarityThreshold = 0.7);

    /// <summary>
    /// Retrieves memory entries by their unique identifiers.
    /// This method provides direct access to specific memory entries
    /// when their IDs are known.
    /// </summary>
    /// <param name="ids">Collection of memory entry IDs to retrieve</param>
    /// <returns>An enumerable of MemoryEntry objects for the specified IDs</returns>
    /// <exception cref="ArgumentNullException">Thrown when ids is null</exception>
    /// <exception cref="ArgumentException">Thrown when ids is empty</exception>
    Task<IEnumerable<MemoryEntry>> GetByIdsAsync(IEnumerable<string> ids);

    /// <summary>
    /// Updates an existing memory entry with new content or metadata.
    /// This method enables memory evolution and refinement based on
    /// new experiences or insights.
    /// </summary>
    /// <param name="id">The unique identifier of the memory entry to update</param>
    /// <param name="content">New content for the memory entry</param>
    /// <param name="metadata">Updated metadata for the memory entry</param>
    /// <param name="tags">Updated tags for the memory entry</param>
    /// <returns>True if the memory entry was successfully updated, false if not found</returns>
    /// <exception cref="ArgumentNullException">Thrown when id is null or empty</exception>
    /// <exception cref="ArgumentNullException">Thrown when content is null or empty</exception>
    Task<bool> UpdateAsync(string id, string content, Dictionary<string, object>? metadata = null, List<string>? tags = null);

    /// <summary>
    /// Deletes a memory entry by its unique identifier.
    /// This method provides cleanup capabilities for outdated or
    /// irrelevant memory entries.
    /// </summary>
    /// <param name="id">The unique identifier of the memory entry to delete</param>
    /// <returns>True if the memory entry was successfully deleted, false if not found</returns>
    /// <exception cref="ArgumentNullException">Thrown when id is null or empty</exception>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Searches for memory entries using tags for categorization.
    /// This method enables tag-based retrieval and organization
    /// of memory entries.
    /// </summary>
    /// <param name="tags">Tags to search for (entries must have all specified tags)</param>
    /// <param name="maxResults">Maximum number of results to return</param>
    /// <returns>An enumerable of MemoryEntry objects matching the tags</returns>
    /// <exception cref="ArgumentNullException">Thrown when tags is null</exception>
    /// <exception cref="ArgumentException">Thrown when tags is empty</exception>
    Task<IEnumerable<MemoryEntry>> SearchByTagsAsync(IEnumerable<string> tags, int maxResults = 10);

    /// <summary>
    /// Gets memory metrics and statistics for monitoring and analysis.
    /// This method provides insights into memory usage, patterns, and
    /// system learning effectiveness.
    /// </summary>
    /// <returns>MemoryStatistics containing statistics and analytics data</returns>
    Task<MemoryStatistics> GetMetricsAsync();

    /// <summary>
    /// Clears all memory entries, providing a fresh start.
    /// This method should be used with caution as it permanently
    /// removes all stored memories and learning outcomes.
    /// </summary>
    /// <returns>True if all memory entries were successfully cleared</returns>
    Task<bool> ClearAllAsync();
}

/// <summary>
/// Represents a semantic memory entry with full metadata and context.
/// 
/// This class provides:
/// - Semantic content and metadata
/// - Context and categorization information
/// - Access patterns and relevance scoring
/// - Version history and learning trajectory
/// 
/// DO NOT DELETE: Required for memory entry handling
/// </summary>
public class MemoryEntry
{
    /// <summary>
    /// Unique identifier for the memory entry
    /// </summary>
    public string Key { get; set; } = string.Empty;
    
    /// <summary>
    /// Semantic content of the memory entry
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Additional metadata and context
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
    
    /// <summary>
    /// Memory category (episodic, semantic, procedural, working)
    /// </summary>
    public string Category { get; set; } = "semantic";
    
    /// <summary>
    /// Tags for categorization and search
    /// </summary>
    public List<string> Tags { get; set; } = new();
    
    /// <summary>
    /// When the memory was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// When the memory was last accessed
    /// </summary>
    public DateTime LastAccessedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// When the memory was last updated
    /// </summary>
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Relevance score for search results (0.0 to 1.0)
    /// </summary>
    public double RelevanceScore { get; set; }
    
    /// <summary>
    /// Number of times this memory has been accessed
    /// </summary>
    public int AccessCount { get; set; }
    
    /// <summary>
    /// Version number for tracking changes
    /// </summary>
    public int Version { get; set; } = 1;
    
    /// <summary>
    /// Whether this memory entry is active (not deleted)
    /// </summary>
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Represents memory statistics and analytics data.
/// 
/// This class provides:
/// - Memory usage statistics and metrics
/// - Learning patterns and trends
/// - Performance analytics and insights
/// - System optimization recommendations
/// 
/// DO NOT DELETE: Required for memory analytics handling
/// </summary>
public class MemoryStatistics
{
    /// <summary>
    /// Total number of memory entries
    /// </summary>
    public int TotalEntries { get; set; }
    
    /// <summary>
    /// Total size of memory data in bytes
    /// </summary>
    public long TotalSizeBytes { get; set; }
    
    /// <summary>
    /// Distribution of entries by category
    /// </summary>
    public Dictionary<string, int> CategoryDistribution { get; set; } = new();
    
    /// <summary>
    /// Average relevance score across all entries
    /// </summary>
    public double AverageRelevanceScore { get; set; }
    
    /// <summary>
    /// Most frequently accessed entries
    /// </summary>
    public List<string> TopAccessedKeys { get; set; } = new();
    
    /// <summary>
    /// Most common tags across all entries
    /// </summary>
    public Dictionary<string, int> TopTags { get; set; } = new();
    
    /// <summary>
    /// Memory growth rate (entries per day)
    /// </summary>
    public double GrowthRatePerDay { get; set; }
    
    /// <summary>
    /// Average access frequency (accesses per day)
    /// </summary>
    public double AverageAccessFrequency { get; set; }
    
    /// <summary>
    /// Memory age distribution (entries by age ranges)
    /// </summary>
    public Dictionary<string, int> AgeDistribution { get; set; } = new();
    
    /// <summary>
    /// System health indicators and recommendations
    /// </summary>
    public List<string> HealthRecommendations { get; set; } = new();
} 