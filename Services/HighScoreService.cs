using System.Text.Json;
using OrbitDefender.Core;
using OrbitDefender.Models;

namespace OrbitDefender.Services;

public sealed class HighScoreService : IHighScoreService
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    private readonly string _filePath;

    public HighScoreService(string? filePath = null)
    {
        var storageDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AndrinPortfolio",
            "OrbitDefender");

        Directory.CreateDirectory(storageDirectory);
        _filePath = filePath ?? Path.Combine(storageDirectory, "highscores.json");
    }

    public IReadOnlyList<HighScoreEntry> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<HighScoreEntry>();
        }

        try
        {
            var rawJson = File.ReadAllText(_filePath);
            var entries = JsonSerializer.Deserialize<List<HighScoreEntry>>(rawJson, _jsonOptions)
                ?? new List<HighScoreEntry>();

            return Normalize(entries).ToList();
        }
        catch
        {
            return new List<HighScoreEntry>();
        }
    }

    public bool Record(string playerName, int score, int level)
    {
        if (score <= 0)
        {
            return false;
        }

        var normalizedName = string.IsNullOrWhiteSpace(playerName) ? GameSettings.DefaultPilotName : playerName.Trim();
        var existing = Load().ToList();

        var newEntry = new HighScoreEntry
        {
            PlayerName = normalizedName,
            Score = score,
            Level = level,
            CreatedAtUtc = DateTime.UtcNow
        };

        existing.Add(newEntry);

        var ranked = Normalize(existing)
            .Take(GameSettings.MaxHighScores)
            .ToList();

        Save(ranked);

        return ranked.Any(item =>
            item.PlayerName == newEntry.PlayerName
            && item.Score == newEntry.Score
            && item.Level == newEntry.Level
            && item.CreatedAtUtc == newEntry.CreatedAtUtc);
    }

    private static IEnumerable<HighScoreEntry> Normalize(IEnumerable<HighScoreEntry> entries)
    {
        return entries
            .Where(entry => entry.Score > 0 && !string.IsNullOrWhiteSpace(entry.PlayerName))
            .OrderByDescending(entry => entry.Score)
            .ThenByDescending(entry => entry.Level)
            .ThenByDescending(entry => entry.CreatedAtUtc);
    }

    private void Save(List<HighScoreEntry> entries)
    {
        try
        {
            var json = JsonSerializer.Serialize(entries, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            // Fails silently to avoid crashing the game on local file permission issues.
        }
    }
}
