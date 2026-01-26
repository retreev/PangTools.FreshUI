using System.Xml.Serialization;

namespace PangTools.FreshUI.Serialization.Models;

public class Parameter
{
    [XmlAttribute("name")]
    public string Name;
    
    [XmlAttribute("var")]
    public string Value;
}