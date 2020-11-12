---
uid: ViewLocatorConfigurator
---
# ViewLocator Configurator

Imagine a requirement that you need to build an application that supports normal and touch views. Usually you need to build a view model per view or more bad two applications which consist of many duplicate lines of code.
The client framework comes with a so called [ViewLocatorConfigurator](xref:Moryx.ClientFramework.Kernel.ViewLocatorConfigurator) (VLC) which allows you to implement the One-ViewModel-Many-Views concept. This article will explain to you what things you have to consider.

## Basics

The VLC is configured via a so called preset. A preset returns a`IViewLocatorConfiguratorSet` that contains all view suffixes that controls the `Caliburn.Micro.ViewLocator`. Note that `Caliburn` follows last wins strategy. Let's have a look on the following view suffixes:

* View -> MyModelView
* Page -> MyModelPage

If `Caliburn.Micro` finds both class types it will instantiate the last one. If you don't change anything this default is used.

But how can you influence these suffixes?

## How the Client Framework uses the VLC

The VLC always comes to play when the framework starts. It uses the persisted `ViewPreset` property of the [AppConfig][xref:Moryx.ClientFramework.AppConfig]. The VLC tries to resolve the preset and reconfigures the `Caliburn.Micro.ViewLocator`. If that fails the application will exit with a message. Then you need to check why the framework cannot load the saved preset.
There is one exception where the VLC doesn't come to play: When the ClientFramework starts in `CONFIG_MODE`. In this mode the VLC is disabled. So you have always the chance to reconfigure the `ViewPreset`.

## Create your own preset

The Client Framework comes with a set of builtin presets which you can always select:

| Set name      | Suffixes      |
|---------------|---------------|
| Default | View, Page |
| Touch | View, Page, View_Touch |
| WideScreen | View, Page, View_WideScreen |

But you can extent the list of available presets by creating you own preset. Just have a look on the following example:

````cs
[KernelComponent(typeof(IViewLocatorConfiguratorPreset))]
public class MyPreset : IViewLocatorConfiguratorPreset
{
    /// <inheritdoc />
    public string Name => "MyPreset";

    /// <inheritdoc />
    public IEnumerable<string> ViewSuffixes => new[] { "Advanced" };
}
````

Please note that the preset names have to be unique. So now you can implement `TestViewModel`, `TestView`, `TestView_Advanced` classes and you will see that the ClientFramework will use the `TestView` when you configured the `Default` preset and the `TestView_Advanced` when you configured `MyPreset`.

## Advanced

One last word if ou want to override the default behavior. That's possible.

````cs
var viewLocatorConfigurator = _container.Resolve<IViewLocatorConfigurator>();
viewLocatorConfigurator.ActivateSet("MyPreset");
````

Note that the preset has to be available.