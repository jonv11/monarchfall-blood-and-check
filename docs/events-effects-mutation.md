# Events, Effects, and State Mutation Model

## Context

MFBC requires a deterministic, replayable model where actions and outcomes can be reconstructed and validated. Events must provide an official record of state changes, while effects enable roguelike behavior through triggers.

## Decisions

### 1. Event Semantics

- Events are descriptive facts that record what happened.
- Events do not contain code and do not execute themselves.
- The engine processes a trigger queue: when an event is emitted, rules and effects may emit additional events, processed deterministically.

### 2. Effect Model

Effects are data-driven modifiers with explicit triggers.

Minimum effect schema:

- EffectId
- Source (tile, piece, or mutation)
- Triggers (e.g., OnEnterTile, OnTurnStart, OnDealDamage, OnDeath)
- Duration (turns, actions, permanent)
- Stacking (none, refresh, stack with cap, unique by source)
- Parameters (numbers, tags, target selectors)
- Optional Priority (only if trigger ordering matters)

Effects must be deterministic and serializable.

### 3. State Mutation

- All gameplay state changes must be observable as events.
- Rules may compute or stage changes internally, but must commit changes via emitted events.
- The engine applies state changes through a single mechanism (e.g., Apply(Event) or a reducer).
- Rules must not silently mutate the GameState.

Allowed exception: internal, non-gameplay caches may exist, but must not be part of persisted domain state.

## Rationale

- Event-driven changes enable reproducibility, replay, and testing.
- A single mutation pathway prevents hidden state transitions.
- Data-driven effects keep roguelike behavior extensible and serialization-friendly.

## Alternatives Considered

- Event objects that execute behavior: rejected to prevent imperative coupling and non-determinism.
- Direct mutation without events: rejected because it breaks replay and auditability.

## Consequences / Constraints

- Every meaningful state change must be represented as an event.
- Event processing order must be deterministic.
- Effects must remain serializable and stable across versions.

## Open Questions

- None in this document. Related questions may appear in RNG, serialization, or testing documents.
