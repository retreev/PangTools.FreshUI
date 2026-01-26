namespace PangTools.FreshUI.Serialization.Models;

using System.Xml.Serialization;

[XmlRoot("resource")]
public class Resource
{
    [XmlAttribute("count")]
    public int Count;

    [XmlElementAttribute("element")]
    public List<Element> Elements;
}