using System.CommandLine;
using System.Text;
using System.Xml.Serialization;
using PangTools.FreshUI.Renderer;
using PangTools.FreshUI.Serialization.DTO;
using PangTools.FreshUI.Serialization.Factories;
using SixLabors.ImageSharp;
using PangTools.FreshUI.Serialization.Models;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

Option<DirectoryInfo> dataDirectoryOption = new("--data-directory")
{
    Description = "The Pangya 'data' asset directory path"
};

Option<string> fileOption = new("--file")
{
    Description = "Name of the UI file to preview"
};

Option<string> buttonStateOption = new("--button-state")
{
    Description = "Interaction state of buttons",
    DefaultValueFactory = (result => "normal")
};

Option<bool> debugOption = new("--debug")
{
    Description = "Render borders around certain elements",
    DefaultValueFactory = (result => false)
};

Option<string> outputDirectoryOption = new("--output-directory")
{
    Description = "Specify a folder to output rendered images to",
    DefaultValueFactory = (result => "")
};

RootCommand rootCommand = new("Preview Pangya UI files");
rootCommand.Options.Add(dataDirectoryOption);
rootCommand.Options.Add(fileOption);
rootCommand.Options.Add(buttonStateOption);
rootCommand.Options.Add(debugOption);
rootCommand.Options.Add(outputDirectoryOption);

rootCommand.SetAction(parseResult =>
{
    string outputDirectory = parseResult.GetValue(outputDirectoryOption);
    string fileName = parseResult.GetValue(fileOption);
    DirectoryInfo dataDirectory = parseResult.GetValue(dataDirectoryOption);
    string buttonState = parseResult.GetValue(buttonStateOption);
    bool debug = parseResult.GetValue(debugOption);
    
    string[] files = Directory.GetFiles(dataDirectory.FullName, "*.*", SearchOption.AllDirectories);
    FileAtlas fileAtlas = new(files);
    
    string? uiFile = fileAtlas.GetFullPath(fileName);
    string? framesFile = fileAtlas.GetFullPath("frames.xml");

    if (uiFile == null)
    {
        return 0;
    }
    
    XmlSerializer resourceSerializer = new(typeof(Resource));

    FileStream elementFile = new FileStream(uiFile, FileMode.Open);
    FileStream frameFile = new FileStream(framesFile, FileMode.Open);

    Resource resource = (Resource)resourceSerializer.Deserialize(elementFile);
    Resource frameResource = (Resource)resourceSerializer.Deserialize(frameFile);

    FrameInfoAtlas frameInfoAtlas =
        FrameInfoFactory.BuildFrameInfo(frameResource.Elements);

    ImageSharpRenderer renderer = new(fileAtlas, frameInfoAtlas, debug);
    Dictionary<string, Image> images = renderer.RenderAllElements(resource, buttonState);

    if (outputDirectory != "")
    {
        Directory.CreateDirectory(outputDirectory);    
    }
    
    foreach(KeyValuePair<string, Image> entry in images)
    {
        entry.Value.Save(outputDirectory + entry.Key);
    }
    
    return 0;
});

ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();

