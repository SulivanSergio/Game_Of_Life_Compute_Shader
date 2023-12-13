using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGPU
{

     struct Cell {
        public int x;
        public int z;
        public int dead;
        public int auxDead;
        public int size;
        public Vector4 nextDoor1;
        public Vector4 nextDoor2;
    }

    ComputeShader computeShader;
    public ComputeBuffer computeBuffer;
    
    Cell[,] cells;
    Cell[] cellsBuffer;

    GameObject[,] cellGO;
    int size;

    public GridGPU(int size = 5)
    {
        this.size = size;
        CreateGrid();



        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == j)
                {
                    cells[i, j].auxDead = 0;
                }
                if (j == (size - 1 - i))
                {
                    cells[i, j].auxDead = 0;
                }
            }
        }

        

        

        UpdateColor();

        computeShader = Resources.Load<ComputeShader>("ComputeShader");


        int totalsize = sizeof(int) * 5 + sizeof(float) * 8;


        computeBuffer = new ComputeBuffer(size * size, totalsize);
        computeBuffer.SetData(cellsBuffer);
        computeShader.SetBuffer(computeShader.FindKernel("CSMain"), "cells", computeBuffer);
        
    }

    public void Update()
    {
        
        computeBuffer.SetData(cells);
        computeShader.Dispatch(computeShader.FindKernel("CSMain"), Mathf.CeilToInt(size*size/10),1, 1);
        computeBuffer.GetData(cells);
        UpdateCell();
        UpdateColor();

    }

    public void CreateGrid()
    {
        cellsBuffer = new Cell[size*size];
        cells = new Cell[size,size];
        cellGO = new GameObject[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j] = new Cell();
                cells[i, j].x = i;
                cells[i, j].z = j;
                cells[i, j].dead = 1;
                cells[i, j].auxDead = 1;
                cells[i, j].size = size;
                UpdateNextDoor(cells[i, j]);
                CreateGameObject(i,j);
            }
        }
        for (int i = 0; i < size*size; i++)
        {
             cellsBuffer[i] = cells[i / size, i % size];
        }

    }
    private void CreateGameObject(int x,int z)
    {
        cellGO[x,z] = new GameObject("Cell_" + x + "." + z);
        cellGO[x, z].AddComponent<MeshFilter>();
        cellGO[x, z].AddComponent<MeshRenderer>();
        cellGO[x, z].GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Cube");
        cellGO[x, z].GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material");
        cellGO[x, z].transform.localScale = new Vector3(50, 50, 50);
        cellGO[x, z].transform.position = new Vector3(x, 0, z);
    }

    private void UpdateNextDoor(Cell cell)
    {
 
        cell.nextDoor1.x = 1;
        cell.nextDoor1.y = 1;
        cell.nextDoor1.z = 1;
        cell.nextDoor1.w = 1;

        cell.nextDoor2.x = 1;
        cell.nextDoor2.y = 1;
        cell.nextDoor2.z = 1;
        cell.nextDoor2.w = 1;

        if (cell.x < 1)
        {
            cell.nextDoor1.x = 0;
            cell.nextDoor2.x = 0;
            cell.nextDoor2.w = 0;
        }
        if (cell.x > size - 2)
        {
            cell.nextDoor2.y = 0;
            cell.nextDoor1.z = 0;
            cell.nextDoor2.z = 0;

        }
        //Z
        if (cell.z > size - 2)
        {
            cell.nextDoor2.x = 0;
            cell.nextDoor1.y = 0;
            cell.nextDoor2.y = 0;

        }
        if (cell.z < 1)
        {
                    
            cell.nextDoor2.z = 0;
            cell.nextDoor1.w = 0;
            cell.nextDoor2.w = 0;
        }
        
    }

    private void UpdateColor()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                //if (cells[i, j].dead != cells[i, j].auxDead)
                //{
                    Debug.Log("id: " + i + "_" + j + " AuxDead: " + cells[i, j].auxDead + " Dead: " + cells[i, j].dead + "CONT: " + cells[i, j].size);
                //}

                cells[i, j].dead = cells[i, j].auxDead;
                
                if (cells[i, j].dead == 1)
                {
                    cellGO[i, j].GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    cellGO[i, j].GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
                }
            }
        }
        
        
    }
    
    private void UpdateCell()
    {
        for (int i = 0; i < size * size; i++)
        {

            cells[i / size, i % size] = cellsBuffer[i];

            
        }
    }
   

}
