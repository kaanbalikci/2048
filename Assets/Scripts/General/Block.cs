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
        switch (value)
        {
            case 2:
                sr.color = cs.colorList[0];
                blockValueText.text = "2";
                blockValueText.fontSize = 7;             
                break;
            case 4:
                sr.color = cs.colorList[1];
                blockValueText.text = "4";
                blockValueText.fontSize = 7;
                break;
            case 8:
                sr.color = cs.colorList[2];
                blockValueText.text = "8";
                blockValueText.fontSize = 7;
                break;
            case 16:
                sr.color = cs.colorList[3];
                blockValueText.text = "16";
                blockValueText.fontSize = 6.5f;
                break;
            case 32:
                sr.color = cs.colorList[4];
                blockValueText.text = "32";
                blockValueText.fontSize = 6.5f;
                break;
            case 64:
                sr.color = cs.colorList[5];
                blockValueText.text = "64";
                blockValueText.fontSize = 6.5f;
                break;
            case 128:
                sr.color = cs.colorList[6];
                blockValueText.text = "128";
                blockValueText.fontSize = 5;
                blockValueText.color = Color.white;
                break;
            case 256:
                sr.color = cs.colorList[7];
                blockValueText.text = "256";
                blockValueText.fontSize = 5;
                blockValueText.color = Color.white;
                break;
            case 512:
                sr.color = cs.colorList[8];
                blockValueText.text = "512";
                blockValueText.fontSize = 5;
                blockValueText.color = Color.white;
                break;
            case 1024:
                sr.color = cs.colorList[9];
                blockValueText.text = "1024";
                blockValueText.fontSize = 3.5f;
                blockValueText.color = Color.white;
                break;
            case 2048:
                sr.color = cs.colorList[10];
                blockValueText.text = "2048";
                blockValueText.fontSize = 3.5f;
                blockValueText.color = Color.white;
                break;

        }
    }


}
