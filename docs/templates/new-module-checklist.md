# New Module Checklist

## Purpose
Provide a concise, repeatable checklist for introducing a new MFBC module with correct architecture, naming, testing, and documentation.

## Scope
Use this when adding a new project/module such as `MFBC.RuleSet`, `MFBC.ProcGen`, `MFBC.AI`, or other solution modules.

## Prerequisites
- Review `docs/architecture/overview.md`
- Review `docs/architecture/dependency-boundaries.md`
- Review `docs/development/naming-conventions.md`
- Review `docs/development/testing-strategy.md`
- Decide whether an ADR is required (`docs/decisions/README.md`)

## Module Naming
- Use project name pattern: `MFBC.<ModuleName>` (PascalCase segment)
- Match assembly and root namespace:
  - `<AssemblyName>MFBC.<ModuleName></AssemblyName>`
  - `<RootNamespace>MFBC.<ModuleName></RootNamespace>`
- Use matching test project name: `MFBC.<ModuleName>.Tests`

## Folder Structure
```text
src/
  MFBC.<ModuleName>/
    MFBC.<ModuleName>.csproj
    <module source files>

tests/
  MFBC.<ModuleName>.Tests/
    MFBC.<ModuleName>.Tests.csproj
    <test files>
```

## .csproj Template (Module)
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <AssemblyName>MFBC.<ModuleName></AssemblyName>
    <RootNamespace>MFBC.<ModuleName></RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <!-- Keep references minimal and architecture-compliant -->
    <!-- Example:
    <ProjectReference Include="..\\..\\src\\MFBC.Core\\MFBC.Core.csproj" />
    -->
  </ItemGroup>
</Project>
```

## .csproj Template (Tests)
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <AssemblyName>MFBC.<ModuleName>.Tests</AssemblyName>
    <RootNamespace>MFBC.<ModuleName>.Tests</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="xunit" Version="2.9.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.5">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.0.1" />
    <PackageReference Include="coverlet.collector" Version="6.0.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\\..\\src\\MFBC.<ModuleName>\\MFBC.<ModuleName>.csproj" />
  </ItemGroup>
</Project>
```

## Architecture Checklist
- [ ] Dependency direction is valid (Core never depends on CLI/presentation)
- [ ] New module references only what is allowed by architecture docs
- [ ] Public APIs are intentional and minimal
- [ ] No accidental infrastructure leakage into `MFBC.Core`

## Test Checklist
- [ ] Test project created under `tests/`
- [ ] Behavior changes covered by tests
- [ ] Deterministic behavior tested when relevant (fixed seed / stable output)
- [ ] `dotnet test` passes locally

## Documentation Checklist
- [ ] Add or update architecture docs if module boundaries changed
- [ ] Update `docs/README.md` when adding new long-lived docs
- [ ] Update `README.md`/`CONTRIBUTING.md` if contributor workflow changed
- [ ] Add ADR for significant architecture decisions

## ADR Decision Gate
Create an ADR if any of the following is true:
- New module changes dependency boundaries
- New module introduces cross-cutting contracts
- New module requires non-trivial tradeoffs that need durable rationale

ADR references:
- `docs/decisions/README.md`
- `docs/decisions/TEMPLATE.md`

## Validation Commands
```bash
dotnet restore MFBC.sln
dotnet format MFBC.sln --verify-no-changes --verbosity diagnostic
dotnet build MFBC.sln --no-restore --configuration Release
dotnet test MFBC.sln --no-build --configuration Release
```
