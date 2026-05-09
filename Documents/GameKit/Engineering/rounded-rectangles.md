# Rounded Rectangles

Rounded rectangles are a common UI element. There are several ways to implement them in Unity, including
third-party shape assets, procedural graphics, shaders, and sliced sprites.

This instruction describes the sliced sprite approach. It is a simple, predictable, and time-tested option for UI
rectangles that need to resize without distorting their corners.

## Scope

This workflow fits filled or stroked UI rectangles with fixed corner radius that must be scaled or resized in Unity.

## Figma Asset

Create the rectangle image in Figma.

Use white (`#FFFFFF`) for the fill or stroke. Unity `Image` color should be used for the final UI color.

Name the asset by its corner radius and variant:

- `Rectangle_CornerRadius52_Fill`
- `Rectangle_CornerRadius52_Stroke8`

Export the image as PNG at `2x` scale and include `@2x` in the exported file name:

- `Rectangle_CornerRadius52_Fill@2x.png`
- `Rectangle_CornerRadius52_Stroke8@2x.png`

Place rectangle sprites under `Assets/_Project/Images/Rectangles`.

## Unity Import

After adding the PNG to Unity, configure the texture import settings:

- `Sprite Mode` - `Single`
- `Pixels Per Unit` - `200`

Then open `Sprite Editor` and set the sprite border:

- `Left` - `CornerRadius * 2`
- `Right` - `CornerRadius * 2`
- `Top` - `CornerRadius * 2`
- `Bottom` - `CornerRadius * 2`

For example, `Rectangle_CornerRadius52_Fill@2x.png` uses a border value of `104` on every side.

Click `Apply` in `Sprite Editor` after changing the border values.

## Usage

Create a UI `Image`, assign the rectangle sprite, choose color and set:

- `Image Type` - `Sliced`

The `RectTransform` can then be resized without stretching the rounded corners.
