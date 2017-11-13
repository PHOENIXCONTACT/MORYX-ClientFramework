Facades {#client-facades}
========

# General
For the general concept of module facades, you should read the server concept carefully: [FacadeGuide](@ref runtime-facadeGuide).

# Client Implementation
On the client side, the modules have no lifecycle as on the server (Started, Stopped, Running, Warning, ...). So the facade implementation is more easy and simpler. 
On client modules, the module controller will handle the facade management. The client module can export several facades (interfaces between the modules). 
The interfaces are defined in the [ClientModuleAttribute](@ref Marvin.ClientFramework.ClientModuleAttribute).

````cs
[ClientModule("Playground", typeof(IPlayGroundFacade))]
public class ModuleController : WorkspaceModuleBase<ModuleConfig>, IPlayGroundFacade
{
    ...
    public void SayHello()
    {
       MessageBox.Show("Hello From PlaygroundModule");
    }
}
````

The exported interface will be registered in the global container and can be imported in every module.
````cs
[ClientModule("DialogManagerTest")]
public class ModuleController : WorkspaceModuleBase<ModuleConfig>
{
    public IPlayGroundFacade PlayGroundFacade { get; set; }

    protected override void OnInitialize()
    {
        PlayGroundFacade.SayHello();
    }
}
````

It is very important that on the client module, the same rules are existing as on the server side (no exporting of internal components, naming, ...).
