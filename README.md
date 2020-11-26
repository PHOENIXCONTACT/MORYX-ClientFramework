<p align="center">
    <img src="docs/resources/MORYX_logo.svg" alt="MORYX Logo" width="300px" />
</p>

<p align="center">
    <a href="https://github.com/PHOENIXCONTACT/MORYX-ClientFramework/workflows">
        <img src="https://github.com/PHOENIXCONTACT/MORYX-ClientFramework/workflows/CI/badge.svg" alt="CI">
    </a>
    <a href="https://codecov.io/gh/PHOENIXCONTACT/MORYX-ClientFramework/coverage.svg?branch=dev">
        <img alt="Coverage" src="https://codecov.io/gh/PHOENIXCONTACT/MORYX-ClientFramework/coverage.svg?branch=dev" />
    </a>
    <a href="https://github.com/PHOENIXCONTACT/MORYX-ClientFramework/blob/dev/LICENSE">
        <img src="https://img.shields.io/github/license/PHOENIXCONTACT/MORYX-ClientFramework" alt="License">
    </a>
    <a href="https://gitter.im/PHOENIXCONTACT/MORYX?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge">
        <img src="https://badges.gitter.im/PHOENIXCONTACT/MORYX.svg" alt="Gitter">
    </a>
</p>

# MORYX ClientFramework

The **MORYX ClientFramework** is the foundation for MORYX WPF desktop frontends. It provides the same modularity as the MORYX platform and organizes modules in isolated workspaces. Each client deployment can have its own set of modules and configuration.  

**Links**
- [Package Feed](https://www.myget.org/feed/Packages/moryx)
- [MORYX Template](https://github.com/PHOENIXCONTACT/MORYX-Template)
- [MORYX Platform](https://github.com/PHOENIXCONTACT/MORYX-Platform)
- [MORYX Maintenance](https://github.com/PHOENIXCONTACT/MORYX-MaintenanceWeb)
- [MORYX Abstraction Layer](https://github.com/PHOENIXCONTACT/MORYX-AbstractionLayer)

## Getting started

If you want to start developing with or for MORYX, the easiest way is our [template repository](https://github.com/PHOENIXCONTACT/MORYX-Template). It comes with two empty solutions, the necessary package feeds and preinstalled empty MORYX runtime. Add projects and packages to backend and frontend solutions depending on your specific requirements. Install stable releases via Nuget; development releases are available via MyGet.

| Package Name | Release (NuGet) | CI (MyGet) |
|--------------|-----------------|------------|
| `Moryx.WpfToolkit` | [![NuGet](https://img.shields.io/nuget/v/Moryx.WpfToolkit.svg)](https://www.nuget.org/packages/Moryx.WpfToolkit/) | [![MyGet](https://img.shields.io/myget/moryx/vpre/Moryx.WpfToolkit)](https://www.myget.org/feed/moryx/package/nuget/Moryx.WpfToolkit) |
| `Moryx.Controls` | [![NuGet](https://img.shields.io/nuget/v/Moryx.Controls.svg)](https://www.nuget.org/packages/Moryx.Controls/) | [![MyGet](https://img.shields.io/myget/moryx/vpre/Moryx.Controls)](https://www.myget.org/feed/moryx/package/nuget/Moryx.Controls) |
| `Moryx.ClientFramework` | [![NuGet](https://img.shields.io/nuget/v/Moryx.ClientFramework.svg)](https://www.nuget.org/packages/Moryx.ClientFramework/) | [![MyGet](https://img.shields.io/myget/moryx/vpre/Moryx.ClientFramework)](https://www.myget.org/feed/moryx/package/nuget/Moryx.ClientFramework) |
| `Moryx.ClientFramework.Configurator` | [![NuGet](https://img.shields.io/nuget/v/Moryx.ClientFramework.Configurator.svg)](https://www.nuget.org/packages/Moryx.ClientFramework.Configurator/) | [![MyGet](https://img.shields.io/myget/moryx/vpre/Moryx.ClientFramework.Configurator)](https://www.myget.org/feed/moryx/package/nuget/Moryx.ClientFramework.Configurator) |
| `Moryx.ClientFramework.Kernel` | [![NuGet](https://img.shields.io/nuget/v/Moryx.ClientFramework.Kernel.svg)](https://www.nuget.org/packages/Moryx.ClientFramework.Kernel/) | [![MyGet](https://img.shields.io/myget/moryx/vpre/Moryx.ClientFramework.Kernel)](https://www.myget.org/feed/moryx/package/nuget/Moryx.ClientFramework.Kernel) |
| `Moryx.ClientFramework.SimpleShell` | [![NuGet](https://img.shields.io/nuget/v/Moryx.ClientFramework.SimpleShell.svg)](https://www.nuget.org/packages/Moryx.ClientFramework.SimpleShell/) | [![MyGet](https://img.shields.io/myget/moryx/vpre/Moryx.ClientFramework.SimpleShell)](https://www.myget.org/feed/moryx/package/nuget/Moryx.ClientFramework.SimpleShell) |
| `Moryx.Tools.WcfClient.UI.Viewer` | [![NuGet](https://img.shields.io/nuget/v/Moryx.Tools.WcfClient.UI.Viewer.svg)](https://www.nuget.org/packages/Moryx.Tools.WcfClient.UI.Viewer/) | [![MyGet](https://img.shields.io/myget/moryx/vpre/Moryx.Tools.WcfClient.UI.Viewer)](https://www.myget.org/feed/moryx/package/nuget/Moryx.Tools.WcfClient.UI.Viewer) |
