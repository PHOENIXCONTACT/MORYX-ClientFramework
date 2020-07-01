---
uid: Themes
---
# Themes

The ClientFramework's [PhoenixShell](xref:PhoenixShell) has its own theme defined that is automatically used when your UI is starting. Sometimes it is neccessary to override a style of a standard Eddie control or the complete style set.
The ClientFramework does not follow own rules how styles are overridden the WPF way is used. See how you can override those styles in your ClientFramework application.

## File organization of styles

If you want to override styles you should add a subfolder named `Theme` to your project. There you can organize all styles you want to override. Please use only one resource file per Type. For easier use you should create a global resource file (`Generic.xaml`) that references all other resource files.

## How to override styles application wide

Imagine that you have to develop a client for a customer that follows his own rules of styling. So you have to override all neccessary styles but how to use them? Let's have a look on the following example.

````cs
using System;
using Moryx.ClientFramework.Kernel;

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
            hol.AddStyleExtension(new Uri("pack://application:,,,/MyApplication;component/Theme/Generic.xaml", UriKind.RelativeOrAbsolute));
            hol.Start();
        }
    }
}
````

As you can see a call to `AddStyleExtension` is added which marks the commited `Uri` as override style.

## How to override styles workspace wide

There might are requirements that styling of a single module should be different to the rest of the application. This is also possible. Create a folder structure described above and add a reference to the concered workspace view.

````xml
<UserControl x:Class="TestModule.Workspace.WorkspaceView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:TestLibrary.Workspace"
             xmlns:cal="http://www.caliburnproject.org"
             mc:Ignorable="d" d:DesignHeight="300" d:DesignWidth="300">
    <UserControl.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="../Theme/Generic.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </UserControl.Resources>
    <Grid>
        ...
    </Grid>
</UserControl>
````

All views inside the workspace will apply the styles defined in your `Generic.xaml`.