using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
