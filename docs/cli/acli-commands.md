# acli Commands (Jira)

## Purpose
Provide a quick reference for Atlassian CLI commands used with Jira.

## Scope
Applies to contributors interacting with MFBC Jira via `acli`.

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
