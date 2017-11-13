Tipps and Tricks {#client-tippsTicks}
========

# Using own executable
In some cases it can be required that you want to have your own executable.
* Own icon
* Other start lifecycle
* ...

Just create a new WPF application and delete everything from it. 
Create a Program.cs and add a normal main method.
Reference this Application with the framework HeartOfLead.exe.
Attribute this Main() with the *STAThreadAttribute* and create the ClientKernal:

````cs
[STAThread]
public static void Main(string[] args)
{
    new ClientKernel(args).Start();
}
````

Build the new executable to the ClientRuntime folder. It needs access to all folders and binarys.
