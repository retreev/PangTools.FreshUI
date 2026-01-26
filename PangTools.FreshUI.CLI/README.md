# PangTools.FreshUI.CLI

This application is a CLI designed to quickly allow rendering Pangya XML UI
definitions to PNG images.

## Usage

```shell
$ .\PangTools.FreshUI.CLI.exe --help                                                                    
Description:
  Preview Pangya UI files

Usage:
  PangTools.FreshUI.CLI [options]

Options:
  --data-directory <data-directory>  The Pangya 'data' asset directory path
  --file <file>                      Name of the UI file to preview
  --button-state <button-state>      Interaction state of buttons [default: normal]
  --debug                            Render borders around certain elements
  -?, -h, --help                     Show help and usage information
  --version                          Show version information
```

## Building

To build the CLI (and any other project in this repository) you need the [.NET SDK](https://dotnet.microsoft.com/en-us/download) (in version 8, at least).

Once that is installed the project can be built using following commands:

```shell
$ dotnet restore
$ dotnet build
```

