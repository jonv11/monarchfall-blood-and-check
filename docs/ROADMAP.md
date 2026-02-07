# MFBC Architecture Roadmap

## Purpose
Define a lightweight, maintainable architecture roadmap for the next 2-3 releases so contributors can align implementation sequencing and cross-cutting decisions.

## Scope
This roadmap is intentionally high-level and milestone-oriented (no calendar dates). It documents:
- Phase goals and milestones
- Planned module responsibilities
- Non-goals for near-term releases

## Planning Horizon
- Near term: next 2-3 releases
- Cadence: review and adjust during sprint planning and after major architecture decisions
- Format: milestone-only to reduce churn from date changes

## Phase 0: Scaffold
Status focus: project foundations and enforceable boundaries.

Milestones:
- Solution scaffold and baseline CI quality gates
- Core/CLI dependency direction codified and tested
- Documentation/ADR foundation for future architectural changes

## Phase 1: Core Rules
Status focus: deterministic domain mechanics in `MFBC.Core`.

Milestones:
- Move representation and validation flow stabilized
- Rule execution pipeline contract established
- State mutation model and replay-friendly determinism patterns enforced
- Core behavior covered by fast deterministic tests

## Phase 2: Procedural Generation
Status focus: controlled variation on top of stable core rules.

Milestones:
- Procedural content boundaries defined around core contracts
- Deterministic generation inputs/outputs documented for reproducibility
- Serialization/save/load compatibility constraints validated against generated content

## Placeholder Modules (Non-Prescriptive)
These modules are directional placeholders, not implementation commitments.

### `MFBC.RuleSet`
- Encapsulate configurable rule definitions and composition patterns
- Provide a stable seam between core state transitions and rule selection
- Support future extension without coupling rule authorship to CLI concerns

### `MFBC.ProcGen`
- Encapsulate procedural board/content generation workflows
- Preserve deterministic behavior via controlled RNG stream usage
- Emit outputs that remain compatible with core validation and replay

### `MFBC.AI`
- Host opponent decision strategies and simulation helpers
- Consume core contracts without introducing reverse dependencies into `MFBC.Core`
- Allow iterative strategy complexity without forcing UI/runtime coupling

## Cross-Cutting Architecture Guardrails
- `MFBC.Core` remains pure domain logic with no CLI/presentation dependency
- New modules depend inward on core contracts, never the reverse
- Significant architecture shifts are captured via ADRs under `docs/decisions/`
- Determinism, testability, and explicit boundaries are prioritized over feature breadth

## Near-Term Non-Goals
- Implementing GUI/web presentation layers
- Multiplayer/network architecture
- Asset/content pipeline tooling
- Premature optimization of AI sophistication before core rule stability

## How to Keep This Current
- Update this file when phase boundaries or module responsibilities materially change
- Link relevant ADRs when roadmap assumptions become decisions
- Keep entries short, directional, and implementation-agnostic
