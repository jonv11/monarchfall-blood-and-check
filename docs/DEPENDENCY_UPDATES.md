# Handling Dependency Updates

This guide explains how to manage and review dependency update pull requests in Monarchfall: Blood & Check.

## Overview

Dependency updates are handled automatically by **GitHub Dependabot**, which:
- Checks for NuGet package updates monthly
- Creates pull requests for security fixes and new versions
- Runs CI on each update PR to ensure compatibility
- Allows manual review and merging by maintainers

## Review Process

When Dependabot creates a pull request:

1. **Check the PR title and description**
   - Security updates are marked as such
   - Version bumps are listed clearly
   - Breaking changes (if any) are noted in release notes

2. **Review the changes**
   - `.csproj` files show the version changes
   - CI must pass (build, tests, coverage)
   - Check the GitHub issue/release notes for the package for breaking changes

3. **Approve and Merge**
   - If CI passes and no breaking changes apply: approve and merge
   - If there are breaking changes: coordinate with maintainers before merging
   - For security patches: prioritize merging quickly

## Workflow

### For Security Updates

Security updates should be reviewed and merged quickly:

```bash
# Verify the PR title indicates a security update
# Check the release notes or GitHub security advisory
# Review the PR for any additional breaking changes
# Merge if CI passes and no conflicts exist
```

### For Minor/Patch Updates

Minor version updates are lower risk:

```bash
# Ensure all tests still pass
# Run dotnet build locally to verify
# Merge if green
```

### For Major Version Updates

Major version updates may introduce breaking changes:

```bash
# Read the package's release notes carefully
# Check the PR for integration issues
# Test locally if concerned about compatibility
# Discuss with team if significant changes are needed
```

## Dependabot Configuration

Dependabot is configured with:
- **Update frequency:** Monthly (1st day of month)
- **Target:** NuGet packages (`.csproj` files)
- **Automerge:** Disabled (manual review required)

To adjust settings, edit `.github/dependabot.yml` (if created) or manage Dependabot via GitHub repo settings.

## Vulnerability Alerts

GitHub automatically alerts maintainers of known vulnerabilities in dependencies:

1. Check **Security** tab → **Dependabot alerts** on GitHub
2. Review the advisory details
3. Merge the corresponding Dependabot PR to remediate
4. Verify the vulnerability is resolved

## Best Practices

- **Review regularly** — Check for Dependabot PRs weekly
- **Test updates** — Ensure `dotnet build` and `dotnet test` pass
- **Document breaking changes** — Update CHANGELOG if significant
- **Keep main healthy** — Maintain a policy of not blocking main with stale PRs
- **Communicate** — Note major upgrades in team channels or comments

## Rollback

If a dependency update introduces issues:

1. Revert the PR
2. Open an issue describing the problem
3. Coordinate with maintainers on a fix or downgrade strategy
4. Update the PR or create a new one with a workaround

## Additional Scanning

For now, we rely on Dependabot and GitHub's built-in security scanning. Future consideration:
- Snyk integration for deeper vulnerability analysis
- License scanning for compliance
