# AI Prompt Crafting Guidelines

This guide provides best practices for crafting high-quality prompts for AI coding agents in VS Code (GitHub Copilot, Claude, etc.). The focus is on preventing hallucinations, ensuring grounded reasoning, and achieving reliable results when agents interact with code, Jira, CLIs, and formatted data.

## Table of Contents

1. [Core Principles](#core-principles)
2. [Prompt Structure](#prompt-structure)
3. [Scoping Rules](#scoping-rules)
4. [Standard Agent Workflow](#standard-agent-workflow)
5. [VS Code Prompt File Conventions](#vs-code-prompt-file-conventions)
6. [Formatting & Encoding Pitfalls](#formatting--encoding-pitfalls)
7. [MFBC-Oriented Examples](#mfbc-oriented-examples)
8. [Prompt Quality Checklist](#prompt-quality-checklist)
9. [Anti-Hallucination Patterns](#anti-hallucination-patterns)

---

## Core Principles

### 1. **Grounding & Concreteness**
- **Avoid:** Vague abstractions ("write good code", "handle errors properly", "think about efficiency")
- **Use:** Specific, measurable criteria ("code follows `.editorconfig` rules", "all tests pass", "method names use camelCase")
- **Why:** Vague instructions lead to hallucinated implementations that don't match actual project standards

### 2. **Explicit Constraints**
- Always state what **should NOT** happen
- List tool/command availability upfront (e.g., "Use `dotnet format`", "Don't use `npm`")
- Specify limits (file size, token count, output length)
- **Why:** Agents without constraints will attempt anything, including invalid or unsafe actions

### 3. **Step-by-Step Reasoning**
- Break complex tasks into numbered substeps
- Add verification checkpoints between steps
- **Why:** Linear reasoning reduces hallucinations; agent commits to intermediate states and can validate before continuing

### 4. **Verifiable Acceptance**
- Define how success is measured: "tests pass", "no warnings", "matches existing pattern X"
- Include validation commands that can be run to confirm completion
- **Why:** If task completion can't be verified, agent may claim success regardless

### 5. **Context First, Details Second**
- Start with problem statement and why the work matters
- Then provide step-by-step execution
- Include examples and counter-examples
- **Why:** Context helps agent understand intent; helps catch hallucinations that violate intent

---

## Prompt Structure

A well-structured prompt has these sections in order:

### 1. **Role** (1-2 sentences)
Define what the agent is doing and its responsibilities.

```
You are an AI coding agent responsible for implementing features in the MFBC 
(Monarchfall: Blood & Check) C# game engine. Your task is to write production-ready code 
following all MFBC architecture rules and coding standards.
```

### 2. **Constraints** (Bullet list)
List what the agent **must follow** and what it **must avoid**.

```
MUST:
- Write code in C# targeting .NET 8.0
- Follow all rules in .editorconfig (indentation, naming, formatting)
- Include unit tests for all logic in MFBC.Core
- Treat all warnings as errors; build must have zero warnings

MUST NOT:
- Introduce dependencies from MFBC.Cli to MFBC.Core (layering violation)
- Use reflection or dynamic code generation
- Commit directly; always push to feature branch first
- Skip writing XML documentation for public methods
```

### 3. **Inputs** (What you're providing to the agent)
State what context, files, or data the agent is working with.

```
Input:
- Ticket MFBC-24 (Jira work item with acceptance criteria)
- Codebase structure: src/MFBC.Core, src/MFBC.Cli, tests/MFBC.Core.Tests
- Reference: .editorconfig for formatting rules
- Reference: docs/architecture/overview.md for layering rules
```

### 4. **Steps** (Numbered instructions)
Break the task into sequential steps with verification points.

```
Steps:
1. Read and understand ticket acceptance criteria
2. Review existing MFBC.Core architecture (docs/architecture/overview.md)
3. Design the feature with reference to existing patterns
4. Implement in MFBC.Core, add unit tests
5. Run: dotnet build --configuration Release (verify no warnings)
6. Run: dotnet test --configuration Release (verify all tests pass)
7. Commit with message: "feat(core): <description> (MFBC-24)"
8. Push branch: git push -u origin feature/MFBC-24-<title>
9. Create PR with acceptance criteria checklist
```

### 5. **Outputs** (What the agent should produce/report)
Describe what deliverables, outputs, or reports the agent should provide.

```
Output:
- Code implementing the feature (MFBC.Core only, no Cli changes unless specified)
- Unit tests (minimum 90% coverage on new code)
- Commit message referencing MFBC-24
- PR description with acceptance criteria checklist (all items ✓ checked)
- Brief summary of what was implemented and any challenges encountered
```

---

## Scoping Rules

### When to Include Code Inline

**Use inline code blocks when:**
- Code example is short (< 20 lines)
- Code demonstrates a pattern or template
- Code is complete/compilable

```markdown
Example usage:
\`\`\`csharp
public class MoveValidator
{
    public bool IsLegalMove(Board board, Move move)
    {
        // Validate move based on piece type
        return true;
    }
}
\`\`\`
```

### When to Reference Files

**Reference files when:**
- File is large (> 50 lines) and agent needs to understand structure
- Agent needs to follow existing patterns from that file

```
See src/MFBC.Core/Board.cs for the Board class structure; follow the same pattern 
for ValidateMove() implementation (public method, summary comment, parameter validation).
```

### When to Summarize

**Summarize when:**
- File is very large (thousands of lines) and only a section is relevant
- Goal is to give agent context without overwhelming token count

```
MFBC.Core.Tests/BoardTests.cs contains 200+ test cases. For your feature, focus on 
\`MoveValidationTests\` section (lines 50-120) which shows the test pattern: 
Arrange board state, Act (call MoveValidator), Assert (verify result).
```

### When to Truncate

**Truncate when:**
- File is extremely large and only beginning/end matters
- Middle section is repetitive or not relevant to task

```
The CHANGELOG.md file has 500+ entries. Review only the most recent 10 entries 
(top of file) to understand our versioning and changelog style. Each entry uses:
- Date, version, section headers (Added, Fixed, Changed)
- Bullet points under headers
- Reference to GitHub issues where applicable
```

---

## Standard Agent Workflow

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

---

## VS Code Prompt File Conventions

### File Structure

VS Code prompt files (`.copilot-instructions.md`, `.github/copilot-instructions.md`) have this structure:

```markdown
# Agent Instructions for [Project Name]

## Overview
[1-3 sentence summary of project and agent purpose]

## Architecture Rules
[Key architectural constraints that agents must follow]

## Development Standards
[Code style, testing, documentation requirements]

## Workflow
[Standard steps for implementing features]

## Verification
[Commands agents should run to validate work]

## References
[Links to key documentation]
```

### Front Matter & Variable Interpolation

**VS Code provides variables in prompt file context:**
- `${workspaceFolder}` – Workspace root path
- `${file}` – Current file being edited
- `${selectedText}` – Selected text in editor

**Use in prompts like:**
```
The project is in ${workspaceFolder}. When referencing files, use paths relative 
to the project root (e.g., src/MFBC.Core/Board.cs).

Current file: ${file}. If modifying this file, ensure changes are compatible 
with its existing structure and imports.
```

### Context File Referencing

Reference project files and documentation directly:

```
The architecture rules are in docs/architecture/overview.md. Pay special attention 
to the "Layering" section: MFBC.Core must have no dependencies on MFBC.Cli.

Code style is defined in .editorconfig. Before committing, run: dotnet format
```

### Instruction Style

**Use imperative, clear language:**
- ✓ "Write unit tests for all public methods"
- ✗ "You should probably write tests if you feel like it"

**Avoid assumptions about agent knowledge:**
- ✓ "Use C# XML documentation: /// <summary>...</summary>"
- ✗ "Add docs (agents know how)"

**Include "Why" context:**
```
MUST: Include unit tests in MFBC.Core.Tests for all new logic.
WHY: MFBC enforces comprehensive test coverage; builds fail without 90%+ coverage.
HOW: Create a test class in tests/MFBC.Core.Tests/, use XUnit framework, 
     follow naming: [Feature]Tests.cs
```

---

## Formatting & Encoding Pitfalls

### Pitfall 1: Jira's ADF JSON Format

**Problem:** Jira descriptions use ADF (Atlassian Document Format), a JSON structure that's different from plain Markdown.

**Detection:** If sending data to Jira via CLI or API, ADF is required. Check if output needs to be JSON or plain text.

```json
{
  "version": 1,
  "type": "doc",
  "content": [
    {
      "type": "paragraph",
      "content": [
        {
          "type": "text",
          "text": "This is a paragraph"
        }
      ]
    }
  ]
}
```

**Best Practice:** When creating Jira tickets or descriptions:
- Use plain Markdown in prompts (easier for humans to read)
- If CLI requires ADF, convert after human approval
- Document the expected format in prompts: "Output should be plain Markdown; Jira CLI will convert to ADF"

### Pitfall 2: Shell Escaping

**Problem:** Special characters in shell commands need escaping; different shells (bash, PowerShell) have different rules.

**Example Issues:**
```bash
# BASH - requires escaped quotes
git commit -m "message with \"quotes\""

# PowerShell - different escaping
git commit -m 'message with "quotes"'

# Both fail if not careful with $ or backticks
```

**Best Practice:** 
- Use file-based payloads instead of inline strings (see below)
- If using inline: specify the shell (bash, PowerShell, etc.)
- Document escaping rules in prompt

```
When running dotnet CLI commands, escape special characters:
- Bash: Use single quotes for strings with spaces/special chars
- PowerShell: Use both single and double quotes as needed
Better: Save complex payloads to files and reference them
```

### Pitfall 3: File-Based Payloads

**Problem:** Large or complex data (commit messages, PR bodies, JSON) are hard to escape inline.

**Solution:** Use file-based payloads.

```bash
# Instead of:
git commit -m "Long message with special chars..."

# Use:
cat > .commit-msg.txt << 'EOF'
feat(core): implement move validation

- Validate pawn movement
- Validate knight movement
- All tests passing

Resolves MFBC-24
EOF
git commit -F .commit-msg.txt
rm .commit-msg.txt
```

**Benefits:**
- No escaping needed (content is literal)
- Easier to review (human can see exact content being sent)
- Works across all shells
- Easier for agents to generate correctly

**When to use file-based payloads:**
- PR descriptions (multi-line, formatted)
- Complex commit messages with details
- JSON payloads for APIs
- Long descriptions or documentation

---

## MFBC-Oriented Examples

### Example 1: Create Jira Ticket from Analysis

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

### Example 2: Audit Repository for Architecture Violations

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

### Example 3: Update Documentation with Code Examples

**Scenario:** Agent adds code examples to user guide based on actual codebase patterns.

```
Task: Add code examples to docs/naming-conventions.md demonstrating correct patterns.

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
- Target file: docs/naming-conventions.md
- Examples should be based on: GameState.cs, Board.cs, MoveValidator.cs

Steps:
1. Read docs/naming-conventions.md and understand sections
2. Identify sections lacking code examples
3. Review src/MFBC.Core/ for real examples matching each convention
4. Add examples with brief explanations
5. Verify examples are correct (simple syntax check)
6. Commit with message: "docs(naming): add code examples (MFBC-26)"

Output:
- Updated docs/naming-conventions.md with 3-5 new code examples
- Commit message showing additions
- Brief summary of examples added and which conventions they illustrate
```

---

## Prompt Quality Checklist

Use this checklist when crafting prompts for agents. A prompt should meet ALL criteria before execution.

- [ ] **Role is clear and specific**
  - Agent knows its purpose ("implement feature X" not "help with code")
  - Role references project (MFBC) and technology (C#, .NET 8.0)

- [ ] **Constraints are explicit and complete**
  - "MUST follow" list is present and specific
  - "MUST NOT" list prevents common mistakes
  - Tool/command availability is stated (e.g., "dotnet format is available")

- [ ] **Inputs are well-defined**
  - Agent knows what context it has access to
  - References to files/docs use relative paths
  - No assumption that agent knows anything not stated

- [ ] **Steps are sequential and numbered**
  - Each step is atomic (accomplishes one thing)
  - Verification checkpoints are included between major phases
  - No assumed knowledge of MFBC patterns (reference them)

- [ ] **Outputs are concrete and measurable**
  - "Generated code" not "some code"
  - "Tests pass, 0 warnings" not "quality code"
  - Include reporting format (text, JSON, Markdown, etc.)

- [ ] **No vague language**
  - ✗ "write good code", "handle errors", "think about efficiency"
  - ✓ "follow .editorconfig rules", "catch FileNotFoundException", "optimize O(n²) loop to O(n log n)"

- [ ] **Architecture rules are referenced**
  - Key rules from docs/architecture/overview.md are stated or linked
  - Layering rules (MFBC.Core independence) explicitly mentioned if relevant
  - Links to ADRs if design decisions are involved

- [ ] **No assumptions about knowledge**
  - If agent needs to understand MFBC patterns, show examples
  - If special syntax/format is needed, include sample
  - Don't assume "everyone knows C#" if non-obvious syntax is used

- [ ] **Acceptance criteria are verifiable**
  - How will success be measured? (tests pass, no warnings, matches pattern X)
  - What commands will verify completion? (dotnet build, grep for pattern, etc.)
  - Can criteria be objectively evaluated?

- [ ] **Edge cases are mentioned**
  - What should agent do if input is invalid? (e.g., "If FEN is invalid, stop and ask user")
  - What if prerequisites aren't met? ("If MFBC-17 not merged, MFBC-25 cannot start")
  - Handling for ambiguous requirements (stop and ask, or make decision?)

- [ ] **Formatting pitfalls are addressed**
  - If JSON is needed, show sample structure
  - If shell commands are used, specify shell (bash or PowerShell)
  - For large outputs, specify format (file vs stdout, plain text vs JSON)

- [ ] **Anti-hallucination checkpoints are present**
  - "Stop and ask if..." scenarios for ambiguities
  - Validation steps where agent checks own work before proceeding
  - References to canonical sources (docs/, codebase) not hypothetical patterns

- [ ] **Context size is reasonable**
  - Prompt is focused on immediate task, not entire project
  - Large files are summarized or referenced, not fully included
  - Acceptance criteria are detailed enough to implement, succinct enough to read

- [ ] **Failure scenarios are defined**
  - What should agent do if build fails? (stop and report, or keep trying?)
  - What if tests fail? (debug or ask user?)
  - How to handle blockers or dependencies?

---

## Anti-Hallucination Patterns

### Pattern 1: Anchor to Canonical Sources

**Bad (Hallucination Risk):**
```
Follow MFBC naming conventions. Method names should be... [describe from memory]
```

**Good (Grounded):**
```
Follow MFBC naming conventions as defined in docs/naming-conventions.md. 
For methods specifically, see the "Method Names" section which states:
- Public methods: PascalCase (e.g., GetThreats)
- Private methods: camelCase (e.g., validateMove)
```

### Pattern 2: Show Counter-Examples

**Bad (Ambiguous):**
```
Write good, clean code.
```

**Good (Concrete):**
```
Write code following .editorconfig rules. Examples of what NOT to do:
- ✗ Using tabs (use spaces as per .editorconfig)
- ✗ Method names like getThreats (use PascalCase)
- ✗ Unused variables (remove cruft)
- ✗ Missing XML comments on public methods

Examples of what TO do:
- ✓ Method: public List<Threat> GetThreats() { ... }
- ✓ Documented: /// <summary>Returns threats to this piece.</summary>
```

### Pattern 3: Stop and Ask Checkpoints

**Bad (Assumes agent will guess right):**
```
Implement move validation.
```

**Good (Checkpoints prevent bad assumptions):**
```
STOP AND ASK IF:
1. "Are pawns the only piece type I should implement, or all 6 pieces?"
2. "Should en passant (special pawn capture) be included, or just basic moves?"
3. "Is castling (king+rook move) part of this task, or a separate task?"

Once you clarify these questions with the user, proceed with implementation.
```

### Pattern 4: Intermediate Validation

**Bad (Agent claims success without evidence):**
```
Implement tests and make sure they pass.
```

**Good (Validates at each step):**
```
Steps:
1. Write test file: tests/MFBC.Core.Tests/MoveValidatorTests.cs
2. Add 20 test cases (pawn, knight, bishop, rook, queen, king coverage)
3. Run locally: dotnet test MFBC.Core.Tests
4. CHECK: All tests pass (green checkmarks)
5. Run: dotnet build Release (check for warnings)
6. CHECK: Build output shows "0 warnings"
7. Commit with message: "test(core): add move validator tests (MFBC-24)"
```

### Pattern 5: Explicit Verification Before Completion

**Bad (No verification):**
```
Implement feature X.
```

**Good (Explicitly checks work):**
```
After implementing, verify completion:

VERIFY each acceptance criterion:
- [ ] Pawn: forward 1, forward 2 from start, diagonal captures—all working
- [ ] Knight: L-shaped move in any direction—test 5+ positions
- [ ] All 6 pieces: manually verify each against chess rules

VERIFY quality:
- Command: dotnet build --configuration Release → "0 projects with warnings"
- Command: dotnet test --configuration Release → "X tests passed, 0 failed"
- Command: cd src/MFBC.Core && check comments on every public method

If ANY verification fails, STOP and report the issue before marking task complete.
```

---

## Additional Resources

- **[VS Code Documentation: Prompt Files](https://code.visualstudio.com/docs/copilot/)** – Official syntax and features
- **[MFBC Architecture Overview](architecture/overview.md)** – Constraints and patterns agents must follow
- **[MFBC Naming Conventions](naming-conventions.md)** – Code style agents should implement
- **[Jira Conventions & Process Guide](jira-conventions.md)** – Format for ticket creation prompts
- **[CONTRIBUTING.md](../CONTRIBUTING.md)** – Development workflow agents should follow

---

## Summary: The Golden Rules of Prompt Crafting

1. **Ground in concrete examples, not abstract ideals**
   - "Follow .editorconfig" beats "write clean code"

2. **State constraints explicitly**
   - What the agent MUST and MUST NOT do

3. **Break into steps with checkpoints**
   - Agents validate work before proceeding; catch hallucinations early

4. **Reference canonical sources**
   - Link to docs/, code examples, not descriptions from memory

5. **Show counter-examples**
   - What NOT to do is as important as what to do

6. **Stop and ask for ambiguities**
   - Better to block than to proceed on wrong assumptions

7. **Verify at the end**
   - Agent explicitly checks work against acceptance criteria before marking complete

---

**Version History**

| Date | Version | Change |
|------|---------|--------|
| 2026-02-06 | 1.0 | Initial AI prompt crafting guidelines for MFBC agents |

---

**Questions or improvements?** Update this guide with a new PR or raise a discussion in Slack.
