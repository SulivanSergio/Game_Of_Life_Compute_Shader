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
    public bool[] nextDoor = new bool[8];
    public Cell(int x , int z, int size)
    {
        this.x = x;
        this.z = z;
        UpdateNextDoor(size);
    }

    public void CreateGameObject()
    {
        cellGO = new GameObject("Cell_" + x + "." + z);
        cellGO.AddComponent<MeshFilter>();
        cellGO.AddComponent<MeshRenderer>();
        cellGO.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Cube");
        cellGO.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material");
        cellGO.transform.localScale = new Vector3(50, 50, 50);
        cellGO.transform.position = new Vector3(x, 0, z);
    }

    public void UpdateNextDoor(int size)
    {
        for(int i = 0; i< nextDoor.Length; i ++)
        {
            nextDoor[i] = true;
        }
        if(x < 1)
        {
            nextDoor[0] = false;
            nextDoor[1] = false;
            nextDoor[7] = false;
        }
        if (x > size - 2)
        {
            nextDoor[3] = false;
            nextDoor[4] = false;
            nextDoor[5] = false;
            
        }
        //Z
        if (z > size - 2)
        {
            
            nextDoor[1] = false;
            nextDoor[2] = false;
            nextDoor[3] = false;
            
        }
        if ( z < 1 )
        {
            nextDoor[5] = false;
            nextDoor[6] = false;
            nextDoor[7] = false;
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
        
    }

}
