# Current task notes

Check [workspace folder](Documents/Workspace/) before starting work if you need the latest task context

# Fail fast

Follow the fail-fast principle when writing code. Do not introduce defensive checks (null, empty, existence, etc.) unless they are clearly necessary and justified.

# Public Repository

This repository is intended to be public. Do not add code, assets, documentation, or other materials unless their license allows redistribution in a public source repository.

Do not add Unity Asset Store assets in raw source form under the standard Asset Store EULA unless there is a separate license or written permission that allows redistribution in a public source repository.

When adding third-party code, packages, assets, fonts, art, generated SDK files, or other materials, record the source and license in `THIRD_PARTY_NOTICES.md`.

# Unity Meta Files

Agents must not create or edit `.meta` files.

Agents may delete or move `.meta` files only when deleting or moving the corresponding asset/file.

# Backward compatibility

Backward compatibility for save data is not required before version 1.0.0.
If version matters for the task and is not specified in project notes or documentation, determine it from the project configuration.

# Build And Tests

Agents must not attempt to verify this project with `dotnet build`, other build commands, or automated test commands.

Build verification and test execution are currently not supported in this repository environment, so agents should state that they were not run instead of trying ad-hoc workarounds.

Git Staging

Agents must not stage changes (git add) unless explicitly instructed to create a commit.
