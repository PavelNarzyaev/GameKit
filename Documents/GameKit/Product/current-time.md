# Current Time

Current Time defines what the game treats as "now" for time-based systems such as resource regeneration.

In non-production builds, you can inspect the current time and apply a debug offset in the [Debug Panel](debug-panel.md).

## Purpose

This feature gives the project a single source of truth for the current time, so gameplay systems can use one consistent timestamp instead of reading the clock in different ways.

## Time Sources

> [!WARNING]
> Server-authoritative time is not implemented yet. The project currently relies on the device clock, so changing the device time can affect time-based progression.

### Client Time

The current implementation uses the device's UTC clock as its base time source.

**Advantages:**
- Works without an internet connection
- Simple to implement
- Is available immediately during application startup

**Disadvantages:**
- Players can change the device time and affect time-based progression
- Devices with incorrect clocks may produce inconsistent timestamps

### Server Time

Server time is retrieved from a backend or another trusted remote source.

**Advantages:**
- Reduces simple time-based cheating
- Keeps time-based mechanics consistent across players and devices

**Disadvantages:**
- Requires network access
- Adds integration and maintenance cost
- Can delay startup or degrade behavior when the time source is unavailable

## Technical Article

- [Current Time](../Engineering/current-time.md)
