Terms and Folder Structure {#client-termsAndFolderStructure}
========

# Folder Structure

**ClientFramework** <br />
├───Bundles-> Bundle libraries <br />
├───Configs -> Config Files <br />
├───Core -> All framework libraries <br />
├───Crashlogs -> Crashlogs from framework <br />
├───Libraries -> External libraries <br />
├───Modules -> Modules which should be available in ui <br />
├───Plugins -> ModulePlugins customizing modules <br />
└───Settings -> ConfigRunMode <br />

# Terms
| Term | Description |
|------|-------------|
| RootView | invisible technical component used to interact with the visual tree |
| LoaderView | Used to visualize the loading process |
| Configurator | Configure Framework when no configuration available |
| Error Messages | Show errors while loading |
| Shell | Visual container for the loaded modules (tab control, content control with buttons, ...), Notification and MessageBox overlays, Optional Messagebar  |