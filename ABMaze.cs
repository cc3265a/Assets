// using UnityEngine;
// using System;
// using System.Collections;
// using System.Collections.Generic;

// public class GameManager : MonoBehaviour {
//     public int width, height;
//     public Material brick;
//     private int[,] Maze;
//     private List<Vector3> pathMazes = new List<Vector3>();
//     private Stack<Vector2> _tiletoTry = new Stack<Vector2>();
//     private List<Vector2> offsets = new List<Vector2> { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
//     private System.Random rnd = new System.Random();
//     private int _width, _height;
//     private Vector2 _currentTile;
//     public Vector2 CurrentTile
//     {
//         get { return _currentTile; }
//         private set
//         {
//             if (value.x < 1 || value.x >= this.width - 1 || value.y < 1 || value.y >= this.height - 1)
//             {
//                 throw new ArgumentException("CurrentTile must be within the one tile border all around the maze");
//             }
//             if (value.x % 2 == 1 || value.y % 2 == 1)
//             { _currentTile = value; }
//             else
//             {
//                 throw new ArgumentException("The current square must not be both on an even X-axis and an even Y-axis, to ensure we can get walls around all tunnels");
//             }
//         }
//     }

//     private static GameManager instance;
//     public static GameManager Instance
//     {
//         get
//         {
//             return instance;
//         }
//     }

//     void Awake()
//     {
//         instance = this;
//     }

//     void Start()
//     {
//         Camera.main.orthographic = true;
//         Camera.main.orthographicSize = 30;
//         var myMaze = GenerateMazeAB();
//         print("MAZE GENERATED");

//         GameObject ptype = null;

//         for (int i = 0; i <= width-1; i++)
//         {
//             for (int j = 0; j <= height-1; j++)
//             {
//                 if (myMaze[i, j] == 1)
//                 {
//                     ptype = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                     ptype.transform.position = new Vector3(i * ptype.transform.localScale.x, j * ptype.transform.localScale.y, 0);
//                     if (brick != null)
//                     {
//                         ptype.GetComponent<Renderer>().material = brick;
//                     }
//                     ptype.transform.parent = transform;
//                 }
//                 else if (myMaze[i, j] == 0)
//                 {
//                     pathMazes.Add(new Vector3(i, j, 0));
//                 }

//             }
//         }
//     }

//     int[,] GenerateMazeAB()
//     {
//         //new 2d array of width and height
//         int[,] genMaze = new int[width,height];
//         int unvisited = 0;

//         for (int x = 0; x < width; x++)
//         {
//             for (int y = 0; y < height; y++)
//             {
//                 genMaze[x,y] = 1;
//                 if (x % 2 ==1 && y % 2 == 1)
//                 {
//                     unvisited++;
//                 }
//             }
//         }

//         // int[] on;
//         Vector2 onVec;
//         do
//         {
//             onVec.x = (int)UnityEngine.Random.Range(0,width);
//             onVec.y = (int)UnityEngine.Random.Range(0,height);
//             // on[0] = (int)Mathf.Floor(UnityEngine.Random.Range(0,2) * height);
//             // on[1] = (int)Mathf.Floor(UnityEngine.Random.Range(0,2) * width);
//         }while (onVec.x % 2 == 0 || onVec.y % 2 == 0);

//         Vector2 holdVec = new Vector2((int)onVec.x, (int)onVec.y);
//         genMaze[(int)holdVec.x, (int)holdVec.y] = 1;
//         unvisited--;

//         //List of Vectors unvisited
//         List<Vector2> unvisitedArr = new List<Vector2>();
//         for (int x = 0; x < width; x++)
//             {
//                 for (int y = 0; y < height; y++)
//                 {
//                     if (x % 2 ==1 && y % 2 == 1)
//                     {
//                         if(genMaze[x,y] == 1){
//                             Vector2 unvisVec = new Vector2(x,y);
//                             unvisitedArr.Add(unvisVec);
//                         }
//                     }
//                 }
//             }

//         int breakCount = 0;
//         // while (unvisitedArr.Count != 0)
//         int CRASH = unvisitedArr.Count*3;
//         int MAXCRASH = 90000;
//         print("ENTERING WHILE");
//         while(CRASH > 0)
//         {
//             MAXCRASH--;
//             if (MAXCRASH <= 0)
//             {
//                 print("GIVING UP");
//                 break;
//             }
//             print("while" + unvisitedArr.Count);
//             if (unvisitedArr.Count > CRASH)
//             {
//                 CRASH = unvisitedArr.Count;
//                 print("WAGH");
//             }
                
