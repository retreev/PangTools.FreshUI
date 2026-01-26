using System.Xml.Serialization;

namespace PangTools.FreshUI.Serialization.Models;

public class Layer
{
    [XmlAttribute("type")]
    public int Type;

    [XmlAttribute("height")]
    public int Height;

    [XmlAttribute("pos")]
    public int Position;
}