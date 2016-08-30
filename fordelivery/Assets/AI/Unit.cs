using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;

	float x_temp, y_temp;
	float x_temp_ahead, y_temp_ahead;

	public List<TileMap.Node> currentPath = null;
	void Start()
	{
//		tileX = 7;
//		tileY = 7;
	}
	void Update()
	{
		if (currentPath != null) 
		{
			int currNode=0;
			while(currNode<currentPath.Count-1)
			{

				if(currentPath[currNode].y%2==1)
				{
					x_temp=currentPath[currNode].x*2.2f+1.1f;
					y_temp=currentPath[currNode].y*1.9f;
				}
				else
				{
					x_temp=currentPath[currNode].x*2.2f;
					y_temp=currentPath[currNode].y*1.9f;
				}
				if(currentPath[currNode+1].y%2==1)
				{
					x_temp_ahead=currentPath[currNode+1].x*2.2f+1.1f;
					y_temp_ahead=currentPath[currNode+1].y*1.9f;
				}
				else
				{
					x_temp_ahead=currentPath[currNode+1].x*2.2f;
					y_temp_ahead=currentPath[currNode+1].y*1.9f;
				}
				Vector2 start=map.TileCoordToWorldCoord(x_temp,y_temp);
				Vector2 end=map.TileCoordToWorldCoord(x_temp_ahead,y_temp_ahead);
				Debug.DrawLine(start,end,Color.red);
				currNode++;
			}
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			MoveNextTile();
				}
	}
	

	public void MoveNextTile()
	{
		if (currentPath == null) {
			return;
				}
		if (currentPath [1].y % 2 == 1) {
						transform.position = map.TileCoordToWorldCoord (currentPath [1].x * 2.2f+1.1f, currentPath [1].y * 1.9f);

		} else {
					transform.position=map.TileCoordToWorldCoord(currentPath[1].x*2.2f,currentPath[1].y*1.9f);

		}
		Debug.Log("tere chunariyaa   :"+currentPath[1].x + " " + currentPath[1].y);
		tileX = currentPath [1].x;
		tileY = currentPath [1].y;
		currentPath.RemoveAt (0);
		if (currentPath.Count == 1) {
			currentPath=null;
				}

	}
}
