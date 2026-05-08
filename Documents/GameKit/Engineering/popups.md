# Popups

## Product Article

- [Popups](../Product/popups.md)

## Responsibilities

- Open and close popup `UiRegionElement` instances in the `Popups` region.
- Track the current front popup in the stack.
- Manage the `PopupBackdropView` element and keep its sibling index in sync with the stack.
- Expose modal-state information for the front popup.

## Boundaries

- `UiPopups` depends on [UiRegions](../Product/ui-regions.md) because it uses the shared region-host infrastructure.

## Modules

### UiPopups

`UiPopups` contains the shared popup navigation logic, the popup backdrop, and the presenters that expose stack state to popup views.

### UiErrorPopup

`UiErrorPopup` contains the example error popup view and presenter.
