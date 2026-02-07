# Namespace Conventions

## Purpose
Define namespace structure and naming patterns for MFBC projects.

## Scope
Applies to all projects and folders in the repository.

## Structure

Namespaces follow the folder structure and use PascalCase:

```
MFBC.Core
MFBC.Core.Board
MFBC.Core.Rules
MFBC.Core.Pieces
MFBC.Cli
MFBC.Cli.Commands
```

## Naming Pattern

```
<Project>.<Module>[.<SubModule>]
```

**Examples:**
```csharp
namespace MFBC.Core;
namespace MFBC.Core.Movement;
namespace MFBC.Core.Pieces;
namespace MFBC.Cli.Commands;
```

## Guidelines

- Keep nesting shallow (max 3-4 levels)
- Use singular nouns unless the namespace represents a collection concept
- Avoid generic names like `Utilities` or `Helpers`
