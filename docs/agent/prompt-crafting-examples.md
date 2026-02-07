# MFBC-Oriented Prompt Examples

## Purpose
Provide concrete prompt examples tailored to MFBC workflows.

## Scope
Use when drafting prompts for audits, Jira ticket creation, or documentation updates.

## Example 1: Create Jira Ticket from Analysis

**Scenario:** Agent analyzes repository for testing gaps and reports findings as a Jira ticket.

```
Task: Analyze board test coverage and create a Jira issue summarizing findings.

Role: 
AI coding agent conducting code audit.

Constraints:
- Only read files; do NOT commit or make changes to code
- Jira CLI (acli) is available; use it to create tickets
- Output ticket as formatted Markdown first (for approval), then submit via acli

Inputs:
- Codebase in ${workspaceFolder}
- Project: MFBC (Monarch Fall: Blood & Check)
- Focus: tests/MFBC.Core.Tests/BoardTests.cs

Steps:
1. Analyze tests/MFBC.Core.Tests/BoardTests.cs and src/MFBC.Core/Board.cs
2. Calculate test coverage: identify methods with 0 test cases
3. List concrete gaps (e.g., "Board.GetThreats() has no null input tests")
4. Create ticket content in Markdown (Context, Goal, AC, Scope)
5. Present ticket content to user for approval
6. Once approved, submit via: acli jira workitem create --type Task --project MFBC ...

Output:
- Report listing coverage gaps (3-5 examples)
- Proposed Jira task description (Markdown format)
- Confirmation that ticket was created (include ticket number)
```

## Example 2: Audit Repository for Architecture Violations

**Scenario:** Agent scans code for layering violations and generates a report.

```
Task: Audit src/ for architecture rule violations (MFBC.Core independence).

Role:
AI code auditor checking for architecture compliance.

Constraints:
- Read-only; no file modifications
- Reference: docs/architecture/overview.md section "Layering"
- Output: bullet list of violations with line numbers and severity (error/warning)

Inputs:
- Project files in ${workspaceFolder}
- Architecture rule: MFBC.Core must NOT reference MFBC.Cli

Steps:
1. Scan all .cs files in src/MFBC.Core/ for imports
2. Check for any imports from MFBC.Cli namespace
3. For each violation found, note: file, line, import statement, severity
4. Generate report with summary (total violations, breakdown by severity)

Output:
- Report: "Architecture Audit Results"
- Summary: X errors, Y warnings
- Violations list with actionable remediation steps
- If clean: "✓ No architecture violations detected"
```

## Example 3: Update Documentation with Code Examples

**Scenario:** Agent adds code examples to user guide based on actual codebase patterns.

```
Task: Add code examples to docs/development/naming-conventions.md demonstrating correct patterns.

Role:
AI documentation assistant adding code samples to a naming conventions guide.

Constraints:
MUST:
- Follow existing examples in the documentation file
- Pull code patterns from actual MFBC codebase (src/)
- Test that examples are syntactically valid
- Maintain consistent formatting and style

MUST NOT:
- Fabricate examples; they must exist in the codebase
- Break existing documentation structure
- Introduce new sections; only enhance existing sections

Inputs:
- Codebase: ${workspaceFolder}
- Target file: docs/development/naming-conventions.md
- Examples should be based on: GameState.cs, Board.cs, MoveValidator.cs

Steps:
1. Read docs/development/naming-conventions.md and understand sections
2. Identify sections lacking code examples
3. Review src/MFBC.Core/ for real examples matching each convention
4. Add examples with brief explanations
5. Verify examples are correct (simple syntax check)
6. Commit with message: "docs(naming): add code examples (MFBC-26)"

Output:
- Updated docs/development/naming-conventions.md with 3-5 new code examples
- Commit message showing additions
- Brief summary of examples added and which conventions they illustrate
```
