# Fresh UI Renderer

This repository contains libraries and tooling to render Pangya's custom
UI markup to images.

"Fresh UI" is an assumption of the internal name used by Ntreev Soft. There's
a lot of references to this name, and the new UI system introduced in Season 8
is called "Refresh" (very obviously in files and class names).

## Repository Overview

### `PangTools.FreshUI.Serialization`

`PangTools.FreshUI.Serialization` contains models and utilities to serialize
the XML format into usable data structures.

### `PangTools.FreshUI.Renderer`

`PangTools.FreshUI.Renderer` contains rendering methods to render the different
UI elements into an `ImageSharp` canvas. Most methods are provided as extension
methods to the regular image processing context.

### `PangTools.FreshUI.CLI`

`PangTools.FreshUI.CLI` contains a CLI application that renders the XML definition
to images.

## License

This project is licensed under [aGPL v3](./LICENSE)