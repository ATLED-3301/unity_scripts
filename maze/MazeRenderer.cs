using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Range(1, 500)]
    private int Width = 10;
    [SerializeField]
    [Range(1,500)]
    private int Height = 10 ;
    [SerializeField]
    
    private Transform MazeWallPrefab = null;
    [SerializeField]
    private float size = 1f;

    void Start()
    {
        var maze = MazeGenerator.Generate(Width,Height);
        DrawMaze(maze);
    }

    private void DrawMaze(WallState[,] maze) {
        for (int i = 0 ; i < Width ; ++i){
            for(int j = 0 ; j < Height ; j++){
                var cell = maze[i,j];
                var position = new Vector3(- Width / 2 + i , 0 , - Height / 2 + j );

                if(cell.HasFlag(WallState.UP)){
                    var topWall = Instantiate(MazeWallPrefab,transform) as Transform;
                    topWall.position = position + new Vector3(0,0,size/2);
                    topWall.localScale = new Vector3(size,topWall.localScale.y,topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT)){
                    var leftWall = Instantiate(MazeWallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2 , 0 , 0);
                    leftWall.eulerAngles = new Vector3(0,90,0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                }

                if(i == Width - 1 ){
                    if (cell.HasFlag(WallState.RIGHT)){
                        var rightWall = Instantiate(MazeWallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                    }
                }

                if (j==0){
                    if (cell.HasFlag(WallState.DOWN)){
                        var downWall = Instantiate(MazeWallPrefab, transform) as Transform;
                        downWall.position = position + new Vector3(0 , 0 , - size / 2);
                        downWall.localScale = new Vector3(size, downWall.localScale.y, downWall.localScale.z);
                    }
                }

            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
