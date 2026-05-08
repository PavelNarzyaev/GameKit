# Production Mode

## Product Article

- [Production Mode](../Product/production-mode.md)

## How to Switch

Production mode is controlled through the Unity Editor menu: `GameKit/Enable Production Mode` and `GameKit/Disable Production Mode`.

> [!WARNING]
> Configure your CI/CD workflow to ensure that Production Mode is enabled in release builds.

## Coding Recommendations

If you need to exclude code from the final build, use preprocessor directives with `IS_PRODUCTION`. This is the right choice for build-specific code paths and for code that should not be included in the compiled player.

If you need to exclude an entire module from the final build, prefer `.asmdef` `defineConstraints`:
```json
    "defineConstraints": [
        "!IS_PRODUCTION"
    ],
```

If the code is only part of the application logic, prefer `ProductionModeProvider.IsProduction`. This keeps the code easier to read, test, and maintain.

## Modules

### ProductionMode
