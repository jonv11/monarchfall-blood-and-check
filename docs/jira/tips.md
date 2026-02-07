# Tips for Contributors

## Purpose
Provide practical guidance for reporters, implementers, and reviewers.

## Scope
Applies to all contributors working with MFBC Jira issues.

## For Reporters (Creating Issues)

1. **Use the Right Type**  
   Ask: "Can this be shipped independently?" → Feature  
   Ask: "Can one person complete this in 1-2 days?" → Task  
   Ask: "Does this take multiple weeks?" → Epic

2. **Write for Future You (and AI agents)**  
   Assume the person reading this has context only from the issue
   – link everything relevant

3. **Be Specific with AC**  
   Instead of: "Add comments to code"  
   Use: "Add XML documentation comments to public methods in BoardValidator class (5 methods currently missing)"

4. **Test Your Own Acceptance Criteria**  
   When you finish writing AC, ask: "Could someone else independently verify each point?" If no, rewrite.

## For Implementers (Working on Issues)

1. **Clarify Before Starting**  
   If anything is unclear, don't guess—ask in the issue or Slack
   Status Definition of Ready requires clarity

2. **Transition Status When Appropriate**  
   - Move to **In Progress** immediately when you start
   - Move to **In Review** when PR is opened (not just drafted)
   - Don't move to **Done** until merged

3. **Reference the Issue in Your PR and Commits**  
   Include "Resolves MFBC-XX" in PR body and commit messages
   This links history and helps others understand context

4. **Use Checklists for Complex Tasks**  
   Before marking In Review, print Acceptance Criteria and verify each one is met

## For Reviewers

1. **Review Against AC**  
   Check PR against each Acceptance Criterion
   If AC unclear, ask reporter/implementer to clarify before merging

2. **Check for Quality Standards**  
   - Code follows naming conventions
   - No build warnings
   - Tests included and passing
   - Documentation updated if needed

3. **Approve or Request Changes Clearly**  
   Approval = ready to merge  
   Request Changes = rework needed (give specific guidance)
