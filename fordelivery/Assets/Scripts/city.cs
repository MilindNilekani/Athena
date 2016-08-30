using UnityEngine;
using System.Collections;

public class city : MonoBehaviour {
	public GameObject straightroad;
	public GameObject crossroad;
	public GameObject noroad;
	public GameObject[] dec_placeholder;

	private int[,] roadbuilder;
	private int[,] Archbuilder;
	// Use this for initialization
	void Start () {
		 StartCoroutine("road_build");
	
	}


	// Update is called once per frame
	IEnumerator road_build () {
		yield return new WaitForSeconds(0.1f);
		//waiting for the reading of datas
		Archbuilder=new int[grid.instance.scoremap.GetLength(0),grid.instance.scoremap.GetLength(1)];
		copymatrix(grid.instance.scoremap,Archbuilder);

		/*
		for (int i=0;i<grid.instance.powerUPs.GetLength(0);i++)
		Archbuilder[grid.instance.powerUPs[i,0],grid.instance.powerUPs[i,1]]=0;
        */

		roadbuilder=new int[Archbuilder.GetLength(0),Archbuilder.GetLength(1)];
		clearmatrix(roadbuilder);
	//	generate_roadmap();
		build_road();
		//beautify();
		//


	
		//read the grid map and score map
		//build up the roads
	  /*
	   * garbage is for decorating in the future;
	   * cars, small facilities
		GameObject garbage=new GameObject("Garbage");
	    */

	}

	void copymatrix(int[,]matrix,int[,] target)
	{
		for(int x=0;x<matrix.GetLength(0);x++)
			for (int y=0;y<matrix.GetLength(1);y++)
				target[x,y]=matrix[x,y];
		
	}

	void addmatrix(int[,]matrix,int[,] target)
	{
		for(int x=0;x<matrix.GetLength(0);x++)
			for (int y=0;y<matrix.GetLength(1);y++)
				target[x,y]+=matrix[x,y];
		
	}
  
	void flipmatrix(int[,]matrix)
	{
		for(int x=0;x<matrix.GetLength(0);x++)
			for (int y=0;y<matrix.GetLength(1);y++)
		     { 
				if(matrix[x,y]==0)
					matrix[x,y]=1;
		         else
			     matrix[x,y]=0;
		      }
	}


	void clearmatrix(int[,]matrix)
	{
		for(int x=0;x<matrix.GetLength(0);x++)
			for (int y=0;y<matrix.GetLength(1);y++)
				matrix[x,y]=0;

	}

	bool horizontal_road(int y)
	{
		bool horizontal_road=true;
		int blankcount=0;
		for (int x=0;x<Archbuilder.GetLength(0);x++)
		{
			//Debug.Log("horizontal road grid"+x+","+y+" : "+Archbuilder[x,y]);
			if (Archbuilder[x,y]>0)	
			{
	            horizontal_road=false;
			Debug.Log(y+"is not a qualified horizontal road at #"+x);
			}
			else if (grid.instance.gridmap[x,y]==0)
				blankcount+=1;
		}
		if (blankcount>(Archbuilder.GetLength(0)/2))
		{
			horizontal_road=false;
			Debug.Log(y+"is not a qualified horizontal road because of too many unwalkable grids");
		}

		return horizontal_road;
	}

	bool vertical_road(int x)
	{
			bool vertical_road=true;
		    int blankcount=0;
			for (int y=0;y<Archbuilder.GetLength(1);y++)
			{
			//Debug.Log("vertical road grid"+x+","+y+" : "+Archbuilder[x,y]);
			   if (Archbuilder[x,y]>0)
			     {
					vertical_road=false;
				Debug.Log(x+"is not a qualified vertical road at #"+y);
			      }
			else if (grid.instance.gridmap[x,y]==0)
				blankcount+=1;
			}
		if (blankcount>(Archbuilder.GetLength(1)/2))
		{
			vertical_road=false;
			Debug.Log(x+"is not a qualified vertical road because of too many unwalkable grids");
		}

			return vertical_road;
	}

	void fill(int[,]matrix, int x,int result, bool row)
	{
		if (row==true)
		{
			for(int y=0;y<matrix.GetLength(0);y++)
				matrix[y,x]+=result;
		}else
		{
			for(int y=0;y<matrix.GetLength(1);y++)
				matrix[x,y]+=result;
		}

	}

	void generate_roadmap()
	{
		//traverse rows
		for (int y=0;y<Archbuilder.GetLength(1);y++)
		{
			if (horizontal_road(y))
			{
				fill (roadbuilder,y,1,true);
				Debug.Log("horizontal road: "+y);
			}
		}

		for (int x=0;x<Archbuilder.GetLength(0);x++)
		{
			if (vertical_road(x))
			{
				fill (roadbuilder,x,2,false);
			Debug.Log("vertical road: "+x);
			}
		}

	}

	GameObject GetRoad(int id){
		switch(id){
		case 0:
			return noroad;
		case 1:
			return straightroad;
		case 2:
			return straightroad;
		case 3:
			return crossroad;
		default:
			return null;
		}
	}


	GameObject random_tile_building()
	{
		int total=dec_placeholder.GetLength(0);
		int pick=(int)(total*Random.value);
		return dec_placeholder[pick];
	}

	void build_road()
	{

		GameObject beautifulworld=new GameObject("beautifulworld");
		for (int x=0;x<roadbuilder.GetLength(0);x++)
			for (int y=0;y<roadbuilder.GetLength(1);y++)
				if (grid.instance.gridmap[x,y]==1)
		{
		GameObject road= (GameObject)Instantiate(GetRoad(roadbuilder[x,y]));
		//Current position in grid
		Vector2 gridPos = new Vector2(x, y);
		road.transform.position = grid.instance.calcWorldCoord(gridPos);
		road.transform.Translate(0,-0.51f*road.transform.localScale.y,0);
		road.transform.parent = beautifulworld.transform;
				//vertical road,rotate
				if(roadbuilder[x,y]==2)
					road.transform.Rotate(0,90,0);
		}

	}

	void beautify()
	{
		GameObject nonsense=new GameObject("nonsense");
		addmatrix(roadbuilder,Archbuilder);
		flipmatrix(Archbuilder);
		for (int x=0;x<Archbuilder.GetLength(0);x++)
			for (int y=0;y<Archbuilder.GetLength(1);y++)
				if(Archbuilder[x,y]>0)
			{
		     GameObject decoration=(GameObject)Instantiate(noroad);
				Vector2 gridPos = new Vector2(x, y);
				decoration.transform.position = grid.instance.calcWorldCoord(gridPos);
				//decoration.transform.localScale=new Vector3(4.6f,4*Random.value,4.6f);
				//decoration.transform.Translate(0,-0.5f*decoration.GetComponent<MeshRenderer>().bounds.extents.y,0);
				decoration.transform.parent = nonsense.transform;
			}

	}



}
