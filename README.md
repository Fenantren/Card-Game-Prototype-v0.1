# Card Game Prototype
 
A solo, in-progress **Slay the Spire-style deck-building roguelite**, built in Unity (C#).
 
<!-- Add a gameplay GIF or screenshot here -->
<!-- ![Card game prototype](docs/gameplay.gif) -->
 
> **Status: active prototype.** This is a larger, ongoing project rather than a finished jam game — architecture and systems are still evolving. See [Roadmap](#roadmap) below.
 
## Overview
 
A turn-based deck-building roguelite where the player fights through procedurally-connected rooms, building a deck of cards and managing hero health across runs.
 
## Core Systems
 
- **Deck system** — persists across scenes using `DontDestroyOnLoad`, so the player's deck survives transitions between combat, map, and room scenes
- **Map system** — node-based run structure using `MapNodeData` / `MapData` ScriptableObjects, rendered via BFS traversal, with `DoorView` handling room-type sprite lookup
- **Room types** — placeholder scenes for Rest, Treasure, Elite, and Boss rooms
- **Hero system** — tracks and persists hero health across the run
- **HUD** — event-driven UI using C# `Action` delegates rather than polling, so UI updates react to game state changes
- **Map interaction gating** — an `IsMapOpen` flag blocks other interactions while the map UI is active
- **Scene management** — a `SceneNames` static constants class avoids magic strings when loading scenes
## Architecture Notes
 
The codebase started as ~60 existing scripts inherited from an earlier prototyping pass. Before adding new features, it went through a full system map and health check, which surfaced and fixed several priority issues:
 
- Tight coupling in `TurnSystem`
- Duplicated shield-handling logic
- A view layer referencing the wrong underlying system
- A camera caching bug
Core game data (cards, player state, map generation) is structured around **ScriptableObjects**, keeping content and logic separated so new cards/rooms can be added without touching code.
 
## Tech Stack
 
- Unity
- C#
- ScriptableObject-driven architecture
- Aseprite for pixel art assets
## Roadmap
 
- [ ] Card variety / content pass
- [ ] Status effects system
- [ ] Elite combat differentiation
- [ ] Deck viewer UI
- [ ] Map path marking
- [ ] Procedural map generation
- [ ] Character selection screen
- [ ] Parry/dodge mechanic (predictive, card-based blocking — inspired by *Clair Obscur*)
