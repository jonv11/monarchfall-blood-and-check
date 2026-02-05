# ADR Template

Use this template when creating new architectural decision records. Replace `NNNN` with the next sequence number.

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

## Notes

- Keep ADRs concise but complete
- Use plain language; avoid jargon
- Link to related docs, code, or other ADRs
- Update the index in [README.md](README.md) when a new ADR is created
