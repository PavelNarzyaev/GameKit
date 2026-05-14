# Current task notes

Check the workspace folder inside the current project before starting work if you need the latest task context.
Use `[ProjectRoot]/Documents/Workspace` as the project-local workspace path.

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

# Git Staging

The developer uses the staging area as a review boundary. Files or hunks already in stage should be treated as reviewed or intentionally separated from later work.

Agents must not stage changes (`git add`) unless explicitly instructed to create a commit of unstaged changes.

Agents must not unstage changes (`git restore --staged`, `git reset`, etc.).

When the developer asks to create a commit, commit the files that are already staged unless instructed otherwise.

When adding new edits on top of an existing staged set, leave the new edits unstaged so the developer can review them separately.
