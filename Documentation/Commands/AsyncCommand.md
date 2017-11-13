AsyncCommand {#client-asyncCommand}
========

The async command is a special *ICommand* implementation for the async and await pattern. It executes the given methods asynchrounos with the basic command functionallity: CanExecute, Key and Touch bindings.

For this description it is nessessary that you understand the [task based async await](https://msdn.microsoft.com/de-de/library/hh191443.aspx) pattern: and the [ICommand](https://msdn.microsoft.com/de-de/library/system.windows.input.icommand(v=vs.110).aspx).

# API

## Non public Execute
This method will be called by the WPF and is not visible in the AsyncCommand class.

## ExecuteAsnyc(object parameters)
Will execute the given method with an await. The returned task will be capsulated in the *Execution* property.

## RaiseCanExecuteChanged
The asnyc command is not bounded by default to the .NET CommandManager because the command manager will raise the CanExecuteChanged event really often (every mouse key, keyboard key or ui change). 
So in case of complex CanExecute methods we decided to execute the method manually. The event will also be raised if the task will start and if it will finish.

If you still want to use the .NET CommandManager you can just set it as a parameter when you instantiate the Command. 
After that you don´t have to call the RaiseCanExecuteChanged method but remember that a complex CanExecute method can lead to a performance leak in this case.

# Usage
The command should be declared in the view model (initialize or constructor):
````cs
LongCmd = new AsyncCommand(LongMethod, CanExecuteLongMethod);
````

The property can be binded to an command property in xaml. The following example is the button:
````xml
<EddieButton Command="{Binding LongCmd}">Long Async Task</EddieButton>
````
As you can see, the command is not binded to any other property. You do not need to bind it to "IsEnabled" or something else. The button implementation will whatch the events automatically.

You also can add keybindings to the user control to execute the command by key or any other gestures:

````xml
<UserControl x:Class="Marvin.Jobs.UI.Viewer.TestJobCreatorWorkspaceView"
             ...>
    <UserControl.InputBindings>
        <KeyBinding Command="{Binding CancelCmd}" Key="Escape" />
        <KeyBinding Command="{Binding CreateCmd}" Key="Enter" Modifiers="Control"/>
    </UserControl.InputBindings>
...
</UserControl>
````

Within the given delegate for the execution you can access all the needed information from the view model. Or access task based wcf services.
````cs
public async Task LongMethod()
{
    var task = Task.Run(delegate
    {
        Thread.Sleep(5000);
    });

    await task;
}
````

The CanExecuted can be implemented as known from other commands:
````cs
private bool CanExecuteLongMethod(object parameters)
{
    return LongActivated;
}
````