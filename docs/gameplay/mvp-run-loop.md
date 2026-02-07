# MVP Run Loop (v0)

## Purpose
Define the minimal run lifecycle and CLI interaction contract for v0 so `new`, `show`, and `play` behave consistently across process invocations.

## Scope
- In scope: run definition, lifecycle, one-shot command flow, minimal persistence rules, error behavior, transcript examples.
- Out of scope: adding commands, changing Core APIs, full save/load or replay system design.

## References
- `docs/architecture/core-cli-contract.md`
- `docs/architecture/overview.md`
- `docs/cli/cli-cheatsheet.md`
- Related: `MFBC-45`

## Definitions
### Run
A run is one gameplay attempt with:
- a start (`mfbc new`)
- zero or more progression steps (`mfbc play <action>`)
- a terminal state (`Won` or `Lost`) or replacement by a new run (`Aborted`)

### Invocation model (v0)
- CLI is one-shot: each command runs once and exits.
- No interactive/REPL mode is part of this contract in v0.
- `show` and `play` must load the active run from disk.

## Responsibilities (Core vs CLI)
### Core responsibilities
- Create initial run state from seed/version inputs.
- Return structured snapshot/state/events/errors (no presentation formatting).
- Validate and apply actions.

### CLI responsibilities
- Parse command arguments/options.
- Load/save active run state from/to disk.
- Render human-readable output from Core structured data.
- Enforce command-level behavior (missing run, usage errors, terminal-run refusal).

## Minimal persistence contract (v0)
### Active run location
- Runtime folder: `.mfbc/`
- Active run file: `.mfbc/run-state.json`

### Read/write rules
- `new`: creates/replaces `.mfbc/run-state.json` with a fresh run.
- `show`: reads `.mfbc/run-state.json`, does not mutate it.
- `play`: reads `.mfbc/run-state.json`; on successful apply, writes updated state back.
- If Core rejects a move/action, persisted state is unchanged.
- If no active run file exists, `show` and `play` return an error.

### Temporary implementation note
Current in-memory session storage (`src/MFBC.Cli/SessionStore.cs`) is a temporary placeholder and does not satisfy this contract. v0 run-loop behavior requires disk-backed active-run state across separate CLI invocations.

## Run lifecycle rules
### Start
- `mfbc new [--seed <seed>]`
- If `--seed` is provided, use exactly that seed value.
- If omitted, generate/select a seed and print the exact seed used.
- Run starts with `Status=InProgress`, `Step=0`.

### Progression
- `mfbc show`: inspect current state.
- `mfbc play <action>`: attempt one action.
- `Step` increments only when action application succeeds.

### Termination
- Terminal states are `Won` or `Lost` (set by Core).
- `mfbc play` on terminal run must fail with clear guidance to start `mfbc new`.
- Starting `mfbc new` when an active run exists aborts/replaces the previous active run.

## v0 command flow and I/O expectations
### `mfbc new [--seed <seed>]`
- Success output includes at least: `Version`, `Seed`, `Status`, `Step`, and board snapshot.
- Unknown options/usage errors return non-zero.
- If seed is rejected by Core, return an error and do not replace current active run.

### `mfbc show`
- Requires active run file.
- Output order (stable minimum):
1. `Version`
2. `Seed`
3. `Status`
4. `Step`
5. board snapshot

### `mfbc play <action>`
- Requires active run file.
- `<action>` is required and non-empty.
- CLI performs envelope checks (present/non-empty command input).
- Core owns action semantic validation.
- Success: updated snapshot printed and persisted.
- Invalid/illegal action: error printed; state unchanged.

## Error behavior
- No active run (`show`/`play`): error, non-zero.
- Missing required argument/unknown option: usage error, non-zero.
- Invalid/illegal gameplay action: error from Core surfaced; state unchanged.
- I/O failure on `.mfbc/run-state.json`: error, non-zero.

## Example transcript (illustrative)
```text
$ mfbc show
ERROR: No active run. Start one with: mfbc new

$ mfbc new --seed 12345
New run created.
Version: 0.1.0-dev+abc123
Seed: 12345
Status: InProgress
Step: 0
Board: (initial snapshot)

$ mfbc play
ERROR: Missing <action>.
Usage: mfbc play <action>

$ mfbc play e9e5
ERROR: Invalid move format.
State unchanged.

$ mfbc play e2e4
OK.
Version: 0.1.0-dev+abc123
Seed: 12345
Status: InProgress
Step: 1
Board: (updated snapshot)

$ mfbc new
WARNING: Active run will be aborted and replaced.
New run created.
Version: 0.1.0-dev+abc123
Seed: 987654321
Status: InProgress
Step: 0
Board: (initial snapshot)
```
