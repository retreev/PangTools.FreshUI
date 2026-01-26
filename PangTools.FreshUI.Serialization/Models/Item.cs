using System.Xml.Serialization;

namespace PangTools.FreshUI.Serialization.Models;

public class Item
{
    [XmlAttribute("type")]
    public string Type;
    
    [XmlAttribute("flag")]
    public int Flag;
    
    [XmlAttribute("name")]
    public string Name;
    
    [XmlAttribute("caption")]
    public string Caption;
    
    [XmlAttribute("pos")]
    public string Position;
    
    [XmlAttribute("rect")]
    public string Rectangle;

    [XmlElementAttribute("param")]
    public List<Parameter> Parameters;
}