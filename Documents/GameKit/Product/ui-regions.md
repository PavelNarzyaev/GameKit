# UI Regions

UI Regions define the main screen areas where the game places pages, popups, panels, and other screen-level UI elements.

A region is not just a draw-order layer. It is a named area in the scene that can fill the whole screen, occupy only part of it, participate in Unity layout, or stay hidden until the project needs it.

## Terms

- `UiRegion` is a scene container where UI elements are placed.
- `UiRegionElement` is a prefab that can be shown inside a region.

## Purpose

UI Regions give the project a shared way to describe where major UI parts should appear on screen.

This lets different UI features work together without each feature creating its own screen layout rules. A project can keep pages, popups, debug panels, tab bars, and similar elements in clearly defined places and adjust those places in Unity when the UI shell changes.

## Technical Article

- [UI Regions](../Engineering/ui-regions.md)
