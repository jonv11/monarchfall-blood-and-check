# Prompt: Repository Documentation Audit

Goal  
Audit the current repository documentation structure and quality and provide actionable feedback to improve clarity, consistency, structure, and usability.

This is a **read-only audit**.

The agent must:
- NOT create files
- NOT modify files
- NOT propose patches or diffs
- ONLY produce a clear text feedback message for the user

Output must be:
- Plain text
- No markdown formatting
- No tables
- No code blocks
- Easy to copy/paste

--------------------------------------------------

Context (Mandatory Reading)

Before starting the audit, read the following files:

1. /docs/core/docs-structure-guide.md  
2. /docs/core/text-file-agent.md  

These documents define:
- Expected documentation structure
- Size and modularity guidelines
- Naming conventions
- Agent-readability best practices
- Content design principles

Use them as the evaluation baseline.

--------------------------------------------------

Scope of the Audit

Analyze the repository documentation including:

- /docs directory structure
- docs/README.md (if present)
- File organization and domain separation
- File naming conventions
- File sizes and modularity
- Content clarity and structure
- Duplication or overlap
- Missing essential documentation
- Deep or unnecessary folder nesting
- Cross-reference dependencies
- Agent-readability issues
- Human onboarding usability

If useful, inspect:
- README.md at repository root
- CONTRIBUTING.md
- Any documentation outside /docs

--------------------------------------------------

Evaluation Criteria

Structure
- Clear domain grouping
- Flat hierarchy (avoid deep nesting)
- One topic per file
- Logical separation (core / process / reference / examples)

Size and Modularity
- Avoid large monolithic files
- Critical rule files should be small
- Split files that mix unrelated concerns

Naming
- lowercase
- hyphen-separated
- descriptive and consistent

Content Quality
- Clear purpose and scope
- Structured sections
- Critical rules near the top
- Avoid long narrative text
- Avoid hidden constraints

Agent Optimization
- Minimal cross-file dependency chains
- No reliance on implicit context
- Explicit, rule-oriented writing

Usability
- Easy navigation for new contributors
- Clear documentation entry point
- No duplicated or conflicting information

--------------------------------------------------

Output Format (Strict)

Return a plain text message structured as:

1. Overall assessment (2–3 sentences)

2. Strengths
Short bullet-style lines (using "- " prefix)

3. Issues / Risks
Short bullet-style lines

4. High-priority improvements (most impactful changes)

5. Nice-to-have improvements

Do NOT use markdown headers or formatting.
Do NOT include file diffs.
Do NOT modify the repository.

If documentation quality is already good, still provide improvement suggestions.
