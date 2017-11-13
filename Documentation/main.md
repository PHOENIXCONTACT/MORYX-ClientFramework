ClientFramework {#clientFrameworkMain}
========

The ClientFramework will be developed for synergy effects at Phoenix Contact especially in the department //Manufacturing Solutions//. 
Therefore everyone can develop modules for the client, which then will be used by other colleagues or project teams.

## Motivation

* Module based client
* Consistent Software (Design, Codebase)
* Client-Server Architecture

## Framework features
The **ClientFramework** is an application framework for the client part of a client-server application. It is based on the concept of modular software development and the service pattern. The frameworks main features are listed below:

The most of the provided features are inherited by the [Platform](@ref platformMain)
* [Logging](@ref platform-logging)
* Configuration Management
* Attributes and Base Classes
* [Wcf Connections](@ref platform-wcf) 

The client framework will also provide their own WPF based features:

* @subpage client-configurations
* @subpage client-runModes
* Modules
* Frame (Shell)
* Converter, Extensions, Markup-Extensions
* @subpage client-modalDialogs
* @subpage controls4Industry
* @subpage client-screensConductors
* @subpage client-commands
* @subpage client-facades

## Deployment and Contribution
The framework will be deployed with the MarvinPlatform development.

## Tools & Templates
* @subpage client-tippsTicks
* @subpage client-termsAndFolderStructure

## Tutorials and Conventions
The most decisions are made by the GUI Taskforce

* Getting Started
* @subpage client-caliburnConventions
* Model View ViewModel
* TypesOfClients