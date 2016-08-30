using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class LevelData
{
    [XmlAttribute("LevelNumber")]
    public int levelNumber;

    [XmlElement("Texture_Index")]
    public int text_ind;

    [XmlArray("GridMap"), XmlArrayItem("GMElements")]
    public int[] gridmap = new int[200];

    [XmlElement("GMx")]
    public int gmx;

    [XmlElement("GMy")]
    public int gmy;

    [XmlArray("ScoreMap"), XmlArrayItem("SMElements")]
    public int[] scoremap = new int[200];

    [XmlArray("PlayerInitPosition"), XmlArrayItem("PIPElements")]
    public int[] player_initpos = new int[2];

    [XmlElement("EnemyNumber")]
    public int enemy_num;

    [XmlArray("Enemy_InitPosition")]
    public int[] enemy_initpos = new int[100];


}
