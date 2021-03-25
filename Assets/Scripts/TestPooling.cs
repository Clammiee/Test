using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPooling : MonoBehaviour
{
    void Start()
    {
        PoolingSystem.Instance.SpawnFromPool("Sphere", this.transform.position);
        PoolingSystem.Instance.ExpandPool("Sphere", 3);
    }

    void Update()
    {
        
    }
}
