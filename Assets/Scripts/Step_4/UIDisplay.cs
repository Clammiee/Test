using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private Text amountSpawnedText;
    private GameObject poolOBJ;
    [SerializeField] private int spawnedCubesX;
    [SerializeField] private Text inputFieldText;

    private void Start() 
    {
        poolOBJ = PoolingSystem.Instance.gameObject;
    }

    private void SpawnCubes()
    {
        int amountActive = 0;

        for (int i = 0; i < poolOBJ.transform.childCount; i++)
        {
            if(poolOBJ.transform.GetChild(i).gameObject.activeInHierarchy) amountActive++;
        }

        if(amountActive < spawnedCubesX)
        {
            int SpawnAmount = spawnedCubesX - amountActive;

            for (int i = 0; i < SpawnAmount; i++)
            {
                //set to zero just to initialize
                GameObject cube = PoolingSystem.Instance.SpawnFromPool("Cube", Vector3.zero);

                //set real randomPos
                MoveRandomlyWithinZone moveScript = cube.GetComponent<MoveRandomlyWithinZone>();

                float xZone = moveScript.x;
                float yZone = moveScript.y;
                float zZone = moveScript.z;

                cube.transform.position = new Vector3(Random.Range(0f, xZone), Random.Range(0f, yZone), Random.Range(0f, zZone));
            }
        }
    }

    private void DeSpawnCubes()
    {
        int amountActive = 0;

        for (int i = 0; i < poolOBJ.transform.childCount; i++)
        {
            if(poolOBJ.transform.GetChild(i).gameObject.activeInHierarchy) amountActive++;
        }

        if(spawnedCubesX < amountActive)
        {
            int DeSpawnAmount = amountActive - spawnedCubesX;
            int DeSpawnCounter = 0;

            for (int i = 0; i < poolOBJ.transform.childCount; i++)
            {
                if(poolOBJ.transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    if(DeSpawnCounter <= DeSpawnAmount)
                    {
                        poolOBJ.transform.GetChild(i).gameObject.SetActive(false);
                        DeSpawnCounter++;
                    }
                }
            }
        }
    }

    public void GetInput(string input)
    {
        int xValue = 0;

        if(int.TryParse(input, out xValue))
        {
            spawnedCubesX = xValue;
            DeSpawnCubes();
            SpawnCubes();
        }
    }

    private void FixedUpdate() 
    {
        ShowAmountSpawned();
        GetInput(inputFieldText.text.ToString());
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
