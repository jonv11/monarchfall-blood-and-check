# Move Representation and Application Flow

## Context

MFBC must support classic chess moves while remaining flexible for roguelike rule mutations. A stable, extensible action model is required to keep Core and CLI decoupled and deterministic.

## Decisions

### 1. Canonical Action Model

All player inputs are modeled as Actions. Move is one Action kind.

Action = { kind, actorId, parameters, clientTag? }

MoveAction parameters minimally:

- from: Coord
- to: Coord

Captures, promotions, special effects, and rule-specific behavior are derived during apply and emitted as events. When a player must make an explicit choice (e.g., promotion), it is represented as a distinct action, such as:

- PromoteAction { pieceId, promoteTo }

### 2. Validation Flow

Two-stage deterministic validation is required:

- GetLegalActions(state, context) -> Action[] (advisory)
- ApplyAction(state, action) -> ApplyResult (authoritative, revalidates)

The legal list is for UX guidance. Apply always revalidates to prevent stale or desynced actions.

### 3. Special Moves and Variants

Initial version (v0):

- Support promotion (as a follow-up action)
- Defer castling and en passant unless classic parity is a requirement

Modeling approach:

- Special moves are rule-driven expansions, not hardcoded move types
- Example: a rule recognizes king moving two squares and emits the rook move as additional events
- En passant is modeled by a capture rule that targets a different tile than to
- When special cases require a player choice, they become explicit actions

If castling or en passant are required early, the action should carry a stable actionId or variantId derived from GetLegalActions so the CLI can apply the exact suggested action deterministically.

## Rationale

- A single Action model stays stable as roguelike variants grow.
- Rule-driven behavior avoids brittle special-case logic.
- Revalidation on apply protects determinism and testing integrity.

## Alternatives Considered

- Hardcoded move types (e.g., CastlingMove, EnPassantMove): rejected due to brittleness and reduced extensibility.
- ApplyAction without revalidation: rejected due to desync risk and unsafe scripting.

## Consequences / Constraints

- All input must be expressible as Actions.
- ApplyAction must be the single authoritative entry point for state changes.
- Special behaviors must be expressed via rules and events.

## Open Questions

- Whether castling or en passant must be supported in v0 (to be confirmed if classic parity is required).
