using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public static MovingObject instance=null;
	//record the most recent tap position; 
	public Vector3 tap_position;
	public bool quick_tap;

	int[,] intend_move0= new int[8,2]
	{
		{0,-1},
		{-1,0},
		{0,1},
		{1,1},
		{1,0},
		{1,-1},
		{-1,1},
		{-1,-1}
	};


	/*
	//y%2==0,intend_grids
	int[,] intend_move0= new int[6,2]
	{
		{0,-1},
		{-1,0},
		{0,1},
		{1,1},
		{1,0},
		{1,-1}
	};

	//y%2==1,intend_grids
	int[,] intend_move1= new int[6,2]
	{
		{0,-1},
		{1,0},
		{0,1},
		{-1,1},
		{-1,0},
		{-1,-1}
	};*/

	
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
        quick_tap = false;
		//DontDestroyOnLoad (gameObject);
	}

	
	void Init_matrix(int[,] matrixA)
	{

		for (int r=0;r<matrixA.GetLength(0);r++)
			for (int l=0;l<matrixA.GetLength(1);l++)
				matrixA[r,l]=0;
	}

//calculate the potential grids that the object may move to(6 Hexagon grids around it)
    public int[,] CalIntendPos (int xGrid, int yGrid) {

		int[,] intend_pos = new int[8,2];
		Init_matrix(intend_pos);
		int count=8;
		for (int i=0; i<count; i++)
		{
			intend_pos[i,0] = xGrid+intend_move0[i,0];
			intend_pos[i,1] = yGrid+intend_move0[i,1];
		}
		return intend_pos;
	
	}

//can the object move to all the potential grid?
// results are saved in the bool array Canmove

    public bool[] CanMove(int[,] intend_pos)
	{
		bool[] can_move=new bool[8]{false,false,false,false,false,false,false,false};

		int boundx=grid.instance.gridmap.GetLength(0);
		int boundy=grid.instance.gridmap.GetLength(1);

		for (int count=0; count <8;count++)
		{
			int x=intend_pos[count,0];
			int y=intend_pos[count,1];
			//in the range of map and not the obstacle
			if(x>=0&&x<boundx&&y>=0&&y<boundy&&grid.instance.gridmap[x,y]==1)
				can_move[count]=true;
			else
				can_move[count]=false;
		}

		return can_move;

    }
	
	// Update is called once per frame

	public void SetLayerof(GameObject go, int layerNumber)
	{
		foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
		{
			trans.gameObject.layer = layerNumber;
		}
	}

	public bool Receive_Tap()
	{
#if UNITY_STANDALONE || UNITY_WEBPLAYER

        bool receive_mouse;
        if (quick_tap)
        { receive_mouse = Input.GetMouseButton(0); }
        else
        { receive_mouse = Input.GetMouseButtonDown(0); }

		 if (receive_mouse)
		{
			Ray ray = camera_controller.instance.current_camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100)) 
				Debug.DrawLine(ray.origin, hit.point);
			bool tap=true;
			tap_position=hit.point;
            return tap;  
		}

		else
		{
			bool tap=false;
		    return tap;
		}
	
		//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		//pinch or tap detection

		if (Input.touchCount>0)
		{
			Touch myTouch = Input.touches[0];
			
			//Check if the phase of that touch equals Began
			//this is the normal tap that conflicts with drag
			//if in the coin collecting mode, this should cahnge to touchphase.began
			bool type_tap;
			if (quick_tap)
			{type_tap=(myTouch.phase==TouchPhase.Began); }
			else
			{type_tap=(myTouch.phase==TouchPhase.Began);}


			if (type_tap)
			{
				Ray ray = camera_controller.instance.current_camera.ScreenPointToRay(myTouch.position);
				RaycastHit hit;
				
				if (Physics.Raycast(ray, out hit, 100)) 
					Debug.DrawLine(ray.origin, hit.point);
				bool tap=true;
				tap_position=hit.point;
				return tap;
			}
			else
			{
				bool tap=false;
				return tap;
			}
		}
		else
		{
			bool tap=false;
			return tap;
		}
		
		#endif //End of mobile platform dependendent compilation section started above with #elif
	}


}
