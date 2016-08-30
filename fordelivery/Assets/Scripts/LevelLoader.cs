using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {


	public static LevelLoader instance=null;
	public int[,] Levelgridmap;
	public int[,] Levelscoremap;
	public int[,] LevelpowerUPs;
	public int[] Levelplayer_initpos;
	public int[,] Levelenemy_initpos;
	public int  Levelenemy_number;
	
	
	public int level;
	public int current_level;
    public int[] biglevels;

	void Awake()
	{
      
        if (instance==null)
			instance=this;
		else if (instance!=this)
			Destroy(gameObject);
		//Eternity!!!!
		DontDestroyOnLoad(gameObject);		
	}
	// Use this for initialization

	void Start () {
		//If scene order changed change THIS!
        /*if (Application.loadedLevel == 1) {
            
            SoundManager.instance.playBGM (SoundManager.instance.BGM_gameplay);
			SoundManager.instance.BGM.loop = true;
			level=1000;
		}*/

    }

	void Update()
	{
		switch(level)
		{
		case 1:
			current_level=1;
                SaveLevel.instance.GetThatLevelData(1);
           
                level =1000;
			Application.LoadLevel("main");
			return;

	

		case 2:
			current_level=2;
            SaveLevel.instance.GetThatLevelData(2);
               
                level =1000;
			Application.LoadLevel("main");
			return;

		case 3:
			current_level=3;
                SaveLevel.instance.GetThatLevelData(3);
               
                level =1000;
                Application.LoadLevel("main");
			return;
		
		case 4:
			current_level=4;
                SaveLevel.instance.GetThatLevelData(4);
                
                level =1000;
			
			Application.LoadLevel("main");
			
			return;


		case 5:
			current_level=5;
                SaveLevel.instance.GetThatLevelData(5);
                
                level =1000;

			Application.LoadLevel("main");

			return;
		
		case 6:
			current_level=6;
                SaveLevel.instance.GetThatLevelData(6);
               
                level =1000;

			Application.LoadLevel("main");
			return;
		case 7:
			current_level=7;
                SaveLevel.instance.GetThatLevelData(7);
                
                level =1000;
			Application.LoadLevel("main");
			return;
            case 8:
                current_level = 8;
                SaveLevel.instance.GetThatLevelData(8);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 9:
                current_level = 9;
                SaveLevel.instance.GetThatLevelData(9);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 10:
                current_level = 10;
                SaveLevel.instance.GetThatLevelData(10);

                level = 1000;
                Application.LoadLevel("main"); ;
                return;
            case 11:
                current_level = 11;
                SaveLevel.instance.GetThatLevelData(11);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 12:
                current_level = 12;
                SaveLevel.instance.GetThatLevelData(12);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 13:
                current_level = 13;
                SaveLevel.instance.GetThatLevelData(13);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 14:
                current_level = 14;
                SaveLevel.instance.GetThatLevelData(14);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 15:
                current_level = 15;
                SaveLevel.instance.GetThatLevelData(15);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 16:
                current_level = 16;
                SaveLevel.instance.GetThatLevelData(16);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 17:
                current_level = 17;
                SaveLevel.instance.GetThatLevelData(17);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 18:
                current_level = 18;
                SaveLevel.instance.GetThatLevelData(18);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 19:
                current_level = 19;
                SaveLevel.instance.GetThatLevelData(19);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 20:
                current_level = 20;
                SaveLevel.instance.GetThatLevelData(20);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 21:
                current_level = 21;
                SaveLevel.instance.GetThatLevelData(21);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 22:
                current_level = 22;
                SaveLevel.instance.GetThatLevelData(22);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 23:
                current_level = 23;
                SaveLevel.instance.GetThatLevelData(23);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 24:
                current_level = 24;
                SaveLevel.instance.GetThatLevelData(24);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 25:
                current_level = 25;
                SaveLevel.instance.GetThatLevelData(25);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 26:
                current_level = 26;
                SaveLevel.instance.GetThatLevelData(26);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 27:
                current_level = 27;
                SaveLevel.instance.GetThatLevelData(27);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 28:
                current_level = 28;
                SaveLevel.instance.GetThatLevelData(28);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 29:
                current_level = 29;
                SaveLevel.instance.GetThatLevelData(29);

                level = 1000;
                Application.LoadLevel("main");
                return;
            case 30:
                current_level = 30;
                SaveLevel.instance.GetThatLevelData(30);

                level = 1000;
                Application.LoadLevel("main");
                return;
              
            default:
			return;




		}

	}
	
	// Update is called once per frame

}
