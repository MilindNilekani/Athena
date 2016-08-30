using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class ScoreData
{
    [XmlAttribute("LevelNumber")]
    public int levelNumber;

    [XmlElement("OptimatalPathMoves")]
    public int optimalpath_moves;

    [XmlElement("LevelStatus")]
    public bool level_status;

    [XmlElement("2ndStar")]
    public bool star2_status;

    [XmlElement("TripletsUnlock")]
    public bool unlock_triplets;

    [XmlElement("HighScore")]
    public int highScore;

    [XmlElement("Star1")]
    public int star1;

    [XmlElement("Star2")]
    public int star2;

    [XmlElement("Star3")]
    public int star3;
    


}

