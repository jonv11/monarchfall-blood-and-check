# Definition of Done

## Purpose
Define when a Jira Task or Feature can be marked complete.

## Scope
Applies to Tasks and Features before transitioning to Done.

A Task/Feature is **Done** when:

- ✓ **All Acceptance Criteria are met** – demonstrated and tested
- ✓ **Code changes are implemented** (if applicable)
  - Follows MFBC style guide and naming conventions
  - No build warnings (treat warnings as errors per `Directory.Build.props`)
  - All public APIs have XML documentation
- ✓ **Tests are added** (if logic added)
  - Unit tests for MFBC.Core logic
  - Integration tests for workflows
  - All tests pass locally and in CI
- ✓ **Code review is complete** – PR approved by at least one team member
- ✓ **Branch is merged to main** – code is in production-ready state
- ✓ **Documentation is updated** (if applicable)
  - README, guides, or inline code comments
  - CHANGELOG.md updated with user-facing changes
- ✓ **Jira status transitioned to Done** – when PR is merged

**Anti-Pattern:** Marking as Done when PR is opened; wait until merged.
