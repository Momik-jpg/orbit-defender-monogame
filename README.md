# Projekt - Orbit Defender (MonoGame)

## Kurz erklärt
Orbit Defender ist mein grösstes C#-Game im Portfolio.  
Ziel war, saubere Architektur, Spielmechanik und gute Bedienung in einem grösseren Projekt zu verbinden.

## Was kann das Projekt?
- Echtzeit-Gameplay mit Game-Loop (`Menu`, `Playing`, `Paused`, `GameOver`)
- Steuerung per WASD oder Pfeiltasten
- schnelleres Schiff für flüssigeres Gameplay
- Schusssystem mit Cooldown, Score und Level-Fortschritt
- Gegner-Spawn mit steigender Schwierigkeit
- Kollisionen (`Player <-> Asteroid`, `Laser <-> Asteroid`)
- Namenseingabe für personalisierte Highscores
- persistente Top-10-Highscores als JSON

## Projektaufbau (OOP)
```text
Core/
  GameMode, GameSettings, GameSession
Models/
  PlayerShip, LaserShot, Asteroid, Star, HighScoreEntry
Services/
  IInputService + InputService
  ISpawnService + SpawnService
  ICollisionService + CollisionService
  IHighScoreService + HighScoreService
OrbitDefenderGame.cs
```

## Start
1. Voraussetzungen:
   - .NET 8 SDK
   - OpenGL-fähige Umgebung (MonoGame DesktopGL)
2. Im Projektordner ausführen:
   - `dotnet restore`
   - `dotnet run`

## Steuerung
- Bewegung: `WASD` oder Pfeiltasten
- Schiessen: `SPACE`
- Pause/Fortsetzen: `P` oder `ENTER`
- Neustart nach Game Over: `ENTER` oder `R`
- Zurück ins Menü: `ESC`

## Was ich gelernt habe
- grössere C#-Projekte in klare Komponenten aufteilen
- stabile Game-Loop mit Zustandswechseln bauen
- Services und Interfaces für wartbaren Code nutzen
- JSON-Persistenz robust in ein Spiel integrieren

## Warum im Portfolio?
Orbit Defender zeigt, dass ich OOP-Strukturen nicht nur für Business-Apps, sondern auch für interaktive Echtzeit-Anwendungen einsetzen kann.
