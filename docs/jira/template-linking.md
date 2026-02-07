# Linking Related Items

## Purpose
Explain how to link Jira work items and references consistently.

## Scope
Applies to parent-child links, dependencies, and PR references.

## Parent-Child Links

When you create child items (Feature under Epic, Task under Feature), Jira will link them automatically. No manual action needed.

**To link:**
1. Open the parent issue (Epic or Feature)
2. Click "Link child" or in Description, note child issues: `- [ ] MFBC-XX (Feature: ...)`
3. Create new issues as children of the parent
4. Verify parent-child relationship is set correctly

## Dependencies

In the "Depends on" section, list tickets that **must complete before this one can start**.

```
## Dependencies / References
Depends on: MFBC-17 (Board model must be stable)
Blocks: MFBC-20, MFBC-21 (these features depend on this task)
```

## Related Links

In the "Related" section, list tickets that are **relevant but not blocking**:
- Design documents
- Related features working in parallel
- Prior work this task extends

## References in Description

Reference other work inline:
```
This implements the strategy outlined in MFBC-18. See also the design doc at [link].
Complements MFBC-22 (board analysis) and MFBC-23 (threat detection).
```

## PR Links

When creating a PR, include "Resolves MFBC-XX" in the PR description:
```
## Description
Implements pawn movement validation for MFBC-30.

## Resolves
Resolves MFBC-30
```

Jira will automatically link the PR to the ticket once merged.
