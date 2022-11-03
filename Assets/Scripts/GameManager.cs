using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //List
    public List<GameObject> gridList = new List<GameObject>();
    //public List<Grids> gridList2 = new List<Grids>();

    //SerializeField
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject boardPrefab;

 



    private int width = 4;
    private int height = 4;

    void Awake()
    {
        Grid();
    }

    
    void Update()
    {
       // gridList[0].GetComponent<Grids>().isEmpty = false;

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K");
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x = (i * 4) - j;



                    Debug.Log(x);

                   // if(gridList[x].GetComponent<Grids>().isEmpty == false) { continue; }

                }
            }
        }

    }

    public void Grid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                
                var _grid = Instantiate(grid, new Vector2(i, j), Quaternion.identity);
                //Debug.Log(gridMatrix[i, j]);
                gridList.Add(_grid);
                
               
            }
        }

        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);

        var board = Instantiate(boardPrefab, center, Quaternion.identity);

        Camera.main.transform.position = new Vector3(center.x, center.y + 1, -10f);


       

    }





    public void StartGrid()
    {
        int x1 = Random.Range(0, 16);
        int x2 = Random.Range(0, 16);

        if(x1 == x2)
        {
            if(x2 == 0)
            {
                x2++;
            }
            else if(x2 == 15)
            {
                x2--;
            }
        }




    }


    public void spawnGrid()
    {
        while (true)
        {
            int x1 = Random.Range(0, 16);
            
            //if(gridList[x1].is)
            
        }
        


    }


}
