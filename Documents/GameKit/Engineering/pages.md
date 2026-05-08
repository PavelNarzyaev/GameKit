# Pages

## Product Article

- [Pages](../Product/pages.md)

## Responsibilities

- Keep track of the addressable id of the current page.
- Hide the previous page before showing the next one.
- Notify other UI objects when the current page changes.

## Boundaries

- `UiPages` depends on [UiRegions](ui-regions.md) because it uses the shared region-host infrastructure.

## Modules

### UiPages

`UiPages` contains `PageNavigator`, which stores the current page state and switches the visible page region element.

### UiCorePage

`UiCorePage` contains the example core page view and presenter.

### UiMetaPage

`UiMetaPage` contains the example meta page view and presenter.
