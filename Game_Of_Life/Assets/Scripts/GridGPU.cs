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
    

    GameObject[,] cellGO;
    int size;

    public GridGPU(int size = 5)
    {
        this.size = size;
        CreateGrid();
        

        computeShader = Resources.Load<ComputeShader>("ComputeShader");
        

        int totalsize = sizeof(int) * 5 + sizeof(float) * 8;

        
        computeBuffer = new ComputeBuffer(size*size, totalsize);
        computeBuffer.SetData(cells);
        computeShader.SetBuffer(0, "cells", computeBuffer);

        cells[6, 5].auxDead = 0;
        cells[6, 4].auxDead = 0;
        cells[7, 5].auxDead = 0;
        cells[8, 4].auxDead = 0;
        cells[8, 3].auxDead = 0;
        cells[7, 3].auxDead = 0;

        UpdateColor();


    }

    public void Update()
    {
        
        computeBuffer.SetData(cells);
        computeShader.Dispatch(0, Mathf.CeilToInt(size*size / 10), Mathf.CeilToInt(size*size / 10), 1);
        computeBuffer.GetData(cells);
        UpdateColor();

    }

    public void CreateGrid()
    {
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
                UpdateNextDoor(size);
                CreateGameObject(i,j);
            }
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

    public void UpdateNextDoor(int size)
    {

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                
                cells[i, j].nextDoor1.x = 1;
                cells[i, j].nextDoor1.y = 1;
                cells[i, j].nextDoor1.z = 1;
                cells[i, j].nextDoor1.w = 1;

                cells[i, j].nextDoor2.x = 1;
                cells[i, j].nextDoor2.y = 1;
                cells[i, j].nextDoor2.z = 1;
                cells[i, j].nextDoor2.w = 1;

                if (i < 1)
                {
                    cells[i,j].nextDoor1.x = 0;
                    cells[i, j].nextDoor2.x = 0;
                    cells[i, j].nextDoor2.w = 0;
                }
                if (i > size - 2)
                {
                    cells[i, j].nextDoor2.y = 0;
                    cells[i, j].nextDoor1.z = 0;
                    cells[i, j].nextDoor2.z = 0;

                }
                //Z
                if (j > size - 2)
                {
                    cells[i, j].nextDoor2.x = 0;
                    cells[i, j].nextDoor1.y = 0;
                    cells[i, j].nextDoor2.y = 0;

                }
                if (j < 1)
                {
                    
                    cells[i, j].nextDoor2.z = 0;
                    cells[i, j].nextDoor1.w = 0;
                    cells[i, j].nextDoor2.w = 0;
                }

            }
        }
    }

    private void UpdateColor()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                //if(cells[i, j].dead != cells[i, j].auxDead)
                //{
                    Debug.Log("id: "+ i+"_"+ j+" AuxDead: " + cells[i, j].auxDead + " Dead: " + cells[i, j].dead + "CONT: "+ cells[i, j].size);
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
    
   

}
