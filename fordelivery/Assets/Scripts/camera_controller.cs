using UnityEngine;
using System.Collections;

public class camera_controller : MonoBehaviour {
	public static  camera_controller instance=null;

	public Camera current_camera;
	public Camera followplayer;
	public Camera followenemy;
	public Camera supersmash;
	public int camera_switcher;
	public int camera_rotater;
	public bool rotating;

	private float camera_angle;
	private float dis_playercamera=15f;
    public float camera_adj = 5f;
    private float dis_fxcamera=7f;
	private float dis_enemycamera=15f;

	private float centerpoint;
    private bool level_start;

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
		followplayer.enabled=true;
		current_camera=followplayer;
		camera_switcher=5;
		camera_rotater=13;
		rotating=false;
        level_start = true;
        followenemy.transform.position = new Vector3(-dis_playercamera, 2 * dis_playercamera, -dis_playercamera);
        /* ======================test only==================================
		//if using control in this camera view, change it to world position transition instead of setparent 
		Vector3 playerposition=GameManager.instance.player.transform.position;
		Vector3 backward=-5f*GameManager.instance.player.transform.forward;

		followplayer.transform.position=playerposition+backward+new Vector3(0f,10f,0f);
		followplayer.transform.LookAt(GameManager.instance.player.transform);

		*/
        current_camera =followplayer;
	}
	
	// Update is called once per frame
	void Update () {


		switch(camera_switcher)
		{
		case 0:
			//
			followenemy.enabled=true;
			followplayer.enabled=false;
			current_camera=followenemy;
			StartCoroutine("CameraFollowEnemy");
			camera_switcher=3;
			// only call it for once;
			return;
		
		case 1:

			followplayer.enabled=true;
			current_camera=followplayer;
			StartCoroutine("CameraFollowPlayer");
			camera_switcher=3;
			// only call it for once;
			return;	
        
		case 2:
		  // supersmash camera
			supersmash.enabled=true;
			followplayer.enabled=false;
			followenemy.enabled=false;
			StartCoroutine("SpecialAttack");
			current_camera=supersmash;
			camera_switcher=3;
			return;
		
		case 5:
			StartCoroutine("StartDelay");
               camera_switcher = 3;
                return;

         case 6:
             
                followplayer.enabled = true;
                current_camera = followplayer;
                StartCoroutine("overview");
                camera_switcher = 3;
                return;


            default:
			return;
		}
	}

	IEnumerator StartDelay()
	{
		yield return new WaitForSeconds(0.02f);
        camera_switcher =6;
	}


    IEnumerator overview()
    {
        Vector3 playerposition = GameManager.instance.player.transform.position;
		centerpoint=grid.instance.gridmap.GetLength(1)*4.6f/2;
		dis_playercamera=1f*Mathf.Max(centerpoint,grid.instance.gridmap.GetLength(0)*4.6f/2);
        camera_angle = camera_rotater * Mathf.PI * 0.02f;
		float camera_radius=2*Vector2.Distance(new Vector2(playerposition.x,playerposition.z),new Vector2(0,0));
	
		Vector3 start_position = new Vector3(0, 2*dis_playercamera,-centerpoint);
	
		Vector3 camera_vector = new Vector3(-dis_playercamera * Mathf.Cos(camera_angle),
			2*dis_playercamera,
			-dis_playercamera * Mathf.Sin(camera_angle));

		Vector3 final_position = playerposition + camera_vector;
        followplayer.transform.position = start_position;
		followplayer.transform.LookAt(new Vector3(0,0,-centerpoint));

        yield return new WaitForSeconds(0.5f);
		GameManager.instance.ready=true;
        //how many frames it takes for the transformation
        int frames = 60;
        for (int cnt = 0; cnt < frames; cnt++)
        {
            followplayer.transform.position=cnt*(final_position-start_position)/frames+start_position;
			followplayer.transform.LookAt(new Vector3(0,0,-centerpoint));
            yield return new WaitForEndOfFrame();
        }

        camera_switcher = 1;
    }

	IEnumerator CameraFollowPlayer()
	{

        if (level_start) { level_start = false; }
        else {
            StopCoroutine("CameraFollowEnemy");
            Vector3 start_pos = followenemy.transform.position;
            camera_angle = camera_rotater * Mathf.PI * 0.02f;
            float start_fov = followenemy.fieldOfView;
            float final_fov = followplayer.fieldOfView;
            int frames = 30;
            Vector3 camera_vector = followplayer.transform.position;
            for (int cnt = 0; cnt < frames; cnt++)
            {
                followplayer.transform.position = start_pos + cnt * (camera_vector - start_pos) / frames;
                followplayer.transform.LookAt(GameManager.instance.player.transform.position);
                followplayer.fieldOfView = start_fov + cnt * (final_fov - start_fov) / frames;
                yield return new WaitForEndOfFrame();
            }
        }

        while (true)
		{
			Vector3 playerposition=GameManager.instance.player.transform.position;
			Vector3 cameraposition=followplayer.transform.position;

			if(GameManager.instance.allow_freecamera==false)
			{
             camera_angle = camera_rotater * Mathf.PI * 0.02f;
				float dist=Vector2.Distance(new Vector2(playerposition.x,playerposition.z),
					                        new Vector2(cameraposition.x,cameraposition.z));
				followplayer.transform.position= playerposition+
                                                 new Vector3(-dis_playercamera* Mathf.Cos(camera_angle), cameraposition.y, -dis_playercamera * Mathf.Sin(camera_angle));
				followplayer.transform.LookAt(playerposition);   
            }
			else
			{
				if(rotating)
			{
				camera_angle = camera_rotater * Mathf.PI * 0.02f;
				float dist=Vector2.Distance(new Vector2(playerposition.x,playerposition.z),
					                        new Vector2(cameraposition.x,cameraposition.z));
				followplayer.transform.position= playerposition+
                                                 new Vector3(-dist* Mathf.Cos(camera_angle), cameraposition.y, -dist * Mathf.Sin(camera_angle));
				followplayer.transform.LookAt(playerposition);
                    // followplayer.transform.LookAt(GameManager.instance.player.transform.position);
                    yield return new WaitForEndOfFrame();
                }
			}
		   yield return new WaitForEndOfFrame();
			
		}
	}

	IEnumerator CameraFollowEnemy()
	{
		StopCoroutine("CameraFollowPlayer");

           // Vector3 playerposition = GameManager.instance.player.transform.position;
           // Vector3 enemyposition=EnemyManager.instance.current_enemy.position;
		Vector3 camera_vector = new Vector3(-dis_playercamera,2 * dis_playercamera,-dis_playercamera);
        Vector3 start_pos = followplayer.transform.position;
        float start_fov = followplayer.fieldOfView;
        float final_fov = followenemy.fieldOfView;
        int frames = 30;
        for (int cnt = 0; cnt < frames; cnt++)
        {
            followenemy.transform.position = start_pos + cnt * (camera_vector - start_pos) / frames;
            followenemy.transform.LookAt(new Vector3(0, 0, -centerpoint));
            followenemy.fieldOfView= start_fov + cnt * (final_fov - start_fov) / frames;
            yield return new WaitForEndOfFrame();
        }

        followenemy.transform.position = camera_vector;
            followenemy.transform.LookAt(new Vector3(0, 0, -centerpoint));
			yield return new WaitForEndOfFrame();

	}

	IEnumerator SpecialAttack()
	{
		while(true)
		{
			Vector3 playerposition=GameManager.instance.player.transform.position;
			supersmash.transform.position=playerposition+new Vector3(-2*dis_fxcamera,dis_fxcamera,-dis_fxcamera);
			supersmash.transform.LookAt(GameManager.instance.player.transform);
			yield return new WaitForEndOfFrame();
		}

	}


    public int calc_theta(float x, float z)
    {
        int rotator;
        if (z == 0) { if (x >= 0) rotator = 0; else rotator = 50; }
        else {
            float theta = Mathf.Atan(x / z);
            if (x <= 0) { theta += Mathf.PI; }
            rotator = Mathf.RoundToInt(theta / (0.02f * Mathf.PI));
			print(rotator);
        }

        return rotator;
    }



}
