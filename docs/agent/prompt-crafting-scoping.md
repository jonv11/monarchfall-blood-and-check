# Scoping Rules

## Purpose
Define when to inline code, reference files, summarize, or truncate content in prompts.

## Scope
Use for prompts that must balance detail and context size.

## When to Include Code Inline

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

## When to Reference Files

**Reference files when:**
- File is large (> 50 lines) and agent needs to understand structure
- Agent needs to follow existing patterns from that file

```
See src/MFBC.Core/Board.cs for the Board class structure; follow the same pattern 
for ValidateMove() implementation (public method, summary comment, parameter validation).
```

## When to Summarize

**Summarize when:**
- File is very large (thousands of lines) and only a section is relevant
- Goal is to give agent context without overwhelming token count

```
MFBC.Core.Tests/BoardTests.cs contains 200+ test cases. For your feature, focus on 
`MoveValidationTests` section (lines 50-120) which shows the test pattern: 
Arrange board state, Act (call MoveValidator), Assert (verify result).
```

## When to Truncate

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
