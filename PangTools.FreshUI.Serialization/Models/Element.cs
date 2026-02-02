using System.Xml.Serialization;

namespace PangTools.FreshUI.Serialization.Models;

public class Element
{
    [XmlAttribute("type")]
    public string Type;
    
    [XmlAttribute("name")]
    public string Name;
    
    [XmlAttribute("size")]
    public string? Size;
    
    [XmlAttribute("resource")]
    public string? Resource;
    
    [XmlElementAttribute("item")]
    public List<Item>? Items;
    
    [XmlElementAttribute("layer")]
    public List<Layer>? Layers;
    
    [XmlElementAttribute("bfrm")]
    public Frame? BorderFrame;
    
    [XmlElementAttribute("sfrm")]
    public Frame? SquareFrame;
    
    [XmlElementAttribute("cfrm")]
    public Frame? ClearFrame;

    [XmlElementAttribute("base")] 
    public Base? Base;
}