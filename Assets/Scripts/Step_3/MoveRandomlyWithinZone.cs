using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomlyWithinZone : MonoBehaviour
{
    //public so we can access from other script
    public float x;
    public float y;
    public float z;
    [SerializeField] private Vector3 randomPos;
    [SerializeField] private float speed = 1f;

    private void OnEnable() 
    {
        GenerateRandomPos();
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private void GenerateRandomPos()
    {
        randomPos = new Vector3(Random.Range(0f, x), Random.Range(0f, y), Random.Range(0f, z));
    }

    private void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, randomPos, speed * Time.deltaTime);    
        CheckArrived();
    }

    private void CheckArrived()
    {
        if (Vector3.Distance(transform.position, randomPos) < 0.5f)
        {
            this.transform.position = randomPos;
            GenerateRandomPos();
        }
    }
}
