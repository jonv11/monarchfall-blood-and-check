# Summary: The Golden Rules of Prompt Crafting

## Purpose
Provide a concise, high-signal summary of prompt authoring rules.

## Scope
Use as a quick refresher when writing or reviewing prompts.

## Golden Rules

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
