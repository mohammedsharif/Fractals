using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandelBrot : MonoBehaviour
{
    [SerializeField] private GameObject cell;

    private GameObject[][] gridOfCells; 
    private int gridX = 200, gridY = 200;

    // Start is called before the first frame update
    void Start()
    {
        InitiateGrids();
        MandelBrotExecution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitiateGrids()
    {
        gridOfCells = new GameObject[gridX][];

        for(int i = 0; i < gridX; i++)
        {
            gridOfCells[i] = new GameObject[gridY];

            for(int j = 0; j < gridY; j++)
            {
                gridOfCells[i][j] = Instantiate(cell, new Vector3(j, i, 0), cell.transform.rotation);
            } 
        }

    }

    void MandelBrotExecution()
    {
        for(int i = 0; i < gridX; i++)
        {
            for(int j = 0; j < gridY; j++)
            {
                Vector2 scale1 = new Vector2(-2,0.47f);
                Vector2 scale2 = new Vector2(-1.12f,1.12f);
                Vector2 scale3 = new Vector2(0,gridX);

                int x0 = (int)ConvertBetweenScales(i,scale1,scale3);
                int y0 = (int)ConvertBetweenScales(j,scale2,scale3);

                int x = 0, y = 0;

                int iteration = 0;
                int maxIteration = 1000;

                while(x*x + y*y <= 2*2 && iteration < maxIteration)
                {
                    int xTemp = x*x - y*y + x0;

                    y = 2*x*y + y0;
                    x = xTemp;

                    iteration++;
                }

                Color color = new Color(0f,0f,iteration/maxIteration);
                gridOfCells[i][j].GetComponent<Renderer>().material.color = color;
            }
        }

    }

    public float ConvertBetweenScales(float old_value, Vector2 first_scale, Vector2 second_scale)
    {
        /** Given a chosen value on one scale, find it's equivalent value on another scale. **/
 
        float normalised_value = (old_value - first_scale[0]) / (first_scale[1] - first_scale[0]);
        float new_value = (normalised_value * (second_scale[1] - second_scale[0])) + second_scale[0];
        return new_value;
    }
}
