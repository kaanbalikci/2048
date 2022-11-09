using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    //List
    public List<GameObject> gridList = new List<GameObject>();
    public List<Transform> emptyList = new List<Transform>();
    public List<Block> temporaryList = new List<Block>();

    //SerializeField
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject boardPrefab;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject gameOverPanel;

    //Private inputs
    private GameObject temporaryObj;

    private Vector2 previousPos;

    private Vector3 defaultScale = new Vector3(0.9f, 0.9f, 0.9f);

    private Transform firstBlock;
    private Transform secondBlokc;

    private Grids thisGrid;
    private Grids thatGrid;

    private int width = 4;
    private int height = 4;

    private int isMove = 0;
    

    //Public inputs
    public static int score = 0;
    public int highScore = 0;

    //Event
    public static event Action<int> UpdateScore;
    public static event Action<int> UpdateHighScore;

    void Awake()
    {
        Grid();  
    }

    private void Start()
    {
        //FirstBlocks();
        highScore = PlayerPrefs.GetInt("High Score", 0);
        UpdateHighScore?.Invoke(highScore);
    }

    void Update()
    {

        
        MoveFunction();
        
    }

    public void Grid() // Instantiate grids
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var _grid = Instantiate(grid, new Vector2(i, j), Quaternion.identity);
                gridList.Add(_grid);
            }
        }
        var center = new Vector2((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);
        var board = Instantiate(boardPrefab, center, Quaternion.identity);

        FirstBlocks();
    }

    private void MoveFunction() // swipe all block to swipeway
    {
        if (SwipeControl.SC.swipeWay == "Up")
        {         
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x = ((i * 4) - j) - 1;
                    Vector2 checkPos;

                    if (gridList[x].GetComponent<Grids>().myOBJ != null)
                    {
                        temporaryObj = gridList[x].GetComponent<Grids>().myOBJ; //take this object's script
                        checkPos = temporaryObj.transform.position;
                    }
                    else
                    {
                        continue;
                    }

                    Vector2 halfWidth = new Vector2(temporaryObj.transform.position.x, temporaryObj.transform.position.y + 0.53f); //raycast starting outside of block                  
                    RaycastHit2D hitUp = Physics2D.Raycast(halfWidth, Vector2.up, Mathf.Infinity);

                    if (hitUp.collider.gameObject)
                    {
                        if (hitUp.collider.CompareTag("Corner")) //if has no block in front of this object go default pos
                        {
                            temporaryObj.transform.DOMoveY(3, 0.1f);
                            previousPos = new Vector2(temporaryObj.transform.position.x, 3);      
                            
                        }

                        else if (hitUp.collider.CompareTag("Block")) //if has block in front of this object:
                        {
                            Block myObjsc = temporaryObj.GetComponent<Block>(); // take this obj script
                            Block hitObjsc = hitUp.collider.gameObject.GetComponent<Block>(); // take that object script
                            GameObject hitObj = hitUp.collider.gameObject; //take that object

                            if (Upgradable(myObjsc, hitObjsc)) // if two blocks value's are same combine these blocks
                            { 
                                CombineTwoBlock(temporaryObj, hitObj, myObjsc, hitObjsc, previousPos);
                            }
                            else
                            {
                                temporaryObj.transform.DOMoveY(previousPos.y - 1, 0.1f);
                                previousPos = new Vector2(temporaryObj.transform.position.x, previousPos.y- 1);
                            }
                            
                        }
                    }
                    CheckisMove(temporaryObj.transform.position, previousPos);


                }
            }
            for (int i = 0; i < temporaryList.Count; i++) // open upgradable of block
            {
                temporaryList[i].canUpgrade = true;
            }
            temporaryList.Clear();
            SwipeControl.SC.swipeWay = "";
            StartCoroutine(spawnBlock());
            
        }
        if (SwipeControl.SC.swipeWay == "Left") //LEFT 3 7 11 15
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x = ((j * 4) + 3 - i);
                    Vector2 checkPos;

                    if (gridList[x].GetComponent<Grids>().myOBJ != null)
                    {
                        temporaryObj = gridList[x].GetComponent<Grids>().myOBJ;
                        checkPos = temporaryObj.transform.position;

                    }
                    else
                    {
                        continue;
                    }

                    Vector2 halfWidth = new Vector2(temporaryObj.transform.position.x - 0.53f, temporaryObj.transform.position.y);
                    RaycastHit2D hitLeft = Physics2D.Raycast(halfWidth, Vector2.left, Mathf.Infinity);

                    if (hitLeft.collider.gameObject)
                    {
                        if (hitLeft.collider.CompareTag("Corner"))
                        {
                            temporaryObj.transform.DOMoveX(0, 0.1f);
                            previousPos = new Vector2(0, temporaryObj.transform.position.y);
                        }

                        else if (hitLeft.collider.CompareTag("Block"))
                        {
                            Block myObjsc = temporaryObj.GetComponent<Block>();
                            Block hitObjsc = hitLeft.collider.gameObject.GetComponent<Block>();
                            GameObject hitObj = hitLeft.collider.gameObject;

                            if (Upgradable(myObjsc, hitObjsc))
                            {
                                CombineTwoBlock(temporaryObj, hitObj, myObjsc, hitObjsc, previousPos);

                            }
                            else
                            {
                                temporaryObj.transform.DOMoveX(previousPos.x + 1, 0.1f);
                                previousPos = new Vector2(previousPos.x + 1, temporaryObj.transform.position.y);
                            }
                        }
                    }
                    CheckisMove(temporaryObj.transform.position, previousPos);
                }

            }
            for (int i = 0; i < temporaryList.Count; i++)
            {
                temporaryList[i].canUpgrade = true;
            }
            temporaryList.Clear();
            SwipeControl.SC.swipeWay = "";
            StartCoroutine(spawnBlock());

        }
        if (SwipeControl.SC.swipeWay == "Right") //Right 15 11 7 3
        {
            for (int i = 1; i < 5; i++)
            {
                for (int j = 4; j > 0; j--)
                {
                    int x = ((j * 4) - (1 * i));

                    Vector2 checkPos;

                    if (gridList[x].GetComponent<Grids>().myOBJ != null)
                    {
                        temporaryObj = gridList[x].GetComponent<Grids>().myOBJ;
                        checkPos = temporaryObj.transform.position;
                    }
                    else
                    {
                        continue;
                    }

                    Vector2 halfWidth = new Vector2(temporaryObj.transform.position.x + 0.53f, temporaryObj.transform.position.y);
                    RaycastHit2D hitRight = Physics2D.Raycast(halfWidth, Vector2.right, Mathf.Infinity);

                    if (hitRight.collider.gameObject)
                    {
                        if (hitRight.collider.CompareTag("Corner"))
                        {
                            temporaryObj.transform.DOMoveX(3, 0.1f);
                            previousPos = new Vector2(3, temporaryObj.transform.position.y);
                          
                        }

                        else if (hitRight.collider.CompareTag("Block"))
                        {
                            Block myObjsc = temporaryObj.GetComponent<Block>();
                            Block hitObjsc = hitRight.collider.gameObject.GetComponent<Block>();
                            GameObject hitObj = hitRight.collider.gameObject;

                            if (Upgradable(myObjsc, hitObjsc))
                            {
                                CombineTwoBlock(temporaryObj, hitObj, myObjsc, hitObjsc, previousPos);
                               

                            }
                            else
                            {
                                temporaryObj.transform.DOMoveX(previousPos.x - 1, 0.1f);
                                previousPos = new Vector2(previousPos.x - 1, temporaryObj.transform.position.y);
                            }
                        }
                    }
                    CheckisMove(temporaryObj.transform.position, previousPos);
                }
            }
            for (int i = 0; i < temporaryList.Count; i++)
            {
                temporaryList[i].canUpgrade = true;
            }
            temporaryList.Clear();
            SwipeControl.SC.swipeWay = "";
            StartCoroutine(spawnBlock());
        }
        if (SwipeControl.SC.swipeWay == "Down") //Down 3 7 11 15
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)   // 0 1 2 3 1 
                {
                    int x = j + (4*i);

                    if (gridList[x].GetComponent<Grids>().myOBJ != null)
                    {
                        temporaryObj = gridList[x].GetComponent<Grids>().myOBJ;
                    }
                    else
                    {
                        continue;
                    }

                    Vector2 halfWidth = new Vector2(temporaryObj.transform.position.x, temporaryObj.transform.position.y - 0.53f);
                    RaycastHit2D hitUp = Physics2D.Raycast(halfWidth, Vector2.down, Mathf.Infinity);

                    if (hitUp.collider.gameObject)
                    {
                        if (hitUp.collider.CompareTag("Corner"))
                        {
                            temporaryObj.transform.DOMoveY(0, 0.1f);
                            previousPos = new Vector2(temporaryObj.transform.position.x, 0);
                        }

                        else if (hitUp.collider.CompareTag("Block"))
                        {
                            Block myObjsc = temporaryObj.GetComponent<Block>();
                            Block hitObjsc = hitUp.collider.gameObject.GetComponent<Block>();
                            GameObject hitObj = hitUp.collider.gameObject;

                            if (Upgradable(myObjsc, hitObjsc))
                            {
                                CombineTwoBlock(temporaryObj, hitObj, myObjsc, hitObjsc, previousPos);

                            }
                            else
                            {
                                temporaryObj.transform.DOMoveY(previousPos.y + 1, 0.1f);
                                previousPos = new Vector2(temporaryObj.transform.position.x, previousPos.y + 1);
                            }
                        }
                    }
                    CheckisMove(temporaryObj.transform.position, previousPos);
                }              
            }
            for (int i = 0; i < temporaryList.Count; i++)
            {
                temporaryList[i].canUpgrade = true;
            }
            temporaryList.Clear();
            SwipeControl.SC.swipeWay = "";
            StartCoroutine(spawnBlock());
        }
    }

    private void CheckisMove(Vector2 startPos, Vector2 arrivePos)
    {
        // if these two vectors are equal isMove input +1 and if ismove equal to 0 it means dont spawn new block 
        if((int)startPos.x != (int)arrivePos.x || (int)startPos.y != (int)arrivePos.y)
        {
            isMove++;
        }
    }

    private bool Upgradable(Block myObj, Block hitObj)
    {
        // if these objects values are same and myobj's canupgrade true and hitobj's canupgrade true, return true means can upgradable
        return (myObj.value == hitObj.value && myObj.canUpgrade && hitObj.canUpgrade);
    }

    private void CombineTwoBlock(GameObject firstObj,GameObject secondObj,Block firstObjsc, Block secondObjsc , Vector2 Pos) //if call this func combine two block
    {       
        firstObj.transform.DOMove(Pos, 0.1f);
        firstObjsc.canUpgrade = false;
        firstObjsc.value *= 2;       

        temporaryList.Add(firstObjsc);

        Destroy(secondObj, 0.11f);

        firstObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        firstObj.transform.DOScale(defaultScale, 0.3f);


        score += firstObjsc.value;


        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
            UpdateHighScore?.Invoke(highScore);
        }

        
        UpdateScore?.Invoke(score);
    }

   


    public void FirstBlocks() //When start game spawn randomly pos 2 blocks 
    {
        int x = UnityEngine.Random.Range(0, 16);
        int y = UnityEngine.Random.Range(0, 16);

        if (x == y)
        {
            while (y == x)
            {
                y = UnityEngine.Random.Range(0, 16);
            }
        }

      
        GameObject xObj = Instantiate(blockPrefab, gridList[x].transform.position, Quaternion.identity);
        GameObject yObj = Instantiate(blockPrefab, gridList[y].transform.position,Quaternion.identity);

        xObj.transform.DOScale(defaultScale, 0.2f);
        yObj.transform.DOScale(defaultScale, 0.2f);
    }


    /*public void spawnBlock()
    {
        for (int i = 0; i < gridList.Count; i++)
        {
            if (gridList[i].GetComponent<Grids>().myOBJ == null)
            {
                emptyList.Add(gridList[i].transform);
            }
        }
        int x = Random.Range(0, emptyList.Count);

        GameObject block2 = ObjectPool.objects2.GetBlock();

        if (block2 != null)
        {
            block2.SetActive(true);
            block2.transform.position = emptyList[x].position;
        }
        emptyList.Clear();
    }*/



    public IEnumerator spawnBlock() //Spawn block after every move
    {

        yield return new WaitForSeconds(0.12f);

        for (int i = 0; i < gridList.Count; i++)
        {
            if (gridList[i].GetComponent<Grids>().myOBJ == null)
            {
                emptyList.Add(gridList[i].transform);
            }
        }
        if(isMove != 0)
        {
            //Spawn new block
            int x = UnityEngine.Random.Range(0, emptyList.Count);
            GameObject xObj = Instantiate(blockPrefab, emptyList[x].transform.position, Quaternion.identity);
            xObj.transform.DOScale(defaultScale, 0.2f);


        }

        if(emptyList.Count == 0)
        {
            //GameOver
            gameOverPanel.SetActive(true);
            Time.timeScale = 0.0f;
        }
        emptyList.Clear();
        isMove = 0;
    }





























    /*public void FirstBlocks()
    {
        int x = Random.Range(0, 16);
        int y = Random.Range(0, 16);
        
        if(x == y)
        {
            while (y == x)
            {
                y = Random.Range(0, 16);
            }
        }

        int[] num = { x, y };


        for (int i = 0; i < 2; i++)
        {
            GameObject _block = ObjectPool.objects2.GetBlock();
            if (_block != null)
            {
                _block.SetActive(true);
                _block.transform.position = gridList[num[i]].transform.position;
            }
        }
    }


    public void spawnBlock()
    {
        for (int i = 0; i < gridList.Count; i++)
        {
            if(gridList[i].GetComponent<Grids>().myOBJ == null)
            {
                emptyList.Add(gridList[i].transform);
            }
        }
        int x = Random.Range(0, emptyList.Count);

        GameObject block2 = ObjectPool.objects2.GetBlock();

        if (block2 != null)
        {
            block2.SetActive(true);
            block2.transform.position = emptyList[x].position;
        }
        emptyList.Clear();        
    }*/
}
