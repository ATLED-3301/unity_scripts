using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum WallState{
    // 0000 -> NO WALLS
    // 1111 -> L R U D
    LEFT = 1 ,
    RIGHT = 2 ,
    UP = 4 ,
    DOWN = 8 ,
    MARKED = 128 ,  // 1000 0000
}


public struct Position{
    public int X ;
     public int Y;
     }

public struct Neighbour{
    public Position Position;
    public WallState SharedWall ;
    }
public class MazeGenerator : MonoBehaviour
{
    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        // here we make changes
        var rng = new System.Random(/*seed*/);
        var positionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };

        maze[position.X, position.Y] |= WallState.MARKED;  // 1000 1111
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnmarkedNeightbours(current, maze, width, height);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.Position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);
                maze[nPosition.X, nPosition.Y] |= WallState.MARKED;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }
    private static List<Neighbour> GetUnmarkedNeightbours(Position p , WallState[,] maze , int width, int height){
            var list = new List<Neighbour>();

            if (p.X > 0) // left
            {
                if (!maze[p.X - 1, p.Y].HasFlag(WallState.MARKED))
                {
                    list.Add(new Neighbour
                    {
                        Position = new Position
                        {
                            X = p.X - 1,
                            Y = p.Y
                        },
                        SharedWall = WallState.LEFT
                    });
                }
            }

            if (p.Y > 0) // DOWN
            {
                if (!maze[p.X, p.Y - 1].HasFlag(WallState.MARKED))
                {
                    list.Add(new Neighbour
                    {
                        Position = new Position
                        {
                            X = p.X,
                            Y = p.Y - 1
                        },
                        SharedWall = WallState.DOWN
                    });
                }
            }

            if (p.Y < height - 1) // UP
            {
                if (!maze[p.X, p.Y + 1].HasFlag(WallState.MARKED))
                {
                    list.Add(new Neighbour
                    {
                        Position = new Position
                        {
                            X = p.X,
                            Y = p.Y + 1
                        },
                        SharedWall = WallState.UP
                    });
                }
            }

            if (p.X < width - 1) // RIGHT
            {
                if (!maze[p.X + 1, p.Y].HasFlag(WallState.MARKED))
                {
                    list.Add(new Neighbour
                    {
                        Position = new Position
                        {
                            X = p.X + 1,
                            Y = p.Y
                        },
                        SharedWall = WallState.RIGHT
                    });
                }
            }

            return list;
        }
    

    public static WallState[,] Generate(int Width, int Height){
        WallState[,] maze = new WallState[Width,Height];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for(int i = 0 ; i < Width ; ++i){
            for (int j = 0 ; j < Height ; ++j){
                maze[i,j] = initial ;
            }
        }
        return ApplyRecursiveBacktracker(maze,Width,Height);
    }
}
