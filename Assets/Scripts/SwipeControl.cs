using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    Vector2 firstPos;
    Vector2 secondPos;


    void Update()
    {
      
        Swipe();
        
        
    }

    public void Swipe()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
          
        }

        if (Input.GetMouseButtonUp(0))
        {
            secondPos = Input.mousePosition;
           
        }


        if(secondPos != Vector2.zero)
        {
            int controlX = (int)firstPos.x - (int)secondPos.x;
            int controlY = (int)firstPos.y - (int)secondPos.y;

            if(Mathf.Abs(controlX) > Mathf.Abs(controlY))
            {
                if(controlX < 0)
                {
                    Debug.Log("Right");
                }
                else if(controlX > 0)
                {
                    Debug.Log("Left");
                }
            }
            else if(Mathf.Abs(controlX) < Mathf.Abs(controlY))
            {
                if (controlY < 0)
                {
                    Debug.Log("Up");
                }
                else if (controlY > 0)
                {
                    Debug.Log("Down");
                }
            }

            firstPos = Vector2.zero;
            secondPos = Vector2.zero;

        }

    }
}
