using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

public class SaveScores : MonoBehaviour
{
    public static SaveScores instance = null;
    private static string dataPath = string.Empty;
    private static ScoreContainer _scoreContainer = new ScoreContainer();
   
    public int ind = 0;
    

    //bool k1=false, k2=false, k3=false;

    public int robot_textind;
    public List<ScoreData> list = new List<ScoreData>();

    void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            dataPath = System.IO.Path.Combine(Application.persistentDataPath, "Score.xml");
        else
            dataPath = System.IO.Path.Combine(Application.dataPath, "Resources/Score.xml");
    }

    public int GetMostRecentUnlockedLevelID()
    {
        
        return ind;



    }

    public void SetPoints(int hs, int cl)
    {
        list = GetScores();
        if (hs > list[cl - 1].highScore)
        {
            list[cl - 1].highScore = hs;
        }
        UpdateScores(list);
    }

    public int PrintHighScore(int cl)
    {
        list = GetScores();
        return list[cl - 1].highScore;
    }

    

    public void CountIncrement_Last1(int i)
    {
        list = GetScores();
        list[i - 1].unlock_triplets = true;
        UpdateScores(list);
        if ((list[i - 1].unlock_triplets && list[i].unlock_triplets) || (list[i].unlock_triplets && list[i + 1].unlock_triplets) || (list[i - 1].unlock_triplets && list[i + 1].unlock_triplets))
        {

            //Debug.Log("Beaten level " + i + " Unlocked " + (i + 2) + " " + (i + 3) + " " + (i + 4));
            ChangeNextLevelStatus(i + 2);
            //ChangeNextLevelStatus(i + 3);
            //ChangeNextLevelStatus(i + 4);

        }
    }

    public void CountIncrement_Last2(int i)
    {
        list = GetScores();
        list[0].unlock_triplets = true;
        UpdateScores(list);
        if ((list[i - 2].unlock_triplets && list[i - 1].unlock_triplets) || (list[i - 1].unlock_triplets && list[i].unlock_triplets) || (list[i - 2].unlock_triplets && list[i].unlock_triplets))
        {

            //Debug.Log("Beaten level " + i + " Unlocked " + (i + 2) + " " + (i + 3) + " " + (i + 4));
            ChangeNextLevelStatus(i + 1);
            //ChangeNextLevelStatus(i + 3);
            //ChangeNextLevelStatus(i + 4);

        }
    }

    public void CountIncrement_Last3(int i)
    {
        list = GetScores();
        list[0].unlock_triplets = true;
        UpdateScores(list);
        if ((list[i - 3].unlock_triplets && list[i - 2].unlock_triplets) || (list[i - 2].unlock_triplets && list[i - 1].unlock_triplets) || (list[i - 3].unlock_triplets && list[i - 1].unlock_triplets))
        {

            //Debug.Log("Beaten level " + i + " Unlocked " + (i + 2) + " " + (i + 3) + " " + (i + 4));
            ChangeNextLevelStatus(i);
            //ChangeNextLevelStatus(i + 3);
            //ChangeNextLevelStatus(i + 4);

        }
    }



    public void CountIncrement1(int i)
    {
        list = GetScores();
        list[i - 1].unlock_triplets = true;
        UpdateScores(list);
        if ((list[i - 1].unlock_triplets && list[i].unlock_triplets) || (list[i].unlock_triplets && list[i + 1].unlock_triplets) || (list[i - 1].unlock_triplets && list[i + 1].unlock_triplets))
        {

            //Debug.Log("Beaten level " + i + " Unlocked " + (i + 2) + " " + (i + 3) + " " + (i + 4));
            ChangeNextLevelStatus(i + 2);
            if (LevelLoader.instance.current_level < 15)
            {
                ChangeNextLevelStatus(i + 3);
                ChangeNextLevelStatus(i + 4);
            }
        }
    }



    public void CountIncrement2(int i)
    {
        list = GetScores();
        list[i - 1].unlock_triplets = true;
        UpdateScores(list);
        if ((list[i - 2].unlock_triplets && list[i - 1].unlock_triplets) || (list[i - 1].unlock_triplets && list[i].unlock_triplets) || (list[i - 2].unlock_triplets && list[i].unlock_triplets))
        {

            //Debug.Log("Beaten level " + i + " Unlocked " + (i + 2) + " " + (i + 3) + " " + (i + 4));
            ChangeNextLevelStatus(i + 1);
            if (LevelLoader.instance.current_level < 15)
            {
                ChangeNextLevelStatus(i + 2);
                ChangeNextLevelStatus(i + 3);
            }
        }
    }
    public void CountIncrement3(int i)
    {
        list = GetScores();
        list[i - 1].unlock_triplets = true;
        UpdateScores(list);
        if ((list[i - 3].unlock_triplets && list[i - 2].unlock_triplets) || (list[i - 2].unlock_triplets && list[i - 1].unlock_triplets) || (list[i - 3].unlock_triplets && list[i - 1].unlock_triplets))
        {

            //Debug.Log("Beaten level " + i + " Unlocked " + (i + 2) + " " + (i + 3) + " " + (i + 4));
            ChangeNextLevelStatus(i);
            if (LevelLoader.instance.current_level < 15)
            {
                ChangeNextLevelStatus(i + 1);
                ChangeNextLevelStatus(i + 2);
            }
        }
    }



    public void SetJustPlayedLevel(int i)
    {
        ind = i;
    }


    public void UnlockAllLevels()
    {
        list = GetScores();
        foreach (ScoreData data in list)
        {
            data.level_status = true;
        }
        UpdateScores(list);
    }

    // Use this for initialization
    void Start()
    {
        if (!System.IO.File.Exists(dataPath))
            createScoreData();
        list = GetScores();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void Save()
    {
        var serializer = new XmlSerializer(typeof(ScoreContainer));

        using (var stream = new FileStream(dataPath, FileMode.Create))
        {
            var xmlWriter = new XmlTextWriter(stream, Encoding.UTF8);
            serializer.Serialize(xmlWriter, _scoreContainer);
        }
    }



    public void ClearScores()
    {
        list = GetScores();
        foreach (ScoreData data in list)
        {
            // data.moves = 0;
            data.highScore = 0;
            data.star1 = 0;
            data.star2 = 0;
            data.star3 = 0;

        }
        UpdateScores(list);
    }

    public void SetStar1(int cl)
    {
        list = GetScores();
        ScoreData data = list[cl - 1];
        data.star1 = 1;
        UpdateScores(list);
    }

    public void SetStar2(int cl, int i)
    {
        list = GetScores();
        ScoreData data = list[cl - 1];
        if (data.star2 < i)
        {
            data.star2 = i;
            UpdateScores(list);
        }
        
    }

    public void SetStar3(int cl, int i)
    {
        list = GetScores();
        ScoreData data = list[cl - 1];
        if (data.star3 < i)
        {
            data.star3 = i;
            UpdateScores(list);
        }
        
    }

   

    public void ClearLevelProgress()
    {
        list = GetScores();
        foreach (ScoreData data in list)
        {
            if (data.levelNumber == 1)
                continue;
            else
            {
                data.level_status = false;
            }

        }
        UpdateScores(list);
    }

    public int GetOptimalPathValue(int cl)
    {
        list = GetScores();
        return list[cl - 1].optimalpath_moves;
    }


    public int IsStar1(int cl)
    {
        list = GetScores();

        if (list[cl - 1].star1 == 3)
        {
            return 3;
        }
        else if (list[cl - 1].star1 == 2)
        {
            return 2;
        }
        else if (list[cl - 1].star1 == 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int IsStar2(int cl)
    {
        list = GetScores();

        if (list[cl - 1].star2 == 3)
        {
            return 3;
        }
        else if (list[cl - 1].star2 == 2)
        {
            return 2;
        }
        else if (list[cl - 1].star2 == 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int IsStar3(int cl)
    {
        list = GetScores();

        if (list[cl - 1].star3 == 3)
        {
            return 3;
        }
        else if (list[cl - 1].star3 == 2)
        {
            return 2;
        }
        else if (list[cl - 1].star3 == 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public static void Load()
    {
        if (!File.Exists(dataPath))
        {
            TextAsset textAsset = (TextAsset)Resources.Load("Score");
            File.WriteAllBytes(dataPath, textAsset.bytes);

        }


        var serializer = new XmlSerializer(typeof(ScoreContainer));
        using (var stream = new FileStream(dataPath, FileMode.Open))
        {
            _scoreContainer = serializer.Deserialize(stream) as ScoreContainer;
        }
    }

    public static List<ScoreData> GetScores()
    {
        Load();
        return _scoreContainer.LevelScores;

    }

    public static void UpdateScores(List<ScoreData> levelscores)
    {
        _scoreContainer.LevelScores = levelscores;
        Save();
    }

    public bool CheckLevelStatus(int cl)
    {
        list = GetScores();
        return list[cl - 1].level_status;
    }

    public void ChangeNextLevelStatus(int cl)
    {
        list = GetScores();
        ScoreData data = list[cl];
        data.level_status = true;
        UpdateScores(list);
    }

    public bool GetStarStatus(int cl)
    {
        list = GetScores();
        return list[cl - 1].star2_status;
    }




    public static void createScoreData()
    {
        List<ScoreData> list = new List<ScoreData>();

        for (int n = 0; n < 30; n++)
        {
            if (n == 0)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.optimalpath_moves = 5;
                data.unlock_triplets = false;
                data.star1 =0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.level_status = true;
                data.star2_status = false;
                
                list.Add(data);
            }
            if (n == 1)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.optimalpath_moves = 2;
                data.unlock_triplets = false;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.level_status = false;
                data.star2_status = false;

                list.Add(data);
            }
            if (n == 2)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.optimalpath_moves = 1;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 3)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.optimalpath_moves = 8;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 4)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.optimalpath_moves = 6;
                data.highScore = 0;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.star2_status = true;

                list.Add(data);
            }

            if (n == 5)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.optimalpath_moves = 5;
                
                data.highScore = 0;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.star2_status = false;

                list.Add(data);
            }

            if (n == 6)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.highScore = 0;
                data.optimalpath_moves = 1;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.star2_status =false;

                list.Add(data);
            }
            if (n == 7)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.highScore = 0;
                data.optimalpath_moves = 1;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 8)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.highScore = 0;
                data.optimalpath_moves = 2;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 9)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.optimalpath_moves = 2;
                data.highScore = 0;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.star2_status = false;


                list.Add(data);
            }



            if (n == 10)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.unlock_triplets = false;
                data.optimalpath_moves = 0;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }

            if (n == 11)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.unlock_triplets = false;
                data.level_status = false;
                data.optimalpath_moves = 2;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = false;

                list.Add(data);
            }
            if (n == 12)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.unlock_triplets = false;
                data.level_status = false;
                data.optimalpath_moves = 6;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 13)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.unlock_triplets = false;
                data.level_status = false;
                data.optimalpath_moves = 2;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 14)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.unlock_triplets = false;
                data.level_status = false;
                data.optimalpath_moves = 2;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 15)
            {
                ScoreData data = new ScoreData();
                data.levelNumber = (n + 1);
                data.unlock_triplets = false;
                data.level_status = false;
                data.optimalpath_moves = 4;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 16)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 2;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }


            if (n == 17)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 6;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = false;

                list.Add(data);
            }

            ///START CHANGES!
            if (n == 18)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 5;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
				data.star2_status = true;

                list.Add(data);
            }
            if (n == 19)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 3;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 20)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 8;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 21)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 0;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 22)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 6;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 23)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 13;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 24)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 8;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
				data.star2_status = true;

                list.Add(data);
            }
            if (n == 25)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 8;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = false;

                list.Add(data);
            }
            if (n == 26)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 16;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 27)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 7;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 28)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 5;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = true;

                list.Add(data);
            }
            if (n == 29)
            {
                ScoreData data = new ScoreData();
                data.unlock_triplets = false;
                data.levelNumber = (n + 1);
                data.level_status = false;
                data.optimalpath_moves = 5;
                data.star1 = 0;
                data.star2 = 0;
                data.star3 = 0;
                data.highScore = 0;
                data.star2_status = false;

                list.Add(data);
            }



            _scoreContainer.LevelScores = list;

            Save();
        }
    }

}


