using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;

public class SaveLevel : MonoBehaviour
{
    public static SaveLevel instance = null;
    private static string dataPath = string.Empty;
    private static LevelContainer _levelContainer = new LevelContainer();


    //bool k1=false, k2=false, k3=false;

    public int robot_textind;
    public List<LevelData> list = new List<LevelData>();

    void Awake()
    {
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            dataPath = System.IO.Path.Combine(Application.persistentDataPath, "Level.xml");
        else
            dataPath = System.IO.Path.Combine(Application.dataPath, "Resources/Level.xml");
    }


    public void SetRobotTextureIndex(int i)
    {
        list = GetLevels();
        LevelData data = list[0];
        data.text_ind = i;
        UpdateLevel(list);
       
    }

    public int RobotText()
    {
        list = GetLevels();
        LevelData data = list[0];
        return data.text_ind;
    }

    



    public void GetThatLevelData(int cl)
    {
        list = GetLevels();
        LevelLoader.instance.Levelgridmap = SingleToMulti(list[cl - 1].gridmap, list[cl - 1].gmx, list[cl - 1].gmy);
        LevelLoader.instance.Levelscoremap = SingleToMulti(list[cl - 1].scoremap, list[cl - 1].gmx, list[cl - 1].gmy);
        LevelLoader.instance.Levelplayer_initpos = list[cl - 1].player_initpos; ;
        LevelLoader.instance.Levelenemy_number = list[cl - 1].enemy_num;
        LevelLoader.instance.Levelenemy_initpos = SingleToMulti(list[cl - 1].enemy_initpos, list[cl - 1].enemy_num, 2);
    }

   

    // Use this for initialization
    void Start()
    {
        if(!System.IO.File.Exists(dataPath))
            createLevelData();
        list = GetLevels();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void Save()
    {
        var serializer = new XmlSerializer(typeof(LevelContainer));
        using (var stream = new FileStream(dataPath, FileMode.Create))
        {
            var xmlWriter = new XmlTextWriter(stream, Encoding.UTF8);
            serializer.Serialize(xmlWriter, _levelContainer);
        }
    }

    

   

    public static void Load()
    {
        if (!File.Exists(dataPath))
        {
            TextAsset textAsset = (TextAsset)Resources.Load("Level");
            File.WriteAllBytes(dataPath, textAsset.bytes);

        }


        var serializer = new XmlSerializer(typeof(LevelContainer));
        using (var stream = new FileStream(dataPath, FileMode.Open))
        {
            _levelContainer = serializer.Deserialize(stream) as LevelContainer;
        }
    }

    public static List<LevelData> GetLevels()
    {
        Load();
        return _levelContainer.Levels;

    }

    public static void UpdateLevel(List<LevelData> levels)
    {
        _levelContainer.Levels = levels;
        Save();
    }

    static int[,] SingleToMulti(int[] array, int gx, int gy)
    {
        int index = 0;
        int[,] multi = new int[gx, gy];
        for (int x = 0; x < gx; x++)
        {
            for (int y = 0; y < gy; y++)
            {
                multi[x, y] = array[index];
                index++;
            }
        }
        return multi;
    }


    static int[] MultiToSingle(int[,] array)
    {
        int index = 0;
        int width = array.GetLength(0);
        int height = array.GetLength(1);
        int[] single = new int[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                single[index] = array[x, y];
                index++;
            }
        }
        return single;
    }

