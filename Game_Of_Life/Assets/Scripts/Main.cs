using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Grid grid;
    float timer = 0;
    float timerMAX = 1;
    [SerializeField] int size;
    void Start()
    {
        grid = new Grid(size);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerMAX)
        {
            timer = 0;
            grid.Update(Time.deltaTime);
        }
    }
}
