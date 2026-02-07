# Common Jira Anti-Patterns

## Purpose
Identify common Jira anti-patterns and how to fix them.

## Scope
Applies to Epic, Feature, Task, and Subtask creation.

## Anti-Pattern 1: Epic-Sized Tasks

**The Pattern:**
```
Title: "Implement cheating detection system"
AC: "Cheating is detected"
```

**Why It's a Pattern:**
- Takes 3+ weeks of work; spans multiple systems
- Breaks into 5+ distinct Features naturally
- No one knows when it's actually done

**The Fix:**
Create an Epic with 3-5 Features:
```
Epic: "Implement cheating detection system"
├─ Feature: "Detect rapid move sequences indicative of bots"
├─ Feature: "Detect position pre-planning via clustering analysis"
└─ Feature: "Implement admin dashboard to flag suspicious accounts"
```

---

## Anti-Pattern 2: Vague Acceptance Criteria

**The Pattern:**
```
Task: "Improve code quality"
AC:
- ✓ Code quality is improved
- ✓ Refactor as needed
```

**Why It's a Pattern:**
- "Improved" is unmeasurable; how much? by what metric?
- Reviewer has no way to verify completion
- Becomes a constant discussion: "Is this good enough?"

**The Fix:**
Be specific and measurable:
```
Task: "Refactor Board class to reduce cyclomatic complexity"
AC:
- ✓ Cyclomatic complexity per method < 5 (was 8-12)
- ✓ Code coverage remains ≥ 90%
- ✓ All existing tests pass unchanged
- ✓ Performance regression < 5% (measured by benchmark)
```

---

## Anti-Pattern 3: Missing Dependencies

**The Pattern:**
```
Task: "Implement user authentication in CLI"
– (No mention of backend API requirement)
```

**Why It's a Pattern:**
- Developer starts, realizes API doesn't exist yet
- Blocked for days; work is duplicated or conflicting
- No visibility into true critical path

**The Fix:**
Document dependencies explicitly:
```
Task: "Implement user authentication in CLI"
Dependencies: Depends on MFBC-18 (REST API auth endpoint)
Description: "Once MFBC-18 (API auth) is done, implement CLI login flow..."
```

---

## Anti-Pattern 4: Mixing Multiple Concerns

**The Pattern:**
```
Task: "Implement and test piece movement validation and add docs"
```

**Why It's a Pattern:**
- Too many deliverables; no singular focus
- Review becomes unclear: "Is this done? What about tests? Docs?"
- Hard to parallelize work

**The Fix:**
Split into focused Tasks:
```
Task: "Implement piece movement validation"
Task: "Write unit tests for movement validation"
Task: "Document movement rules in user guide"
```

---

## Anti-Pattern 5: Creating Subtasks for Everything

**The Pattern:**
```
Task: "Add new CLI command"
├─ Subtask: "Add new CLI command"
├─ Subtask: "Write help text"
├─ Subtask: "Test it"
```

**Why It's a Pattern:**
- Subtasks mirror the Task; no additional value
- Adds overhead without clarity
- Each Subtask is not truly independent

**The Fix:**
Use Subtasks only when they're independent steps or when Task is complex:
```
Task: "Add board analysis CLI command"

AC:
- ✓ CLI command accepts board position and outputs analysis metrics
- ✓ Metrics include: threats, capture opportunities, position evaluation
- ✓ Help text clearly describes command usage
- ✓ Unit tests cover 10+ board configurations
- ✓ No build warnings
```

Or, if Task is truly complex with parallel work:
```
Task: "Build real-time multiplayer sync system"
├─ Subtask: "Design message protocol for board updates" (design phase)
├─ Subtask: "Implement client-side sync engine" (coding phase)
├─ Subtask: "Implement server-side state reconciliation" (parallel coding)
└─ Subtask: "Write integration tests for sync scenarios" (testing phase)
```

---

## Anti-Pattern 6: No Clear Definition of Ready

**The Pattern:**
```
Task: "Fix performance"
– (No context, no measurement, no AC about what metric improves)
```

**Why It's a Pattern:**
- Developer doesn't know where to start
- "Performance" could mean anything
- Review disagreement: "Is it fast enough now?"

**The Fix:**
Define measurable, specific AC:
```
Task: "Optimize board rendering performance to < 16ms per frame"

Description: "Board currently renders in 40-60ms per frame; goal is 60 FPS (< 16.67ms frame time)"

AC:
- ✓ Board rendering < 16ms per frame (measured at 60 FPS)
- ✓ No memory leaks during 10+ minute sessions
- ✓ GPU utilization remains < 60%
- ✓ Benchmark suite added to CI
```
