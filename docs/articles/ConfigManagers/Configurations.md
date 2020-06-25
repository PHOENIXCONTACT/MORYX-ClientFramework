---
uid: Configurations
---
# Configurations

The configuration management is based on the *MORYX Toolkit* with the platforms configuration management. So they work like in every other platform (e.g. runtime)
To handle the frameworks configurations a configurator can be started with the config runmodes

## Config RunMode

This *RunMode* loads the configurator out of the *Settings* folder from the framework. If you don't want to have this configurator, you can delete the content of this folder. Otherwise you only can deactivate the configuratior in the AppConfig.

The configurator can be opened with keyboard key *CTRL*. You should press the key while the framework is loading. You can also add the ´--configurator´ command line argument. Don't forget to set the RunMode back to your favorite.

## Configurator

### Starting the configurator

You can start the configurator of the framework with the command line argument ´--configurator´. Optionally use the *CTRL* key at startup. (Press *CTRL* and then click on the heart of lead). Don't forget to set the *RunMode* back to your favorite.

## Framework Config Files

### AppConfig

The *AppConfig* provides several options to customize the application.

* **Name:** This name will be shown in the window title
* **Application:** The application name will be used as identification. Currently the application name will only be used by the user management
* **Mode:** Selected RunMode. See [RunModes](xref:RunModes) section.
* **Config CTRL:** Indicates wether the client will start the configurator when pressing *CTRL* or not.

### ProxyConfig

The *ProxyConfig* provides several options to configure proxy usage in several network consuming components (WcfClientFactory, AssemblyDownloader, ...).

* **Enabled:** Enables the whole proxy usage
* **Use Default:** Will be used to configure if the framework should use the default proxy settings from the system
* **Address:** The address of the proxy server
* **Port:** The port of the proxy server

### RuntimeConfig

The *RuntimeConfig* provides several options to configure the connection to the moryx runtime.

* **Hostname:** Where the server will host several web services
* **Port:** Port of the hosted services
* **ClientId:** Id of the client to subscribe into webservices
