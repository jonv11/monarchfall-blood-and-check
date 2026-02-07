# Epic Template

## Purpose
Provide a Jira Epic template and filled example.

## Scope
Use when creating Epic work items in MFBC.

## Template

```
## Context
[Describe the business or strategic motivation. What problem in MFBC are you solving?
Why is this a priority now? What triggered this epic? Who benefits?]

## Goal
[State the high-level objective in 1-2 sentences. What capability or system will exist 
after this epic is complete?]

## Scope

### In Scope
- [Major deliverable or capability area 1]
- [Major deliverable or capability area 2]
- [Major deliverable or capability area 3]
[Add more as needed. Keep at high level—these become Features.]

### Out of Scope
- [What will NOT be included in this epic, and why?]
- [What's deferred for future work?]

## Deliverables
- [Deliverable 1]
- [Deliverable 2]
- [Deliverable 3]

## Acceptance Criteria
- [ ] [Clear, testable acceptance criterion 1]
- [ ] [Clear, testable acceptance criterion 2]
- [ ] [Clear, testable acceptance criterion 3]

## Dependencies / References
- [Link to relevant docs, tickets, or design references]

## Risks / Edge Cases
- [Risk or edge case 1]
- [Risk or edge case 2]

## Assumptions
- [Assumption 1]
- [Assumption 2]
```

## Filled Example

```
## Context
MFBC needs a tactical exchange system that allows pieces to be traded for compensation.
Currently, all captures are zero-sum and static. This limits roguelike depth and makes
piece progression less meaningful.

## Goal
Introduce a flexible piece exchange and compensation system that allows players to trade
pieces for currency or upgrades, creating meaningful long-term strategy.

## Scope

### In Scope
- Exchange UI and workflow
- Exchange legality rules and validation
- Compensation calculation and balance rules
- Integration with run progression and economy

### Out of Scope
- Multiplayer exchange (future milestone)
- Procedural NPC exchange agents (future milestone)

## Deliverables
- Exchange rule engine
- Exchange UI dialog
- Integration tests for exchange flow

## Acceptance Criteria
- [ ] Players can initiate an exchange from the CLI
- [ ] Compensation is calculated based on piece value and state
- [ ] Exchange validation prevents illegal or exploitative trades
- [ ] All exchange actions are deterministic and replayable
- [ ] Unit and integration tests cover exchange flow

## Dependencies / References
- docs/architecture/overview.md
- MFBC-42 (Run economy module)

## Risks / Edge Cases
- Exchange abuse loops (infinite value generation)
- Rare pieces causing compensation imbalance

## Assumptions
- Currency system exists or will be implemented in this Epic
- Exchange actions are modeled as standard Core Actions
```
