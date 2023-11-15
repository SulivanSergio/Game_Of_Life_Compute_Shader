using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    Cell [,]cells;
    int size;
    public Grid(int size = 5)
    {
        this.size = size;
        cells = new Cell[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j] = new Cell(i, j,size);
            }
        }
        cells[1, 3].auxDead = false;
        cells[2, 4].auxDead = false;
        cells[2, 5].auxDead = false;
        cells[3, 4].auxDead = false;
        cells[4, 4].auxDead = false;
        cells[3, 6].auxDead = false;
        cells[3, 5].auxDead = false;
        cells[3, 4].auxDead = false;
        cells[3, 5].auxDead = false;
        cells[4, 4].auxDead = false;
        cells[5, 4].auxDead = false;
        cells[4, 6].auxDead = false;
        cells[4, 5].auxDead = false;
        cells[1, 5].auxDead = false;
        cells[0, 4].auxDead = false;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j].Update(0);
            }
        }
    }
    public void Update(float deltaTime)
    {
        Checks();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                cells[i, j].Update(deltaTime);
            }
        }
    }
    public void Checks()
    {
        int cont = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (cells[i, j].vizinhos[0] == true)
                {
                    //esquerda
                    if (!cells[i - 1, j].dead)
                    {
                        cont++;
                    }
                }
                if (cells[i, j].vizinhos[4] == true)
                {
                    //direita
                    if (!cells[i + 1, j].dead)
                    {
                        cont++;

                    }
                }
                if (cells[i, j].vizinhos[2] == true)
                {
                    //cima
                    if (!cells[i, j + 1].dead)
                    {
                        cont++;

                    }
                }
                if (cells[i, j].vizinhos[6] == true)
                {
                    //baixo
                    if (!cells[i, j - 1].dead)
                    {
                        cont++;

                    }
                }
                //diagonais
                
                if (cells[i, j].vizinhos[1] == true)
                {
                    //esquerda - cima
                    if (!cells[i - 1, j + 1].dead)
                    {
                        cont++;
                    }
                }
                if (cells[i, j].vizinhos[3] == true)
                {
                    //direita - cima
                    if (!cells[i + 1, j +1 ].dead)
                    {
                        cont++;

                    }
                }
                if (cells[i, j].vizinhos[5] == true)
                {
                    //direita - baixo
                    if (!cells[i + 1, j - 1].dead)
                    {
                        cont++;

                    }
                }
                if (cells[i, j].vizinhos[7] == true)
                {
                    //esquerda - baixo
                    if (!cells[i - 1, j - 1].dead)
                    {
                        cont++;

                    }
                }

                //----------------
                if (cells[i, j].dead)
                {
                    if (cont > 2)
                    {
                        cells[i, j].auxDead = false;
                    }
                }
                else
                {
                    if (cont < 2)
                    {
                        cells[i, j].auxDead = true;
                    }
                    if (cont > 3)
                    {
                        cells[i, j].auxDead = true;
                    }
                    if (cont == 2 || cont == 3)
                    {
                        cells[i, j].auxDead = false;
                    }
                }
                cont = 0;
            }
            cont = 0;
        }
        
    }

    

}
