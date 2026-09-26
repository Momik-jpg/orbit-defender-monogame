using Microsoft.Xna.Framework;
using OrbitDefender.Core;
using OrbitDefender.Services;
using Xunit;

namespace OrbitDefender.Tests;

public sealed class SpawnServiceTests
{
    [Fact]
    public void CreateAsteroid_WithSameSeed_ReturnsSameAsteroid()
    {
        var bounds = new Rectangle(0, 0, 800, 600);
        var firstSession = new GameSession();
        var secondSession = new GameSession();

        firstSession.Reset();
        secondSession.Reset();

        var first = new SpawnService(new Random(42)).CreateAsteroid(firstSession, bounds);
        var second = new SpawnService(new Random(42)).CreateAsteroid(secondSession, bounds);

        Assert.Equal(first.Position, second.Position);
        Assert.Equal(first.Velocity, second.Velocity);
        Assert.Equal(first.Size, second.Size);
        Assert.Equal(first.ScoreValue, second.ScoreValue);
    }
}
