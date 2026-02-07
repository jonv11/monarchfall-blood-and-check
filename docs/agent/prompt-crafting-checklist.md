# Prompt Quality Checklist

## Purpose
Provide a checklist to validate prompt completeness and clarity before execution.

## Scope
Use for any prompt that directs an agent to perform MFBC work.

## Checklist

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
