using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool objects2;

    private List<GameObject> blockPoolList = new List<GameObject>();
    private int blockPoolSize = 32;
    [SerializeField] private GameObject blockPrefab;

    private void Awake()
    {
        objects2 = this;
    }

    void Start()
    {
        for (int i = 0; i < blockPoolSize; i++)
        {
            GameObject blockObj = Instantiate(blockPrefab);
            blockObj.SetActive(false);
            blockPoolList.Add(blockObj);
        }
    }

    public GameObject GetBlock()
    {
        for (int i = 0; i < blockPoolList.Count; i++)
        {
            if (!blockPoolList[i].activeInHierarchy)
            {
                return blockPoolList[i];
            }
        }

        return null;
    }
}
