using UnityEngine;
using System.Collections;


public class EnemyManager : MonoBehaviour {



	//public TileMap map;
	//This is used for test now contemporary
	//instead of AI

	//int[] enemy_posnow=new int[2] {0,0};
    int enemy_num;
	public Transform current_enemy;
    public Material moving;

    public Material normal_mat;




    public static EnemyManager instance=null;
	
	int EnemyTurnFlag=0;	
	

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		//DontDestroyOnLoad (gameObject);
	}


	// Use this for initialization
	void Start () {
		enemy_num=0;
	
		//enemy_posnow=new int[2]{GameManager.instance.enemy_pos[enemy_num,0],GameManager.instance.enemy_pos[enemy_num,0]};
		StartCoroutine("enemyturnText");
		enemy_num=0;
	}
	
	// Update is called once per frame
	void Update () {
		switch(EnemyTurnFlag)
		{
			
		case 1:
			//moving function here
			//start coroutine for one time
			if (enemy_num<GameManager.instance.enemy_number)
			{
			StartCoroutine("EnemyMoving");
			EnemyTurnFlag=3;
			}
			else
				EnemyTurnFlag=2;
			return;

		case 2:
			//turn ends;
			GameManager.instance.Player_Score+=1;
			camera_controller.instance.camera_switcher=1;
			GameManager.instance.Enemy_Turn=false;
			Destroy(this.gameObject);
			return;
			
		default:
			return;
		}
	
	}


    //a smoothing camera move towards the current moving object
 


	

	IEnumerator enemyturnText()
	{
		GameManager.instance.allow_freecamera=false;
        camera_controller.instance.camera_switcher = 0;

        yield return new WaitForSeconds(0.8f);
	
		EnemyTurnFlag+=1;


	}


	IEnumerator EnemyMoving()
	{
		
		
		if (GameManager.instance.Enemy_die [enemy_num] == false) {
           

			current_enemy = GameManager.instance.enemy.transform.GetChild (enemy_num);

            //shading change
            MeshRenderer[] rends = current_enemy.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rend in rends)
            {
                rend.material = moving;
            }
            SkinnedMeshRenderer[] rends_skin = current_enemy.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer rend_skin in rends_skin)
            {
                rend_skin.material = moving;
            }



			
			if (!GameManager.instance.Get_PowerUp) {
				
				/*if (GameManager.instance.PowerUpBar == 2 && TileMap.instance.CheckPlayerNeighbours ()) 
				{
					if(TileMap.instance.CheckEnemyNeighbours(enemy_num))
					{
						TileMap.instance.SearchBestNearestNextStep (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);

						GameManager.instance.enemy_pos [enemy_num, 0] = (int)TileMap.instance.nextAttackNode [enemy_num].x;
						GameManager.instance.enemy_pos [enemy_num, 1] = (int)TileMap.instance.nextAttackNode [enemy_num].y;
					
						TileMap.instance.SetEnemyNode (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);

					}
					else
					{
						TileMap.instance.SearchBestFarthestNextStep (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);

						GameManager.instance.enemy_pos [enemy_num, 0] = (int)TileMap.instance.nextDefenseNode [enemy_num].x;
						GameManager.instance.enemy_pos [enemy_num, 1] = (int)TileMap.instance.nextDefenseNode [enemy_num].y;

						TileMap.instance.SetEnemyNode (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);
						
					}
				}
				else {*/
					if(TileMap.instance.CheckEnemyNeighbours(enemy_num))
					{
						TileMap.instance.SearchBestNearestNextStep (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);

						GameManager.instance.enemy_pos [enemy_num, 0] = (int)TileMap.instance.nextAttackNode [enemy_num].x;
						GameManager.instance.enemy_pos [enemy_num, 1] = (int)TileMap.instance.nextAttackNode [enemy_num].y;

						TileMap.instance.SetEnemyNode (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);
					}
					else
					{
						
						TileMap.instance.SearchBestNearestNextStep (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);

						GameManager.instance.enemy_pos [enemy_num, 0] = (int)TileMap.instance.nextAttackNode [enemy_num].x;
						GameManager.instance.enemy_pos [enemy_num, 1] = (int)TileMap.instance.nextAttackNode [enemy_num].y;
						
						TileMap.instance.SetEnemyNode (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);
					}
				//}
			} 
			else 
			{
				TileMap.instance.SearchBestFarthestNextStep (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);

				GameManager.instance.enemy_pos [enemy_num, 0] = (int)TileMap.instance.nextDefenseNode [enemy_num].x;
				GameManager.instance.enemy_pos [enemy_num, 1] = (int)TileMap.instance.nextDefenseNode [enemy_num].y;
				TileMap.instance.SetEnemyNode (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1], enemy_num);
			}


			//YOU DON'T NEED TO REPEAT THIS FOR FOUR TIMES.


			Vector3 transenemy = grid.instance.calcWorldCoord (new Vector2 (GameManager.instance.enemy_pos [enemy_num, 0], GameManager.instance.enemy_pos [enemy_num, 1]));
			GameManager.instance.enemy.transform.GetChild (enemy_num).LookAt (new Vector3 (transenemy.x, 0f, transenemy.z));
			Vector3 initial_pos = new Vector3 (GameManager.instance.enemy.transform.GetChild (enemy_num).position.x, 0f, GameManager.instance.enemy.transform.GetChild (enemy_num).position.z);
            GameManager.instance.enemy.transform.GetChild(enemy_num).GetComponent<Animator>().SetBool("is_walking", true);
			SoundManager.instance.playSFX(SoundManager.instance.monstersound);
            for (int cnt=0; cnt<20; cnt++) 
			{
				GameManager.instance.enemy.transform.GetChild (enemy_num).position = new Vector3 (initial_pos.x + cnt * (transenemy.x - initial_pos.x) * 0.05f,
					0f, initial_pos.z + cnt * (transenemy.z - initial_pos.z) * 0.05f);
				yield return new WaitForEndOfFrame();
			}
            GameManager.instance.enemy.transform.GetChild(enemy_num).GetComponent<Animator>().SetBool("is_walking", false);
            //indicator
           
          
            foreach (MeshRenderer rend in rends)
            { rend.material = normal_mat;}
            foreach (SkinnedMeshRenderer rend_skin in rends_skin)
            { rend_skin.material = normal_mat; }

            GameManager.instance.enemy.transform.GetChild (enemy_num).position = new Vector3 (transenemy.x, 0f, transenemy.z);
			if(GameManager.instance.player_pos[0]==GameManager.instance.enemy_pos [enemy_num, 0]&&
				GameManager.instance.player_pos[1]==GameManager.instance.enemy_pos [enemy_num, 1])
				Destroy(this.gameObject);
		}

        
        yield return new WaitForEndOfFrame();
		   enemy_num+=1;
		   EnemyTurnFlag=1;
		
		
		
		
	}
}
