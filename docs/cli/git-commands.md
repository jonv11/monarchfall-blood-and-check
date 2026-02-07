# git Commands

## Purpose
Provide a quick reference for git commands used in MFBC contributions.

## Scope
Applies to local and remote git operations for MFBC.

**git** manages local and remote code repositories. Requires: `git config --global user.name "Name"` and `git config --global user.email "email@example.com"`.

| Command | Description | Example | Notes |
|---------|-------------|---------|-------|
| `git clone` | Clone a remote repository | `git clone https://github.com/monarchfall/blood-and-check.git` | Creates local copy; use HTTPS or SSH |
| `git checkout` | Switch branch or create new branch | `git checkout -b feature/MFBC-27-task-name` | Use `-b` to create new branch; branch names follow: `feature/MFBC-###-kebab-case` |
| `git branch` | List local branches | `git branch` | Current branch marked with `*`; use `-a` to see remote branches |
| `git status` | Check working directory status | `git status` | Shows modified, staged, and untracked files |
| `git add` | Stage files for commit | `git add src/file.cs` or `git add .` | Use `.` to stage all changes; selective staging keeps commits focused |
| `git commit` | Create a commit with staged changes | `git commit -m "feat(core): description (MFBC-27)"` | Follow: `<type>(<scope>): <summary> (MFBC-###)`; includes ticket number |
| `git push` | Push branch to remote | `git push -u origin feature/MFBC-27-task-name` | Use `-u` for first push to set upstream; subsequent: `git push` |
| `git pull` | Fetch and merge remote changes | `git pull` | Updates current branch from remote; use before pushing to avoid conflicts |
| `git merge` | Merge one branch into current | `git merge feature/MFBC-25` | Typically done via PR (merge button in GitHub), not locally |
| `git diff` | Show changes in files | `git diff` or `git diff --staged` | ` ` shows unstaged; `--staged` shows staged changes |
| `git log` | View commit history | `git log --oneline -n 10` | `--oneline` condenses output; `-n` limits number of commits shown |
| `git rebase main` | Rebase current branch on main | `git rebase main` | Updates your branch with latest main; **avoid on shared branches** |
| `git reset` | Unstage files or reset commits | `git reset src/file.cs` (unstage) or `git reset --soft HEAD~1` (undo last commit) | `--soft` keeps changes staged; `--hard` discards changes (**destructive**) |
| `git stash` | Save changes temporarily | `git stash` (save) or `git stash pop` (restore) | Useful to clean working directory without committing |
