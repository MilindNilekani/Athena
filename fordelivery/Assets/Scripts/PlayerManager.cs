
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    /*
	 * PlayerManager controls one turn of player
	 * instantiate at the beginning of the turn
	 * destroy when get finished
	 * controlled by GameManager
	 */

    //	public int movesTaken=0;


    public GameObject gameInfo;
    public GameObject explosion;
    public GameObject bonus;


    public static PlayerManager instance = null;
    int[] player_posnow = new int[2] { 0, 0 };

    public GameObject UIGrid;

    //public GameObject player;
    int PlayerTurnFlag = 4;

    private int[,] next_step;
    private bool[] canmove;
    public int[] intend_pos;


    private Touch Mytouch;

    List<Transform> killing_i;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }




    // Use this for initialization	
    void Start()
    {
        //get current grid position of player
        player_posnow = GameManager.instance.player_pos;
        next_step = MovingObject.instance.CalIntendPos(player_posnow[0], player_posnow[1]);
        canmove = MovingObject.instance.CanMove(next_step);
        creatingUIgrids();
        killing_i = new List<Transform>();
        intend_pos = new int[2] { 1000, 1000 };
       
        StartCoroutine("inputtest");

		//sound
		float talk=Random.value;

		if(talk<0.4f)
		{int i=Mathf.FloorToInt(Random.value*4);
			SoundManager.instance.playSFX(SoundManager.instance.player_talk[i]);}

    }

    // Update is called once per frame
    void Update()
    {



        switch (PlayerTurnFlag)
        {
            case 0:
                input_hang();
                return;


            case 1:
                //=========using tap-receiving detection to call a seperate moving functiong to tell tap from pinch
                //pinch function also called here
                //moving function here
                //after move flag++
                PlayerMoving();
                return;


            case 2:
                GameManager.instance.allow_freecamera = false;
                GameManager.instance.UI_switcher = false;
                StartCoroutine("Moving");
                return;

            case 3:
                StartCoroutine("tap_attacking");
                return;
            case 5:
                //turn ends;
			    StopCoroutine("tap_attacking");
			    GameManager.instance.turn_finish=true;
                GameManager.instance.Player_Score -= 1;
                if (GameManager.instance.Player_Score == 0)
                    if (GameManager.instance.EnemyAllDie())
                        GameManager.instance.Player_Score += 1;
                    else
                    {
                        GameManager.instance.Enemy_Turn = true;

                        if (!GameManager.instance.EnemyAllDie())
                        {
                            GameManager.instance.levelMoves++;
                 if (GameManager.instance.levelMoves - SaveScores.instance.GetOptimalPathValue(LevelLoader.instance.current_level) >= 1)
                     {
                    GameManager.instance.level_Points -= 100;
                     }
                          
                        }//end if enemy_all_die
                    }
               
                Destroy(this.gameObject);
                return;

            default:
                return;
        }

    }


    IEnumerator inputtest()
    {
    
		GameManager.instance.allow_freecamera = true;
		yield return new WaitForSeconds(0.1f);
        PlayerTurnFlag = 0;

    }


    void creatingUIgrids()
    {

        for (int count = 0; count < 8; count++)
            if (canmove[count] == true)
            {
                grid.instance.createOneGrid(next_step[count, 0], next_step[count, 1], UIGrid, this.gameObject);
            }
        GameManager.instance.UI_switcher = true;
    }

    void input_hang()
    {
        if (MovingObject.instance.Receive_Tap())
        {

            Vector3 mousePositionOnScreen = MovingObject.instance.tap_position;
            Vector3 mouseWorld = mousePositionOnScreen;
            for (int count = 0; count < 8; count++)
            {
                //if the grid is available
                if (canmove[count] == true)
                {
                    //calc the distance btween mouse and gridcenter 
                    Vector3 gridpos = grid.instance.calcWorldCoord(new Vector2(next_step[count, 0], next_step[count, 1]));
                    float dist = Mathf.Max(Mathf.Abs(gridpos.x - mouseWorld.x), Mathf.Abs(gridpos.z - mouseWorld.z));

                    //if the mouse position is in the reacting range 
                    if (dist * 2 < grid.instance.hexWidth)
                    {
						SoundManager.instance.playSFX(SoundManager.instance.righttap);
						intend_pos[0] = next_step[count, 0];
                        intend_pos[1] = next_step[count, 1];
                        //print("good intension");
                        PlayerTurnFlag = 1;
                    }
                }
            }//end canmove
        }//end receiving tap

    }


    void PlayerMoving()
    {
        //allow free aspect now
        GameManager.instance.allow_freecamera = true;

        if (MovingObject.instance.Receive_Tap())
        {


            Vector3 mousePositionOnScreen = MovingObject.instance.tap_position;
            Vector3 mouseWorld = mousePositionOnScreen;

            //calc the distance btween mouse and gridcenter 
            Vector3 gridpos = grid.instance.calcWorldCoord(new Vector2(intend_pos[0], intend_pos[1]));
            float dist = Mathf.Max(Mathf.Abs(gridpos.x - mouseWorld.x), Mathf.Abs(gridpos.z - mouseWorld.z));

            //if the mouse position is in the reacting range 
            if (dist * 2 < grid.instance.hexWidth)
            {
				SoundManager.instance.playSFX(SoundManager.instance.wrongtap);
				GameManager.instance.player_pos[0] = intend_pos[0];
                GameManager.instance.player_pos[1] = intend_pos[1];
                //move player there
                //							print("I'm going to"+new Vector2(next_step[count,0],next_step[count,1]));
                //calculate score
                if (grid.instance.scoremap[intend_pos[0], intend_pos[1]] != 0)
                {
                    GameManager.instance.Player_Score += grid.instance.scoremap[intend_pos[0], intend_pos[1]];
                    GameManager.instance.level_Points += grid.instance.scoremap[intend_pos[0], intend_pos[1]] * 100;
                    //reset scoremap
                    grid.instance.scoremap[intend_pos[0], intend_pos[1]] = 0;
                    GameManager.instance.structures_dest++;
                    //stop function playermoving in update
                    StartCoroutine("GetHit");
                    //disable receiving tapping
                    PlayerTurnFlag = 4;
                }
                else
                    PlayerTurnFlag = 2;

            }//end if moving

            else {
                for (int count = 0; count < 8; count++)
                {
                    //if the grid is available
                    if (canmove[count] == true)
                    {
                        //calc the distance btween mouse and gridcenter 
                        gridpos = grid.instance.calcWorldCoord(new Vector2(next_step[count, 0], next_step[count, 1]));
                        dist = Mathf.Max(Mathf.Abs(gridpos.x - mouseWorld.x), Mathf.Abs(gridpos.z - mouseWorld.z));

                        //if the mouse position is in the reacting range 
                        if (dist * 2 < grid.instance.hexWidth)
                        {
							SoundManager.instance.playSFX(SoundManager.instance.righttap);
							intend_pos[0] = next_step[count, 0];
                            intend_pos[1] = next_step[count, 1];
                            //print("good intension");
                        }
                    }
                }//end for count

            }//end else				
        }//end receive tap

    }

    //attacking or not
    bool attacking()
    {
        bool attack = false;
        if (GameManager.instance.Get_PowerUp)
        {


            int[,] next_step = MovingObject.instance.CalIntendPos(player_posnow[0], player_posnow[1]);
            bool[] canmove = MovingObject.instance.CanMove(next_step);
            for (int count = 0; count < 8; count++)
            {
                if (canmove[count] == true)
                    for (int enemyi = 0; enemyi < GameManager.instance.enemy_number; enemyi++)
                    {
                        if (GameManager.instance.Enemy_die[enemyi] == false &&
                            GameManager.instance.enemy_pos[enemyi, 0] == next_step[count, 0]
                            && GameManager.instance.enemy_pos[enemyi, 1] == next_step[count, 1])
                        {

                            killing_i.Add(GameManager.instance.enemy.transform.GetChild(enemyi));
                            GameManager.instance.Enemy_die[enemyi] = true;
                            TileMap.instance.enemyPosNode[enemyi] = null;
                            attack = true;
                        }
                    }//end for
            }
            for (int z = 0; z < killing_i.Count; z++)
            {
                GameManager.instance.level_Points += 2000 + (z * 1000);
            }

        }//end judging power_up
        return attack;
    }

    /*
	bool ispowerup()
	{
		bool powerup=false;
		for (int cnt=0;cnt<grid.instance.powerUPs.GetLength(0);cnt++)
		{
			if (GameManager.instance.player_pos[0]==grid.instance.powerUPs[cnt,0]
			    &&GameManager.instance.player_pos[1]==grid.instance.powerUPs[cnt,1])
				powerup=true;
		}

		return powerup;
	}
   */


    //moving flag=2 to activate
    IEnumerator Moving()
    {
        PlayerTurnFlag = 4;
        //after 1 move decrease player powerupbar
        if (GameManager.instance.Get_PowerUp)
            GameManager.instance.PowerUpBar -= 1;
        Vector3 transpos = grid.instance.calcWorldCoord(new Vector2(GameManager.instance.player_pos[0], GameManager.instance.player_pos[1]));
        GameManager.instance.player.transform.LookAt(new Vector3(transpos.x, 0f, transpos.z));
        GameManager.instance.player.transform.GetComponent<Animator>().SetBool("is_moving", true);

        if (GameManager.instance.EnemyAllDie())
        {
            GameManager.instance.levelMoves++;
if (GameManager.instance.levelMoves - SaveScores.instance.GetOptimalPathValue(LevelLoader.instance.current_level) >= 1)
            {
                GameManager.instance.level_Points -= 100;
            }
           // Debug.Log(GameManager.instance.levelMoves);
        }
        //sound
		SoundManager.instance.playSFX(SoundManager.instance.Player_walk);
		Vector3 init_pos = new Vector3(GameManager.instance.player.transform.position.x, 0f, GameManager.instance.player.transform.position.z);
        for (int cnt = 0; cnt < 20; cnt++)
        {
            GameManager.instance.player.transform.position = new Vector3(init_pos.x + cnt * (transpos.x - init_pos.x) * 0.05f,
                                                                0f, init_pos.z + cnt * (transpos.z - init_pos.z) * 0.05f);
            yield return new WaitForEndOfFrame();
        }
        GameManager.instance.player.transform.position = new Vector3(transpos.x, 0f, transpos.z);
        GameManager.instance.player.transform.GetComponent<Animator>().SetBool("is_moving", false);
        //=============================================attacking==========================

        bool attackvfx = attacking();
        yield return new WaitForEndOfFrame();
        if (attackvfx)
            StartCoroutine("KillingEnemy");
        else
            PlayerTurnFlag = 5;

    }

    //hide the UI grid
    IEnumerator GetHit()
    {
        GameManager.instance.player.transform.GetComponent<Animator>().SetBool("is_attacking", true);
        GameManager.instance.player.transform.GetChild(2).gameObject.SetActive(true);
		if(GameManager.instance.Get_PowerUp==false)
			GameManager.instance.player.transform.GetChild(3).gameObject.SetActive(true);
		//sound
		SoundManager.instance.playSFX(SoundManager.instance.Player_gun);
        Vector3 transpos = grid.instance.calcWorldCoord(new Vector2(GameManager.instance.player_pos[0], GameManager.instance.player_pos[1]));
        GameManager.instance.player.transform.LookAt(new Vector3(transpos.x, 0f, transpos.z));
        for (int cnt = 0; cnt < this.gameObject.transform.childCount; cnt++)
        {
            Transform UIGridChild = this.gameObject.transform.GetChild(cnt);
            UIGridChild.GetComponent<SpriteRenderer>().sprite = null;
        }
        //turn off UI	
        GameManager.instance.UI_switcher = false;

        /*
		if(ispowerup())
			yield return new WaitForEndOfFrame();
		else
		*/
        yield return new WaitForSeconds(0.8f);
        GameManager.instance.player.transform.GetComponent<Animator>().SetBool("is_attacking", false);
		GameManager.instance.player.transform.GetChild(3).gameObject.SetActive(false);
		GameManager.instance.EnemyStun();
		SoundManager.instance.playSFX(SoundManager.instance.enemystunned);
        GameManager.instance.player.transform.GetChild(2).gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        PlayerTurnFlag = 2;

    }

    IEnumerator KillingEnemy()
    {
        for (int cnt = 0; cnt < this.gameObject.transform.childCount; cnt++)
        {
            Transform UIGridChild = this.gameObject.transform.GetChild(cnt);
            UIGridChild.GetComponent<SpriteRenderer>().sprite = null;
        }

        GameManager.instance.UI_switcher = false;
        //turn off UI

        yield return new WaitForEndOfFrame();
        //GameManager.instance.player.transform.GetComponent<Animator>().SetBool("powerup",true);


        foreach (Transform enemytodie in killing_i)
        {
            GameObject attackui = (GameObject)Instantiate(GameManager.instance.attackingui);
            attackui.transform.position = enemytodie.position;
            attackui.transform.SetParent(enemytodie);
            //enemytodie.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.shader = GameManager.instance.Glow;
        }
        //killing_i.Clear();

        //GameManager.instance.player.transform.GetComponent<Animator>().SetBool("powerup",false);
        PlayerTurnFlag = 3;

    }

	IEnumerator tap_attacking()
    {
		PlayerTurnFlag=4;
        MovingObject.instance.quick_tap = true;
        int killcount = 0;
		while(true)
		{
		if(killing_i.Count>0)
		{
			Transform disable_e=null;
		if (MovingObject.instance.Receive_Tap())
        {
             Vector3 mousePositionOnScreen = MovingObject.instance.tap_position;
            Vector3 mouseWorld = mousePositionOnScreen;

            foreach (Transform enemytodie in killing_i)
            {
                float dist = Mathf.Max(Mathf.Abs(enemytodie.position.x - mouseWorld.x), Mathf.Abs(enemytodie.position.z - mouseWorld.z));
                if (dist * 2 < grid.instance.hexWidth)
                {disable_e=enemytodie;}
				
            }//end foreach
				if(disable_e!=null)
				{
				disable_e.GetComponent<Animator>().SetBool("die",true);
                        //instantiate vfx here
                        GameObject die_vfx = Instantiate(explosion);
                        die_vfx.transform.position = new Vector3(disable_e.position.x,2f,disable_e.position.z);
               GameManager.instance.player.transform.GetComponent<Animator>().SetBool("is_attacking", true);
                        killcount += 1;

                            GameObject combo_vfx = Instantiate(bonus);
                            combo_vfx.transform.position = disable_e.position + new Vector3(0f, 8f, 0f);
                            combo_vfx.GetComponent<feed_back>().combos = killcount;
                
                        //sound
                        SoundManager.instance.playSFX(SoundManager.instance.EnemyDistroyed);
				yield return new WaitForSeconds(0.5f);
                 GameManager.instance.player.transform.GetComponent<Animator>().SetBool("is_attacking", false);
                 disable_e.gameObject.SetActive(false);
				killing_i.Remove(disable_e);
				}

			}
          yield return new WaitForEndOfFrame();
		}
		else
			{yield return new WaitForEndOfFrame();
                MovingObject.instance.quick_tap = false;
                PlayerTurnFlag =5;
				}
    }

}//end coroutine tap_attacking


}
