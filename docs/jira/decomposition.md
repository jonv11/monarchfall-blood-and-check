# Parent-Child Decomposition

## Purpose
Explain how to decompose Epics into Features, Tasks, and Subtasks.

## Scope
Applies to all Jira work item planning.

## Decomposition Hierarchy Tree

```
Epic: Implement piece exchange and compensation system
│
├─ Feature: Implement piece exchange UI with live preview
│   ├─ Task: Design UI mockups and get approval
│   ├─ Task: Implement exchange dialog component
│   │   ├─ Subtask: Build form inputs (piece selection, quantity)
│   │   ├─ Subtask: Implement live preview of exchange
│   │   └─ Subtask: Add validation feedback to user
│   └─ Task: Write integration tests for exchange workflow
│
├─ Feature: Implement piece exchange API endpoint
│   ├─ Task: Design REST API contract
│   ├─ Task: Implement exchange logic and rules
│   │   ├─ Subtask: Validate exchange legality (pieces to exchange exist, types match)
│   │   ├─ Subtask: Calculate compensation based on piece values
│   │   └─ Subtask: Update board state with exchanged pieces
│   ├─ Task: Add authentication and authorization checks
│   └─ Task: Write comprehensive API tests
│
└─ Feature: Implement exchange history and analytics
    ├─ Task: Design database schema for exchange logs
    ├─ Task: Record exchange transactions
    └─ Task: Build analytics dashboard
```

## Decomposition Rules

**Rule 1: Epic → Feature**
- Each Feature delivers one user-facing or developer-facing capability
- If a Feature is not shippable independently, it's too small (merge with another Feature)
- Example: "Exchange UI" and "Exchange API" are separate; UI alone is not useful without API

**Rule 2: Feature → Task**
- Each Task is work that can be done in 1-2 days by one developer
- If a Task requires collaboration or will take 3+ days, split it
- Example: "Implement exchange API" splits into "Design API", "Implement logic", "Test", "Deploy" if needed

**Rule 3: Task → Subtask (Optional)**
- Use Subtasks only if a Task has 4+ distinct steps or parallel work
- If Task is straightforward, use Acceptance Criteria itemization instead
- Example: A "Write tests" Task might not need Subtasks; just list test cases in AC

**Rule 4: Keep Depth Shallow**
- Avoid deep nesting (Epic > Feature > Task > Subtask > Sub-subtask) – stay at 3 levels max
- Subtasks should not have any children
- If a Subtask has children, the Task wasn't properly decomposed

**Rule 5: Parent Completion**
- A parent issue is Complete when all child issues are Complete
- Do not mark Feature as Done until all Tasks are merged
- Do not mark Epic as Done until all Features are merged