    public static void createLevelData()
    {
        List<LevelData> list = new List<LevelData>();

        for (int n = 0; n < 30; n++)
        {
            if (n == 0)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                
                int[,] Levelgridmap = new int[6, 1]
{
{1}, {1}, {1}, {1}, {1 }, {1 }
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 6;
                data.gmy = 1;
                int[,] Levelscoremap = new int[6, 1]
{
{0}, {0}, {0}, {1}, {0 }, {1 }
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 0, 0 };
                data.enemy_num = 0;
                int[,] Levelenemy_initpos = new int[4, 2]
                {{0,0},
                {0,0},
                {0,0},
                {0,0}};
                data.text_ind = 0;
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 1)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[6, 1]
{ {1 }, {1 }, {1 }, {1 },{1 },{1 }

};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 6;
                data.gmy = 1;

                int[,] Levelscoremap = new int[6, 1]
                {
                    {0 }, {0 }, {0 }, {1 }, {0 }, {1 }

                };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 0 };
                data.enemy_num = 1;
                int[,] Levelenemy_initpos = new int[4, 2]
                {{0,0},
                {0,0},
                {0,0},
                {0,0}};
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 2)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[7, 1]
               {
                   {1 }, {1 }, {1 }, {1 }, {1 }, {1 }, {1 }
                };
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 7;
                data.gmy = 1;
                int[,] Levelscoremap = new int[7, 1]
                {
                    {0 }, {0 }, {1 }, {1 }, {1 }, {0 }, {1 }
                };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 0 };
                data.enemy_num = 2;
                int[,] Levelenemy_initpos = new int[4, 2]
                {{0,0},
                {5,0},
                {0,0},
                {0,0}};
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 3)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
               
                int[,] Levelgridmap = new int[12, 4]
 { {1,0,0,0 }, {1,0,0,0 }, {1,0,0,0 }, {1,0,0,0 }, {1,0,0,0 }, {1,0,0,0 }, {1,1,1,1 },  {1,0,0,0 }, {1,0,0,0 }, {1,0,0,0 }, {1,0,0,0 }, {1,0,0,0 }

 };
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 12;
                data.gmy = 4;

                int[,] Levelscoremap = new int[12, 4]
{ {0,0,0,0 },{0,0,0,0 },{0,0,0,0 },{1,0,0,0 },{1,0,0,0 },{1,0,0,0 },{0,0,0,0 },{0,0,0,0 },{0,0,0,0 },{0,0,0,0 },{0,0,0,0 },{1,0,0,0 }

};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 0, 0 };
                data.enemy_num = 1;
                int[,] Levelenemy_initpos = new int[4, 2]
                {{6,3},
                {0,0},
                {0,0},
                {0,0}};
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 4)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[14, 6]
{ {0,0,0,1,0,0 },
{0,0,0,1,0,0 },
{1,1,1,0,0,0 },
{0,0,0,1,0,0 },
{0,0,0,1,0,0 },
{0,0,0,1,0,0 },
{0,0,0,1,0,0 },
{0,0,0,1,0,0 },
{0,0,0,1,0,0 },
{0,0,0,1,1,0 },
{0,0,0,1,1,1 },
{0,0,0,1,1,0 },
{0,0,0,1,1,0 },
{0,0,0,1,0,0 }

};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 14;
                data.gmy = 6;

                int[,] Levelscoremap = new int[14, 6]
{ {0,0,0,0,0,0 },
{0,0,0,0,0,0 },
{0,0,0,0,0,0 },
{0,0,0,0,0,0 },
{0,0,0,0,0,0 },
{0,0,0,1,0,0 },
{0,0,0,0,0,0 },
{0,0,0,0,0,0 },
{0,0,0,0,0,0 },
{0,0,0,1,1,0 },
{0,0,0,0,1,1 },
{0,0,0,0,1,0 },
{0,0,0,0,1,0 },
{0,0,0,1,0,0 }

};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 3 };
                data.enemy_num = 2;
                int[,] Levelenemy_initpos = new int[4, 2]
                {{0,3},
                {2,0},
                {0,0},
                {0,0}};
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }

            if (n == 5)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[10, 8]
{ { 0,0,1,0,0,0,0,0},
  { 0,0,1,0,0,0,0,0},
  { 0,0,1,0,0,0,0,0},
  { 0,0,1,0,0,0,0,0},
  { 0,0,1,0,0,1,1,0},
  { 1,1,1,1,1,1,1,1},
  { 0,0,1,0,0,1,1,0},
  { 0,0,1,0,0,0,0,0},
  { 0,0,1,0,0,0,0,0},
  { 0,0,1,0,0,0,0,0}

};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 10;
                data.gmy = 8;
                int[,] Levelscoremap = new int[10, 8]
                {
                   {0,0,0,0,0,0,0,0},
                   {0,0,0,0,0,0,0,0},
                   {0,0,0,0,0,0,0,0},
                   {0,0,0,0,0,0,0,0},
                   {0,0,0,0,0,0,0,0},
                   {0,1,0,0,1,1,1,0},
                   {0,0,0,0,0,0,0,0},
                   {0,0,0,0,0,0,0,0},
                   {0,0,0,0,0,0,0,0},
                   {0,0,0,0,0,0,0,0},
                };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 5, 2 };
                data.enemy_num = 6;
                int[,] Levelenemy_initpos = new int[6, 2]
                {{3,2},
                {4,5},
                {7,2},
                {6,6},
                {4,6, },
                    {6,5} };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }

            if (n == 6)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[7, 3]
{
{0,0,0},
{0,0,0},
{0,0,1},
{0,0,1},
{1,1,1},
{0,1,0},
{0,0,0}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 7;
                data.gmy = 3;

                int[,] Levelscoremap = new int[7, 3]
{
{0,0,0},
{0,0,0},
{0,0,0},
{0,0,0},
{1,0,0},
{0,0,0},
{0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 3, 2 };
                data.enemy_num = 1;
                int[,] Levelenemy_initpos = new int[2, 2]
                {
{2,2},
{0,0}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 7)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[5, 5]
{
{0,1,1,1,1},
{1,1,1,1,1},
{0,1,1,1,1},
{1,1,1,1,1},
{0,1,0,1,0}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 5;
                data.gmy = 5;

                int[,] Levelscoremap = new int[5, 5]
 {
{0,0,0,0,1},
{0,0,0,0,0},
{0,1,0,1,0},
{0,0,1,0,0},
{0,0,0,0,0}
 };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 2 };
                data.enemy_num = 4;
                int[,] Levelenemy_initpos = new int[4, 2]
                {
{1,1},
{1,4}, {3,0 }, {3,4 }
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 8)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[9, 5]
{ {0,0,0,1,1 },
{0,0,0,1,1 },
{1,1,1,1,1 },
{1,1,1,1,1 },
{1,1,1,1,1 },
{1,1,1,1,1 },
{1,1,1,1,1 },
{1,1,1,1,1 },
{1,1,1,1,1 }

};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 9;
                data.gmy = 5;

                int[,] Levelscoremap = new int[9, 5]
{ {0,0,0,0,0 },{0,0,0,0,0 },{0,0,0,0,0 },{0,0,1,0,0 },{0,1,0,0,0 },{1,0,0,0,0 },{0,1,0,1,0 },{0,0,1,1,0 },{0,0,0,1,0 }

};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 0, 4 };
                data.enemy_num = 4;
                int[,] Levelenemy_initpos = new int[4, 2]
                {
{5,1},
{5,2},
{5,3 },
{6,2}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 9)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                

                int[,] Levelgridmap = new int[6, 6]
{
{0,1,0,0,1,1},
{0,1,1,0,1,0},
{0,1,1,0,1,0},
{1,1,1,1,1,0},
{0,0,0,1,1,0},
{0,0,0,1,1,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 6;
                data.gmy = 6;
                int[,] Levelscoremap = new int[6, 6]
{
{0,0,0,0,0,0},
{0,0,0,0,0,0},
{0,0,1,0,0,0},
{0,0,0,0,1,0},
{0,0,0,0,0,0},
{0,0,0,0,1,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 3, 2 };
                data.enemy_num = 3;
                int[,] Levelenemy_initpos = new int[4, 2]
                {{3,0},
                {0,1},
                {0,5},
                {0,0}};
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }



            if (n == 10)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
               
                int[,] Levelgridmap = new int[5, 5]
{
{0,1,0,1,0},
{1,1,1,1,1},
{0,1,1,1,0},
{1,1,1,1,1},
{0,1,0,1,0}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 5;
                data.gmy = 5;

                int[,] Levelscoremap = new int[5, 5]
{
{0,0,0,0,0},
{0,1,1,1,0},
{0,1,0,1,0},
{0,1,1,1,0},
{0,0,0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 2, 2 };
                data.enemy_num = 8;
                int[,] Levelenemy_initpos = new int[8, 2]
                {{1,0},
                {0,1},
                {0,3},
                {1,4},
                {3,4 },
                {4,3 },
                {3,0 },
                {4,1 }
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }

            if (n == 11)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[5, 7]
{
{0,1,1,1,1,1,1},
{1,1,1,1,1,1,1},
{1,1,1,1,1,1,1},
{0,0,1,1,1,1,0},
{0,0,1,1,1,1,0}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 5;
                data.gmy = 7;

                int[,] Levelscoremap = new int[5, 7]
{
{0,0,0,0,0,0,0},
{0,0,0,0,0,0,0},
{0,0,0,0,0,1,0},
{0,0,0,0,0,0,0},
{0,0,0,0,1,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 2, 2 };
                data.enemy_num = 3;
                int[,] Levelenemy_initpos = new int[3, 2]
                {{0,1},
                {0,5},
                {2,0 }

                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 12)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[9, 5]
{ {0,0,0,1,0 },
{0,0,1,1,0 },
{0,1,1,1,0 },
{1,1,1,1,1 },
{1,1,1,1,1 },
{0,0,1,0,1 },
{0,0,0,0,1 },
{0,0,0,0,1 },
{0,0,0,0,1 }

};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 9;
                data.gmy = 5;

                int[,] Levelscoremap = new int[9, 5]
 {
{0,0,0,0,0 }, {0,0,0,0,0 }, {0,0,0,0,0 }, {0,0,0,0,0 }, {0,1,0,1,0 }, {0,0,1,0,0 },{0,0,0,0,0 },{0,0,0,0,0 },{0,0,0,0,1 }
 };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 0, 3 };
                data.enemy_num = 2;
                int[,] Levelenemy_initpos = new int[2, 2]
                {{3,4},
                {5,4},


                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 13)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[5, 3]
{ {1,1,1 }, {1,1,1 }, {1,1,1 }, {1,1,1 }, {1,1,1 }
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 5;
                data.gmy = 3;

                int[,] Levelscoremap = new int[5, 3]
 {
{0,0,0 }, {0,0,1 }, {0,0,1 }, {1,0,1 }, {0,0,0 }
 };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 2, 1 };
                data.enemy_num = 2;
                int[,] Levelenemy_initpos = new int[3, 2]
                {{0,1},
                {4,1},
                {0,0},

                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 14)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[5, 3]
{ {1,1,1 }, {1,1,1 }, {1,1,1 }, {1,1,1 }, {1,1,1 }
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 5;
                data.gmy = 3;

                int[,] Levelscoremap = new int[5, 3]
 {
{0,1,0 }, {0,0,0 }, {0,0,0 }, {1,1,0 }, {1,0,0 }
 };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 2, 1 };
                data.enemy_num = 3;
                int[,] Levelenemy_initpos = new int[3, 2]
                {{1,0},
                {1,1},
                {1,2},

                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 15)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
               
                int[,] Levelgridmap = new int[6, 5]
{
{0,1,1,1,1 }, {0,1,1,1,0 }, {1,1,1,0,0 }, {1,0,0,0,0 }, {1,0,0,0,0 },{1,0,0,0,0 }
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 6;
                data.gmy = 5;

                int[,] Levelscoremap = new int[6, 5]
{
{ 0,0,0,0,1}, {0,0,1,0,0 }, {0,0,1,0,0 }, {1,0,0,0,0 }, {0,0,0,0,0 }, {0,0,0,0,0 }
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 0, 2 };
                data.enemy_num = 2;
                int[,] Levelenemy_initpos = new int[3, 2]
                {{4,0},
                {0,3},
                {0,0},

                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 16)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[10, 5]
{ {1,1,1,1,1 },{1,1,1,1,1 },{1,1,1,1,1 },{1,1,1,1,1 },{1,1,1,1,1 },{1,1,1,1,1 },{1,1,1,1,1 },{1,1,1,1,1 },{1,1,1,1,1 },{1,1,1,1,1 }

};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 10;
                data.gmy = 5;



                int[,] Levelscoremap = new int[10, 5]
{ {1,1,0,0,0 },{1,1,0,0,0 },{0,0,0,0,0 },{0,0,1,0,0 },{0,1,0,1,0 },{1,0,1,0,1 },{0,1,0,1,0 },{0,0,1,0,0 },{0,0,0,0,0 },{0,0,0,0,0 }

};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 9, 2 };
                data.enemy_num = 9;
                int[,] Levelenemy_initpos = new int[9, 2]
                {{0,2},
                {0,4},
                {2,0},
                {5,1},
                {4,2},
                {6,2},
                {5,3},
                {9,0},
                {9,4},
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }


            if (n == 17)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);
                
                int[,] Levelgridmap = new int[5, 4]
{
{0,1,1,0 }, {1,1,1,1 }, {1,1,1,1 }, {1,1,1,1 }, {0,1,1,0 }
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 5;
                data.gmy = 4;



                int[,] Levelscoremap = new int[5, 4]
                {
{0,0,1,0 }, {1,0,0,1 }, {0,0,0,0 }, {1,0,0,1 }, {0,0,1,0 }
                };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 2, 2 };
                data.enemy_num = 2;
                int[,] Levelenemy_initpos = new int[3, 2]
                {{1,1},
                {3,1},
                {0,0},

                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }

            if (n == 18)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[5, 3]
                {
{1,1,1},
{1,1,1},
{1,1,1},
{1,1,1},
{1,1,1}
                };
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 5;
                data.gmy = 3;

                int[,] Levelscoremap = new int[5, 3]
                {
{1,0,0},
{0,0,1},
{0,1,0},
{0,0,0},
{1,1,1}
                };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 1 };
                data.enemy_num = 4;
                int[,] Levelenemy_initpos = new int[4, 2]
                {
{3,0},
{3,1},
{0,2},
{3,2}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 19)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[6, 6]
{
{0,0,0,1,0,0},
{1,1,1,1,0,0},
{0,1,1,1,0,0},
{0,0,0,1,1,1},
{1,1,1,1,1,1},
{1,0,0,0,0,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 6;
                data.gmy = 6;

                int[,] Levelscoremap = new int[6, 6]
{
{0,0,0,0,0,0},
{0,1,0,0,0,0},
{0,0,1,0,0,0},
{0,0,0,0,0,0},
{0,0,0,1,0,1},
{0,0,0,0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 2, 3 };
                data.enemy_num = 3;
                int[,] Levelenemy_initpos = new int[3, 2]
                {
{1,0},
{5,0},
{0,3}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 20)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[2, 6]
{
{1,1,1,1,0,1},
{1,1,1,0,1,0}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 2;
                data.gmy = 6;

                int[,] Levelscoremap = new int[2, 6]
{
{0,1,0,1,0,1},
{1,0,1,0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);

                data.player_initpos = new int[2] { 0, 2 };
                data.enemy_num = 3;

                int[,] Levelenemy_initpos = new int[3, 2]
                {
{0,0},
{1,1},
{1,4}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 21)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[7, 5]
 {
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1}
 };
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 7;
                data.gmy = 5;

                int[,] Levelscoremap = new int[7, 5]
{
{0,0,0,0,0},
{0,1,0,1,0},
{0,1,1,1,0},
{0,1,1,1,0},
{0,0,0,0,0},
{0,0,0,0,0},
{0,0,0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 2 };
                data.enemy_num = 5;
                int[,] Levelenemy_initpos = new int[5, 2]
                {
{6,0},
{6,1},
{6,2},
{6,3},
{6,4}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 22)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[2, 7]
{
{1,1,1,1,1,1,1},
{1,1,1,1,1,1,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 2;
                data.gmy = 7;

                int[,] Levelscoremap = new int[2, 7]
{
{1,0,0,0,1,1,1},
{0,0,0,0,0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 0, 3 };
                data.enemy_num = 1;
                int[,] Levelenemy_initpos = new int[1, 2]
                {
{0,1}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }

            if (n == 23)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[7, 6]
{
{1,1,1,1,1,1},
{1,1,1,1,1,1},
{1,1,1,1,1,1},
{1,1,1,1,1,1},
{1,1,1,1,1,1},
{1,1,1,1,1,1},
{1,1,1,1,1,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 7;
                data.gmy = 6;

                int[,] Levelscoremap = new int[7, 6]
{
{0,0,0,0,0,0},
{0,1,1,1,0,0},
{0,0,0,0,0,0},
{0,0,0,0,0,0},
{0,0,0,0,0,0},
{0,0,0,0,0,0},
{0,1,0,0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 0, 2 };
                data.enemy_num = 5;
                int[,] Levelenemy_initpos = new int[5, 2]
                {
{5,0},
{6,0},
{5,1},
{5,2},
{6,2}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 24)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[6, 5]
{
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 6;
                data.gmy = 5;

                int[,] Levelscoremap = new int[6, 5]
{
{0,0,0,1,0},
{1,0,0,1,1},
{1,0,1,0,0},
{0,0,0,0,0},
{1,0,1,0,1},
{0,1,0,1,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 2 };
                data.enemy_num = 7;
                int[,] Levelenemy_initpos = new int[7, 2]
                {
{0,0},
{4,1},
{5,2},
{2,3},
{4,3},
{0,4},
{2,4}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 25)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[9, 5]
{
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 9;
                data.gmy = 5;

                int[,] Levelscoremap = new int[9, 5]
{
{0,0,0,1,1},
{0,0,0,0,0},
{0,0,0,1,0},
{0,0,0,0,0},
{0,0,0,0,0},
{0,0,0,0,0},
{0,0,0,0,0},
{1,0,0,0,0},
{0,0,0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 3 };
                data.enemy_num = 8;
                int[,] Levelenemy_initpos = new int[8, 2]
                {
{6,0},
{8,0},
{7,1},
{8,1},
{7,2},
{8,2},
{7,3},
{8,3}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 26)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[7, 7]
{
{1,1,1,1,1,1,1},
{1,1,1,1,1,1,1},
{1,1,1,1,1,1,1},
{1,1,1,1,1,1,1},
{1,1,1,1,1,1,1},
{1,1,1,1,1,1,1},
{1,1,1,1,1,1,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 7;
                data.gmy = 7;

                int[,] Levelscoremap = new int[7, 7]
{
{1,0,0,0,0,0,1},
{0,0,0,0,0,0,0},
{0,0,1,0,0,1,0},
{0,1,0,1,0,1,0},
{0,1,0,0,1,0,0},
{0,0,0,0,0,0,0},
{1,0,0,0,0,0,1}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 5 };
                data.enemy_num = 7;
                int[,] Levelenemy_initpos = new int[7, 2]
                {
{0,1},
{2,1},
{5,1},
{2,3},
{4,3},
{4,5},
{5,5}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 27)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[6, 4]
 {
{1,1,1,1},
{1,1,1,1},
{1,1,1,1},
{1,1,1,1},
{1,1,1,1},
{1,1,1,1}
 };
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 6;
                data.gmy = 4;


                int[,] Levelscoremap = new int[6, 4]
                {
{0,0,0,0},
{0,1,0,0},
{0,0,0,1},
{0,0,0,0},
{0,0,0,1},
{0,0,1,1}
                };
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 0, 3 };
                
                data.enemy_num = 1;
                int[,] Levelenemy_initpos = new int[1, 2]
                {
{4,2}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 28)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[6, 3]
{
{1,1,1},
{1,1,1},
{1,1,1},
{1,1,1},
{1,1,1},
{1,1,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 6;
                data.gmy = 3;

                int[,] Levelscoremap = new int[6, 3]
{
{1,0,0},
{0,0,1},
{0,0,0},
{0,1,0},
{1,0,0},
{0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 2, 1 };
                data.enemy_num = 1;
                int[,] Levelenemy_initpos = new int[1, 2]
                {
{3,2}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }
            if (n == 29)
            {
                LevelData data = new LevelData();
                data.levelNumber = (n + 1);


                int[,] Levelgridmap = new int[5, 5]
{
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1},
{1,1,1,1,1}
};
                data.gridmap = MultiToSingle(Levelgridmap);
                data.gmx = 5;
                data.gmy = 5;

                int[,] Levelscoremap = new int[5, 5]
{
{0,0,0,0,0},
{0,1,0,0,1},
{0,0,0,0,0},
{1,0,0,0,0},
{0,1,0,0,0}
};
                data.scoremap = MultiToSingle(Levelscoremap);
                data.player_initpos = new int[2] { 1, 2 };
                data.enemy_num = 4;
                int[,] Levelenemy_initpos = new int[4, 2]
                {
{0,0},
{0,2},
{0,3},
{1,3}
                };
                data.enemy_initpos = MultiToSingle(Levelenemy_initpos);
                list.Add(data);
            }


            _levelContainer.Levels = list;

            Save();
        }
    }

    }

