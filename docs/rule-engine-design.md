# Rule Engine Architecture and Execution Order

## Context

MFBC requires a deterministic, extensible rule engine to support classic chess rules and roguelike mutations. The engine must support dynamic rules, triggered effects, and phased execution without coupling to the CLI.

## Decisions

### 1. Rule Categories

Define rule categories as interfaces or conceptual groups:

- Action generation (produces candidate actions for UX/CLI)
- Action validation (authoritative legality checks on apply)
- Resolution rules (what the action actually does)
  - Movement resolution
  - Capture resolution (including special capture patterns)
  - Multi-step resolutions (push, chain, AoE, summon)
- Passive effects
  - Tile effects (OnEnter, OnExit, OnTurnStart, OnTurnEnd)
  - Piece/status effects (bleed, shield, poison, auras)
- Turn/phase progression (end-turn rules, phase transitions, cooldown ticks)
- Win/lose conditions (king dead, survive N turns, boss defeated)
- Run modifiers or mutations (global rules injected into the engine)

Mutations are treated as additional rule sources injected into the engine, not a separate subsystem.

### 2. Authoritative Apply Pipeline

A deterministic pipeline is required for ApplyAction:

1. Pre-validate (structural)
   - Action shape, required params present
   - Actor exists and belongs to current side
   - Target tile exists
2. Generate or resolve action context
   - Compute path, targets, and derived intent
3. Validate (rules)
   - Move legality, blocking, immunities
   - Resource costs or status restrictions
4. Resolve core action
   - Apply movement state changes
   - Emit core events (Moved, AttemptedCapture)
5. Resolve captures and collisions
   - Determine actual captures, deaths, knockbacks
   - Emit events (Captured, Died, Displaced)
6. Apply triggered effects
   - Tile triggers (OnEnter, OnLeave, OnCaptureOnTile)
   - Piece or status triggers (OnMove, OnHit, OnDeath)
   - Process triggers via a deterministic queue
7. End-of-action maintenance
   - Update durations, cooldowns, XP, currency
8. Check terminal conditions
   - Win or lose checks, run end events
9. Turn phase rules (if action ends the turn or auto-advance is triggered)
   - StartTurn and EndTurn hooks

Invariant: all steps emit structured events, processed in deterministic order.

### 3. Rule Composition Strategy

Use phases as the primary structure, with optional priorities inside a phase.

- Rules register for one or more phases
- Within a phase, ordering is stable by (priority, registrationOrder, ruleId)
- Priorities should be coarse (e.g., -100, 0, +100) unless finer control is necessary

## Rationale

- Phases provide readability and avoid fragile ordering.
- Deterministic ordering supports reproducible runs and testing.
- Rule injection enables roguelike mutations without reworking the engine.

## Alternatives Considered

- Priority-only ordering: rejected due to poor readability and fragile behavior.
- Hardcoded special cases in ApplyAction: rejected to preserve extensibility.

## Consequences / Constraints

- ApplyAction must strictly follow the pipeline.
- All rule execution must be deterministic and reproducible.
- Rule ordering must be stable across runs given the same seed and actions.

## Open Questions

- None in this document. Related questions may appear in events/effects or RNG documents.