//             var n = neighborsAB(genMaze, onVec);
//             print("n count is = " + n.Count);
//             if (n.Count == 0)
//             {
//                 breakCount++;
//                 print("break count = " + breakCount);
//                 continue;
//             }
//             int holdRand = UnityEngine.Random.Range(0,n.Count-1);
//             Vector2 to = n[holdRand];
//             print("to is " + to);
//             int holdOOBX = (int)to.x;
//             int holdOOBY = (int)to.y;
//             bool OOB = (holdOOBX >= width) || (holdOOBY >= height);

//             print("x = " + holdOOBX);
//             print("y = " + holdOOBY);
//             print("width = " + width);
//             print("hieght = " + height);


//             if (OOB == false)
//             {
//               if (genMaze[(int)to.x, (int)to.y] == 1)
//                 {
//                     genMaze[(int)to.x, (int)to.y] = 0;
//                     unvisited--;
//                     int holdX = (int)to.x;
//                     int holdY = (int)to.y;
                    
//                     // int holdX = ((int)to.x + (int)onVec.x);
//                     // int holdY = ((int)to.y + (int)onVec.y);

//                     Vector2 minusVec = to - onVec;
//                     minusVec = minusVec/2;
//                     print(minusVec + "= minusVec");
//                     Vector2 wallVec = to - minusVec;
                    
//                     print("toVec is "+ to);

//                     print("x = " + holdX);
//                     print("y = " + holdY);
//                     genMaze[holdX, holdY] = 0;
//                     genMaze[(int)wallVec.x, (int)wallVec.y] = 0;
//                     print("toVec setting (" + holdX + ", " + holdY + ") to zero");
//                     unvisitedArr.Remove(to);
                    
//                 }  
//             }
//             // unvisited--;
//             onVec = to;
//             print("CRASH = " + CRASH);
//             CRASH--;
//         }
//         print("LEAVING");

//         genMaze[0,1] = 0;
//         genMaze[height -1, width -2] =0;


//         // for (int i = 0; i <= width-1; i++)
//         // {
//         //     for (int j = 0; j <= height-1; j++)
//         //     {
//         //         // print("thing " + genMaze[i,j]);
//         //     }
//         // }

//         return genMaze;
//     }

//     List<Vector2> neighborsAB(int[,] passMaze, Vector2 homeVec)
//     {
//         List<Vector2> validNeighborsAB = new List<Vector2>();
//         // int vecCount = 0;
//         int vecX = (int)homeVec.x;
//         int vecY = (int)homeVec.y;
//         for (int i = 0; i < 4; i++)
//         {

//             Vector2 neighborVec = new Vector2(-1, -1);

//             if (i == 0) //LEFT
//             {
//                 if (vecY >= 2){
//                     neighborVec = new Vector2(vecX, vecY - 2);
//                     validNeighborsAB.Add(neighborVec);
//                 }
//                 else
//                 {
//                     continue;
//                 }
//             }
//             if (i == 1) //RIGHT
//             {
//                 if (vecY <= width -2 ){
//                     neighborVec = new Vector2(vecX, vecY + 2);
//                     validNeighborsAB.Add(neighborVec);
//                 }
//                 else
//                 {
//                     continue;
//                 }
//             }
//             if (i == 2) //DOWN
//             {
//                 if (vecX >= 2){
//                     neighborVec = new Vector2(vecX -2, vecY);
//                     validNeighborsAB.Add(neighborVec);
//                 }
//                 else
//                 {
//                     continue;
//                 }
//             }
//             if (i == 3) //UP
//             {
//                 if (vecX <= height -2){
//                     neighborVec = new Vector2(vecX +2, vecY);
//                     validNeighborsAB.Add(neighborVec);
//                 }
//                 else
//                 {
//                     continue;
//                 }
//             }

//         }
//         List<Vector2> YESvalidNeighborsAB = new List<Vector2>();

//         for (int i = 0; i < validNeighborsAB.Count; i++)
//         {
//             Vector2 nVec = validNeighborsAB[i];
//             int xVec = (int)nVec.x;
//             int yVec = (int)nVec.y;
//             // print("xVec = " + xVec);
//             // print("yVec = " + yVec);
//             bool validX = (xVec >= 0) && (xVec < width);
//             bool validY = (yVec >= 0) && (yVec < height);
//             if (validX && validY){
//                 YESvalidNeighborsAB.Add(validNeighborsAB[i]);
//             }
//         }

//         return validNeighborsAB;
//     }


// }