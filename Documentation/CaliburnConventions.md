Caliiburn Conventions {#client-caliburnConventions}
========

# Naming Conventions
Guidelines for usage of Caliburn.Micro
- Usage of caliburn micro base classes - wherever possible
- Do not use button name as event. Use the following notation to see the real event which will be wired by caliburn:

````xml
<Button Content="Remove"
        cal:Message.Attach="[Event Click] = [Action Remove($dataContext)]" />
````

- for properties use the good old binding

````xml
<TextBlock Text="{Binding MyProperty}" />
````

- Per default caliburn view locator is used
  - If required castle may be extended to find a custom view for a view model
- Use the default caliburn naming conventions (TestView and TestViewModel)

# IntelliSense
For intellisense add the following code to your Window/UserControl

````xml
<UserControl x:Class="..."
             ...
             mc:Ignorable="d"
             d:DataContext="{d:DesignInstance MyViewModel}">
        [...]
</UserControl>
````
