using UnityEngine;
using UnityEngine.EventSystems;

public class Tree : MonoBehaviour
{
    [SerializeField] private Master master;
    [SerializeField] private Aura aura;
    [Space]
    public Transform rig;
    public Transform beaverPoint;
    public Transform cameraPoint;
    [Space] 
    [SerializeField] public DeadTree deadTree;
    [SerializeField] private Transform upperWoodPart;
    [SerializeField] private Transform lowerWoodPart;
    [SerializeField] public float fullEatingProgress = 0.1f;
    
    private float eatingProgress;
    private bool readyToBeEaten;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Beaver>(out var beaver) && beaver.IsCarryLog == false)
        {
            aura.Show();
            readyToBeEaten = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Beaver>(out var beaver) && beaver.IsCarryLog == false)
        {
            aura.Hide();
            readyToBeEaten = false;
        }
    }

    public void BeEaten(float value)
    {
        upperWoodPart.Translate(0, value, 0, Space.Self);
        lowerWoodPart.Translate(0, value, 0, Space.Self);

        eatingProgress += value;
        master.progressBar.Fullness = eatingProgress / fullEatingProgress;
        if (eatingProgress >= fullEatingProgress)
            master.Eating_Walking(this);
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && readyToBeEaten)
        {
            readyToBeEaten = false;
            master.Walking_Eating(this);
        }
    }
}
