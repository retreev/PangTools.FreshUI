using System.Xml.Serialization;

namespace PangTools.FreshUI.Serialization.Models;

public class Frame
{
    [XmlAttribute("filename")]
    public string FileName;
}