# Formatting and Encoding Pitfalls

## Purpose
Call out common formatting and encoding issues that cause prompt failures.

## Scope
Applies to prompts involving Jira, CLI commands, or structured payloads.

## Pitfall 1: Jira's ADF JSON Format

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

## Pitfall 2: Shell Escaping

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

## Pitfall 3: File-Based Payloads

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
