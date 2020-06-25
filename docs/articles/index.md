---
uid: ClientFramework
---
# ClientFramework

The ClientFramework will be developed for synergy effects at Phoenix Contact especially in the department //Manufacturing Solutions//. Therefore everyone can develop modules for the client, which then will be used by other colleagues or project teams.

## Motivation

* Module based client
* Consistent Software (Design, Codebase)
* Client-Server Architecture

## Framework features

The **ClientFramework** is an application framework for the client part of a client-server application. It is based on the concept of modular software development and the service pattern. The frameworks main features are listed below:

The most of the provided features are inherited by the *MoryxPlatform*

* Logging
* Configuration Management
* Attributes and Base Classes
* Wcf Connections

The client framework will also provide their own WPF based features:

* [Configurations](xref:Configurations)
* [RunModes](xref:RunModes)
* Modules
* Frame (Shell)
* Converter, Extensions, Markup-Extensions
* [Modal dialogs](xref:ModalDialogs)
* [Default shell](xref:PhoenixShell)
* [Screens and Conductors](xref:ScreensAndConductors)
* [Commands](xref:Commands)
* [Facades](xref:Facades)

## Tools & Templates

* [Tipps & Tricks](xref:TippsAndTricks)
* [Terms & Folder structure](xref:TermsAndFolderStructure)

## Tutorials and Conventions

The most decisions are made by the GUI Taskforce

* [Getting Started](xref:Tutorials.Index)
* [Caliburn conventions](xref:CaliburnConventions)
* Model View ViewModel
* TypesOfClients