using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestNeighbour : MonoBehaviour, IGetNeighors
{
    [SerializeField] private List<Transform> allNeighbors = new List<Transform>();
    [SerializeField] private Transform closestNeighbor;
    [SerializeField] private string tag;

    void Start()
    {
        RecalculateNeighors();
    }

    private void OnEnable() 
    {
        //tell all neighbors to recalculate
        RecalculateNeighors();
        GameObject[] spheres = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject sphere in spheres)
        {
            IGetNeighors iGetNeighors = sphere.GetComponent<IGetNeighors>();
            if(iGetNeighors != null)
            {
                iGetNeighors.RecalculateNeighors();
            }
        }
    }

    public void RecalculateNeighors()
    {
        //Not using FindObjectsWithType because it is too slow performance-wise
        GameObject[] spheres = GameObject.FindGameObjectsWithTag(tag);

        //add to neighbor list only if it has script
        for (int i = 0; i < spheres.Length; i++)
        {
            if(spheres[i].GetComponent<FindNearestNeighbour>() != null && spheres[i] != this.gameObject) allNeighbors.Add(spheres[i].transform);
        }
    }
    
    private void FixedUpdate() 
    {
        closestNeighbor = GetClosestNeighbor(allNeighbors);
    }

    void OnDrawGizmos()
    { 
        Gizmos.color = Color.blue;
        if(closestNeighbor != null)
        {
            Gizmos.DrawLine(this.transform.position, closestNeighbor.position);
        }
    }

    private Transform GetClosestNeighbor(List<Transform> neighbors)
    {
        Transform transform_Min = null;
        float min_Dist = Mathf.Infinity;
        Vector3 current_Pos = transform.position;

        foreach (Transform transforms in neighbors)
        {
            float distance = Vector3.Distance(transforms.position, current_Pos);
            if (distance < min_Dist)
            {
                transform_Min = transforms;
                min_Dist = distance;
            }
        }
        return transform_Min;
    }

}
