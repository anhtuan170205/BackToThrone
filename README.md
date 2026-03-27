# Back To Throne

**Author:** Tuan Tran Anh

**Back To Throne** is a 3D endless runner built in Unity. The project focuses on clean gameplay architecture, reusable systems, and maintainable code, with player behavior driven by a custom state machine and gameplay systems coordinated through events and singleton managers.

---

## Overview
This project is structured around small, focused systems rather than one large controller script. The player logic is separated into states such as idle, move, jump, stumble, and lose. Game flow is controlled by a `GameManager`, while stamina, score, audio, level generation, upgrades, and UI are handled by dedicated components.

---

## Project Demo
https://youtu.be/fKqodS0EmkA

---

## Key Characteristics
- Modular architecture with clear system boundaries
- Event-driven communication between gameplay systems
- Finite state machine for player behavior and transitions
- Object pooling for runtime performance optimization
- Data-driven configuration using ScriptableObjects
- Centralized game state management
- Separation between runtime logic and configuration data

---

## Architecture
The project follows a layered and decoupled structure:

### Core System
- `GameManager`: owns and broadcasts the current game state
- `ScoreManager`: tracks run and total score, applies multipliers
- `StaminaManager`: manages stamina consumption and recovery
- `AudioManager`: handles music playback and sound effects

### Player System
- `PlayerStateMachine`: controls player behavior via state transitions
- `PlayerState hierarchy`: encapsulates behavior per state
- `PlayerStatProvider`: computes runtime stats from base values and upgrades

### Gameplay System
- `LevelGenerator`: controls chunk spawning, speed progression, and difficulty
- `Obstacle`: interacts with the player and returns to pool
- `ChunkPool`: manages reusable level segments

### Shop System
- `ShopManager`: handles upgrades and bonus calculations
- `ShopUpgradeData`: defines upgrade progression

### UI System
UI components react to events from managers rather than polling

---

## Player State Machine
Player behavior is implemented as a finite state machine.

States include:
- Idle
- Move
- Jump
- Stumble
- Lose

Each state:
- Encapsulates its own behavior
- Defines transitions explicitly
- Operates independently of other states

This approach avoids large conditional blocks and enables controlled extension of player mechanics.

---

## Event-Driven Design
Systems communicate through C# events instead of direct dependencies.

Examples:
- Game state changes propagate from `GameManager`
- UI updates subscribe to score and shop events
- Input is exposed as events through `InputReader`
- Gameplay systems react to collisions and upgrades

This design reduces coupling and allows systems to evolve independently.

---

## Performance Considerations
The project uses object pooling for reusable gameplay elements such as level chunks and obstacles.

Benefits:
- Avoids frequent allocation and deallocation
- Reduces garbage collection overhead
- Ensures stable performance during continuous gameplay

---

## Data-Driven Configuration
Game configuration is separated from logic using ScriptableObjects and serialized data.
Examples:
- Base player stats
- Upgrade definitions

Runtime systems compute final values dynamically, allowing easy balancing without modifying code.

---

## Project Structure
```
Assets/
  Scripts/
    Input/
    Level/
    Manager/
    Obstacle/
	Physics/
	Pickup/
    Player/
    Shop/
    StateMachine/
    UI/
	Utilities/
```
Each directory represents a distinct responsibility, supporting maintainability and scalability.

---

## Technical Highlights
- Finite state machine for player behavior
- Event-based system coordination
- Object pooling for performance optimization
- Decoupled architecture with minimal direct dependencies
- Runtime stat computation based on upgrade data

--- 

## Potential Extensions
- Persistent save/load system for upgrades and progression
- Additional player states and mechanics
- Procedural variation in level generation
- Expanded upgrade system and balancing tools
- Platform-specific optimization
