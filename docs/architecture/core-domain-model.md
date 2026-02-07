# Core Domain Model

## Context

Monarchfall: Blood & Check (MFBC) is a chess roguelite with procedural boards, dynamic rules, and permadeath. The core model must be flexible enough to support irregular boards, evolving rules, and mutable tiles/pieces while preserving deterministic behavior and clean Core/CLI separation.

## Decisions

### 1. Board Representation

- The board is a sparse map of tiles keyed by coordinate: Board = Map<Coord, Tile>.
- Tile existence is defined by membership in the map: tile exists iff it is present in the map.
- Coordinates are integer-based and may be negative.
- The board is implicitly bounded by existing tiles; legality checks use existence, not optional metadata bounds.
- Optional Bounds metadata may exist for generation or UI, but it must not control legality rules.

### 2. Tile Model

- A tile minimally guarantees:
  - Existence (it is in the board map).
  - Single-piece occupancy (at most one piece can occupy a tile).
  - Enterability (can a standard move end here).
  - Deterministic passive effects.
  - Rule-selecting tags.
- Tile behavior should be expressed via events/rules rather than hardcoded fields beyond the minimal requirements.

### 3. Piece Model

A piece minimally defines:

- Identity.
- Type.
- Side or faction.
- Current position (or tile reference).
- Alive state (HP or equivalent).
- Move set reference.

All temporary power, status, and behavior changes are expressed via effects or modifiers rather than dedicated fields.

### 4. Sides and Neutral Entities

- There are always two player sides.
- Neutral entities are modeled as pieces with side = Neutral.

### 5. Enterability vs Placement

- Enterability means: can a normal move end on this tile.
- Placement (spawn, teleport, push, summon) can override enterability only via explicit rules or effects.
- A piece may occupy a non-enterable tile only if an explicit rule or effect permits it.

### 6. Tile Addition and Removal

- Tiles may be added or removed during a board via explicit events with deterministic resolution.
- Two removal modes are defined:
  - Safe remove (default): cannot remove an occupied tile.
  - Forced remove (explicit rule/event): allowed, must specify a deterministic disposition:
    - CaptureOccupant
    - DisplaceOccupant (to nearest valid tile, or a specified tile, or fail if none)
    - ConvertToNeutralPieceObstacle (rare; deterministic)

### 7. Core Invariants

- Any position reference must point to an existing tile.
- No piece may occupy a non-existing tile.
- Single-piece occupancy is always enforced.
- Enterability blocks standard move legality unless an explicit effect bypasses it.

## Rationale

- Sparse maps allow irregular shapes, holes, and dynamic tile changes without artificial bounds.
- Separating enterability from placement keeps standard move legality stable while allowing roguelike effects.
- Modeling neutrals as pieces keeps interactions (blocking, capture, effects) consistent.
- Minimal core fields reduce premature locking of roguelike mechanics while preserving determinism.

## Alternatives Considered

- Fixed 8x8 array board: rejected due to irregular board requirements and tile removal/addition.
- Hardcoded tile properties per effect: rejected in favor of event-driven behavior.
- Separate neutral obstacle type (non-piece): rejected to keep interaction rules uniform.

## Consequences / Constraints

- All legality checks must consult tile existence and enterability from the map.
- Any system that references positions must validate tile existence.
- Forced tile removal events must declare deterministic occupant disposition.
- Future rule expansions must remain compatible with the minimal core model.
- Deterministic initialization must use explicit tile ordering and seed-derived random streams.

## Open Questions

- None in this document. Related questions may be captured in future topic documents (rules, events, serialization).
