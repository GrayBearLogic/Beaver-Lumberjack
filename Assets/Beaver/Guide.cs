using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject model;
    [SerializeField] private float hideDistance = 1f;

    void Update()
    {
        transform.LookAt(target, Vector3.up);

        if (Vector3.Distance(transform.position, target.position) < hideDistance)
            model.SetActive(false);
        else
            model.SetActive(true);
    }
}
