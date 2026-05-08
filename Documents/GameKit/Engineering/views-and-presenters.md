# Views and Presenters

- UI `MonoBehaviour` classes are views. A view is responsible only for display, Unity lifecycle integration, and forwarding user interaction through its presenter.
- Every view must work through its own presenter layer instead of accessing project logic directly.
- A presenter is a thin API layer between a view and the rest of the project logic. The presenter must expose the API used by the view and keep that API usable without the view itself, for example in integration tests.
