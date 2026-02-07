# Local Development Setup Guide

This guide provides step-by-step instructions to set up Monarchfall: Blood & Check for local development on Windows, macOS, and Linux.

## Requirements

- **.NET 8 SDK** (LTS) — [Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **Git** — [Download](https://git-scm.com/)
- **Visual Studio Code** (recommended) — [Download](https://code.visualstudio.com/)

Verify your .NET version:

```bash
dotnet --version
# Expected: 8.0.x or later
```

## Platform-Specific Setup

### Windows

1. **Install .NET 8 SDK**
   - Download from [Microsoft .NET Downloads](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
   - Run the installer and follow the prompts
   - Restart your terminal or PowerShell after installation

2. **Clone the Repository**
   ```powershell
   git clone https://github.com/jonv11/monarchfall-blood-and-check.git
   cd monarchfall-blood-and-check
   ```

3. **Verify Installation**
   ```powershell
   dotnet --version
   dotnet --list-sdks
   ```

### macOS

1. **Install .NET 8 SDK**
   - Using Homebrew (recommended):
     ```bash
     brew install dotnet@8
     ```
   - Or download from [Microsoft .NET Downloads](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

2. **Clone the Repository**
   ```bash
   git clone https://github.com/jonv11/monarchfall-blood-and-check.git
   cd monarchfall-blood-and-check
   ```

3. **Verify Installation**
   ```bash
   dotnet --version
   ```

### Linux

1. **Install .NET 8 SDK**
   - Follow the [Microsoft instructions](https://learn.microsoft.com/en-us/dotnet/core/install/linux) for your distribution
   - Example for Ubuntu/Debian:
     ```bash
     sudo apt-get update
     sudo apt-get install -y dotnet-sdk-8.0
     ```

2. **Clone the Repository**
   ```bash
   git clone https://github.com/jonv11/monarchfall-blood-and-check.git
   cd monarchfall-blood-and-check
   ```

3. **Verify Installation**
   ```bash
   dotnet --version
   ```

## Recommended VS Code Extensions

Install these extensions in VS Code for the best development experience:

- **C# Dev Kit** — [ms-dotnettools.csharp](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
  - Essential for C# editing, debugging, and IntelliSense
- **C# Extensions** — [jchannon.csharpextensions](https://marketplace.visualstudio.com/items?itemName=jchannon.csharpextensions)
  - Helper for C# development tasks
- **.NET Core Test Explorer** — [formulahendry.dotnet-test-explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer)
  - Visual test runner for xUnit tests
- **Pylance** (optional) — Useful for Python-related analysis if needed

**Quick Install from Command Line:**
```bash
code --install-extension ms-dotnettools.csharp
code --install-extension jchannon.csharpextensions
code --install-extension formulahendry.dotnet-test-explorer
```

## Quick Verification

After cloning the repository, run these commands to verify everything is working:

### 1. Restore Dependencies
```bash
dotnet restore
```

### 2. Build the Project
```bash
dotnet build --configuration Release
```
Expected: All projects build successfully with no warnings.

### 3. Run Tests
```bash
dotnet test --configuration Release
```
Expected: All tests pass.

### 4. Run the CLI
```bash
dotnet run --project src/MFBC.Cli
```
Expected: CLI application starts without errors.

If all commands complete successfully, your environment is ready for development!

## Troubleshooting

### NuGet Cache Issues

If you encounter package restoration failures:

```bash
# Clear local NuGet cache
dotnet nuget locals all --clear

# Restore again
dotnet restore
```

### Build Failures

**"Project file not found" or build errors:**
- Ensure you're in the repository root (`monarchfall-blood-and-check/`)
- Verify the `.sln` file exists: `MFBC.sln`

**Formatting or warning errors:**
```bash
# Auto-format code
dotnet format

# Then rebuild
dotnet build
```

### Test Discovery Issues

If tests don't appear in VS Code Test Explorer:

1. Ensure you have the **.NET Core Test Explorer** extension installed
2. Reload the VS Code window: `Ctrl+Shift+P` → "Reload Window"
3. Wait 10-15 seconds for test discovery to complete
4. Check the **Test Explorer** panel on the left sidebar

If tests still don't appear, try:
```bash
dotnet test --list-tests
```

### SDK Version Mismatch

If you see "Project requires .NET 8":

```bash
# Check installed SDKs
dotnet --list-sdks

# If .NET 8 is missing, install it from:
# https://dotnet.microsoft.com/en-us/download/dotnet/8.0
```

### Port Conflicts (if running a local server later)

If you get a "port already in use" error:

**Windows (PowerShell):**
```powershell
Get-NetTCPConnection -LocalPort 5000 | Select-Object -ExpandProperty OwningProcess
```

**macOS/Linux:**
```bash
lsof -i :5000
```

Then kill the process or use a different port.

## Next Steps

1. **Review the architecture:** See [Architecture Overview](../architecture/overview.md)
2. **Read contribution guidelines:** See [CONTRIBUTING.md](../../CONTRIBUTING.md)
3. **Check decision records:** See [docs/decisions/](../decisions/)
4. **Pick an issue:** Visit the [Jira Board](https://jonv11.atlassian.net/jira/software/projects/MFBC/boards/1)

## Questions or Issues?

- **Setup problems?** Open an issue on [GitHub](https://github.com/jonv11/monarchfall-blood-and-check/issues)
- **Development questions?** Check the [Jira board](https://jonv11.atlassian.net/jira/software/projects/MFBC/boards/1) for context
- **Architecture questions?** See [docs/architecture/](../architecture/overview.md)
