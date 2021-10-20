using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        transform.LookAt(target, Vector3.up);
    }
}
