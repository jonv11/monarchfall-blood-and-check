# Serialization, Save/Load, and Versioning

## Context

MFBC must support saving and resuming runs without replaying thousands of actions. Saves must be verifiable, debuggable, and resilient to version evolution.

## Decisions

### 1. Save Format

- Use JSON (optionally compressed) for v0 and v1.
- Easy to debug, diff, and test.
- Explicit versioning for stable compatibility.
- Binary format may be adopted later without affecting the domain model.

### 2. Save Contents

Persist a full snapshot plus run header, not just seed + action log.

Run header:

- saveVersion or schemaVersion
- gameBuildVersion (or ruleset version/hash)
- runSeed

RNG state:

- Current state of each named RNG stream (or the PRNG state object per stream)

Full GameState snapshot:

- Board tiles + occupancy
- All pieces (type, side, position, HP, etc.)
- Turn and phase counters
- Active effects or statuses with remaining duration + stacking state
- Active mutations or modifiers for the run

Optional but useful:

- Action log since last snapshot (for audit)
- Event log in debug builds only

Snapshots ensure O(1) resumption without replaying action history.

### 3. Versioning Policy

- Best-effort forward compatibility (new versions load old saves) with explicit version checks and a migration layer.
- No backward compatibility (old builds cannot load new saves).

Policy:

- New build loads saves with schemaVersion <= current via migrations.
- If schemaVersion > current, fail with a clear error message.
- If migration is not implemented for a version gap, fail explicitly.
- Documentation must include:
  - Compatibility guarantees (e.g., "supports last N minor versions" or "from v0.1 onward")
  - A changelog entry whenever schema changes

## Rationale

- Full snapshots avoid costly replay and reduce desync risk.
- JSON provides auditability during early development.
- Forward-compatible migration preserves player progress across versions.

## Alternatives Considered

- Seed + action log only: rejected because resuming would require full replay, risking brittleness and performance cost.
- Backward compatibility: rejected to avoid constraining future schema changes.
- Binary-first format: deferred until performance becomes a concern.

## Consequences / Constraints

- Save schema must be versioned explicitly.
- Every schema change must include a migration path or explicit version bump.
- Saves from future versions must fail cleanly.

## Open Questions

- None in this document. Related questions may appear in testing or dependency boundary documents.
