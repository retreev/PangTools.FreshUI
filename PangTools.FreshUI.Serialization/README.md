# PangTools.FreshUI.Serialization

This library contains models/factories to serialize Pangya's UI XML
into proper data structures.

> [!NOTE]  
> Some of Pangya's UI XML files might be malformed. The XML (de)serializer
> used by the game (TinyXML) **is not spec compliant** and thus simply ignored
> errorneous syntax. C#'s XML parser does not do that.

## Requirements

To handle Korean XML files using C#, you will need `System.Text.Encoding.CodePages`.

Install that using:

```shell
$ dotnet package add System.Text.Encoding.CodePages
```

## Usage

```csharp
using System.Xml.Serialization;
using PangTools.FreshUI.Serialization.Models;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

// Define XmlSerializer with the FreshUI Resource node
XmlSerializer resourceSerializer = new(typeof(Resource));

// Load target XML file as stream
FileStream resourceFile = new FileStream("filepath-to-ui.xml", FileMode.Open);

// Deserialize filestream into Resource class
Resource resource = (Resource)resourceSerializer.Deserialize(resourceFile);
```