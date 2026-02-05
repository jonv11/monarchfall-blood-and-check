## Summary
<!-- Provide a clear and concise summary of your changes. What does this PR accomplish? -->

## Type of Change
- [ ] Feature (new functionality)
- [ ] Bug fix
- [ ] Documentation update
- [ ] Refactoring or code improvement
- [ ] Chore (tooling, dependencies, etc.)

## Related Jira Issue
<!-- Link to the Jira ticket: https://jonv11.atlassian.net/browse/MFBC-### -->

Resolves: MFBC-

## Changes
<!-- Provide a detailed description of what changed and why. -->

- Point 1
- Point 2
- Point 3

## Testing
<!-- Describe how you tested these changes. Include commands run and results. -->

```bash
# Build
dotnet build

# Run tests
dotnet test

# Any other validation steps
```

## Checklist
- [ ] Code follows the [Naming Conventions & Style Guide](../../docs/naming-conventions.md)
- [ ] Changes comply with the [Architecture Rules](../../docs/architecture/overview.md) (MFBC.Core has no CLI dependencies)
- [ ] All new logic in `MFBC.Core` has corresponding unit tests in `MFBC.Core.Tests`
- [ ] Tests pass locally: `dotnet test`
- [ ] Build passes with no warnings: `dotnet build`
- [ ] Code is formatted: `dotnet format` (if available)
- [ ] XML documentation added for public APIs
- [ ] Comments explain complex logic or non-obvious decisions
- [ ] CONTRIBUTING guidelines followed
- [ ] No merge conflicts with `main`

## Breaking Changes
<!-- Are there any breaking changes to the public API or architecture? -->

- [ ] No breaking changes
- [ ] Yes, breaking changes:
  - Change 1
  - Change 2

## Additional Notes
<!-- Any additional context for reviewers? Potential concerns? Areas needing extra attention? -->

## Screenshots or Output (if applicable)
<!-- For UI changes, bug fixes with visual impact, or CLI changes, include screenshots or console output. -->

---

**Before you submit:** Ensure your branch is up-to-date with `main` and all checks pass.
