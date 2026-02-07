# Troubleshooting

## Purpose
Provide fixes for common CLI issues encountered by contributors.

## Scope
Applies to acli, git, gh, and dotnet workflows.

## Issue 1: "Authentication Failed" or "Permission Denied"

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

## Issue 2: "Merge Conflict" When Pulling or Rebasing

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

## Issue 3: "Rejected (non-fast-forward)" When Pushing

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

## Issue 4: "Detached HEAD" or "You are not on a branch"

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

## Issue 5: "Branch Tracking Not Set" or "No Upstream"

**Symptoms:** `git push` asks to set upstream; `git pull` fails silently.

**Solution:**

```bash
git push -u origin feature/MFBC-27-task-name  # set upstream on first push
git branch -u origin/feature/MFBC-27-task-name  # or set after the fact
```

**Prevention:** Always use `git push -u` on first push to new branches.

---

## Issue 6: ".NET Build Errors or Warnings"

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

## Issue 7: "No active session" or "Active session data is invalid"

**Symptoms:** `mfbc show` or `mfbc play` returns:
- `No active session. Run 'new' first.`
- `Active session data is invalid. Run 'new' to reset.`

**Solution:**

1. **Create or reset active session:**
   ```bash
   dotnet run --project src/MFBC.Cli -- new --no-interactive
   ```

2. **Verify session continuity across separate invocations:**
   ```bash
   dotnet run --project src/MFBC.Cli -- show
   dotnet run --project src/MFBC.Cli -- play e1e2
   ```

3. **If corruption persists, remove local session state:**
   ```bash
   # Windows PowerShell
   Remove-Item -Recurse -Force .mfbc
   ```
   Then run `new` again.

**Prevention:** Avoid manually editing `.mfbc/run-state.json` and ensure CLI commands can write to the workspace directory.
