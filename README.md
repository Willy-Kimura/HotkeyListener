# HotkeyListener
[![hotkeylistener-nuget](https://img.shields.io/badge/NuGet-1.0.0-brightgreen.svg)](https://www.nuget.org/packages/HotkeyListener/) [![wk-donate](https://img.shields.io/badge/Gumroad-Purchase-blue.svg)](https://www.buymeacoffee.com/willykimura)

![hotkeylistener-logo](Assets/hkl-logo.png)

**HotkeyListener** is a .NET library that allows registering system-wide hotkeys that can be used to trigger features in .NET applications. It also provides support for enabling standard Windows and User controls to select hotkeys at runtime.

# Installation

To install via the [NuGet Package Manager](https://www.nuget.org/packages/HotkeyListener/) Console, run:

> `Install-Package HotkeyListener`

# Features
- Supports [.NET Framework 4.0](https://www.microsoft.com/en-us/download/details.aspx?id=17718) and higher.
- Manages system-wide hotkeys in a crud-like fashion using the methods `Add`, `Update`, `Remove` and `RemoveAll`. You can also suspend and resume using hotkeys using the methods `Suspend` and `Resume`.
- Determines the pressed hotkey(s) using the `HotkeyPressed` event and its `Hotkey` argument.
- Determines the application from which a hotkey was pressed using the  `HotkeyPressed` event's `SourceApplication` argument.
- In addition to hotkey-listening, the `HotkeySelector` class lets you enable any control to be used for selecting hotkeys at runtime. `HotkeyListener` would be pretty much half-baked if this class wasn't available, and thus the need for it. This will be super handy for applications that let end-users choose their own preferred hotkey(s) for certain features to be invoked/launched.
- As a nifty addition to this library, `HotkeyListener` includes a helper method, `GetSelection`, which lets you get the text selected in any active application from where a hotkey was pressed. To get a glimpse of what's possible with this feature, think of how [WordWeb]( https://wordweb.info/free/ ) is able to lookup the definition of any phrase you've selected at the press of a hotkey... Or how [Cintanotes]( http://cintanotes.com/ ) lets you save highlighted texts as notes from any active application in an instant. That's precisely what you can achieve with `HotkeyListener`.

# Usage

### Adding Hotkeys

First, ensure you import the library's namespace:

```c#
using WK.Libraries.HotkeyListenerNS;
```

...then instantiate the class and add some hotkeys:

```c#
var hkl = new HotkeyListener();

// Define a new hotkey using the Hotkey class.
Hotkey hotkey1 = new Hotkey(Keys.Control | Keys.Shift, Keys.J);

// You can also define a hotkey in string format.
Hotkey hotkey2 = new Hotkey("Control+Shift+D4");

hkl.Add(hotkey1);
hkl.Add(hotkey2);
```

The `Add()` method also allows adding an array of hotkeys at once:

```c#
hkl.Add(new[] { hotkey1, hotkey2 });
```

> **Important:** If you're building an application that has no external user-option for changing or customizing the default hotkey(s) set, something you'll need to consider when working with global hotkeys is that there are a number of predefined keys or key combinations already in use within a number of applications such as [Google Chrome](https://chrome.google.com) - for example, `Control+Tab`. This then means that you may need to find the right key or key combination to use when shipping your applications.

### Listening to Hotkeys

To listen to key presses, use the `HotkeyPressed` event:

```c#
// Add a HotkeyPressed event.
hkl.HotkeyPressed += Hkl_HotkeyPressed;

private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
{
    if (e.Hotkey == hotkey1)
        MessageBox.Show($"First hotkey '{hotkey1}' was pressed.");
    
    if (e.Hotkey == hotkey2)
        MessageBox.Show($"Second hotkey '{hotkey2}'was pressed.");
}
```

Unlike with the standard Windows `KeyDown` and `KeyUp` events, here only the registered keys will be detected.

If you'd like to get the details of the active application where a hotkey was pressed, simply use the `SourceApplication` argument property:

```c#
private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
{
    if (e.Hotkey == hotkey2)
    {
    	MessageBox.Show(
            "Application:" + e.SourceApplication.Name + "\n" +
            "Title: " + e.SourceApplication.Title + "\n" +
            "ID: " + e.SourceApplication.ID + "\n" +
            "Handle: " + e.SourceApplication.Handle + "\n" +
            "Path: " + e.SourceApplication.Path + "\n"
        );
    }
}
```

As a special feature (in `Beta` though), if you'd like to get any text that may have been selected when a hotkey added was pressed, use Hotkey Listener's `GetSelection()` method:

```c#
private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
{
    if (e.Hotkey == hotkey1)
    {
        // Get the selected text, if any.
        string selection = hkl.GetSelection();
    	
        // If some text was selected, 
        // display a message box.
        if (selection != string.Empty)
            MessageBox.Show(selection);
    }
}
```

### Updating Hotkeys

You can update hotkeys using the `Update()` method. It works the same way as *string replacement* where you provide the current string and its replacement option:

```c#
hkl.Update(hotkey1, new Hotkey(Keys.Control | Keys.Alt, Keys.T));
```

Hotkey updates can occur even when the application is running. **However**, something important you need to note is that **always use variables to store hotkeys** since in this way, whenever an update to the hotkey occurs, it will automagically be detected in the `HotkeyPressed` event. 

Here's what I mean:

```c#
Hotkey myHotkey = new Hotkey(Keys.Control | Keys.Alt, Keys.T);

// To update our hotkey, simply pass the current hotkey 
// with a ref keyword to the variable and its replacement.
hkl.Update(ref myHotkey, new Hotkey(Keys.Alt, Keys.T);
```

This will ensure that both the hotkey and its variable have been updated to reflect the changes made. This design is especially handy if your application saves *user settings* after update.

Here's another classical example of updating a hotkey:

```c#
Hotkey hotkey1 = new Hotkey(Keys.Control | Keys.Alt, Keys.T);
Hotkey hotkey2 = new Hotkey(Keys.Alt, Keys.T);

// Once we reference the variable hotkey1, this will 
// update the hotkey and listen for its key-presses.
hkl.Update(ref hotkey1, hotkey2);
```

### Removing Hotkeys

To remove a hotkey, we use the `Remove()` method. This method has two variants:

>`Remove()`: This accepts a *single* hotkey or an *array* of hotkeys.
>
>`RemoveAll()`: This removes all the registered hotkeys.

Below are some examples:

```c#
// Remove a single hotkey.
hkl.Remove(hotkey1);
```

```c#
// Let's use an array of already registered hotkeys.
Hotkey[] hotkeys = new Hotkey[] 
{ 
    new Hotkey(Keys.Alt, Keys.E), 
    new Hotkey(Keys.Alt, Keys.J) 
};

// Remove the array of hotkeys.
hkl.Remove(hotkeys);
```

```c#
// This will remove all the registered hotkeys.
hkl.RemoveAll();
```

### Suspending/Resuming Hotkeys

**Suspending** hotkeys simply refers to *disabling* or *deactivating* the hotkeys while **resuming** refers to *enabling* or *reactivating* the hotkeys for continued use. 

These two methods are very applicable in a case scenario where a user would prefer to change a specific hotkey while its currently active.

 *What do you mean?* 

We'll let us imagine we have two hotkeys `Control+Shift+E` and `Alt+X`, but the user prefers to change `Control+Shift+E` to `Alt+X` and `Alt+X` to something else. In this case, we cannot simply change one hotkey to another if the hotkeys are currently active. *So what do we do?* We first of all need to **suspend** the currently active hotkeys to prevent them from being detected or listened to, change the respective hotkeys, then **resume** listening to the hotkeys having made the necessary changes.

Here's an example:

```c#
Hotkey hotkey1 = new Hotkey(Keys.Control | Keys.Shift, Keys.E);
Hotkey hotkey2 = new Hotkey(Keys.Alt, Keys.X);

// Suspend all registered hotkeys.
hkl.Suspend();

// Update hotkeys to newer keys.
hkl.Update(ref hotkey1, new Hotkey(Keys.Alt, Keys.X));
hkl.Update(ref hotkey2, new Hotkey(Keys.Shift, Keys.PrintScreen));

// Resume listening to the hotkeys.
hkl.Resume();
```

Hope that's much clearer now...

Now, let's find out more on how to work with this feature using one very important class that comes with Hotkey Listener - the `HotkeySelector` class.

## The `HotkeySelector` Class

As noted earlier, `HotkeyListener` would have been pretty much half-baked had the ability to provide hotkey selection not been there. That's the whole intention of this class. It is able to "convert" any control into an actual hotkey selector for usage at runtime.

Here's a preview of an application using this feature:

![sample-hotkey-selector-usage](Assets/sample-hotkey-selector-usage.gif)

### Enabling Hotkey Selection For Controls

Firstly off, instantiate a new `HotkeySelector` instance:

```c#
var hks = new HotkeySelector();
```

To enable any control for hotkey selection, use the `Enable()` method:

```c#
// Enable textBox1.
hks.Enable(textBox1);
```

When enabling a control, you can set the default hotkey to be displayed:

```c#
hks.Enable(textBox1, "Control+Shift+S");
```

To set a default hotkey without necessarily enabling it, use the `Set()` method:

```c#
hks.Set(textbox1, "Control+Shift+S");
```

You can even use the standard `Keys` enumeration to set the default hotkey in a control: 

```c#
// Sets "Control+Shift+S" as the default hotkey.
hks.Set(textbox1, Keys.S, Keys.Control | Keys.Shift);
```





*Made with* 😊 *by* [*Willy Kimura*]([https://github.com/Willy-Kimura).