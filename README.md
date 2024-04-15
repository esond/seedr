# seedr

A toy project to experiment with .NET MAUI and ReactiveUI.

## Contents

- [Overview](#overview)
- [Build](#build)

## Overview

This application imitates the operation of an agricultural air seeder in an
extremely simplified way.

There are two main components to this application: the monitor software that
runs inside the cab of the tractor, and the controller that runs on the air
seeder itself. The monitor application sends commands to the controller over
a gRPC connection (this would be something much more low-level in the real
world), adjusting settings of the air seeder implement.

To see the application in action, run both the `Seedr.Monitor` projects and
`Seedr.Controller.Service` projects, and adjust the available settings. Observe
the console logs of the controller application to view messages being received
and processed.

## Build

This project uses the [NUKE](https://nuke.build/) build tool. You can invoke a
build in the following ways:

### Command line

#### Script

There are PowerShell, cmd, and bash scripts for invoking builds and build
targets. To select a build target, specify it either as an argument or with the
`--target` switch. For example:

```powershell
./build.ps1 # Run the default build
./build.ps1 test # Run the 'test' target
./build.ps1 --target test # Run the 'test' target
./build.ps1 --help # Lists available targets
```

#### NUKE global tool

The .NET tools manifest for this solution includes the [NUKE global
tool](https://nuke.build/docs/getting-started/setup.html). You can install local
tools with this command:

```powershell
dotnet tool restore
```

Then, targets can be run from a shell with the `nuke` command. This tool is
very convenient as it enables tab completion of target names. For example:

```powershell
nuke build
nuke test
nuke integration-test
# etc.
```

### Console app

NUKE builds are pure C# console apps. So, to run a build you can just run the
`_build` project from your IDE, just as you would any other executable.

### IDE plugins

These support plugins can invoke builds, and make working with NUKE easier:

- [Microsoft VisualStudio](https://nuke.build/visualstudio)
- [Microsoft VSCode](https://nuke.build/vscode)
- [JetBrains ReSharper](https://nuke.build/resharper)
- [JetBrains Rider](https://nuke.build/rider)
