using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Grid gridCPU;
    GridGPU gridGPU;
    float timer = 0;
    float timerMAX = 0.5f;
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
                float speedUp = 0;
                float tempoInicial = Time.realtimeSinceStartup;

                
                gridGPU.Update();

                float tempoFinal = Time.realtimeSinceStartup;
                speedUp = 1.0f / (tempoFinal - tempoInicial);

                //Debug.Log("SpeedUp: " + speedUp);
            }
            else
            {
                float speedUp = 0;
                float tempoInicial = Time.realtimeSinceStartup;

                gridCPU.Update(Time.deltaTime);

                float tempoFinal = Time.realtimeSinceStartup;
                speedUp = 1.0f/ (tempoFinal - tempoInicial);

                Debug.Log("SpeedUp: " + speedUp);
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
