using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public int value;
    public int color;
    public bool canUpgrade = true;
    public Color defColor;

    [SerializeField] private ColorScript cs;
    [SerializeField] private TMP_Text blockValueText;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public int[] holdNo = { 2, 4 };
    private void Start()
    {
        
        int Range = Random.Range(0, 2);
        value = holdNo[Range];
    }

    private void Update()
    {
        switch (value) // this function set different settings for every value
        {
            case 2:
                UpdateBlock(0, "2", 7f, defColor);       
                break;
            case 4:
                UpdateBlock(1, "4", 7f, defColor);
                break;
            case 8:
                UpdateBlock(2, "8", 7f, defColor);
                break;
            case 16:
                UpdateBlock(3, "16", 6.5f, defColor);
                break;
            case 32:
                UpdateBlock(4, "32", 6.5f, defColor);
                break;
            case 64:
                UpdateBlock(5, "64", 6.5f, defColor);
                break;
            case 128:
                UpdateBlock(6, "128", 5f, Color.white);
                break;
            case 256:
                UpdateBlock(7, "256", 5f, Color.white);
                break;
            case 512:
                UpdateBlock(8, "512", 5f, Color.white);
                break;
            case 1024:
                UpdateBlock(9, "1024", 3.5f, Color.white);
                break;
            case 2048:
                UpdateBlock(10, "2048", 3.5f, Color.white);
                break;

        }

        if(value > 2050)
        {
            UpdateBlock(11 , value.ToString() , 2.5f, Color.white);
        }
    }

    private void UpdateBlock(int x,string text,float fontSize,Color _color)
    {
        sr.color = cs.colorList[x]; //Block color
        blockValueText.text = text; //Block number
        blockValueText.fontSize = fontSize; //Block text size
        blockValueText.color = _color; //Block text color
        
    }

}
