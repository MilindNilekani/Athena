using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	public static GameManager instance=null;
	public GameObject playermanager;
	public GameObject enemymanager;
	public GameObject[] player_figure;
	public GameObject enemy_figure;


	public GameObject player;
	public GameObject enemy;
	public GameObject map;
	public GameObject attackvfx;
    public GameObject[] rampages; 
    public GameObject attackingui;

    //public GameObject textrender;




    public int[] player_pos;
	public int[,] enemy_pos;

	//this parameter is only used for test.
	public bool allow_freecamera=true;
	public bool[] Enemy_die;
	public bool Enemy_Turn=false;
	public int Player_Score=1;
	public bool Get_PowerUp;
	public bool UI_switcher;

	public int PowerUpBar=0 ;

	public int Score=0;
	// Enemy number controls the number of enemy
	// this will directly change the bool[] enemy_die, so remember to fix the processing of checking win and lose.
	public int enemy_number;

	public Shader Glow;

    public int levelMoves = 0;
    public int level_Points = 0;
    public int structures_dest=0;
    public int[] stars;

	public bool turn_finish;
	public bool enemy_stun;
    public bool rampageUI;
    public Sprite[] anim_circle;
	public bool ready;
    


    //instance control
    void Awake()
	{
		if (instance==null)
			instance=this;
		else if (instance!=this)
			Destroy(gameObject);
		//DontDestroyOnLoad(gameObject);		
	}



	void Start () {

		StartCoroutine("FixPlace");
		int i=Mathf.FloorToInt(Random.value*SoundManager.instance.BGM_gameplay.GetLength(0));
		if(i==SoundManager.instance.BGM_gameplay.GetLength(0)){i-=1;}
		SoundManager.instance.playBGM (SoundManager.instance.BGM_gameplay[i]);

      
		ready=false;
        stars = new int[3] {0,0,0};

	
	}


	// after star there is a time gap. Using a loading screen!! and disable the input.

    IEnumerator FixPlace()
	{
        int player_index = SaveLevel.instance.RobotText();
        print("player index:" + player_index);
         yield return new WaitForEndOfFrame ();
			enemy_pos = LevelLoader.instance.Levelenemy_initpos;
		    enemy_stun=false;
			//print (enemy_pos[0,0]+","+enemy_pos[0,1]);

			player_pos = LevelLoader.instance.Levelplayer_initpos;
			enemy_number = LevelLoader.instance.Levelenemy_number;
		    
			player = (GameObject)Instantiate (player_figure[player_index]);
           attackvfx = rampages[player_index];
		
			enemy = new GameObject ("enemy");
		if(enemy_number==0)
		{
			Enemy_die=new bool[1]{true};
			GameObject enemychild = (GameObject)Instantiate (enemy_figure);
			enemychild.transform.SetParent (enemy.transform);
		}
		else
		{Enemy_die = new bool[enemy_number];
		  for (int cnt=0; cnt<enemy_number; cnt++) {
			   Enemy_die[cnt]=false;
				GameObject enemychild = (GameObject)Instantiate (enemy_figure);
				enemychild.transform.SetParent (enemy.transform);
			}
		}
		
		
			player.transform.position = new Vector3 (-20f, 0f, -20f);
		
	
		  yield return new WaitForEndOfFrame();
			Vector3 transpos = grid.instance.calcWorldCoord (new Vector2 (player_pos [0], player_pos [1]));

			//print (player_pos[0]+","+player_pos[1]);

			player.transform.LookAt (new Vector3 (transpos.x, 0f, transpos.z));
			player.transform.position = new Vector3 (transpos.x, 0f, transpos.z);
		   



		if(enemy_number>0)
		{
		for (int cnt=0; cnt<enemy_number; cnt++) {
			
					transpos = grid.instance.calcWorldCoord (new Vector2 (enemy_pos [cnt, 0], enemy_pos [cnt, 1]));
					enemy.transform.GetChild (cnt).LookAt (new Vector3 (transpos.x, 0f, transpos.z));
					enemy.transform.GetChild (cnt).position = new Vector3 (transpos.x, 0f, transpos.z);
				}
		}
		else{enemy.transform.GetChild(0).gameObject.SetActive(false);}
			GameObject pat = (GameObject)Instantiate (attackvfx, GameManager.instance.player.transform.position, Quaternion.identity);
			pat.transform.position = GameManager.instance.player.transform.position;
        yield return new WaitForEndOfFrame();


            StartCoroutine ("FixingUpdate");
		 //ready=true;
			Instantiate (map);




	}

	// Update is called once per frame
	IEnumerator FixingUpdate () {
		bool haswin=false;
		turn_finish=false;
		while(true)
		{
		//Debug.Log("check win"+ checkwin);
			if (PowerUpBar==0)
				Get_PowerUp=false;
			
		if(turn_finish==true)	
			{
			if (check_win())
				if (haswin==false)
				{
			    print ("win!");
			    StartCoroutine("win");
					haswin=true;
				}
				yield return new WaitForSeconds(0.1f);
				turn_finish=false;
			}

		//winning
		//Losing
		else if (check_lose())
				{StartCoroutine("lose");}
			

	  else
		  if (Player_Score>0&&PlayerManager.instance==null)
			{
					Instantiate(playermanager);
			}
		else 
			if (Enemy_Turn==true&&EnemyAllDie()!=true)
		{
			//enemy's turn stop power_up
			//Get_PowerUp=false;
			if(enemy_stun)
			{
                    for (int cnt = 0; cnt < enemy.transform.childCount; cnt++)
                    {
                        enemy.transform.GetChild(cnt).GetComponent<Animator>().SetBool("stun", false);
                        enemy.transform.GetChild(cnt).GetChild(2).gameObject.SetActive(false);
                        enemy.transform.GetChild(cnt).GetChild(3).gameObject.SetActive(true);
                    }
                        enemy_stun =false;
			yield return new WaitForSeconds(1f);
						}
			Instantiate(enemymanager);
                
		}

		yield return new WaitForEndOfFrame();
		}
	    
	}

	//if scores are cleared win 
	 bool check_win()
	{
		int Hexwidth=grid.instance.scoremap.GetLength(0);
		int Hexheight=grid.instance.scoremap.GetLength(1);
		bool win=true;

		for (int y = 0; y <Hexheight ; y++)
		{
			for (int x = 0; x < Hexwidth; x++)
			{
				if (grid.instance.scoremap[x,y]>0)
					win=false;
			}
		}
		return win;

	}
	//showing the winning scene
  
	bool check_lose()
	{
		//cahnge here for dynamic enemy number
		bool lose=false;

		if(enemy_number==0){return lose;}
		else
		{
		for(int cnt=0;cnt<enemy_number;cnt++)
			if(Enemy_die[cnt]==false&&player_pos[0]==enemy_pos[cnt,0]&&player_pos[1]==enemy_pos[cnt,1])
				lose=true;
		return lose;
		}
	}

	public bool EnemyAllDie()
	{
		bool enemyalldie=true;
		if(enemy_number==0)
		{return enemyalldie;}
		else{
		for (int cnt=0;cnt<enemy_number;cnt++)
		{
			if (Enemy_die[cnt]==false)
				
				enemyalldie=false;
		}
		
		return enemyalldie;	
		}
	}

    public int EnemiesKilled()
    {
        int enemies_killed=0;
        if(EnemyAllDie())
        {
            return enemy_number;
        }
        else
        {
            for(int cnt=0;cnt<enemy_number;cnt++)
            {
                if(Enemy_die[cnt]==true)
                {
                    enemies_killed++;
                   
                }
            }
            return enemies_killed;
        }
    }



	IEnumerator win()
	{
        StopCoroutine("FixingUpdate");
        if (EnemyAllDie() && enemy_number > 0)
        {
            level_Points += 10000;
        }
        SaveScores.instance.SetPoints(level_Points,LevelLoader.instance.current_level);
        SaveScores.instance.SetStar1(LevelLoader.instance.current_level);
        stars[0] = 3;
        if (SaveScores.instance.GetStarStatus(LevelLoader.instance.current_level))
        {
            if (EnemiesKilled() == enemy_number)
            {
                SaveScores.instance.SetStar3(LevelLoader.instance.current_level, 3);
                stars[2] = 3;
            }
            else if (EnemiesKilled() == enemy_number - 1)
            {
                SaveScores.instance.SetStar3(LevelLoader.instance.current_level, 2);
                stars[2] = 2;
            }
            else if(EnemiesKilled()<enemy_number-1)
            {
                SaveScores.instance.SetStar3(LevelLoader.instance.current_level, 1);
                stars[2] = 1;
            }
        }
        else
        {
            SaveScores.instance.SetStar3(LevelLoader.instance.current_level, -1);
            stars[2] = -1;
        }
       
        
        
        //=========================================scoring=========================================================
        if(levelMoves<=SaveScores.instance.GetOptimalPathValue(LevelLoader.instance.current_level))
        {
            SaveScores.instance.SetStar2(LevelLoader.instance.current_level, 3);
            stars[1] = 3;
        }
        else if (levelMoves > SaveScores.instance.GetOptimalPathValue(LevelLoader.instance.current_level) && levelMoves <= SaveScores.instance.GetOptimalPathValue(LevelLoader.instance.current_level)+2)
        {
            SaveScores.instance.SetStar2(LevelLoader.instance.current_level, 2);
            stars[1] = 2;
        }
        else 
        {
            SaveScores.instance.SetStar2(LevelLoader.instance.current_level, 1);
            stars[1] = 1;
        }
        //=========================================scoring=========================================================
        //=================================unlock levels=======================================================
        if (LevelLoader.instance.current_level < 5)
        {
            SaveScores.instance.ChangeNextLevelStatus(LevelLoader.instance.current_level);
        }
        else if (LevelLoader.instance.current_level == 5)
        {
            SaveScores.instance.ChangeNextLevelStatus(LevelLoader.instance.current_level);
            SaveScores.instance.ChangeNextLevelStatus(LevelLoader.instance.current_level + 1);
            SaveScores.instance.ChangeNextLevelStatus(LevelLoader.instance.current_level + 2);
        }
        else if (LevelLoader.instance.current_level > 5 && LevelLoader.instance.current_level<27)
        {
            if (LevelLoader.instance.current_level % 3 == 0)
            {
                SaveScores.instance.CountIncrement1(LevelLoader.instance.current_level);
            }
            else if (LevelLoader.instance.current_level % 3 == 1)
            {
                SaveScores.instance.CountIncrement2(LevelLoader.instance.current_level);
            }
            else
            {
                SaveScores.instance.CountIncrement3(LevelLoader.instance.current_level);
            }
        }
       


        else if (LevelLoader.instance.current_level >= 27&&LevelLoader.instance.current_level<30)
        {
            if (LevelLoader.instance.current_level % 3 == 0)
            {
                SaveScores.instance.CountIncrement_Last1(LevelLoader.instance.current_level);
            }
            else if (LevelLoader.instance.current_level % 3 == 1)
            {
                SaveScores.instance.CountIncrement_Last2(LevelLoader.instance.current_level);
            }
            else
            {
                SaveScores.instance.CountIncrement_Last3(LevelLoader.instance.current_level);
            }
        }

        yield return new WaitForEndOfFrame();
        //=================================unlock levels=======================================================


        SaveScores.instance.SetJustPlayedLevel(LevelLoader.instance.current_level);
		godseye.instance.winning();
        
     
	}
   
	IEnumerator lose()
	{
		levelMoves = 0;
        level_Points = 0;
		player.transform.GetComponent<Animator>().SetBool("die",true);

		yield return new WaitForSeconds(1f);
	
        //		LevelLoader.instance.level=LevelLoader.instance.current_level;
        godseye.instance.lose();

    }


	public void EnemyStun()
	{
		if((EnemyAllDie()==false)&&(enemy_stun==false))
		{
		enemy_stun=true;
		for(int cnt=0;cnt<enemy.transform.childCount;cnt++)
            {
                enemy.transform.GetChild(cnt).GetComponent<Animator>().SetBool("stun", true);
                enemy.transform.GetChild(cnt).GetChild(2).gameObject.SetActive(true);
                enemy.transform.GetChild(cnt).GetChild(3).gameObject.SetActive(false);
            }
			
        }

    }

   

}
