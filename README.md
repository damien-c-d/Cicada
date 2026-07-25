# Cicada

A Windows auto clicker with a native-feeling Fluent interface, built with Avalonia and .NET 10.

## Features

- Left, middle or right click, sent as single, double or triple clicks.
- A delay before each click, in seconds.
- Repeat a set number of times, or until you stop it.
- System / Light / Dark theme, remembered between runs.
- A Mica backdrop on the whole window, title bar included, on Windows 11 22H2 and later.

## Requirements

- Windows. Clicks are sent through the Win32 `SendInput` API, so the app is Windows-only.
- The [.NET 10 SDK](https://dotnet.microsoft.com/download) to build it.

Windows 11 22H2 or later is needed for the Mica backdrop; older versions fall back to a plain
window.

## Building and running

```
dotnet run --project src/Cicada.App
```

## Layout

| Project | Contents |
| --- | --- |
| `src/Cicada.Core` | Click generation and the `SendInput` interop it sits on. |
| `src/Cicada.App` | The Avalonia UI, its view models, and the theme and clicking services. |

Set `CICADA_SELFCHECK=1` to run the title-bar theme self-check on startup; the app exits with the
number of failures rather than showing its window.

## License

[MIT](LICENSE.txt).
