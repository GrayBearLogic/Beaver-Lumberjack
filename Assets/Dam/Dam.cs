using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dam : MonoBehaviour
{
    [SerializeField] private Master master;
    [SerializeField] public Transform cameraPoint;
    [Space]
    [SerializeField] private Transform[] buildPoints;
    [SerializeField] private List<GameObject> leftSections;
    [Space]
    public int buildingTime;
    private int buildProgress;
    private NumbersLoop indices;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Beaver>(out var beaver) && beaver.IsCarryLog)
        {
            indices = new NumbersLoop(1, buildPoints.Length - 1);
            master.Walking_Building(this);
            leftSections.First().SetActive(true);
            master.beaver.MoveToPoint(buildPoints[indices.Next]);
            buildProgress = 0;
        }
    }

    public void BeBuilt()
    {
        buildProgress++;
        if (buildProgress < buildingTime)
        {
            master.beaver.MoveToPoint(buildPoints[indices.Next]);
        }
        else
        {
            Complete();
        }

        master.progressBar.Fullness = (float)buildProgress / buildingTime;
    }
    
    private void Complete()
    {
        master.beaver.MoveToPoint(buildPoints.First());

        var newestSection = leftSections.First().transform.position;
        transform.position = new Vector3(newestSection.x, 0, newestSection.z);

        master.Building_Walking();

        leftSections.RemoveAt(0);
        if (leftSections.Count == 0)
        {
            master.Building_EndMenu();
            Destroy(gameObject);
        }
    }
}
