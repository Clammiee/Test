using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPooling : MonoBehaviour
{
    void Start()
    {
        PoolingSystem.Instance.SpawnFromPool("Cube", this.transform.position);
        PoolingSystem.Instance.ExpandPool("Cube", 3);
    }
}
