using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scorehex : MonoBehaviour {

	public GameObject Flame;
	public bool process_flag=true;
	Vector3 playerWorldpos;
	Material mat;
	List<Shader> origin;
    MeshRenderer[] trans_chart;

	// Use this for initialization
	void Start () {

        origin = new List<Shader>();

        //loop through all nodes, get all meshrenderer
        trans_chart=GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < trans_chart.GetLength(0); i++)
        {
            origin.Add(trans_chart[i].material.shader);
        }

            StartCoroutine("buildingglow");
        //StartCoroutine("lookaround");
    }
	
	// Update is called once per frame
	void Update () {
 
		int gridx=GameManager.instance.player_pos[0];
		int gridy=GameManager.instance.player_pos[1];
		playerWorldpos=grid.instance.calcWorldCoord(new Vector2(gridx,gridy));

		if(process_flag)
		{
		if (this.transform.position.x==playerWorldpos.x&&this.transform.position.z==playerWorldpos.z)
		{
				if (!GameManager.instance.Get_PowerUp)
				{
				GameManager.instance.PowerUpBar+=1; 
				if(GameManager.instance.PowerUpBar==3)
				  {
					GameManager.instance.PowerUpBar=4;
					GameManager.instance.Get_PowerUp=true;
						//sound
					SoundManager.instance.playdialoge(SoundManager.instance.rampagevoice);
				  }
				}
				StopCoroutine("buildingglow");
				StartCoroutine("destroybuilding");
				
				process_flag=false;
		}//end judge
	   }//end flag
	

	}

    void back_to_origin()
    {
        for (int i = 0; i < trans_chart.GetLength(0); i++)
        {
            mat = trans_chart[i].material;
            mat.shader=origin[i];
        }
    }

    void blinging()
    {
        for (int i = 0; i < trans_chart.GetLength(0); i++)
        {
            mat = trans_chart[i].material;
            mat.shader = GameManager.instance.Glow;
        }
    }

    IEnumerator destroybuilding()
	{

          back_to_origin();
        GameManager.instance.rampageUI = true;
		 GameManager.instance.Score+=1;
		  GameManager.instance.player.transform.GetComponent<Animator>().SetBool("is_moving",false);
		 
		   

		  //change to vfx camera
		  // this.gameObject.layer=8;
		 // camera_controller.instance.camera_switcher=2;


		  yield return new WaitForEndOfFrame();
		  StartCoroutine("ShakeCamera");
			SoundManager.instance.playSFX(SoundManager.instance.Explo);
			GameObject explosion=(GameObject)Instantiate(Flame);
		   // explosion.layer=8;
			explosion.transform.position=transform.position;

		  //during this period activate tap-collect function
		   // StartCoroutine("collectingcoin");
		    MovingObject.instance.quick_tap=true;
		    yield return new WaitForSeconds(0.1f);
		    // StopCoroutine("collectingcoin");
		    MovingObject.instance.quick_tap=false;
		  
		     
		   
		  // camera_controller.instance.camera_switcher=1;

			//Destroy(explosion);
			Destroy(this.gameObject);
			
	}

	IEnumerator ShakeCamera()
	{
		for(int cnt=0;cnt<20;cnt++)
		{
			camera_controller.instance.current_camera.transform.Translate(-0.05f,0f,0f);
			yield return new WaitForSeconds(0.02f);
			camera_controller.instance.current_camera.transform.Translate(0.05f,0f,0f);
			yield return new WaitForSeconds(0.02f);
		}


	}

    IEnumerator lookaround()
    {
        while (true)
        {
            float rotate_speed = 5 * (Random.value - 0.5f);
            for(int cnt = 0;cnt < 10;cnt++)
            {
                transform.GetChild(0).Rotate(0f, rotate_speed, 0f);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(Random.value);
        }
    }


    IEnumerator buildingglow()
	{
		yield return new WaitForSeconds(0.2f);
	
		while (true)
		{
		if(GameManager.instance.UI_switcher)
		{	
			float player_near=Vector2.Distance(new Vector2(this.transform.position.x,this.transform.position.z),new Vector2(playerWorldpos.x,playerWorldpos.z));
			if(player_near<1.5f*grid.instance.hexWidth)
			{
				yield return new WaitForSeconds(0.4f);
                 blinging();
				yield return new WaitForSeconds(0.4f);
				back_to_origin();
				
		    }

	    }//end if/while
            back_to_origin();
			yield return new WaitForEndOfFrame();
		}//end while 
	}


}
