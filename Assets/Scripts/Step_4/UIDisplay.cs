using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private Text amountSpawnedText;
    private GameObject poolOBJ;
    

    private void Start() 
    {
        poolOBJ = PoolingSystem.Instance.gameObject;
    }

    private void FixedUpdate() 
    {
        ShowAmountSpawned();
    }

    private void ShowAmountSpawned()
    {
        int amount = 0;

        for (int i = 0; i < poolOBJ.transform.childCount; i++)
        {
            if(poolOBJ.transform.GetChild(i).gameObject.activeInHierarchy) amount++;
        }
        amountSpawnedText.text = "Amount of spawned Prefabs: " + amount.ToString();
    }
}
