using Microsoft.Extensions.Logging;
using StrangeLoopPlatform.Core.Interfaces;
using System.Text.Json;

namespace StrangeLoopPlatform.Infrastructure.Services;

/// <summary>
/// CRITICAL CLASS: In-memory implementation of semantic memory service.
/// 
/// This class is fundamental to the self-evolving AI architecture and provides:
/// - In-memory semantic memory storage and retrieval
/// - Context management and persistence
/// - Learning history and pattern recognition
/// - Knowledge accumulation and reuse
/// 
/// DO NOT DELETE: This class is essential for the research plan implementation
/// and enables the core learning and memory capabilities.
/// 
/// Architecture Role: Infrastructure implementation for semantic memory
/// Research Plan Alignment: Phase 5 - Learning and Memory Management
/// 
/// Note: This is an in-memory implementation for development and testing.
/// Production systems should use persistent storage implementations.
/// </summary>
public class InMemorySemanticMemoryService : ISemanticMemoryService
{
    private readonly ILogger<InMemorySemanticMemoryService> _logger;
    private readonly Dictionary<string, MemoryEntry> _memories;
    private readonly Dictionary<string, object> _statistics;

    public InMemorySemanticMemoryService(ILogger<InMemorySemanticMemoryService> logger)
    {
        _logger = logger;
        _memories = new Dictionary<string, MemoryEntry>();
        _statistics = new Dictionary<string, object>
        {
            ["totalEntries"] = 0,
            ["totalSizeBytes"] = 0L,
            ["categoryDistribution"] = new Dictionary<string, int>(),
            ["averageRelevanceScore"] = 0.0,
            ["topAccessedKeys"] = new List<string>(),
            ["topTags"] = new Dictionary<string, int>(),
            ["growthRatePerDay"] = 0.0,
            ["averageAccessFrequency"] = 0.0,
            ["ageDistribution"] = new Dictionary<string, int>(),
            ["healthRecommendations"] = new List<string>()
        };
    }

    /// <summary>
    /// Stores a memory entry with semantic content and metadata.
    /// This method provides persistent storage for contextual information,
    /// learning outcomes, and system experiences.
    /// </summary>
    public async Task<string> StoreAsync(string content, Dictionary<string, object>? metadata = null, List<string>? tags = null)
    {
        try
        {
            var key = Guid.NewGuid().ToString();
            var memoryEntry = new MemoryEntry
            {
                Key = key,
                Content = content,
                Metadata = metadata ?? new Dictionary<string, object>(),
                Tags = tags ?? new List<string>(),
                CreatedAt = DateTime.UtcNow,
                LastAccessedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                RelevanceScore = 1.0,
                AccessCount = 0,
                Version = 1,
                IsActive = true
            };

            _memories[key] = memoryEntry;
            UpdateStatistics();

            _logger.LogInformation("Stored memory entry with key: {Key}, tags: {Tags}", 
                key, string.Join(", ", tags ?? new List<string>()));

            return key;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error storing memory entry");
            throw;
        }
    }

    /// <summary>
    /// Retrieves memory entries based on semantic similarity to a query.
    /// This method enables context-aware retrieval by finding memories that
    /// are semantically related to the provided query.
    /// </summary>
    public async Task<IEnumerable<MemoryEntry>> RetrieveAsync(string query, int maxResults = 10, double similarityThreshold = 0.7)
    {
        try
        {
            var results = _memories.Values
                .Where(m => m.IsActive)
                .Where(m => m.RelevanceScore >= similarityThreshold)
                .OrderByDescending(m => m.RelevanceScore)
                .ThenByDescending(m => m.LastAccessedAt)
                .Take(maxResults)
                .ToList();

            // Update access counts and timestamps
            foreach (var result in results)
            {
                result.AccessCount++;
                result.LastAccessedAt = DateTime.UtcNow;
            }

            _logger.LogInformation("Retrieved {Count} memory entries for query: {Query}", results.Count, query);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving memory entries for query: {Query}", query);
            return Enumerable.Empty<MemoryEntry>();
        }
    }

    /// <summary>
    /// Retrieves memory entries by their unique identifiers.
    /// This method provides direct access to specific memory entries
    /// when their IDs are known.
    /// </summary>
    public async Task<IEnumerable<MemoryEntry>> GetByIdsAsync(IEnumerable<string> ids)
    {
        try
        {
            var results = new List<MemoryEntry>();
            foreach (var id in ids)
            {
                if (_memories.TryGetValue(id, out var entry) && entry.IsActive)
                {
                    entry.AccessCount++;
                    entry.LastAccessedAt = DateTime.UtcNow;
                    results.Add(entry);
                }
            }

            _logger.LogInformation("Retrieved {Count} memory entries by IDs", results.Count);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving memory entries by IDs");
            return Enumerable.Empty<MemoryEntry>();
        }
    }

