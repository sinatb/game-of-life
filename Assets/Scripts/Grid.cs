using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int SizeX = 100;
    public int SizeY = 100;
    public float Stride = 1.5f;
    public GameObject GridPrefab;
    private Vector2 basePos;

    private Cell[,] grid;
    void Awake()
    {
        grid = new Cell[SizeY, SizeX];
        basePos = new Vector2(transform.position.x,transform.position.y);
    }

    private void AddNeighbours()
    {
        for (int i = 0; i < SizeY; i++)
        {
            for (int j = 0; j < SizeX; j++)
            {
                //basic
                if (i>0)
                    grid[i,j].AddNeighbour(grid[i-1,j]);
                if (j>0)
                    grid[i,j].AddNeighbour(grid[i,j-1]);
                if (i<SizeY-1)
                    grid[i,j].AddNeighbour(grid[i+1,j]);
                if (j<SizeX-1)
                    grid[i,j].AddNeighbour(grid[i,j+1]);
                //combination
                if (i>0 && j<SizeX-1)
                    grid[i,j].AddNeighbour(grid[i-1,j+1]);
                if (j>0 && i<SizeY-1)
                    grid[i,j].AddNeighbour(grid[i+1,j-1]);
                if (i<SizeY-1 && j<SizeX-1)
                    grid[i,j].AddNeighbour(grid[i+1,j+1]);
                if (i>0 && j>0)
                    grid[i,j].AddNeighbour(grid[i-1,j-1]);
            }
        }
    }
    public void CreateGrid()
    {
        for (int i = 0; i < SizeY; i++)
        {
            for (int j = 0; j < SizeX; j++)
            {
                GameObject g = Instantiate(GridPrefab,
                    new Vector3(basePos.x + (j * Stride), basePos.y + (i * Stride), 0),
                    Quaternion.identity);
                grid[i, j] = g.GetComponent<Cell>();
            }
        }
        AddNeighbours();
    }
    public void UpdateGrid()
    {
        for (int i = 0; i < SizeY; i++)
        {
            for (int j = 0; j < SizeX; j++)
            {
                grid[i,j].UpdateState();
            }
        }
        for (int i = 0; i < SizeY; i++)
        {
            for (int j = 0; j < SizeX; j++)
            {
                grid[i,j].ChangeState();
            }
        }
    }
}
