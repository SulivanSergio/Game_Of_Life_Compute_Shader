using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    public int x;
    public int z;
    public bool dead = true;
    public bool auxDead = true;
    public GameObject cellGO;
    public bool[] vizinhos = new bool[8];
    public Cell(int x , int z, int size)
    {
        this.x = x;
        this.z = z;
        cellGO = new GameObject("Cell_" + x + "." + z);
        cellGO.AddComponent<MeshFilter>();
        cellGO.AddComponent<MeshRenderer>();
        cellGO.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Cube");
        cellGO.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material");
        cellGO.transform.localScale = new Vector3(50, 50, 50);
        cellGO.transform.position = new Vector3(x,0,z);

        UpdateVizinhos(size);
    }

    public void UpdateVizinhos(int size)
    {
        for(int i = 0; i< vizinhos.Length; i ++)
        {
            vizinhos[i] = true;
        }
        if(x < 1)
        {
            vizinhos[0] = false;
            vizinhos[1] = false;
            vizinhos[7] = false;
        }
        if (x > size - 2)
        {
            vizinhos[3] = false;
            vizinhos[4] = false;
            vizinhos[5] = false;
            
        }
        //Z
        if (z > size - 2)
        {
            
            vizinhos[1] = false;
            vizinhos[2] = false;
            vizinhos[3] = false;
            
        }
        if ( z < 1 )
        {
            vizinhos[5] = false;
            vizinhos[6] = false;
            vizinhos[7] = false;
        }

    }

    public void Update(float deltaTime)
    {
        dead = auxDead;
        if(dead)
        {
            cellGO.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            cellGO.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
        }
        Debug.Log("Name: " + cellGO.name + " Dead: " + dead);
    }

}
