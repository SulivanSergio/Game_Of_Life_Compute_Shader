using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Grid gridCPU;
    GridGPU gridGPU;
    float timer = 0;
    float timerMAX = 1;
    [SerializeField] int size;
    [SerializeField] bool useGPU;

    void Start()
    {
        if(useGPU)
        {
            gridGPU = new GridGPU(size);
        }
        else
        {
            gridCPU = new Grid(size);
        }
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timerMAX)
        {
            timer = 0;
            if (useGPU)
            {
                gridGPU.Update();
<<<<<<< HEAD

                float tempoFinal = Time.realtimeSinceStartup;
                speedUp = 1.0f / (tempoFinal - tempoInicial);

                //Debug.Log("SpeedUp: " + speedUp);
=======
>>>>>>> parent of 457de10 (SpeedUp adicionado)
            }
            else
            {
                gridCPU.Update(Time.deltaTime);
            }
        }
    }

    private void OnDestroy()
    {
        if(useGPU)
        {
            gridGPU.computeBuffer.Dispose();
        }
    }

}
