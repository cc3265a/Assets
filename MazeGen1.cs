// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Random = UnityEngine.Random;
// using System;
// public class GameManager : MonoBehaviour
// {
//     public GameObject pixelPrefab;
//     public pixelScript[,] pixels; 
//     public int mazeWidth = 31;
//     public int mazeHeight = 31;


//     // Start is called before the first frame update
//     void Start()
//     {
//         pixels = new pixelScript[mazeWidth,mazeHeight];
//         for (int x = 0; x < mazeWidth; x++){
//             for (int y = 0; y < mazeHeight; y++){
//                 Vector3 pos = transform.position;
//                 float pixelWidth = 1f;
//                 float spacing = 0.0f;
//                 pos.x = pos.x + x * (pixelWidth + spacing);
//                 pos.z = pos.z + y * (pixelWidth + spacing);
//                 pos.y = 1;
//                 GameObject pixelObj = Instantiate(pixelPrefab, pos, transform.rotation);
//                 pixels[x,y] = pixelObj.GetComponent<pixelScript>();
//                 pixels[x,y].x = x;
//                 pixels[x,y].y = y;
//             }
//         }
//         MazeGen();
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     void MazeGen()
//     {
//         //start opening location
//         pixels[0,1].path = true;
//         Debug.Log("mazegen");

//         //pick random valid place to start exploring from
//         int[] start;
//         do
//         {
//             start[0] = (int)(Mathf.FloorToInt(Random.Range(0,1) * mazeHeight));
//             Debug.Log(start[0]);
            
//         } while (start[0] % 2 == 0);
//         do
//         {
//             start[1] = (int)Mathf.FloorToInt(Random.Range(0,1) * mazeWidth);
//         } while (start[1] % 2 == 0);

//         pixels[start[0],start[1]].path = true;

//         //open cells
//         int[] openCells = start;
//         while (openCells.Length > 0)
//         {
//             int[] cell;
//             pixelScript[] n;
//             // openCells.push(-1,-1);

//             do
//             {
//                 Array newOpenCells = new Array[openCells.Length -1];
//                 Array.Copy(openCells, 1, newOpenCells, 0, openCells.Length -1);
//                 if (openCells.Length == 0)
//                 {
//                     break;
//                 }
//                 cell = openCells[openCells.Length - 1];
//                 n = neighbors(pixels, cell[0], cell[1]);
//             } while (n.Length == 0 && openCells.Length > 0);

//             if (openCells.Length == 0)
//             {
//                 break;
//             }

//             var choice = n[Mathf.Floor(Random.Range(0,1) * n.Length)];
//             openCells.push(choice);

//             pixels[ choice[0] ][ choice[1] ].path = true;
//             pixels[ (choice[0] + cell[0]) / 2][ (choice[1] + cell[1]) / 2 ].path = true;
//         }

//         pixels[pixels.Length - 1][pixels[0].Length -2].path = true;
//         pixels[pixels.Length - 2][pixels[0].Length -2].path = true;

//         //return pixels ???

//     }

//     void neighbors(pixelScript[,] pixels, int ic, int jc)
//     {
//         pixelScript[,] final;
//         for (var i = 0; i < 4; i++)
//         {
//             pixelScript[,] n = new pixelScript[ic, jc];

//             n[i%2] += (Mathf.Floor(i /2) * 2) || -2;
//             if (n[0] < pixels.Length &&
//                 n[1] < pixels[0].Length &&
//                 n[0] > 0 &&
//                 n[1] > 0)
//             {
//                 if (pixels[n[0]][n[1]].path == false)
//                 {
//                     final.push(n);
//                 }
//             }
//         }
//         return final;
//     }

// }
