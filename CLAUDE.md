# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

The single project file is `DayCare/DayCare.csproj`. The solution file is `DayCare.sln`.

```bash
# Build for all targets
dotnet build DayCare/DayCare.csproj

# Build for a specific platform
dotnet build DayCare/DayCare.csproj -f net10.0-windows10.0.19041.0
dotnet build DayCare/DayCare.csproj -f net10.0-android
dotnet build DayCare/DayCare.csproj -f net10.0-ios
dotnet build DayCare/DayCare.csproj -f net10.0-maccatalyst

# Run on Windows
dotnet run --project DayCare/DayCare.csproj -f net10.0-windows10.0.19041.0
```

There are no automated tests at this time.

## Architecture

**TotDesk** (app display name) is a .NET 9 MAUI cross-platform app (Android, iOS, macCatalyst, Windows) targeting daycare parents. It uses the MVVM pattern via **CommunityToolkit.Mvvm**.

### Auth flow
- App starts at `LoginPage` (wrapped in `NavigationPage`).
- After successful sign-in, `LoginViewModel` replaces the root page with `AppShell`.
- `AppShell` is a `Shell` with a `TabBar` containing three tabs: **Pick Up/Drop Off**, **Feed**, and **Billing**.
- Settings is pushed as a named route (`nameof(SettingsPage)`) from a toolbar gear icon on the shell.
- Sign-out in `SettingsViewModel` restores the root back to `NavigationPage(LoginPage)`.

### Auth service
- `IAuthService` is the contract (`SignInAsync`, `SignOutAsync`, `IsAuthenticatedAsync`, `CurrentUser`).
- **`MockAuthService`** is currently registered in `MauiProgram.cs` — it bypasses real auth and always succeeds. This is the active implementation.
- **`MsalAuthService`** is the production implementation using MSAL (Microsoft.Identity.Client) for Microsoft Entra (Azure AD) sign-in. To activate it, replace `MockAuthService` with `MsalAuthService` in `MauiProgram.cs` and fill in `ClientId` / `TenantId` constants in `MsalAuthService.cs`. Also update the `DataScheme` in `Platforms/Android/MsalActivity.cs` to `msal{ClientId}`.

### ViewModel conventions
- All ViewModels inherit `BaseViewModel` (extends `ObservableObject` from CommunityToolkit.Mvvm).
- `BaseViewModel` provides `IsBusy`, `IsNotBusy`, and `Title`.
- Use `[ObservableProperty]` for bindable properties and `[RelayCommand]` for commands — the source generator creates the public property/command.
- ViewModels are registered as `Transient` in DI; `IAuthService` is `Singleton`.

### DI and page wiring
- All pages and ViewModels are registered in `MauiProgram.CreateMauiApp()`.
- `AppShell` manually assigns tab content via DI (`_serviceProvider.GetRequiredService<TPage>()`) so ViewModels are properly constructor-injected.
- XAML pages use `x:DataType` for compiled bindings — always specify the correct ViewModel type on each `ContentPage` or `DataTemplate`.

### Converters
- `BoolToColorConverter` — maps `bool` to a color (used for invoice paid/due status).
- `InvertedBoolConverter` — negates a `bool` binding (e.g., hide Pay button when already paid).
