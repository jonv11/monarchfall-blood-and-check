# Core to CLI Contract

## Context

MFBC uses a CLI-first architecture with a pure Core engine. The CLI must remain a presentation layer that translates user input into Core actions and renders structured output from Core. The contract must be minimal, deterministic, and testable.

## Decisions

### 1. CLI Inputs (Minimum for a Playable Run)

Minimum commands and arguments for a reproducible run:

- new
- --seed <int|string> (optional but strongly recommended)
- --mode <string> (optional default)
- --difficulty <int|enum> (optional default)
- show (print current board/state)
- moves <from> or moves --at <coord> (list legal actions for a piece/tile)
- play <move> (e.g., play e2e4 or play --from e2 --to e4)
- end-turn (if the loop is turn-based with phases)
- save <path> and load <path> (optional for v0)

Absolute smallest set for v0: new, show, play, and --seed.

### 2. Core API Surface (Minimum Use-Cases)

Core exposes use-cases, not UI formatting:

- CreateRun(config) -> RunId + initial Snapshot
- GetSnapshot(runId) -> Snapshot
- GetLegalActions(runId, selection?) -> ActionList
- ApplyAction(runId, action) -> ApplyResult

ApplyResult includes:

- newState (or delta)
- events[]
- errors[] (if invalid)

Optional but valuable early:

- Save(runId, path) and Load(path) -> RunId
- Serialize(runId) and Deserialize(blob) for tests

Design simplification: everything is an Action, and every action produces Events.

### 3. Output Responsibility

- Core returns structured data only.
- CLI owns all formatting and rendering.

Core returns:

- Snapshot (structured)
- Events (structured)
- Diagnostics (structured)

CLI renders:

- ASCII board
- tables (moves list, piece stats)
- human-readable messages

Optional exception: Core may provide debug strings for logs, but not as the primary contract.

## Rationale

- Keeps Core deterministic and testable.
- Prevents UI concerns from contaminating domain logic.
- Allows multiple front ends (CLI today, UI later) without changing Core.

## Alternatives Considered

- Core returns render-ready strings: rejected because it couples Core to a presentation format.
- CLI performs rules/validation: rejected because it duplicates domain logic outside Core.

## Consequences / Constraints

- The CLI must translate user input into Core Actions consistently.
- All errors and events must be returned in structured form.
- Any future UI must consume the same Core use-cases.

## Open Questions

- None in this document. Related questions may appear in move representation, events, or serialization documents.
