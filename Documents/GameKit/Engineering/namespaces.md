# Namespaces

This document describes how namespaces should be organized in the project and how to keep them consistent when working in JetBrains Rider.

## Namespace Pattern

Project namespaces follow these conventions:

- `{ProjectName}.{ModuleName}`
- `{ProjectName}.{ModuleName}.Tests`
- `{ProjectName}.{ModuleName}.Editor`

## Rider Folder Configuration

Rider can generate and adjust namespaces automatically based on folder settings.

Use `Context Menu -> Properties` on folders and configure the `Namespace Provider` option according to each folder's role:

- Enable `Namespace Provider` when the folder name must be part of the namespace
- Disable `Namespace Provider` when the folder is used only for organization and should not appear in the namespace

## asmdef Configuration

Each project `.asmdef` file should define the root namespace explicitly:

```json
"rootNamespace": "{ProjectName}"
```

This gives Rider and related tooling a stable base namespace for namespace suggestions and validation.

## Adjusting Existing Namespaces

After changing `Namespace Provider` settings or `.asmdef` configuration, you can ask Rider to update the existing code automatically.

To do that:

1. Right-click the `_Project` folder
2. Select `Refactor This...`
3. Select `Adjust Namespaces`

This refactoring updates the namespaces in the affected scripts so they match the current folder and project configuration.

## Recommended Workflow

When creating a new module:

1. Create the module folder
2. Configure `Namespace Provider` on the relevant folders
3. Make sure the module asmdef contains `"rootNamespace": "{ProjectName}"`
4. Run `Adjust Namespaces` if existing files need to be synchronized

Following this workflow helps keep namespaces predictable and prevents Rider from suggesting or highlighting incorrect namespace declarations.
