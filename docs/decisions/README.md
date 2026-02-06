# Architectural Decision Records (ADRs)

This directory contains architectural decision records for Monarchfall: Blood & Check.

## What is an ADR?

An **Architectural Decision Record** (ADR) documents a decision that affects the design, structure, or implementation of the project. ADRs capture the context, decision, rationale, and consequences so future contributors understand *why* decisions were made.

## Status Values

ADRs use the following status values:

- **Proposed** — A decision has been drafted but not yet accepted; open for discussion
- **Accepted** — The decision has been reviewed and approved; guides current development
- **Deprecated** — The decision is no longer valid; superseded by a newer ADR or decision
- **Superseded** — Explicitly replaced by another ADR (reference the newer ADR)

## When to Write an ADR

Create a new ADR when:

- Making a significant architectural or design decision
- Choosing between multiple valid approaches with trade-offs
- Establishing a new pattern or convention for the codebase
- Recording a decision that affects multiple modules or long-term direction

Do **not** create ADRs for:
- Trivial bug fixes or refactoring
- Minor implementation details
- Decisions that don't affect architecture or broader design

## ADR Format

Use the template below. File each ADR as `NNNN-kebab-case-title.md`, where `NNNN` is a zero-padded sequence number (e.g., `0002-async-patterns.md`).

```markdown
# ADR-NNNN: Decision Title

**Date:** YYYY-MM-DD  
**Status:** Proposed | Accepted | Deprecated | Superseded  
**Deciders:** Names or roles of decision makers

## Context

Describe the issue or challenge that prompted this decision. Provide background, constraints, and the problem being solved.

## Decision

State the decision clearly and concisely. Explain *what* was decided.

## Rationale

Explain *why* this decision was made. Include:
- Benefits and advantages
- How it addresses the context
- Any trade-offs accepted

## Consequences

Describe the results and implications of this decision:
- **Positive:** Benefits and improvements
- **Negative:** Drawbacks or costs
- **Neutral:** Other impacts or side effects

## Alternatives Considered

List and briefly describe alternatives that were evaluated but not chosen. Explain why the chosen decision is better.

## Related Decisions

Link to other ADRs or documentation that relate to or depend on this decision.
```

## Decision Index

| # | Title | Date | Status |
|---|-------|------|--------|
| [0001](0001-project-scaffold.md) | Project Scaffold and Architecture | 2026-02-04 | Accepted |
| [0002](0002-cli-library.md) | CLI Parsing and Rendering Library | 2026-02-06 | Accepted |

## Process

1. **Draft** — Create a new ADR with `Proposed` status
2. **Discuss** — Share with the team for review and feedback
3. **Finalize** — Incorporate feedback and update status to `Accepted` (or `Deprecated` if rejected)
4. **Update Index** — Add or update the table above
5. **Reference** — Link the ADR in code, docs, or related decisions

## Questions?

Refer to existing ADRs for examples, or see [CONTRIBUTING.md](../CONTRIBUTING.md) for more guidance on the ADR process.
