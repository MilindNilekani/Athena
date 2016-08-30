using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("LevelCollection")]
public class LevelContainer
{
    [XmlArray("Levels"), XmlArrayItem("Level")]
    public List<LevelData> Levels = new List<LevelData>();
}
