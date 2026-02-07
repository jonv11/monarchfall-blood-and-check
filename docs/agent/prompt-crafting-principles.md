# Prompt Crafting Principles

## Purpose
Define the core principles for writing reliable, grounded prompts for AI coding agents.

## Scope
Use for any prompt that directs an agent to read, analyze, or modify MFBC code or documentation.

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
