# End-to-End Workflows

## Purpose
Provide step-by-step workflows that combine acli, git, and gh commands.

## Scope
Applies to common contributor workflows for MFBC.

## Workflow 1: Claim a Jira Task and Start Work

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
   git add docs/cli/cli-cheatsheet.md
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

9. **Validate CLI session persistence (when changing `new/show/play`):**
   ```bash
   dotnet run --project src/MFBC.Cli -- new --no-interactive
   dotnet run --project src/MFBC.Cli -- show
   dotnet run --project src/MFBC.Cli -- play e1e2
   ```
   Run each command as a separate process to verify disk-backed session continuity.

---

## Workflow 2: Push Code and Create a Pull Request

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

## Workflow 3: Review, Approve, and Merge a Pull Request

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
