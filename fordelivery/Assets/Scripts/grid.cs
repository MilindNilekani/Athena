using UnityEngine;
using System.Collections;

public class grid: MonoBehaviour
{
	/*
	 * This is the grid manager, which includes:
	 * gridmap: normal_grids and obstacles
	 * calWorldCoord(): to calc the world position of the existing grid position 
	 */

	//allow others to call functions from grid
	public static grid instance=null;



	void Awake()
	{
		if (instance==null)
			instance=this;
		else if (instance!=this)
			Destroy(gameObject);
		//DontDestroyOnLoad(gameObject);		
	}



	// hex model prefab;
	public GameObject Hex;
	//obstacles in the map;
	public GameObject Obstables;
	

	public GameObject[] score1point;
	public GameObject[] score2points;
	public GameObject[] score3points;


	

	//map version1

	/*
 public int[,] gridmap= new int[5,4]
	{
		{1,1,1,1},
		{1,1,1,1},
		{1,1,1,1},
		{1,1,1,1},
		{1,0,1,0},
	};

	public int[,] scoremap= new int[5,4]
	{
		{0,3,0,0},
		{1,0,0,2},
		{0,1,0,1},
		{0,2,0,0},
		{0,0,0,0}
	};
   */


	//bigger map

	public int[,] gridmap;
	public int[,] scoremap;
	public int[,] powerUPs;

	//change it when 
	 int gridWidthInHexes;
	 int gridHeightInHexes;
	
	//Hexagon tile width and height in game world
	public float hexWidth;
	private float hexHeight;
	
	//Method to initialise Hexagon width and height
	void setSizes()
	{
		gridWidthInHexes=gridmap.GetLength(0);
		gridHeightInHexes=gridmap.GetLength(1);

		//renderer component attached to the Hex prefab is used to get the current width and height


		hexWidth = Hex.GetComponent<Renderer>().bounds.size.x;
		print(hexWidth);
		hexHeight = Hex.GetComponent<Renderer>().bounds.size.z;
		print(hexHeight);
	}
	
	//Method to calculate the position of the first hexagon tile
	//The center of the hex grid is (0,0,0)
	Vector3 calcInitPos()
	{
		Vector3 initPos;
		//the initial position will be in the left upper corner
		initPos = new Vector3(-hexWidth * gridWidthInHexes / 2f +hexWidth / 2,0, gridHeightInHexes / 2f * hexHeight - hexHeight / 2);
		
		return initPos;
	}

	//get different hex according to the gridmap; 
	GameObject GetHex(int id){
		switch(id){
		case 1:
				return Hex;
		case 0:
			return Obstables;
		default:
			  return Hex;
		}
	}

	GameObject random_building(GameObject[] gameoblist)
	{
		int total=gameoblist.GetLength(0);
		int pick=(int)(total*Random.value);
		return gameoblist[pick];
	}

	//get different hex according to the scoremap
	GameObject GetScoreHex(int id){
		switch(id){
		case 1:
			return random_building(score1point);
		case 2:
			return random_building(score2points);
		case 3:
			return random_building(score3points);
		default:
			return null;
		}
	}
	
	
	//method used to convert hex grid coordinates to game world coordinates
	public Vector3 calcWorldCoord(Vector2 gridPos)
	{
		//Position of the first hex tile
		Vector3 initPos = calcInitPos();
		//square grid offset=0;
		//if (gridPos.y % 2 != 0)
		//	offset = hexWidth / 2;
		
		float x =  initPos.x + gridPos.x * hexWidth;
		float y = initPos.y - gridPos.y * hexHeight;
		return new Vector3(x,0,y);
	}
	
	//Finally the method which initialises and positions all the tiles
	void createGrid()
	{
		//Game object which is the parent of all the hex tiles
		GameObject hexGridGO = new GameObject("HexGrid");
		
		for (int y = 0; y < gridHeightInHexes; y++)
		{
			for (int x = 0; x < gridWidthInHexes; x++)
			{
				//GameObject assigned to Hex public variable is cloned
				GameObject hex = (GameObject)Instantiate(GetHex(gridmap[x,y]));

				//Current position in grid
				Vector2 gridPos = new Vector2(x, y);
				hex.transform.position = calcWorldCoord(gridPos);
				hex.transform.parent = hexGridGO.transform;
			}
		}
	}

	void createScoreGrid()
	{
		//Game object which is the parent of all the hex tiles

		GameObject ScoreGrid = new GameObject("ScoreGrid");
		
		for (int y = 0; y < gridHeightInHexes; y++)
		{
			for (int x = 0; x < gridWidthInHexes; x++)
			{
				//GameObject assigned to Hex public variable is cloned
				if(scoremap[x,y]!=0)
				{
					
					GameObject hex = (GameObject)Instantiate(GetScoreHex(scoremap[x,y]));
					//hex.transform.localScale=new Vector3(1f,(float)scoremap[x,y],1f);	
				//Current position in grid
				Vector2 gridPos = new Vector2(x, y);
				Vector3 worldpos=calcWorldCoord(gridPos);
					hex.transform.position =new Vector3(worldpos.x,0.1f,worldpos.z) ;
				//hex.transform.Translate(0,0.5f*hex.GetComponent<MeshRenderer>().bounds.size.z,0);
				hex.transform.parent = ScoreGrid.transform;
				}//end if
			}//end for x
		}//end for y
	}
	
	public void createOneGrid(int xGrid, int yGrid, GameObject HexType, GameObject Parent)
	{
		GameObject hex = (GameObject)Instantiate(HexType);
		Vector3 gridpos=grid.instance.calcWorldCoord(new Vector2(xGrid,yGrid));
		hex.transform.position =new Vector3(gridpos.x,0.01f,gridpos.z) ;
		hex.transform.parent = Parent.transform;
	}



	//The grid should be generated on game start
	void Start()
	{
		gridmap=LevelLoader.instance.Levelgridmap;
		scoremap=LevelLoader.instance.Levelscoremap;
		//powerUPs=LevelLoader.instance.LevelpowerUPs;
		StartCoroutine("Latestart");
	}

	IEnumerator Latestart()
	{
		yield return new WaitForEndOfFrame();
		setSizes();
		createScoreGrid();
		createGrid();
	}

}
