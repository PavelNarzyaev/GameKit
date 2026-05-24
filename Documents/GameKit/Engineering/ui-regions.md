# UiRegions

## Product Article

- [UI Regions](../Product/ui-regions.md)

## How It Works

The scene contains a `UiRegionHost` object with references to the main region containers.

When a region element is requested, the region system loads the registered Addressables prefab and places it under the requested region transform.

The region's transform and layout are controlled in Unity. This means a project can decide whether a region is full-screen, fixed-size, layout-driven, above the content, below the content, or part of a more custom UI shell.

## Regions Management

- Main regions are created and adjusted on the `UiRegionHost` object on the main canvas.
- `UiRegionId` defines the available UI regions.
- `UiRegionElementAddressableIds` contains the Addressables ids for registered region elements.
- The `GameKit/Rebuild UI Region Element Addressable Ids` Unity Editor menu item rebuilds `UiRegionElementAddressableIds.Generated.cs`.
- Main region transforms and their identifiers are linked in `UiRegionHostView`.
- Nested or dynamic regions can use their own `RectTransform` as the parent for spawned region elements.

## Creating a New UiRegionElement

1. Create a component that inherits from `UiRegionElement`.
2. Create a prefab for that component and attach the component to the prefab root.
3. Add the prefab to Addressables.
4. Set the Addressables address to the prefab file name without the `.prefab` extension.
   For example, `MetaPage.prefab` must use the `MetaPage` address.
   Production mode restores debug-only region elements by using the prefab file name as the Addressables address, so the prefab name and address must stay in sync.
5. Run `GameKit/Rebuild UI Region Element Addressable Ids` in the Unity Editor.
6. Use the generated constant from `UiRegionElementAddressableIds.Generated.cs` when calling `UiRegionHostPresenter.OnRegionElementShowing`.

## Limitations

The system does not support multiple simultaneous instances of the same region-element addressable id.

## Modules

### UiRegionsControl

`UiRegionsControl` stores the shared ids that other UI modules use to work with UI regions and region elements.

### UiRegions

`UiRegions` contains the runtime region host, region elements, and the logic that loads registered region-element prefabs into the requested region.
