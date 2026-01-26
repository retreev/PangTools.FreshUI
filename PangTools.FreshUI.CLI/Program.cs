using System.CommandLine;
using System.Text;
using System.Xml.Serialization;
using PangTools.FreshUI.Renderer;
using PangTools.FreshUI.Serialization.DTO;
using PangTools.FreshUI.Serialization.Factories;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using PangTools.FreshUI.Serialization.Models;
using SixLabors.ImageSharp.Processing;

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

RootCommand rootCommand = new("Preview Pangya UI files");
rootCommand.Options.Add(dataDirectoryOption);
rootCommand.Options.Add(fileOption);
rootCommand.Options.Add(buttonStateOption);
rootCommand.Options.Add(debugOption);

rootCommand.SetAction(parseResult =>
{
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

    Element[] elements = resource.Elements.Where(item => item.Type.Equals("FORM")).ToArray();

    foreach (Element element in elements)
    {
        Item[] areaItems = element.Items.Where(item => item.Type.Equals("AREA")).ToArray();
        Item[] buttonItems = element.Items.Where(item => item.Type.Equals("BUTTON")).ToArray();

        using (Image image = new Image<Rgba32>(800, 600, Color.Transparent))
        {
            image.Mutate(ctx =>
            {
                ctx.DrawElement(element, fileAtlas, frameInfoAtlas, debug);

                foreach (Item area in areaItems)
                {
                    ctx.DrawArea(area, fileAtlas, debug);
                }

                foreach (Item button in buttonItems)
                {
                    ctx.DrawButton(button, buttonState, fileAtlas);
                }
            });
        
            image.Save($"{element.Name}_{buttonState}.png");
        }        
    }
    
    return 0;
});

ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();

