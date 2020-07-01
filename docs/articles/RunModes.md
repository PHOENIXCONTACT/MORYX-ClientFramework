---
uid: RunModes
---
# RunModes

The run modes in the client framework will handle several start behaviors of the process how the framework will load their modules and plugins.

## ConfigRunMode

The ConfigRunMode will load the configurator provided by the framework. The mode will only load *Modules* and *Shells*  which are related to configuration.

## LocalRunMode

The *LocalRunMode* loads the *Modules*, *Shells* and other which are located in the current AppDomain.