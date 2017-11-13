RunModes {#client-runModes}
========

The run modes in the client framework will handle several start behaviors of the process how the framework will load their modules and plugins.

## ConfigRunMode
The ConfigRunMode will load the configurator provided by the framework. The mode will only load *Modules* and *Shells*  which are located in the *Settings*-Folder.

## RemoteRunMode
This *RunMode* loads the modules configuration from a running runtime. The UserManagement and AssemblyProvider should be configured. Don't forget to configure this endpoints in the ClientFramework configurator.

## LocalRunMode
The *LocalRunMode* loads the *Modules*, *Shells* and other which are located in the *Modules*-Folder.