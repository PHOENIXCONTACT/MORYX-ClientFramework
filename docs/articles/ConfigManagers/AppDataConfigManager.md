---
uid: AppDataConfigManager
---
# AppDataConfigManager & loading custom user specific configurations

The ClientFramework supports user specific configurations which are stored in `%APPDATA%\Phoenix Contact\__AppName__` folder. The [AppDataConfigManager](xref:Moryx.ClientFramework.Kernel) is responsible to take care about loading and saving of this configurations. Note that you don't need to do any initialization steps because this is done automatically.

## How can I make a configuration user specific

First, of course, you need to create a configuration class.

````cs
[DataContract]
public class ZombieConfig : ConfigBase
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public DateTime FirstAppearanceInFamousTVShow  { get; set; }

    [DataMember]
    public DateTime? DateOfSalvation  { get; set; }

}
````

Now you can access the instance of your configuration through the `AppDataConfigManager`.

````cs
[ClientModule("Wcf Viewer")]
public class ModuleController : WorkspaceModuleBase<ModuleConfig>
{
    private IAppDataConfigManager _appDataConfigManager;

    protected override void OnInitialize()
    {
        // load configuration
        var config = _appDataConfigManager.GetConfiguration<ZombieConfig>();
        ...
    }
}
````

That's it. The config is saved automaticallay when the application is closed. If you want to save the configuration manually just call `SaveConfiguration(config)`.

## How can I apply default configuration

That's rather easy as well. Just add a folder named `AppDataDefaults` to your Config-Directory and copy your default user specific configurations to it. These files will be copied to the `%APPDATA%` folder when the application starts. Existing files won't be touched. Promise.