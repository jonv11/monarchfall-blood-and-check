# Changelog

All notable changes to Monarchfall: Blood & Check are documented in this file.

## [Unreleased]

### Added

- Codecov coverage reporting in CI with coverage badge
- Coverage threshold policy documentation (80% minimum, hard-fail PR checks)
- Local Development Setup Guide with platform-specific instructions and troubleshooting
- ADR process guide with standardized template and status values
- Decision index for all architectural decision records
- Dependabot configuration for monthly NuGet dependency updates
- Dependency security scanning via GitHub and Dependabot
- Dependency update review and merge process documentation
- Prompt for drafting Jira tickets from raw notes

## [0.0.0] — 2026-02-04

### Added

- Initial project scaffold with clean layered architecture
- `MFBC.Core` class library with minimal placeholder infrastructure
- `MFBC.Cli` console application entry point
- `MFBC.Core.Tests` xUnit test suite with example test
- Solution file (`MFBC.sln`) tying together all projects
- Directory.Build.props for centralized compiler settings
- Documentation: Architecture overview and decision records
- CI/CD workflow for build and test validation
- .gitignore, .editorconfig, and contribution guidelines
- MIT license

### Infrastructure

- Build: `dotnet build` succeeds
- Test: `dotnet test` succeeds
- Formatting: .editorconfig enforces consistent style
- Version: Targets .NET 8 LTS
