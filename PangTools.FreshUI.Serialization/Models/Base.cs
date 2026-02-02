using System.Xml.Serialization;

namespace PangTools.FreshUI.Serialization.Models;

public class Base
{
    [XmlAttribute("width")] 
    public int Width;
    
    [XmlAttribute("height")]
    public int Height;
    
    [XmlAttribute("color")]
    public string? Color;
    
    [XmlAttribute("background")]
    public string? Background;
}