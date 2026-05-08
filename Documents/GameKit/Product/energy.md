# Energy

Energy is a spendable player resource that limits how often certain actions can be performed.

When the player spends energy, the value decreases immediately. If the current amount is below the restoration limit, energy restores automatically over time until it reaches that limit again.

The restoration behavior is configured through the project config. The current implementation lets you define how many seconds are required to restore one energy point and what value stops automatic restoration.

In non-[production](production-mode.md) builds, you can inspect and change the current energy through the [Debug Panel](debug-panel.md).

## Technical Article

- [Energy](../Engineering/energy.md)
