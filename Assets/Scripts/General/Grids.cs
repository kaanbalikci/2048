using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grids : MonoBehaviour
{
    public bool isEmpty = true;

    public Block myOBJScript;
    public GameObject myOBJ;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.1f);
        if (hit)
        {
            if(hit.collider.CompareTag("Block"))
            {
                //Debug.Log("Block");
                myOBJScript = hit.collider.GetComponent<Block>();
                myOBJ = hit.collider.gameObject;
            }
                 
        }
        else
        {
            myOBJ = null;
            myOBJScript = null;
        }



    }




}
