# Popups

The Popups system manages a stack of popups in the `Popups` [region](ui-regions.md) above the current page. It is useful for short interactions that should not replace the main screen, such as confirmations, alerts, or game offers.

When the first popup opens, the system shows a backdrop that covers the current page. Each new popup is added above the previous one, and the backdrop is kept directly behind the front popup.

Popups can be modal or non-modal. A modal popup cannot be closed by clicking the backdrop.

## Technical Article

- [Popups](../Engineering/popups.md)
