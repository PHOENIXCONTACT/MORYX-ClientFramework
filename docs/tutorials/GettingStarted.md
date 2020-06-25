---
uid: GettingStarted
---
# Getting Started

This tutorial will show you how MARVIN WPF clients should be created.

## Prerequisites

First of all you need to create a new `WPF Application` solution and name it `TestClient`. After the solution and the project `TestClient` has been created, delete `App.xaml` and `MainWindow.xaml` files and its code behinds.
Open now the project properties and set the `Target Framework` at least to version `.NET Framework 4.6.1`. That's rather important because the `ClientFramework` assemblies do not support older .NET Framework versions.

Manage Nuget packages on `TestClient` project. There install the following three packages:

- `Marvin.ClientFramework.Kernel`
- `Marvin.ClientFramework.Configurator`
- `Marvin.ClientFramework.PhoenixShell`

You need only these three packages. All neccessary dependancies are installed automatically.

You are very close now. Add a new file named `Program.cs` and copy the following code to it:

````cs
using System;
using Marvin.ClientFramework.Kernel;

namespace TestClient
{
    public class Program
    {
        /// <summary>
        /// Main start point of this application
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            var hol = new HeartOfLead(args);
            hol.Start();
        }
    }
}
````

That's it. Build the solution and start it. You should now see the `Configurator`. You'll see that `Mode` is currently not configured. This causes that the `TestClient` always starts the configurator. Set `Mode` to _`Local_RunMode`_ and save the configuration. Now the `TestClient` starts always the `PhoenixShell`. If you want to start the configurator again just hold the CTRL key when the application is starting.

The next chapter describes how to create a sample module.

## Sample module

As you might recognized your `PhoenixShell` is empty. That's due to missing modules in the sample application. So let's build up a sample module that is shown within the `PhoenixShell`.

Create a new C# Library named `TestModule` and add the following framework assemblies:

- PresentationCore
- PresentationFramework
- System.Xaml
- System.Xml
- System.Xml.Linq
- WindowsBase

Don't forget to set the Target .NET Framework version to 4.6.1.

Then open the `NuGet Package Manager` and add `Marvin.ClientFramework` package to your project. This will add all dependencies to your library you need.

Now create the following two folders to the `TestModule`:

- ModuleController
- Workspace

The `ModuleController folder` contains two classes:

The modules configuration. Which is in our case is an empty hull. But in the real world all needed settings will find their place here.

````cs
using System.Runtime.Serialization;
using Marvin.ClientFramework;

namespace TestModule.ModuleController
{
    [DataContract]
    public class ModuleConfig : ClientModuleConfigBase
    {
    }
}
````

And it contains the `ModuleController` itself. It's the entry point for a module. There you set the `DisplayName` and the initial workspace view model.

````cs
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Marvin.ClientFramework;
using TestLibrary.Workspace;

namespace TestModule.ModuleController
{
    [ClientModule("TestModule")]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        protected override void OnInitialize()
        {
            // Display name of this module
            Config.DisplayName = "Test";
        }

        protected override void OnActivate()
        {
        }

        protected override void OnDeactivate(bool close)
        {
        }

        protected override IModuleWorkspace OnCreateWorkspace()
        {
            // Return the default workspace
            return Container.Resolve<IModuleWorkspace>(nameof(TestWorkspaceViewModel));
        }

        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {
        }

        public override Geometry Icon { get; }
    }
}
````

As you might have noticed there is no workspace implemented yet. We'll make it up now. Add a UserControl named `TestWorkspaceView` and a simple class `TestWorkspaceViewModel` to the `Workspace folder`.

Code behind of __workspace view__

````cs
namespace TestModule.Workspace
{
    /// <summary>
    /// Interaction logic for TestWorkspaceView.xaml
    /// </summary>
    public partial class TestWorkspaceView
    {
        public TestWorkspaceView()
        {
            InitializeComponent();
        }
    }
}
````

XAML of __workspace view__

````xml
<UserControl x:Class="TestModule.Workspace.TestWorkspaceView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:local="clr-namespace:TestLibrary.Workspace"
             xmlns:cal="http://www.caliburnproject.org"
             mc:Ignorable="d"
             d:DesignHeight="300" d:DesignWidth="300">
    <Grid>
        <TextBlock Text="My first workspace!" />
    </Grid>
</UserControl>

````

The __workspace view model__

````cs
using Marvin.ClientFramework;
using Marvin.Container;

namespace TestModule.Workspace
{
    [Plugin(LifeCycle.Singleton, typeof(IModuleWorkspace), Name = nameof(TestWorkspaceViewModel))]
    public class TestWorkspaceViewModel : ModuleWorkspace
    {
    }
}
````

Reference now the `TestModule project` to the `TestClient project`. This is needed so that the TestModule assembly is copied to TestClient output folder.

Start your application and be amazed.

## Custom shell

For our first example we went the easy way and used the available `PhoenixShell`. But in some days you'll need to implement your own shell. This chapter will give you a short introduction on how a shell is implemented.

Create a new solution named `MyOwnShell` as described in the _Prerequisites chapter_.

Now add an empty module as described in _Sample Module chapter_.

You need three files to build the simplest __Shell__ ever:

- MyOwnRegionController.cs: A shell can contain regions that contents are loaded from different plugins. In our sample we keep it simple. So no regions needed.
- MyOwnShellViewModel.cs: The view model of our shell.
- MyOwnShellView.xaml/cs: Our view.

The __RegionController__

````cs
using System.Collections.Generic;
using Marvin.ClientFramework.Shell;
using Marvin.Container;

namespace TestModule
{
    public class MyOwnRegionController : ShellRegionController
    {
        protected override void BuildConfig(ShellRegionConfig config)
        {
            config.Regions = new List<RegionConfig>();
        }

        protected override void LoadPlugins(IContainer container)
        {
        }
    }
}
````

The __shell view model__

````cs
using Marvin.ClientFramework;
using Marvin.ClientFramework.Shell;

namespace TestModule
{
    [ModuleShell("MyOwnShell")]
    public class MyOwnShellViewModel : ShellViewModelBase
    {
        protected override IShellRegionController CreateController()
        {
            return new MyOwnRegionController();
        }
    }
}
````

Code behind of __shell view__

````cs
namespace TestModule
{
    /// <summary>
    /// Interaction logic for MyOwnShellView.xaml
    /// </summary>
    public partial class MyOwnShellView
    {
        public MyOwnShellView()
        {
            InitializeComponent();
        }
    }
}
````

XAML of __shell view__

````xml
<UserControl x:Class="TestModule.MyOwnShellView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:TestModule"
             mc:Ignorable="d" 
             d:DesignHeight="300" d:DesignWidth="300">
    <Grid>
        <TextBlock Text="My first own Shell!" />
    </Grid>
</UserControl>
````

Finished. Start thze application and you see your own implemented shell.