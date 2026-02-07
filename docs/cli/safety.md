# Safety Guidance

## Purpose
Define safety practices for destructive CLI operations.

## Scope
Applies to acli, git, gh, and dotnet operations in MFBC.

## Read-Only First Approach

Always verify commands with read-only operations before making changes:

| Read-Only Command | Destructive Command |
|-------------------|-------------------|
| `acli jira workitem view MFBC-27` | `acli jira workitem edit --key MFBC-27 ...` |
| `git status` | `git add .` |
| `git diff` | `git commit -m "..."` |
| `gh pr list` | `gh pr merge 42` |
| `git log --oneline -n 5` | `git reset --hard HEAD~1` |

**Best practice:** Run the read-only version, verify output, then run the destructive version.

---

## Destructive Operations - Handle with Care

⚠️ **These commands delete work or history. Use only if certain.**

| Command | What It Does | How to Avoid Mistakes |
|---------|--------------|----------------------|
| `git push --force` | Overwrites remote branch | **Never use on `main` or shared branches.** Confirm with team first. |
| `git reset --hard` | Permanently discards uncommitted changes | Run `git diff` first; stash with `git stash` instead if unsure. |
| `git branch -D` | Force-delete local branch | Use lowercase `-d` for safe delete; uppercase `-D` skips safety checks. |
| `git rebase -i` | Rewrite commit history | Use only on **personal feature branches**, not shared/main. |
| `acli jira workitem delete` | Delete work item from Jira | **Rarely used.** Archive or mark "Won't Do" instead; consult team. |
| `gh pr merge --delete-branch` | Delete PR branch after merge | Safe; deletes remote branch to keep repo clean. |

---

## Confirmation Checklist Before Destructive Ops

Before running a destructive command, confirm:

- [ ] Is this command safe for this branch/file? (Check with `git branch`, `git status`)
- [ ] Have I backed up important work? (Commit or stash with `git stash`)
- [ ] Am I on the correct branch? (Run `git branch` to verify)
- [ ] Have I tested the opposite operation? (e.g., `git diff` before `git commit`)
- [ ] Have I consulted the team if it affects shared branches?
- [ ] Can I undo this? (Know how to undo: `git reflog`, `git reset`, rebasing, etc.)

---

## Version Information

As of this cheatsheet:

```
acli        2.16.0+ (https://atlassian-cli.com/)
git         2.40+ (https://git-scm.com/downloads)
gh          2.48+ (https://cli.github.com/)
.NET        8.0 (https://dotnet.microsoft.com/)
```

Check installed versions:

```bash
acli --version
git --version
gh --version
dotnet --version
```

If experiencing issues, upgrade to latest stable versions.
