# Contributor CLI Cheatsheet

Quick-reference guide for **acli** (Jira), **git**, and **gh** (GitHub CLI) commands used in the Monarchfall: Blood & Check contribution workflow.

## Table of Contents

1. [acli Commands (Jira)](#acli-commands-jira)
2. [git Commands](#git-commands)
3. [gh Commands (GitHub)](#gh-commands-github)
4. [End-to-End Workflows](#end-to-end-workflows)
5. [Troubleshooting](#troubleshooting)
6. [Safety Guidance](#safety-guidance)

---

## acli Commands (Jira)

**acli** (Atlassian CLI) manages Jira work items. Requires authentication: `acli configure` or `acli login`.

| Command | Description | Example | Notes |
|---------|-------------|---------|-------|
| `acli jira workitem search` | Search for work items by JQL | `acli jira workitem search 'project = MFBC AND status = "To Do"'` | Use quotes for complex queries; see [JQL docs](https://support.atlassian.com/jira-software-cloud/docs/what-is-advanced-searching-in-jira/) |
| `acli jira workitem view` | View work item details | `acli jira workitem view MFBC-27` | Shows summary, description, acceptance criteria, status |
| `acli jira workitem create` | Create a new work item | `acli jira workitem create --project MFBC --type Task --summary "Task title" --description "Details"` | Requires project, type, and summary; optional fields support more detail |
| `acli jira workitem edit` | Edit a work item | `acli jira workitem edit --key MFBC-27 --summary "Updated title"` | Updates specified field; use `--field` for custom fields |
| `acli jira workitem transition` | Change work item status | `acli jira workitem transition --key MFBC-27 --status "In Progress" --yes` | Use `--yes` to skip confirmation; statuses: "To Do", "In Progress", "In Review", "Done" |
| `acli jira workitem comment` | Add comment to work item | `acli jira workitem comment --key MFBC-27 --comment "Work started"` | Comments are visible to all contributors in Jira |
| `acli jira workitem assign` | Assign work item to user | `acli jira workitem assign --key MFBC-27 --assignee jonv11@gmail.com` | Use email or username; unassign with `--assignee ""` |
| `acli jira workitem link` | Link two work items | `acli jira workitem link --key MFBC-27 --link-key MFBC-28 --link-type "relates to"` | Common types: "relates to", "is blocked by", "blocks" |
| `acli jira workitem priority` | Set work item priority | `acli jira workitem priority --key MFBC-27 --priority High` | Priorities: Lowest, Low, Medium, High, Highest |
| `acli jira workitem label add` | Add label to work item | `acli jira workitem label add --key MFBC-27 --label "documentation"` | Multiple labels: repeat `--label` for each |
| `acli jira workitem label remove` | Remove label from work item | `acli jira workitem label remove --key MFBC-27 --label "wip"` | Removes specified label |
| `acli jira workitem sprint` | Add to sprint | `acli jira workitem sprint --key MFBC-27 --sprint "Sprint 5"` | Requires active sprint; check sprint name in Jira |
| `acli --version` | Check acli version | `acli --version` | Useful for troubleshooting version-specific issues |
| `acli jira workitem watch` | Watch a work item | `acli jira workitem watch --key MFBC-27` | You'll receive notifications for updates |

---

## git Commands

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

---

## gh Commands (GitHub)

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

---

## End-to-End Workflows

### Workflow 1: Claim a Jira Task and Start Work

**Goal:** Pick up a task from Jira and begin implementation.

1. **Search for available tasks in Jira:**
   ```bash
   acli jira workitem search 'project = MFBC AND status = "To Do" AND type = Task'
   ```
   Find a task that interests you.

2. **View task details:**
   ```bash
   acli jira workitem view MFBC-27
   ```
   Review acceptance criteria, description, and dependencies. If requirements are unclear, ask in Jira comments.

3. **Assign task to yourself:**
   ```bash
   acli jira workitem assign --key MFBC-27 --assignee your-email@example.com
   ```

4. **Transition task to "In Progress":**
   ```bash
   acli jira workitem transition --key MFBC-27 --status "In Progress" --yes
   ```

5. **Check out main and pull latest:**
   ```bash
   git checkout main && git pull
   ```

6. **Create feature branch using ticket name:**
   ```bash
   git checkout -b feature/MFBC-27-contributor-cli-cheatsheet
   ```
   **Branch naming:** Use kebab-case with ticket number and summary.

7. **Start development and commit:**
   ```bash
   git add docs/cli-cheatsheet.md
   git commit -m "docs: add CLI cheatsheet (MFBC-27)"
   ```
   **Commit message format:** `<type>(<scope>): <summary> (MFBC-###)`

8. **Verify work locally:**
   ```bash
   dotnet build --configuration Release
   dotnet test --configuration Release
   dotnet format
   git status
   ```

---

### Workflow 2: Push Code and Create a Pull Request

**Goal:** Share your changes via GitHub and request review.

1. **Ensure branch is up-to-date with main:**
   ```bash
   git fetch origin
   git rebase origin/main
   ```
   Resolve any conflicts if they appear.

2. **Push your branch to remote:**
   ```bash
   git push -u origin feature/MFBC-27-contributor-cli-cheatsheet
   ```
   Use `-u` on first push; subsequent pushes: `git push`.

3. **Prepare PR body and create PR:**
   ```bash
   # Create PR body file
   cat > .pr-body-27.md << 'EOF'
   ## Summary
   Add contributor CLI cheatsheet for acli, git, and gh commands.

   ## What was implemented
   - CLI command tables with examples and notes
   - 3+ end-to-end workflows
   - Troubleshooting section with 5+ common issues
   - Safety guidance for destructive operations

   ## Acceptance Criteria
   - ✅ Lists 10-15 most impactful commands for acli
   - ✅ Lists 10-15 most impactful commands for git
   - ✅ Lists 10-15 most impactful commands for gh
   - ✅ Provides 3+ end-to-end workflows
   - ✅ Troubleshooting section covers 5+ issues
   - ✅ Safety guidance included
   - ✅ All examples are copy-paste ready

   ## Testing/Validation
   - Markdown renders correctly
   - All example commands are valid
   - Links to external docs verified

   Resolves MFBC-27
   EOF

   # Create PR
   gh pr create --title "docs: add CLI cheatsheet (MFBC-27)" --body-file .pr-body-27.md --base main

   # Cleanup
   rm .pr-body-27.md
   ```

4. **Share PR link with reviewers:**
   ```bash
   gh pr view --web
   ```
   Opens created PR in browser; share the link in Slack or comment in Jira.

---

### Workflow 3: Review, Approve, and Merge a Pull Request

**Goal:** Review team member's code, provide feedback, and merge when ready.

1. **List open PRs assigned to you or needing review:**
   ```bash
   gh pr list --state open --reviewRequestedFromMe
   ```

2. **Check out PR locally to test:**
   ```bash
   gh pr checkout 42
   ```
   Switches to PR's branch; you can build, test, and review code locally.

3. **View PR details:**
   ```bash
   gh pr view 42
   ```
   See description, acceptance criteria, test results.

4. **Request changes or approve:**

   **Request changes:**
   ```bash
   gh pr review 42 --request-changes -b "Please update the formatting in line X"
   ```

   **Approve:**
   ```bash
   gh pr review 42 --approve
   ```

5. **Merge PR when approved and tests pass:**
   ```bash
   gh pr merge 42 --squash --auto
   ```
   - `--squash` combines all commits into one
   - `--auto` waits for status checks and automatically merges

6. **Transition Jira ticket to "Done":**
   ```bash
   acli jira workitem transition --key MFBC-27 --status "Done" --yes
   ```

---

## Troubleshooting

### Issue 1: "Authentication Failed" or "Permission Denied"

**Symptoms:** `acli`, `git`, or `gh` commands fail with auth errors.

**Solution:**

- **For acli:**
  ```bash
  acli configure
  acli login
  ```
  Follow prompts to authenticate; provide Jira API token (not password).

- **For git:**
  ```bash
  git config --list | grep user
  ```
  Ensure `user.name` and `user.email` are configured. If using HTTPS remotes, you may need a GitHub personal access token:
  ```bash
  git config --global credential.helper wincred  # Windows
  git config --global credential.helper osxkeychain  # macOS
  git config --global credential.helper cache  # Linux
  ```

- **For gh:**
  ```bash
  gh auth login
  ```
  Re-authenticate; choose HTTPS or SSH based on your preference.

**Prevention:** Run `acli configure`, `git config`, and `gh auth login` once during initial setup.

---

### Issue 2: "Merge Conflict" When Pulling or Rebasing

**Symptoms:** `git pull` or `git rebase main` fails; git shows conflicting files.

**Solution:**

1. **Check which files have conflicts:**
   ```bash
   git status
   ```

2. **Open conflicting files in your editor:**
   - Look for `<<<<<<<`, `=======`, `>>>>>>>` markers
   - Edit to keep correct version (delete markers and unwanted code)

3. **Stage resolved files:**
   ```bash
   git add src/resolved-file.cs
   ```

4. **Complete rebase or merge:**
   ```bash
   git rebase --continue  # if rebasing
   git commit -m "Merge main"  # if merging
   ```

5. **If something goes wrong, abort:**
   ```bash
   git rebase --abort
   git merge --abort
   ```

**Prevention:** Pull/rebase frequently to avoid large conflicts; use `git diff` before committing to detect changes.

---

### Issue 3: "Rejected (non-fast-forward)" When Pushing

**Symptoms:** `git push` fails; message says "non-fast-forward" or "rejected".

**Solution:**

1. **Pull remote changes first:**
   ```bash
   git pull --rebase
   ```
   Reapplies your commits on top of remote changes.

2. **Push again:**
   ```bash
   git push
   ```

**Prevention:** Always `git pull` before `git push` to sync local and remote.

**⚠️ Warning:** Avoid `git push --force`; it overwrites remote history and breaks team collaboration. Use only after `git rebase` if you've agreed with your team.

---

### Issue 4: "Detached HEAD" or "You are not on a branch"

**Symptoms:** `git status` shows "detached HEAD"; commands behave unexpectedly.

**Solution:**

```bash
# See current state
git status

# Switch back to a branch
git checkout feature/MFBC-27-task-name
```

**Prevention:** Always create and work on named branches; avoid checking out commit hashes directly.

---

### Issue 5: "Branch Tracking Not Set" or "No Upstream"

**Symptoms:** `git push` asks to set upstream; `git pull` fails silently.

**Solution:**

```bash
git push -u origin feature/MFBC-27-task-name  # set upstream on first push
git branch -u origin/feature/MFBC-27-task-name  # or set after the fact
```

**Prevention:** Always use `git push -u` on first push to new branches.

---

### Issue 6: ".NET Build Errors or Warnings"

**Symptoms:** `dotnet build` fails or produces warnings.

**Solution:**

1. **Check for missing dependencies:**
   ```bash
   dotnet restore
   ```

2. **Build with verbose output:**
   ```bash
   dotnet build --verbosity detailed
   ```

3. **Format code to eliminate style warnings:**
   ```bash
   dotnet format
   ```

4. **Check for project reference issues:**
   - Ensure `MFBC.Core` and test layers reference correct projects
   - Avoid circular dependencies (e.g., `MFBC.Cli` should not reference `MFBC.Core`)

**Prevention:** Run `dotnet build` and `dotnet test` before committing; address all warnings immediately.

---

## Safety Guidance

### Read-Only First Approach

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

### Destructive Operations - Handle with Care

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

### Confirmation Checklist Before Destructive Ops

Before running a destructive command, confirm:

- [ ] Is this command safe for this branch/file? (Check with `git branch`, `git status`)
- [ ] Have I backed up important work? (Commit or stash with `git stash`)
- [ ] Am I on the correct branch? (Run `git branch` to verify)
- [ ] Have I tested the opposite operation? (e.g., `git diff` before `git commit`)
- [ ] Have I consulted the team if it affects shared branches?
- [ ] Can I undo this? (Know how to undo: `git reflog`, `git reset`, rebasing, etc.)

---

### Version Information

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

---

## Additional Resources

- **Jira Documentation:** [Jira Cloud Docs](https://support.atlassian.com/jira-cloud/)
- **git Documentation:** [git Book](https://git-scm.com/book/en/v2)
- **GitHub CLI:** [gh Documentation](https://cli.github.com/manual/)
- **MFBC Contributing Guide:** [CONTRIBUTING.md](../CONTRIBUTING.md)
- **MFBC Naming Conventions:** [naming-conventions.md](../naming-conventions.md)
