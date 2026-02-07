# Workflow and Status Transitions

## Purpose
Define the Jira workflow states and transition rules for MFBC.

## Scope
Applies to all Jira issues in MFBC.

MFBC uses a four-status workflow optimized for continuous work tracking:

```
To Do → In Progress → In Review → Done
  ↑                                    ↓
  └────────────────────────────────────┘ (only on reject)
```

## Status Definitions

**To Do** (Not Started)
- Work is planned but not yet started
- Acceptance Criteria are defined and ready
- Resources may not be assigned
- **Transition to In Progress** when you begin implementation

**In Progress** (Active)
- Work has begun; developer is actively implementing
- Subtasks may be created and tracked during this phase
- Expected to complete within 1-2 days (Task) or 3-5 days (Feature)
- **Transition to In Review** when implementation is complete and PR is ready

**In Review** (Waiting for Approval)
- Implementation is complete; PR is open for review
- Code is in a reviewable, testable state
- Waiting for approval from reviewer(s)
- **Transition back to In Progress** only if reviewer requests changes (rare; usually done in PR comments)
- **Transition to Done** when PR review is approved and merged

**Done** (Complete)
- PR has been merged to main
- All Acceptance Criteria verified as met
- Code is live in main branch
- No further work on this item (new issues for follow-ups)

## Transition Guidelines

| From | To | When | Notes |
|------|----|----|-------|
| **To Do** | **In Progress** | Start work | Immediate; you began implementation |
| **In Progress** | **In Review** | PR ready | Code complete, PR open for review |
| **In Review** | **Done** | PR merged | Approval given; code in main |
| **In Review** | **In Progress** | Major changes needed | Only if significant rework; minor feedback via PR comments |
| **To Do** | **Done** | Rarely | Only if work determined unnecessary (close without impl.) |
