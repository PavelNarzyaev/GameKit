# Coding Standards

These project-specific standards are based on Unity's C# style guidance, adapted to the conventions used in GameKit:

- [C# Code Style Guide (Unity 6 edition)](https://unity.com/resources/c-sharp-style-guide-unity-6)
- [Formatting best practices for C# scripting in Unity](https://unity.com/how-to/formatting-best-practices-c-scripting-unity)

## Scope

This document defines the C# coding standards for scripts located in the `Assets/_Project/Modules` folder.

## General Rules

IMPORTANT: The primary priority when writing code is readability. Choosing clear and precise names that accurately describe the elements of the system is one of the most important aspects of writing code. Naming must always reflect the actual behavior and purpose of the element. When there is a trade-off between readability and brevity, readability should always be preferred. Abbreviations and acronyms should be avoided whenever possible.

## Casing

Classes - PascalCase  
Structs - PascalCase  
Namespaces - PascalCase  
Enums (both names and values) - PascalCase  
Interfaces - PascalCase with `I` prefix  
Public fields - PascalCase  
Properties - PascalCase  
Methods - PascalCase  
Events - PascalCase  
Constants - PascalCase with `k_` prefix  
Static variables - camelCase with `s_` prefix  
Private member variables - camelCase with `m_` prefix  
Local variables - camelCase  
Parameters - camelCase  

## Naming

- Use nouns or noun phrases for classes, structs, properties, fields, local variables, and parameters
- Use verbs or verb phrases for methods because a method describes an action

### Boolean Names

- Boolean variables, fields, parameters, and properties should read like a yes/no condition
- Prefer prefixes such as `is`, `has`, `can`, `should`, `was`, or `needs`

### Method Names

- Methods must be named with a verb or verb phrase that describes what they do

### Events And Event Listeners

- Events must use PascalCase and describe what is happening or what has happened
- Event listeners must clearly indicate that they handle an event
- Prefer `Handle<EventName>` for local handlers and `<Source>_<EventName>` when the source object matters
- Methods that raise events should use the `On<EventName>` pattern

## Formatting

- Indentation - 4 spaces, no tabs
- Brace style - Allman (opening braces on a new line)
- Braces - Always use braces, even for single-line statements (`if`, `for`, `foreach`, `while`)
- Vertical spacing - Use blank lines to separate distinct parts of a class (e.g., between fields and methods)
- Whitespace - Trim trailing whitespace
- Declarations - One variable declaration per line
- Line length - Max 120 characters (soft limit)

### Spacing

- Single space before flow control conditions (e.g., `while (x == y)`)
- Single space before and after comparison operators (e.g., `x == y`)
- Single space after commas between function arguments
- No spaces inside brackets (e.g., `dataArray[index]`)
- No spaces between function names and parentheses
- No spaces after opening or before closing parentheses

## Usings and Namespaces

- Namespaces - Required in every file to prevent naming conflicts
- Placement - Keep all `using` directives at the top of the file
- Grouping - Place `System` namespaces before others
- Sorting - Sort alphabetically within groups
- Cleanup - Remove all unused `using` directives

More information about namespaces can be found here: [Namespaces](namespaces.md)

## Other Rules

- File Ending - Every file must end with a single empty newline
- One MonoBehaviour per file and the file name must match the class name
- Auto-generated C# files must use the `.Generated.cs` suffix
- Comments should only be added in exceptional cases. For example, when it is necessary to warn developers about non-obvious side effects or potential issues when modifying the code.

## Views and Presenters

- View classes must use the `View` suffix.
- Presenter classes must use the `Presenter` suffix.
- Inside a view, the presenter reference must always be named `m_presenter`.

Class responsibilities are described in [Views and Presenters](views-and-presenters.md).
