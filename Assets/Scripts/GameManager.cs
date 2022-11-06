using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    //List
    public List<GameObject> gridList = new List<GameObject>();
    public List<Transform> emptyList = new List<Transform>();
    public List<GameObject> temporaryList = new List<GameObject>();
    //public List<Grids> gridList2 = new List<Grids>();

    //SerializeField
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject boardPrefab;


    private GameObject temporaryObj;

    private Transform firstBlock;
    private Transform secondBlokc;

    private Grids thisGrid;
    private Grids thatGrid;

    private int width = 4;
    private int height = 4;

    void Awake()
    {
        Grid();
    }

    
    void Update()
    {
        // gridList[0].GetComponent<Grids>().isEmpty = false;

        if (Input.GetKeyDown(KeyCode.A))
        {
            //gridList[0].GetComponent<Grids>().myOBJScript.value = 10;
            //Debug.Log(gridList[0].GetComponent<Grids>().myOBJScript.value);

            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x = (i * 4) - j;
                    Debug.Log(x);
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K");
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x = ((i * 4) - j) - 1;
                    //Debug.Log(x);

                    if(gridList[x].GetComponent<Grids>().myOBJ != null)
                    {
                        temporaryObj = gridList[x].GetComponent<Grids>().myOBJ;
                        
                    }
                    else
                    {
                        continue;
                    }

                    Vector2 halfWidth = new Vector2(temporaryObj.transform.position.x, temporaryObj.transform.position.y + 0.53f);
                    //RaycastHit2D hitDown = Physics2D.Raycast(temporaryObj.transform.position, Vector2.down, Mathf.Infinity);
                    RaycastHit2D hitUp = Physics2D.Raycast(halfWidth, Vector2.up, Mathf.Infinity);

                    if (hitUp.collider.gameObject)
                    {
                        if (hitUp.collider.CompareTag("Corner"))
                        {
                            Debug.Log(hitUp.collider.tag);
                            temporaryObj.transform.position = new Vector2(temporaryObj.transform.position.x , 3);
                            //temporaryObj.transform.DOMoveY(3, 0.05f);
                        }

                        else if(hitUp.collider.CompareTag("Block"))
                        { 

                            Debug.Log(hitUp.collider.tag);
                            Block myObjsc = temporaryObj.GetComponent<Block>();
                            Block hitObjsc = hitUp.collider.gameObject.GetComponent<Block>();
                            GameObject hitObj = hitUp.collider.gameObject;

                            if (Upgradable(myObjsc, hitObjsc))
                            {
                                CombineTwoBlock(temporaryObj, hitObj, myObjsc, hitObjsc);
                                
                            }
                            else
                            {
                                temporaryObj.transform.position = new Vector2(temporaryObj.transform.position.x, hitObj.transform.position.y - 1);
                            }
                        }
                    }                   

                }             
            }
        }

        if (Input.GetKeyDown(KeyCode.L)) //LEFT 3 7 11 15
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x = ((j * 4) + 3 - i);
                    Debug.Log(x);

                    if (gridList[x].GetComponent<Grids>().myOBJ != null)
                    {
                        temporaryObj = gridList[x].GetComponent<Grids>().myOBJ;
                    }
                    else
                    {
                        continue;
                    }

                    Vector2 halfWidth = new Vector2(temporaryObj.transform.position.x, temporaryObj.transform.position.y + 0.53f);
                    //RaycastHit2D hitDown = Physics2D.Raycast(temporaryObj.transform.position, Vector2.down, Mathf.Infinity);
                    RaycastHit2D hitUp = Physics2D.Raycast(halfWidth, Vector2.up, Mathf.Infinity);

                    if (hitUp.collider.gameObject)
                    {
                        if (hitUp.collider.CompareTag("Corner"))
                        {
                            Debug.Log(hitUp.collider.tag);
                            temporaryObj.transform.position = new Vector2(temporaryObj.transform.position.x, 3);
                            //temporaryObj.transform.DOMoveY(3, 0.05f);
                        }

                        else if (hitUp.collider.CompareTag("Block"))
                        {

                            Debug.Log(hitUp.collider.tag);
                            Block myObjsc = temporaryObj.GetComponent<Block>();
                            Block hitObjsc = hitUp.collider.gameObject.GetComponent<Block>();
                            GameObject hitObj = hitUp.collider.gameObject;

                            if (Upgradable(myObjsc, hitObjsc))
                            {
                                CombineTwoBlock(temporaryObj, hitObj, myObjsc, hitObjsc);

                            }
                            else
                            {
                                temporaryObj.transform.position = new Vector2(temporaryObj.transform.position.x, hitObj.transform.position.y - 1);
                            }
                        }
                    }

                }
            }
        }
    }

    private bool Upgradable(Block myObj, Block hitObj)
    {
        return (myObj.value == hitObj.value && myObj.canUpgrade && hitObj.canUpgrade && myObj.value < 2048);
    }

    private void CombineTwoBlock(GameObject firstObj,GameObject secondObj,Block firstObjsc, Block secondObjsc)
    {
        //firstObj.transform.DOMove(secondObj.transform.position, 0.05f);
        firstObj.transform.position = secondObj.transform.position;
        firstObjsc.canUpgrade = false;
        secondObjsc.canUpgrade = false;
        firstObjsc.value *= 2;
        secondObjsc.value *= 2;

        //emptyList.Remove(emptyList[0]);
    }

    public void Grid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                
                var _grid = Instantiate(grid, new Vector2(i, j), Quaternion.identity);
                //Debug.Log();
                gridList.Add(_grid);
                
               
            }
        }

        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);

        var board = Instantiate(boardPrefab, center, Quaternion.identity);

        //Camera.main.transform.position = new Vector3(center.x, center.y + 1, -10f);


       

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
