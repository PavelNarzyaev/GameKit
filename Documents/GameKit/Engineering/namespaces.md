# Namespaces

This document describes how namespaces should be organized in the project.

## Namespace Pattern

Project namespaces follow these conventions:

- `{ProjectName}.{ModuleName}`
- `{ProjectName}.{ModuleName}.Tests`
- `{ProjectName}.{ModuleName}.Editor`
- `{ProjectName}.{ModuleName}.Contracts`

## Enforcement

Namespace consistency is maintained manually during review.

Rider's file-location namespace inspection is disabled in `Assets/_Project/.editorconfig`:

```ini
resharper_check_namespace_highlighting = none
```

Do not rely on Rider `Namespace Provider` folder settings or `.asmdef` `rootNamespace` values for project namespace validation.

Issue: [RSRP-481178](https://youtrack.jetbrains.com/issue/RSRP-481178/Provide-more-control-for-Adjust-Namespace-refactoring).
