using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    public static SwipeControl SC;

    public Vector2 firstPos;
    public Vector2 secondPos;

    public string swipeWay = "";

    public bool move;
    private void Awake()
    {
        SC = this;
    }

    private void Update()
    {      
        if (swipeWay == "") 
        {
            Swipe();
        }
    
        
    }

    public void Swipe() //Check swipe direction
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;

        }

        if (Input.GetMouseButtonUp(0))
        {
            secondPos = Input.mousePosition;

        }


        if (secondPos != Vector2.zero && !ButtonManager.BM.menuPanel.activeInHierarchy) //if mouse button up in different vector and is menu panel setactive false 
        {
            int controlX = (int)firstPos.x - (int)secondPos.x;
            int controlY = (int)firstPos.y - (int)secondPos.y;

            if (Mathf.Abs(controlX) > Mathf.Abs(controlY)) 
            {
                if (controlX < 0)
                {
                    swipeWay = "Right";

                }
                else if (controlX > 0)
                {
                    swipeWay = "Left";
                }
            }
            else if (Mathf.Abs(controlX) < Mathf.Abs(controlY))
            {
                if (controlY < 0)
                {
                    swipeWay = "Up";
                }
                else if (controlY > 0)
                {
                    swipeWay = "Down";
                }
            }

            firstPos = Vector2.zero;
            secondPos = Vector2.zero;

        }

    }

}
