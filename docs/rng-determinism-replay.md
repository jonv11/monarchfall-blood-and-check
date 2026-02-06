# RNG, Determinism, Seed, and Replay Strategy

## Context

MFBC requires reproducible runs for testing, debugging, and player sharing. The run seed and the action sequence must be sufficient to reproduce outcomes, while minimizing desync risk when rules evolve.

## Decisions

### 1. RNG Streams

- Use multiple named RNG streams derived from a single run seed.
- Example stream names: boardGen, mutations, combat, loot.
- Derivation strategy: streamSeed = Hash(runSeed, streamName), each stream owns its own deterministic PRNG state.

### 2. Replay Record

- Primary replay mechanism: run seed + action log.
- Optional periodic snapshots may be stored for fast seek or debugging.
- Optional event log is allowed in debug mode but is not required for normal replays.

### 3. Determinism Scope

- Determinism is guaranteed within the same build or version.
- Runs must include an explicit replay schema or version in the run header.
- Cross-platform determinism is a potential future milestone, not a v0 requirement.

## Rationale

- Multiple RNG streams prevent incidental changes from cascading across unrelated systems.
- Seed + action log is compact, auditable, and sufficient for replay.
- Versioned determinism prevents false expectations when rules or ordering change.

## Alternatives Considered

- Single global RNG stream: rejected due to high coupling between unrelated systems.
- Full event log as primary replay: rejected due to storage size and limited need for normal play.
- Hard cross-platform determinism from day one: deferred to avoid over-constraining early development.

## Consequences / Constraints

- Any new RNG stream must be named and derived from the run seed.
- Action log must be complete and ordered to ensure replay validity.
- Engine must preserve stable ordering within a given version.

## Open Questions

- None in this document. Related questions may appear in serialization or testing documents.
