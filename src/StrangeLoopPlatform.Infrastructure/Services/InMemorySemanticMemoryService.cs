using Microsoft.Extensions.Logging;
using StrangeLoopPlatform.Core.Interfaces;
using System.Text.Json;

namespace StrangeLoopPlatform.Infrastructure.Services;

/// <summary>
/// In-memory implementation of semantic memory service
/// Note: This is a simplified implementation for development. Production should use a proper vector database.
/// </summary>
public class InMemorySemanticMemoryService : ISemanticMemoryService
{
    private readonly ILogger<InMemorySemanticMemoryService> _logger;
    private readonly Dictionary<string, MemoryEntry> _memoryEntries;
    private readonly Dictionary<string, ProcessContext> _processContexts;
    private readonly List<LearningInsight> _learningInsights;
    private readonly MemoryMetrics _metrics;
    private readonly object _lock = new object();

    public InMemorySemanticMemoryService(ILogger<InMemorySemanticMemoryService> logger)
    {
        _logger = logger;
        _memoryEntries = new Dictionary<string, MemoryEntry>();
        _processContexts = new Dictionary<string, ProcessContext>();
        _learningInsights = new List<LearningInsight>();
        _metrics = new MemoryMetrics();
    }

    public async Task<bool> StoreMemoryAsync(string key, string text, Dictionary<string, object>? metadata = null, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            var startTime = DateTime.UtcNow;
            
            lock (_lock)
            {
                var entry = new MemoryEntry
                {
                    Key = key,
                    Text = text,
                    Metadata = metadata ?? new Dictionary<string, object>(),
                    CreatedAt = DateTime.UtcNow,
                    Tags = ExtractTags(text)
                };

                _memoryEntries[key] = entry;
                _metrics.TotalEntries++;
            }

            var storeTime = DateTime.UtcNow - startTime;
            UpdateStoreMetrics(storeTime);

            _logger.LogDebug("Stored memory entry with key: {Key}", key);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to store memory entry with key: {Key}", key);
            UpdateErrorMetrics("store");
            return false;
        }
    }

    public async Task<List<MemoryEntry>> RetrieveMemoryAsync(string query, int limit = 10, double minRelevanceScore = 0.7, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            var startTime = DateTime.UtcNow;
            var results = new List<MemoryEntry>();

            lock (_lock)
            {
                // Simple keyword-based search for now
                // In production, this would use vector embeddings and similarity search
                var queryWords = query.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                var scoredEntries = _memoryEntries.Values
                    .Select(entry => new
                    {
                        Entry = entry,
                        Score = CalculateRelevanceScore(entry, queryWords)
                    })
                    .Where(item => item.Score >= minRelevanceScore)
                    .OrderByDescending(item => item.Score)
                    .Take(limit)
                    .ToList();

                results = scoredEntries.Select(item => 
                {
                    item.Entry.RelevanceScore = item.Score;
                    return item.Entry;
                }).ToList();
            }

            var queryTime = DateTime.UtcNow - startTime;
            UpdateQueryMetrics(queryTime);

            _logger.LogDebug("Retrieved {Count} memory entries for query: {Query}", results.Count, query);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve memory for query: {Query}", query);
            UpdateErrorMetrics("query");
            return new List<MemoryEntry>();
        }
    }

    public async Task<MemoryEntry?> GetMemoryAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            lock (_lock)
            {
                return _memoryEntries.TryGetValue(key, out var entry) ? entry : null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get memory entry with key: {Key}", key);
            return null;
        }
    }

    public async Task<bool> UpdateMemoryAsync(string key, string text, Dictionary<string, object>? metadata = null, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            lock (_lock)
            {
                if (_memoryEntries.TryGetValue(key, out var existingEntry))
                {
                    existingEntry.Text = text;
                    existingEntry.Metadata = metadata ?? existingEntry.Metadata;
                    existingEntry.UpdatedAt = DateTime.UtcNow;
                    existingEntry.Tags = ExtractTags(text);
                    
                    _logger.LogDebug("Updated memory entry with key: {Key}", key);
                    return true;
                }
                
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update memory entry with key: {Key}", key);
            return false;
        }
    }

    public async Task<bool> RemoveMemoryAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            lock (_lock)
            {
                var removed = _memoryEntries.Remove(key);
                if (removed)
                {
                    _metrics.TotalEntries--;
                    _logger.LogDebug("Removed memory entry with key: {Key}", key);
                }
                return removed;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove memory entry with key: {Key}", key);
            return false;
        }
    }

    public async Task<bool> StoreProcessContextAsync(string processId, ProcessContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            lock (_lock)
            {
                _processContexts[processId] = context;
                _metrics.TotalProcessContexts++;
            }

            _logger.LogDebug("Stored process context for process: {ProcessId}", processId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to store process context for process: {ProcessId}", processId);
            return false;
        }
    }

    public async Task<ProcessContext?> GetProcessContextAsync(string processId, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            lock (_lock)
            {
                return _processContexts.TryGetValue(processId, out var context) ? context : null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get process context for process: {ProcessId}", processId);
            return null;
        }
    }

    public async Task<bool> StoreLearningInsightAsync(LearningInsight insight, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            lock (_lock)
            {
                _learningInsights.Add(insight);
                _metrics.TotalLearningInsights++;
            }

            _logger.LogDebug("Stored learning insight: {Title}", insight.Title);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to store learning insight: {Title}", insight.Title);
            return false;
        }
    }

    public async Task<List<LearningInsight>> GetLearningInsightsAsync(string context, int limit = 5, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            var startTime = DateTime.UtcNow;
            var results = new List<LearningInsight>();

            lock (_lock)
            {
                // Simple keyword-based search for now
                var contextWords = context.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                var scoredInsights = _learningInsights
                    .Select(insight => new
                    {
                        Insight = insight,
                        Score = CalculateInsightRelevanceScore(insight, contextWords)
                    })
                    .Where(item => item.Score > 0.3) // Lower threshold for insights
                    .OrderByDescending(item => item.Score)
                    .Take(limit)
                    .ToList();

                results = scoredInsights.Select(item => item.Insight).ToList();
            }

            var queryTime = DateTime.UtcNow - startTime;
            UpdateQueryMetrics(queryTime);

            _logger.LogDebug("Retrieved {Count} learning insights for context: {Context}", results.Count, context);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve learning insights for context: {Context}", context);
            return new List<LearningInsight>();
        }
    }

    public async Task<MemoryMetrics> GetMemoryMetricsAsync()
    {
        await Task.CompletedTask;
        lock (_lock)
        {
            _metrics.TotalStorageSize = CalculateStorageSize();
            return _metrics;
        }
    }

    public async Task<int> CleanupMemoryAsync(TimeSpan olderThan, CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.CompletedTask;
            var cutoffTime = DateTime.UtcNow - olderThan;
            var removedCount = 0;

            lock (_lock)
            {
                var keysToRemove = _memoryEntries
                    .Where(kvp => kvp.Value.CreatedAt < cutoffTime)
                    .Select(kvp => kvp.Key)
                    .ToList();

                foreach (var key in keysToRemove)
                {
                    _memoryEntries.Remove(key);
                    removedCount++;
                }

                _metrics.TotalEntries -= removedCount;
            }

            _logger.LogInformation("Cleaned up {Count} old memory entries", removedCount);
            return removedCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to cleanup memory entries");
            return 0;
        }
    }

    private double CalculateRelevanceScore(MemoryEntry entry, string[] queryWords)
    {
        var entryText = entry.Text.ToLowerInvariant();
        var entryWords = entryText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        var matchingWords = queryWords.Count(word => entryWords.Contains(word));
        var totalWords = Math.Max(queryWords.Length, entryWords.Length);
        
        return (double)matchingWords / totalWords;
    }

    private double CalculateInsightRelevanceScore(LearningInsight insight, string[] contextWords)
    {
        var insightText = $"{insight.Title} {insight.Description}".ToLowerInvariant();
        var insightWords = insightText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        var matchingWords = contextWords.Count(word => insightWords.Contains(word));
        var totalWords = Math.Max(contextWords.Length, insightWords.Length);
        
        return (double)matchingWords / totalWords;
    }

    private string[] ExtractTags(string text)
    {
        // Simple tag extraction - in production, this would use NLP
        var words = text.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var commonTags = new HashSet<string> { "code", "test", "plan", "error", "success", "performance", "security" };
        
        return words.Where(word => commonTags.Contains(word)).Distinct().ToArray();
    }

    private void UpdateStoreMetrics(TimeSpan storeTime)
    {
        lock (_lock)
        {
            _metrics.AverageStoreTime = TimeSpan.FromTicks(
                (_metrics.AverageStoreTime.Ticks * (_metrics.TotalEntries - 1) + storeTime.Ticks) / _metrics.TotalEntries);
        }
    }

    private void UpdateQueryMetrics(TimeSpan queryTime)
    {
        lock (_lock)
        {
            var totalQueries = _metrics.QueryTypes.GetValueOrDefault("retrieve", 0) + 1;
            _metrics.QueryTypes["retrieve"] = totalQueries;
            
            _metrics.AverageQueryTime = TimeSpan.FromTicks(
                (_metrics.AverageQueryTime.Ticks * (totalQueries - 1) + queryTime.Ticks) / totalQueries);
        }
    }

    private void UpdateErrorMetrics(string errorType)
    {
        lock (_lock)
        {
            _metrics.ErrorTypes[errorType] = _metrics.ErrorTypes.GetValueOrDefault(errorType, 0) + 1;
        }
    }

    private long CalculateStorageSize()
    {
        var totalSize = 0L;
        
        foreach (var entry in _memoryEntries.Values)
        {
            totalSize += entry.Text.Length * sizeof(char);
            totalSize += entry.Key.Length * sizeof(char);
            totalSize += entry.Tags.Sum(tag => tag.Length * sizeof(char));
        }

        foreach (var context in _processContexts.Values)
        {
            totalSize += context.ProcessId.Length * sizeof(char);
            totalSize += JsonSerializer.Serialize(context.Data).Length * sizeof(char);
        }

        foreach (var insight in _learningInsights)
        {
            totalSize += insight.Title.Length * sizeof(char);
            totalSize += insight.Description.Length * sizeof(char);
            totalSize += insight.Tags.Sum(tag => tag.Length * sizeof(char));
        }

        return totalSize;
    }
} 