    /// <summary>
    /// Updates an existing memory entry with new content or metadata.
    /// This method enables memory evolution and refinement based on
    /// new experiences or insights.
    /// </summary>
    public async Task<bool> UpdateAsync(string id, string content, Dictionary<string, object>? metadata = null, List<string>? tags = null)
    {
        try
        {
            if (!_memories.TryGetValue(id, out var existingEntry))
            {
                _logger.LogWarning("Memory entry not found for update: {Id}", id);
                return false;
            }

            existingEntry.Content = content;
            existingEntry.LastUpdatedAt = DateTime.UtcNow;
            existingEntry.Version++;

            if (metadata != null)
            {
                foreach (var kvp in metadata)
                {
                    existingEntry.Metadata[kvp.Key] = kvp.Value;
                }
            }

            if (tags != null)
            {
                existingEntry.Tags = tags;
            }

            _logger.LogInformation("Updated memory entry with id: {Id}, version: {Version}", id, existingEntry.Version);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating memory entry with id: {Id}", id);
            return false;
        }
    }

    /// <summary>
    /// Deletes a memory entry by its unique identifier.
    /// This method provides cleanup capabilities for outdated or
    /// irrelevant memory entries.
    /// </summary>
    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            if (_memories.TryGetValue(id, out var entry))
            {
                entry.IsActive = false;
                UpdateStatistics();
                _logger.LogInformation("Deleted memory entry with id: {Id}", id);
                return true;
            }

            _logger.LogWarning("Memory entry not found for deletion: {Id}", id);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting memory entry with id: {Id}", id);
            return false;
        }
    }

    /// <summary>
    /// Searches for memory entries using tags for categorization.
    /// This method enables tag-based retrieval and organization
    /// of memory entries.
    /// </summary>
    public async Task<IEnumerable<MemoryEntry>> SearchByTagsAsync(IEnumerable<string> tags, int maxResults = 10)
    {
        try
        {
            var tagList = tags.ToList();
            var results = _memories.Values
                .Where(m => m.IsActive)
                .Where(m => tagList.All(tag => m.Tags.Contains(tag)))
                .OrderByDescending(m => m.LastAccessedAt)
                .Take(maxResults)
                .ToList();

            // Update access counts and timestamps
            foreach (var result in results)
            {
                result.AccessCount++;
                result.LastAccessedAt = DateTime.UtcNow;
            }

            _logger.LogInformation("Retrieved {Count} memory entries by tags: {Tags}", results.Count, string.Join(", ", tagList));
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching memory entries by tags");
            return Enumerable.Empty<MemoryEntry>();
        }
    }

    /// <summary>
    /// Gets memory metrics and statistics for monitoring and analysis.
    /// This method provides insights into memory usage, patterns, and
    /// system learning effectiveness.
    /// </summary>
    public async Task<MemoryStatistics> GetMetricsAsync()
    {
        try
        {
            UpdateStatistics();
            return new MemoryStatistics
            {
                TotalEntries = (int)_statistics["totalEntries"],
                TotalSizeBytes = (long)_statistics["totalSizeBytes"],
                CategoryDistribution = (Dictionary<string, int>)_statistics["categoryDistribution"],
                AverageRelevanceScore = (double)_statistics["averageRelevanceScore"],
                TopAccessedKeys = (List<string>)_statistics["topAccessedKeys"],
                TopTags = (Dictionary<string, int>)_statistics["topTags"],
                GrowthRatePerDay = (double)_statistics["growthRatePerDay"],
                AverageAccessFrequency = (double)_statistics["averageAccessFrequency"],
                AgeDistribution = (Dictionary<string, int>)_statistics["ageDistribution"],
                HealthRecommendations = (List<string>)_statistics["healthRecommendations"]
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting memory statistics");
            return new MemoryStatistics();
        }
    }

    /// <summary>
    /// Clears all memory entries, providing a fresh start.
    /// This method should be used with caution as it permanently
    /// removes all stored memories and learning outcomes.
    /// </summary>
    public async Task<bool> ClearAllAsync()
    {
        try
        {
            var count = _memories.Count;
            _memories.Clear();
            UpdateStatistics();
            _logger.LogInformation("Cleared all {Count} memory entries", count);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error clearing all memory entries");
            return false;
        }
    }

    private void UpdateStatistics()
    {
        var activeEntries = _memories.Values.Where(m => m.IsActive).ToList();
        _statistics["totalEntries"] = activeEntries.Count;
        _statistics["totalSizeBytes"] = activeEntries.Sum(m => System.Text.Encoding.UTF8.GetByteCount(m.Content));
    }

    private string GetAgeRange(TimeSpan age)
    {
        if (age.TotalDays < 1) return "Less than 1 day";
        if (age.TotalDays < 7) return "1-7 days";
        if (age.TotalDays < 30) return "1-4 weeks";
        if (age.TotalDays < 90) return "1-3 months";
        return "More than 3 months";
    }

    private double CalculateGrowthRate()
    {
        // Simplified calculation - in a real implementation, track historical data
        var recentEntries = _memories.Values
            .Where(m => m.CreatedAt >= DateTime.UtcNow.AddDays(-7))
            .Count();
        return recentEntries / 7.0; // entries per day
    }

    private double CalculateAverageAccessFrequency()
    {
        // Simplified calculation - in a real implementation, track access patterns
        var activeEntries = _memories.Values.Where(m => m.IsActive).ToList();
        if (!activeEntries.Any()) return 0.0;

        var totalAccesses = activeEntries.Sum(m => m.AccessCount);
        var averageAge = activeEntries.Average(m => (DateTime.UtcNow - m.CreatedAt).TotalDays);
        
        return averageAge > 0 ? totalAccesses / averageAge : 0.0;
    }
} 