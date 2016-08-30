using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ScoreCollection")]
public class ScoreContainer
{
    [XmlArray("Levels"), XmlArrayItem("Scores")]
    public List<ScoreData> LevelScores = new List<ScoreData>();
}
