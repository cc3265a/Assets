using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
    public int width, height;
    public Material brick;
    public Material brick2;


    int BTCount = 0;

    public int displace = 25;

    public int many = 0;

    public int sizeInt = 30;
    private int[,] Maze;
    private List<Vector3> pathMazes = new List<Vector3>();
    private Stack<Vector2> _tiletoTry = new Stack<Vector2>();
    private List<Vector2> offsets = new List<Vector2> { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
    private System.Random rnd = new System.Random();
    private int _width, _height;
    private Vector2 _currentTile;
    public Vector2 CurrentTile
    {
        get { return _currentTile; }
        private set
        {
            if (value.x < 1 || value.x >= this.width - 1 || value.y < 1 || value.y >= this.height - 1)
            {
                throw new ArgumentException("CurrentTile must be within the one tile border all around the maze");
            }
            if (value.x % 2 == 1 || value.y % 2 == 1)
            { _currentTile = value; }
            else
            {
                throw new ArgumentException("The current square must not be both on an even X-axis and an even Y-axis, to ensure we can get walls around all tunnels");
            }
        }
    }

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = sizeInt;


        GenBinary();

        GenAB(); 

        for (int i = 0; i < 3; i++)
        {
            GenerateMazeBacktrack();
            many++;
        }

    }
    
    void GenBinary()
    {
        GameObject ptype = null;
        var genMaze = GenerateMazeBinary();
        for (int i = 0; i <= width-1; i++)
        {
            for (int j = 0; j <= height-1; j++)
            {
                if (genMaze[i, j] == 1)
                {
                    ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ptype.transform.position = new Vector3(i * ptype.transform.localScale.x, j * ptype.transform.localScale.y, 0);
                    if (brick != null)
                    {
                        ptype.GetComponent<Renderer>().material = brick;
                        if (i == 2 || j == 2)
                    {
                        ptype.GetComponent<Renderer>().material = brick2;
                    }
                    }
                    ptype.transform.parent = transform;
                    
                }
                else if (genMaze[i, j] == 0)
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }

            }
        }
        var genMaze2 = GenerateMazeBinary();
        for (int i = 0; i <= width-1; i++)
        {
            for (int j = 0; j <= height-1; j++)
            {
                if (genMaze2[i, j] == 1)
                {
                    ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ptype.transform.position = new Vector3(i * ptype.transform.localScale.x + displace, j * ptype.transform.localScale.y, 0);
                    if (brick != null)
                    {
                        ptype.GetComponent<Renderer>().material = brick;
                        if (i == 2 || j == 2)
                    {
                        ptype.GetComponent<Renderer>().material = brick2;
                    }
                    }
                    ptype.transform.parent = transform;
                    
                }
                else if (genMaze2[i, j] == 0)
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }

            }
        }
        var genMaze3 = GenerateMazeBinary();
        for (int i = 0; i <= width-1; i++)
        {
            for (int j = 0; j <= height-1; j++)
            {
                if (genMaze3[i, j] == 1)
                {
                    ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ptype.transform.position = new Vector3(i * ptype.transform.localScale.x + displace*2, j * ptype.transform.localScale.y, 0);
                    if (brick != null)
                    {
                        ptype.GetComponent<Renderer>().material = brick;
                        if (i == 2 || j == 2)
                    {
                        ptype.GetComponent<Renderer>().material = brick2;
                    }
                    }
                    ptype.transform.parent = transform;
                    
                }
                else if (genMaze3[i, j] == 0)
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }

            }
        }
    }

    void GenAB()
    {
        GameObject ptype = null;
        var genMazeAB1 = GenerateMazeAB();
        for (int i = 0; i <= width-1; i++)
        {
            for (int j = 0; j <= height-1; j++)
            {
                if (genMazeAB1[i, j] == 1)
                {
                    ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ptype.transform.position = new Vector3(i * ptype.transform.localScale.x, j * ptype.transform.localScale.y + displace, 0);
                    if (brick != null)
                    {
                        ptype.GetComponent<Renderer>().material = brick;
                        if (i == 2 || j == 2)
                    {
                        ptype.GetComponent<Renderer>().material = brick2;
                    }
                    }
                    ptype.transform.parent = transform;
                    
                }
                else if (genMazeAB1[i, j] == 0)
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }

            }
        }

        var genMazeAB2 = GenerateMazeAB();
        for (int i = 0; i <= width-1; i++)
        {
            for (int j = 0; j <= height-1; j++)
            {
                if (genMazeAB2[i, j] == 1)
                {
                    ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ptype.transform.position = new Vector3(i * ptype.transform.localScale.x + displace, j * ptype.transform.localScale.y + displace, 0);
                    if (brick != null)
                    {
                        ptype.GetComponent<Renderer>().material = brick;
                        if (i == 2 || j == 2)
                    {
                        ptype.GetComponent<Renderer>().material = brick2;
                    }
                    }
                    ptype.transform.parent = transform;
                    
                }
                else if (genMazeAB2[i, j] == 0)
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }

            }
        }
        var genMazeAB3 = GenerateMazeAB();
        for (int i = 0; i <= width-1; i++)
        {
            for (int j = 0; j <= height-1; j++)
            {
                if (genMazeAB3[i, j] == 1)
                {
                    ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ptype.transform.position = new Vector3(i * ptype.transform.localScale.x + displace*2, j * ptype.transform.localScale.y + displace, 0);
                    if (brick != null)
                    {
                        ptype.GetComponent<Renderer>().material = brick;
                        if (i == 2 || j == 2)
                    {
                        ptype.GetComponent<Renderer>().material = brick2;
                    }
                    }
                    ptype.transform.parent = transform;
                    
                }
                else if (genMazeAB3[i, j] == 0)
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }

            }
        }
    }
    
    
    int[,] GenerateMazeBinary()
    {
        int BCount = 0;
        int[,] genMaze = new int[width,height];

        for (int i = 0; i <= width-1; i++)
        {
            for (int j = 0; j <= height-1; j++)
            {
                if (!(i % 2 ==1 && j % 2 == 1)){
                    genMaze[i,j] = 1;
                }
                BCount++;
            }
        }

       for (int i = 1; i < width; i += 2)
        {
            for (int j = 1; j < height; j += 2)
            {
                BCount++;
                int right = (int)UnityEngine.Random.Range(0,2);
                if (j == height - 2)
                {
                    right = 1;
                }
                if (i == width - 2)
                {
                    right = 0;
                }
                if (j == height - 2 && i == width - 2)
                {
                    break;
                }

                if (right == 1)
                {
                    genMaze[i+1,j] = 0;
                }
                else
                {
                    genMaze[i,j+1] = 0;
                }
                // print("x = " + i + " y = " + j + "choice: " + right);
            }
        }
        genMaze[0,1] = 0;
        genMaze[width -1, height -2] = 0;
        // print("BCount = " + BCount);
        return genMaze;
    }

    int[,] GenerateMazeAB()
    {
        int ABCount = 0;
        //new 2d array of width and height
        int[,] genMaze = new int[width,height];
        int unvisited = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                genMaze[x,y] = 1;
                if (x % 2 ==1 && y % 2 == 1)
                {
                    unvisited++;
                }
                ABCount++;
            }
        }

        // int[] on;
        Vector2 onVec;
        do
        {
            onVec.x = (int)UnityEngine.Random.Range(0,width);
            onVec.y = (int)UnityEngine.Random.Range(0,height);
            // on[0] = (int)Mathf.Floor(UnityEngine.Random.Range(0,2) * height);
            // on[1] = (int)Mathf.Floor(UnityEngine.Random.Range(0,2) * width);
            ABCount++;
        }while (onVec.x % 2 == 0 || onVec.y % 2 == 0);

        Vector2 holdVec = new Vector2((int)onVec.x, (int)onVec.y);
        genMaze[(int)holdVec.x, (int)holdVec.y] = 1;
        unvisited--;

        //List of Vectors unvisited
        List<Vector2> unvisitedArr = new List<Vector2>();
        for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x % 2 ==1 && y % 2 == 1)
                    {
                        if(genMaze[x,y] == 1){
                            Vector2 unvisVec = new Vector2(x,y);
                            unvisitedArr.Add(unvisVec);
                        }
                    }
                    ABCount++;
                }
            }

        int breakCount = 0;
        // while (unvisitedArr.Count != 0)
        int CRASH = unvisitedArr.Count*3;
        int MAXCRASH = 100000;
        // print("ENTERING WHILE");
        while(MAXCRASH > 0)
        {
            ABCount++;
            MAXCRASH--;
            if (MAXCRASH <= 0)
            {
                print("GIVING UP");
                break;
            }
            // print("while" + unvisitedArr.Count);
            if (unvisitedArr.Count > CRASH)
            {
                CRASH = unvisitedArr.Count;
                // print("WAGH");
            }
                
            var n = neighborsAB(genMaze, onVec);
            // print("n count is = " + n.Count);
            if (n.Count == 0)
            {
                breakCount++;
                // print("break count = " + breakCount);
                continue;
            }
            int holdRand = UnityEngine.Random.Range(0,n.Count-1);
            Vector2 to = n[holdRand];
            // print("to is " + to);
            int holdOOBX = (int)to.x;
            int holdOOBY = (int)to.y;
            bool OOB = (holdOOBX >= width) || (holdOOBY >= height);

            // print("x = " + holdOOBX);
            // print("y = " + holdOOBY);
            // print("width = " + width);
            // print("hieght = " + height);


            if (OOB == false)
            {
              if (genMaze[(int)to.x, (int)to.y] == 1)
                {
                    genMaze[(int)to.x, (int)to.y] = 0;
                    unvisited--;
                    int holdX = (int)to.x;
                    int holdY = (int)to.y;
                    
                    // int holdX = ((int)to.x + (int)onVec.x);
                    // int holdY = ((int)to.y + (int)onVec.y);

                    Vector2 minusVec = to - onVec;
                    minusVec = minusVec/2;
                    // print(minusVec + "= minusVec");
                    Vector2 wallVec = to - minusVec;
                    
                    // print("toVec is "+ to);

                    // print("x = " + holdX);
                    // print("y = " + holdY);
                    genMaze[holdX, holdY] = 0;
                    genMaze[(int)wallVec.x, (int)wallVec.y] = 0;
                    // print("toVec setting (" + holdX + ", " + holdY + ") to zero");
                    unvisitedArr.Remove(to);
                    
                }  
            }
            // unvisited--;
            onVec = to;
            // print("CRASH = " + CRASH);
            CRASH--;
        }
        // print("LEAVING");

        genMaze[0,1] = 0;
        genMaze[height -1, width -2] =0;


        // for (int i = 0; i <= width-1; i++)
        // {
        //     for (int j = 0; j <= height-1; j++)
        //     {
        //         // print("thing " + genMaze[i,j]);
        //     }
        // }
        print("ABCount = " + ABCount);
        return genMaze;
    }

    List<Vector2> neighborsAB(int[,] passMaze, Vector2 homeVec)
    {
        List<Vector2> validNeighborsAB = new List<Vector2>();
        // int vecCount = 0;
        int vecX = (int)homeVec.x;
        int vecY = (int)homeVec.y;
        for (int i = 0; i < 4; i++)
        {

            Vector2 neighborVec = new Vector2(-1, -1);

            if (i == 0) //LEFT
            {
                if (vecY >= 2){
                    neighborVec = new Vector2(vecX, vecY - 2);
                    validNeighborsAB.Add(neighborVec);
                }
                else
                {
                    continue;
                }
            }
            if (i == 1) //RIGHT
            {
                if (vecY <= width -2 ){
                    neighborVec = new Vector2(vecX, vecY + 2);
                    validNeighborsAB.Add(neighborVec);
                }
                else
                {
                    continue;
                }
            }
            if (i == 2) //DOWN
            {
                if (vecX >= 2){
                    neighborVec = new Vector2(vecX -2, vecY);
                    validNeighborsAB.Add(neighborVec);
                }
                else
                {
                    continue;
                }
            }
            if (i == 3) //UP
            {
                if (vecX <= height -2){
                    neighborVec = new Vector2(vecX +2, vecY);
                    validNeighborsAB.Add(neighborVec);
                }
                else
                {
                    continue;
                }
            }

        }
        List<Vector2> YESvalidNeighborsAB = new List<Vector2>();

        for (int i = 0; i < validNeighborsAB.Count; i++)
        {
            Vector2 nVec = validNeighborsAB[i];
            int xVec = (int)nVec.x;
            int yVec = (int)nVec.y;
            // print("xVec = " + xVec);
            // print("yVec = " + yVec);
            bool validX = (xVec >= 0) && (xVec < width);
            bool validY = (yVec >= 0) && (yVec < height);
            if (validX && validY){
                YESvalidNeighborsAB.Add(validNeighborsAB[i]);
            }
        }

        return validNeighborsAB;
    }

    void GenerateMazeBacktrack()
    {
        //new 2d array of width and height
        Maze = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //put a one in each slot of the array (fill in)
                Maze[x, y] = 1;
                BTCount++;
            }
        }
        CurrentTile = Vector2.one;
        _tiletoTry.Push(CurrentTile);
        Maze = CreateMaze();
        GameObject ptype = null;

        for (int i = 0; i <= Maze.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= Maze.GetUpperBound(1); j++)
            {
                if (Maze[i, j] == 1)
                {
                    ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    ptype.transform.position = new Vector3(i * ptype.transform.localScale.x + displace*many, j * ptype.transform.localScale.y + displace*2, 0);
                    if (brick != null)
                    {
                        ptype.GetComponent<Renderer>().material = brick;
                    }
                    ptype.transform.parent = transform;
                }
                else if (Maze[i, j] == 0)
                {
                    pathMazes.Add(new Vector3(i, j, 0));
                }

            }
        }
        // print("BTCount = " + BTCount);
    }

    public int[,] CreateMaze()
    {
        //local variable to store neighbors to the current square
        //as we work our way through the maze
        List<Vector2> neighbors;
        //as long as there are still tiles to try
        while (_tiletoTry.Count > 0)
        {
            BTCount++;
            //excavate the square we are on
            Maze[(int)CurrentTile.x, (int)CurrentTile.y] = 0;

            //get all valid neighbors for the new tile
            neighbors = GetValidNeighbors(CurrentTile);

            //if there are any interesting looking neighbors
            if (neighbors.Count > 0)
            {
                //remember this tile, by putting it on the stack
                _tiletoTry.Push(CurrentTile);
                //move on to a random of the neighboring tiles
                CurrentTile = neighbors[rnd.Next(neighbors.Count)];
            }
            else
            {
                //if there were no neighbors to try, we are at a dead-end
                //toss this tile out 
                //(thereby returning to a previous tile in the list to check).
                CurrentTile = _tiletoTry.Pop();
            }
        }
        Maze[0,1] = 0;
        Maze[height -1, width -2] =0;

        return Maze;
    }

    private List<Vector2> GetValidNeighbors(Vector2 centerTile)
    {

        List<Vector2> validNeighbors = new List<Vector2>();

        //Check all four directions around the tile
        foreach (var offset in offsets)
        {
            BTCount++;
            //find the neighbor's position
            Vector2 toCheck = new Vector2(centerTile.x + offset.x, centerTile.y + offset.y);

            //make sure the tile is not on both an even X-axis and an even Y-axis
            //to ensure we can get walls around all tunnels
            if (toCheck.x % 2 == 1 || toCheck.y % 2 == 1)
            {
                //if the potential neighbor is unexcavated (==1)
                //and still has three walls intact (new territory)
                if (Maze[(int)toCheck.x, (int)toCheck.y] == 1  && HasThreeWallsIntact(toCheck))
                {
                    //add the neighbor
                    validNeighbors.Add(toCheck);
                }
            }
        }

        return validNeighbors;
    }


    private bool HasThreeWallsIntact(Vector2 Vector2ToCheck)
    {
        int intactWallCounter = 0;

        //Check all four directions around the tile
        foreach (var offset in offsets)
        {
            BTCount++;
            //find the neighbor's position
            Vector2 neighborToCheck = new Vector2(Vector2ToCheck.x + offset.x, Vector2ToCheck.y + offset.y);

            //make sure it is inside the maze, and it hasn't been dug out yet
            if (IsInside(neighborToCheck) && Maze[(int)neighborToCheck.x, (int)neighborToCheck.y] == 1)
            {
                intactWallCounter++;
            }
        }

        //tell whether three walls are intact
        return intactWallCounter == 3;

    }

    private bool IsInside(Vector2 p)
    {
        if (p.x >=0 && p.y >= 0 && p.x < width && p.y < height){
            return true;
        }
        else
        {
            return false;
        }
    }



}