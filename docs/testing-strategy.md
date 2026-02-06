# Testing Strategy and Determinism Guarantees

## Context

MFBC must maintain deterministic behavior for replay, debugging, and testing. The testing strategy must enforce correctness, catch regressions, and verify replay integrity.

## Decisions

### 1. Test Categories (Mandatory for Core)

Four test categories are mandatory:

- Unit tests (rules and reducers)
  - Pure, table-driven tests for move legality, rule conditions, effect stacking, and duration
  - Fast, deterministic, high volume
- Action-flow integration tests
  - Test CreateRun → ApplyAction → Events → NewState
  - Validate ordering, side effects, and invariants
  - Engine correctness tests
- Golden run tests (determinism)
  - Fixed seed + fixed action sequence
  - Assert final snapshot hash and ordered event trace (or canonical digest)
- Replay and load tests
  - Save mid-run → load → continue → compare with uninterrupted run

### 2. Determinism Testing

Determinism must be tested at two levels:

- In-memory replay
  - Same build, same seed, same actions, two fresh runs
  - Assert snapshots are byte-identical (or structurally equal)
  - Assert event sequences match exactly
- Save/load replay
  - Run A: play N actions → save
  - Run B: load save → play remaining actions
  - Assert equivalence with uninterrupted run

Optional (debug builds only):

- Assert RNG stream states match after each action

### 3. Coverage Requirements

- 100% of Core rule logic must be exercised by tests (not necessarily 100% line coverage, but every rule path must be hit)
- Critical paths must have golden runs:
  - Move application
  - Capture resolution
  - Effect triggering
  - Win/lose detection
- No Core code without tests (new rule = new tests in the same PR)
- Failing determinism test = release blocker
- Mutation testing is optional (only if rule engine stabilizes and complexity grows)

## Rationale

- Four test categories catch most regressions without excessive overhead.
- Golden runs enforce determinism at the system level.
- In-memory and save/load replay verify reproducibility under different conditions.
- Strict coverage prevents untested rules from entering the codebase.

## Alternatives Considered

- 100% line coverage: rejected as too strict, prefer rule-path coverage instead.
- Determinism testing only at snapshot level: rejected because event ordering matters for debugging and replay.
- No mandatory replay tests: rejected because save/load bugs are critical and hard to debug.

## Consequences / Constraints

- Every new rule must include corresponding tests.
- Determinism regressions block releases.
- Golden runs must be updated carefully when rules evolve.
- Test execution must remain fast to support CI and local iteration.

## Open Questions

- None in this document. Related questions may appear in the dependency boundaries document.
