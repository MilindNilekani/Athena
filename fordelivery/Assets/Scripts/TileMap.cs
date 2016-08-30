using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour
{

    public static TileMap instance = null;

    //public GameObject[] Enemies;

    public TileType[] tileTypes;


    public List<TileMap.Node>[] enemylist = new List<Node>[100];

    public TileMap.Node[] nextAttackNode = new TileMap.Node[100];
    public TileMap.Node[] nextDefenseNode = new TileMap.Node[100];

    TileMap.Node[] source = new TileMap.Node[100];
    TileMap.Node[] source_dist = new TileMap.Node[100];

    Node temp;
    bool rampageCloud = false;


    public TileMap.Node tt;


    int[] targetTileX_enemy = new int[100];
    int[] targetTileY_enemy = new int[100];

    bool enemy_onPlayer = false;

    public TileMap.Node[] enemyPosNode = new TileMap.Node[100];


    //int enemyTurn=2;
    public Node[,] graph;


    int mapSizeX;
    int mapSizeY;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        //DontDestroyOnLoad (gameObject);
        for (int i = 0; i < enemyPosNode.Length - 1; i++)
        {
            enemyPosNode[i] = new Node();
        }
        for (int i = 0; i < source.Length - 1; i++)
        {
            source[i] = new Node();
        }
        for (int i = 0; i < enemylist.Length - 1; i++)
        {
            enemylist[i] = new List<Node>();
        }
        //CreateEnemyPositionAsNodes();

        SetStartingEnemyNodes();
    }

    void SetStartingEnemyNodes()
    {
        for (int i = 0; i < GameManager.instance.enemy_number; i++)
        {
            SetEnemyNode(GameManager.instance.enemy_pos[i, 0], GameManager.instance.enemy_pos[i, 1], i);
        }
    }

    void Start()
    {

        mapSizeX = grid.instance.gridmap.GetLength(0);
        mapSizeY = grid.instance.gridmap.GetLength(1);

        //		print(mapSizeX+","+mapSizeY);




        GeneratePathfindingGraph();

    }






    public void SetEnemyNode(int x, int y, int ind)
    {
        enemyPosNode[ind].x = x;
        enemyPosNode[ind].y = y;
    }



    public class Node
    {
        public List<Node> neighbours;
        public int x;
        public int y;
        public TileMap map;


        public Node()
        {
            neighbours = new List<Node>();
        }

        public float DistanceTo(Node n)
        {
            if (n == null)
            {
                Debug.LogError("WTF?");
            }
            for (int i = 0; i < GameManager.instance.enemy_number; i++)
            {
                if (TileMap.instance.enemyPosNode[i] == null)
                {
                    continue;
                }
                else
                {
                    if (n.x == TileMap.instance.enemyPosNode[i].x && n.y == TileMap.instance.enemyPosNode[i].y)
                    {
                        //					Debug.Log (TileMap.instance.enemyPosNode [i].x + " " + TileMap.instance.enemyPosNode [i].y + " Infinite from " + x + " " + y);
                        return 15.0f;
                    }
                }
            }
            if (GameManager.instance.Get_PowerUp)
            {
                TileMap.instance.tt = TileMap.instance.graph[GameManager.instance.player_pos[0], GameManager.instance.player_pos[1]];
                foreach (Node v in TileMap.instance.tt.neighbours)
                {
                    if (n.x == v.x && n.y == v.y)
                    {
                        return 15.0f;
                    }
                }
            }

            if (grid.instance.scoremap[(int)n.x, (int)n.y] > 0)
            {
                return Mathf.Infinity;
            }
            return Vector2.Distance(
            new Vector2((int)x, (int)y),
            new Vector2((int)n.x, (int)n.y)
            );
        }
    }



    public void SearchBestNearestNextStep(int x, int y, int index)
    {
        int i = 0;
        int j = 0;
        Node currNode = graph[x, y];
        Node target = graph[GameManager.instance.player_pos[0], GameManager.instance.player_pos[1]];
        float min_dist = Mathf.Infinity;
        foreach (Node v in currNode.neighbours)
        {
            if (currNode.DistanceTo(v) <= Mathf.Sqrt(2))
            {
                i++;
            }
        }

        if (i > 0)
        {
            foreach (Node u in currNode.neighbours)
            {
                j++;
                if (currNode.DistanceTo(u) <= Mathf.Sqrt(2))
                {
                    float distFromTarget = u.DistanceTo(target);
                    if (distFromTarget < min_dist)
                    {
                        min_dist = distFromTarget;
                        temp = u;
                    }
                    //					if(Vector2.Distance(new Vector2(temp.x,temp.y),new Vector2(currNode.x,currNode.y)) > Mathf.Sqrt(2))
                    //					{
                    //						min_dist=Mathf.Infinity;
                    //					}
                }

                else
                {
                    //Debug.Log(u.x + " " + u.y + " Can't go from " + currNode.x + " " + currNode.y);
                    continue;
                }
            }
        }
        else {
            temp = currNode;
        }
        nextAttackNode[index] = temp;

    }


    public bool CheckEnemyNeighbours(int ind)
    {
        Node currNode = graph[(int)GameManager.instance.enemy_pos[ind, 0], (int)GameManager.instance.enemy_pos[ind, 1]];
        Node target = graph[(int)GameManager.instance.player_pos[0], (int)GameManager.instance.player_pos[1]];
        foreach (Node v in currNode.neighbours)
        {
            if (v.x == target.x && v.y == target.y)
            {
                return true;
            }
        }
        return false;
    }

    public void SearchBestFarthestNextStep(int x, int y, int index)
    {
        int i = 0;
        Node currNode = graph[x, y];
        Node target = graph[GameManager.instance.player_pos[0], GameManager.instance.player_pos[1]];
        float max_dist = Mathf.NegativeInfinity;
        foreach (Node v in currNode.neighbours)
        {
            if (currNode.DistanceTo(v) <= Mathf.Sqrt(2))
            {
                i++;
            }
        }
        if (i > 0)
        {
            foreach (Node u in currNode.neighbours)
            {
                if (currNode.DistanceTo(u) <= Mathf.Sqrt(2))
                {
                    float distFromTarget = u.DistanceTo(target);
                    if (distFromTarget > max_dist)
                    {
                        max_dist = distFromTarget;
                        temp = u;
                    }
                    //					if(Vector2.Distance(new Vector2(temp.x,temp.y),new Vector2(currNode.x,currNode.y)) > Mathf.Sqrt(2))
                    //					{
                    //						max_dist=Mathf.NegativeInfinity;
                    //					}
                }

                else
                {
                    Debug.Log(u.x + " " + u.y + " Can't go from " + currNode.x + " " + currNode.y);
                    continue;
                }
            }
        }
        else {
            temp = currNode;
        }
        nextDefenseNode[index] = temp;

    }

    public void GeneratePathfindingGraph()
    {

        graph = new Node[mapSizeX, mapSizeY];


        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }


        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {


                if (x > 0)
                {
                    if (grid.instance.gridmap[(int)graph[x - 1, y].x, (int)graph[x - 1, y].y] == 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        if (grid.instance.gridmap[(int)graph[x - 1, y - 1].x, (int)graph[x - 1, y - 1].y] == 1)
                            graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < mapSizeY - 1)
                        if (grid.instance.gridmap[(int)graph[x - 1, y + 1].x, (int)graph[x - 1, y + 1].y] == 1)
                            graph[x, y].neighbours.Add(graph[x - 1, y + 1]);

                }
                if (x < mapSizeX - 1)
                {
                    if (grid.instance.gridmap[(int)graph[x + 1, y].x, (int)graph[x + 1, y].y] == 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        if (grid.instance.gridmap[(int)graph[x + 1, y - 1].x, (int)graph[x + 1, y - 1].y] == 1)
                            graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < mapSizeY - 1)
                        if (grid.instance.gridmap[(int)graph[x + 1, y + 1].x, (int)graph[x + 1, y + 1].y] == 1)
                            graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }
                if (y > 0)
                {
                    if (grid.instance.gridmap[(int)graph[x, y - 1].x, (int)graph[x, y - 1].y] == 1)
                        graph[x, y].neighbours.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    if (grid.instance.gridmap[(int)graph[x, y + 1].x, (int)graph[x, y + 1].y] == 1)
                        graph[x, y].neighbours.Add(graph[x, y + 1]);
                }

                //				Debug.Log (graph[x,y].neighbours.Count);

            }
        }//end for


    }


    public bool CheckPlayerNeighbours()
    {
        int i = 0;
        Node target = graph[GameManager.instance.player_pos[0], GameManager.instance.player_pos[1]];
        foreach (Node v in target.neighbours)
        {
            if (grid.instance.scoremap[(int)v.x, (int)v.y] > 0)
            {
                i++;
            }
        }
        if (i > 0)
        {
            //			Debug.Log ("Time to run away");
            return true;
        }
        else {
            return false;
        }
    }

    public Vector3 TileCoordToWorldCoord(float x, float y)
    {
        return new Vector3(x, y, 0);
    }



    public void GeneratePathTo(int x, int y, int index)
    {
        // Clear out our unit's old path.
        enemylist[index] = null;

        targetTileX_enemy[index] = x;
        targetTileY_enemy[index] = y;

        //		if (UnitCanEnterTile (x, y) == false) {
        //			return;
        //		}

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();



        List<Node> unvisited = new List<Node>();


        source[index] = graph[
                            GameManager.instance.enemy_pos[index, 0],
                            GameManager.instance.enemy_pos[index, 1]
                            //current position of enemy
                            ];
        //		Debug.Log ("Source1 " + Enemies[0].GetComponent<Unit>().tileX + " " + Enemies[0].GetComponent<Unit>().tileY);


        Node target = graph[
                            x,
                            y
                            ];


        dist[source[index]] = 0;
        prev[source[index]] = null;

        foreach (Node v in graph)
        {
            if (v != source[index])
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }


            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {

            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                float alt = dist[u] + u.DistanceTo(v);
                //float alt = dist[u] + CostToEnterTile(u.x,u.y,v.x,v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }


        List<Node>[] currentPath = new List<Node>[100];
        for (int i = 0; i < currentPath.Length; i++)
        {
            currentPath[i] = new List<Node>();
        }


        Node curr = target;
        if (prev[target] == null)
        {
            //			Debug.Log ("Hello");
            for (int i = 0; i < source[index].neighbours.Count - 1; i++)
            {
                if (source[index].DistanceTo(source[index].neighbours[i]) == 1)
                {
                    currentPath[index].Add(source[index].neighbours[i]);
                    break;
                }
            }
            //return;
        }

        while (curr != null)
        {
            currentPath[index].Add(curr);
            curr = prev[curr];
        }



        currentPath[index].Reverse();

        //		foreach (Node n in currentPath) 
        //		{
        //			Debug.Log(n.x + " " +n.y);
        //		}

        enemylist[index] = currentPath[index];
    }





}