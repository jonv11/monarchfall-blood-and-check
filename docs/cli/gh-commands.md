# gh Commands (GitHub)

## Purpose
Provide a quick reference for GitHub CLI commands used in MFBC contributions.

## Scope
Applies to pull request and issue workflows via `gh`.

**gh** (GitHub CLI) manages pull requests, issues, and repository operations. Requires: `gh auth login` for authentication.

| Command | Description | Example | Notes |
|---------|-------------|---------|-------|
| `gh pr create` | Create a pull request | `gh pr create --title "Title (MFBC-27)" --body-file .pr-body.md --base main` | Use `--body-file` to avoid shell escaping; PR body should include ticket link |
| `gh pr list` | List pull requests | `gh pr list --state open` (or `closed`, `merged`) | Filter by state; use `--assignee @me` to see your PRs |
| `gh pr view` | View PR details | `gh pr view 42` or `gh pr view --web` | Use number or URL; `--web` opens in browser |
| `gh pr review` | Review a pull request | `gh pr review 42 --approve` or `gh pr review 42 --request-changes -b "Request message"` | `--approve`, `--request-changes`, or `--comment` |
| `gh pr comment` | Add comment to PR | `gh pr comment 42 -b "Looks good!"` | Useful for CI/CD feedback or reviewer notes |
| `gh pr checkout` | Check out a PR locally | `gh pr checkout 42` | Downloads PR branch locally for testing |
| `gh pr merge` | Merge a pull request | `gh pr merge 42 --squash --auto` | `--squash` combines commits; `--auto` waits for checks to pass |
| `gh issue create` | Create a new issue | `gh issue create --title "Bug title" --body "Details" --project "Project Name"` | Similar to creating ticket via Jira; include descriptive body |
| `gh issue list` | List issues | `gh issue list --assignee @me` or `gh issue list --label bug` | Filter by state, label, or assignee |
| `gh issue view` | View issue details | `gh issue view 123` | Shows description, comments, linked PRs |
| `gh issue comment` | Comment on issue | `gh issue comment 123 -b "Comment text"` | Engage with discussions or provide updates |
| `gh repo clone` | Clone a repository | `gh repo clone monarchfall/blood-and-check` | Shorter syntax; `cd` and authenticate automatically |
| `gh repo view` | View repository info | `gh repo view --web` | Shows metadata; `--web` opens in browser |
| `gh release create` | Create a GitHub release | `gh release create v1.0.0 --title "Release 1.0.0" --notes "Notes"` | Tags commit as release; useful for versioning |
| `gh auth login` | Authenticate with GitHub | `gh auth login` | Required first step; choose HTTPS or SSH |
| `gh --version` | Check gh version | `gh --version` | Useful for troubleshooting version-specific issues |
