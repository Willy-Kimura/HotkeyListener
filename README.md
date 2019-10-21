# HotkeyListener
[![hotkeylistener-nuget](https://img.shields.io/badge/NuGet-1.0.0-brightgreen.svg)](https://www.nuget.org/packages/HotkeyListener/) [![wk-donate](https://img.shields.io/badge/BuyMeACoffee-Donate-orange.svg)](https://www.buymeacoffee.com/willykimura)

![hotkeylistener-logo](Assets/hkl-logo.png)

**HotkeyListener** is a .NET library that allows registering system-wide hotkeys that can be used to trigger features in .NET applications. It also provides support for enabling standard Windows and User controls to select hotkeys at runtime.

# Installation

To install via the [NuGet Package Manager](https://www.nuget.org/packages/HotkeyListener/) Console, run:

> `Install-Package HotkeyListener`

# Features
Here's a comprehensive list of the features available:

- Supports [.NET Framework 4.0](https://www.microsoft.com/en-us/download/details.aspx?id=17718) and higher.
- Manages system-wide hotkeys in a crud-like fashion using the methods `Add`, `Update`, `Remove` and `RemoveAll`. You can also suspend and resume using hotkeys using the methods `Suspend` and `Resume`.
- Determines the pressed hotkey(s) using the `HotkeyPressed` event and its `Hotkey` argument.
- Determines the application from which a hotkey was pressed using the  `HotkeyPressed` event's `SourceApplication` argument.
- In addition to hotkey-listening, the `HotkeySelector` class lets you enable any control to be used for selecting hotkeys at runtime. `HotkeyListener` would be pretty much half-baked if this class wasn't available, and thus the need for it. This will be super handy for applications that let end-users choose their own preferred hotkey(s) for certain features to be invoked/launched.
- As a nifty addition to this library, `HotkeyListener` includes a helper method, `GetSelection`, which lets you get the text selected in any active application from where a hotkey was pressed. To get a glimpse of what's possible with this feature, think of how [WordWeb]( https://wordweb.info/free/ ) is able to lookup the definition of any phrase you've selected at the press of a hotkey... Or how [Cintanotes]( http://cintanotes.com/ ) lets you save highlighted texts as notes from any active application in an instant. That's precisely what you can achieve with `HotkeyListener`.

# Usage

#### Adding Hotkeys

First, ensure you import the library's namespace:

```c#
using WK.Libraries.HotkeyListenerNS;
```

...then instantiate the class, add some hotkeys:

```c#
var hkl = new HotkeyListener();

hkl.Add("Control+Shift+E");
hkl.Add("Control+R");
```

The `Add` method also allows adding an array of hotkeys at once:

```c#
// Adds an array of hotkeys.
hkl.Add(new[] { hotkey1, hotkey2 });
```

> **Important:** If you're building an application that has no external user-option for changing or customizing the default hotkey(s) set, something you'll need to consider when working with global hotkeys is that there are a number of predefined keys or key combinations already in use within a number of applications such as [Google Chrome](https://chrome.google.com) - for example, `Control+Tab`. This then means that you may need to find the right key or key combination to use when shipping your applications.

#### Listening to Key Presses

Now to listen to key presses, use the `HotkeyPressed` event:

```c#
hkl.HotkeyPressed += Hkl_HotkeyPressed;

private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
{
    if (e.Hotkey == "Control+Shift+E")
        MessageBox.Show("First hotkey was pressed.");
    if (e.Hotkey == "Control+R")
        MessageBox.Show("Second hotkey was pressed.");
}
```






*Made with* 😊 *by* [*Willy Kimura*]([https://github.com/Willy-Kimura).