# Standard Agent Workflow

## Purpose
Provide a repeatable workflow structure with explicit checkpoints for agent tasks.

## Scope
Use for any prompt that asks an agent to analyze, plan, implement, and report work.

## Workflow Phases

All agent tasks should follow this workflow structure with explicit checkpoints:

### Phase 1: Analyze
Agent reads context, understands requirements, identifies potential issues.

```
ANALYZE:
1. Read the ticket (acceptance criteria, dependencies, risks)
2. Check architecture rules (will changes violate layering?)
3. Identify required changes (which files, which tests?)
4. Note any ambiguities or blockers

CHECKPOINT: Stop and ask if:
- Acceptance criteria are unclear or contradictory
- Changes would violate architecture rules
- Dependencies are missing or not listed
```

### Phase 2: Plan
Agent outlines approach, but does NOT implement yet.

```
PLAN:
1. Sketch out code structure (which classes, methods, patterns)
2. Identify test scenarios (happy path, edge cases)
3. Outline commit strategy (how many commits, message style)

CHECKPOINT: Report plan to user; wait for approval before proceeding
```

### Phase 3: Execute
Agent implements changes step-by-step, validating at each step.

```
EXECUTE:
1. Create feature branch
2. Implement code feature by feature
3. After each feature, run: dotnet build (stop if warnings)
4. Add tests for each feature
5. After all tests: dotnet test (stop if failures)
```

### Phase 4: Validate
Agent verifies all acceptance criteria are met before marking complete.

```
VALIDATE:
1. Check each acceptance criterion against implemented code
2. Run full build and test suite (Release configuration)
3. Verify code follows style guide (no warnings)
4. Manual spot-check: does output match expectations?

CHECKPOINT: If any criterion not met, go back to EXECUTE
```

### Phase 5: Report
Agent summarizes what was done, what decisions were made, what's next.

```
REPORT:
- Summary of changes (what feature was implemented)
- Test results (number of tests passed, coverage percentage)
- Code quality (warnings: 0, issues: 0)
- Any challenges encountered and how they were resolved
- Next steps (PR review, merge, transition ticket)
```
