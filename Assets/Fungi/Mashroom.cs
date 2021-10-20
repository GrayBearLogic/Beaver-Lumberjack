using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashroom : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private Wallet wallet;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Beaver>(out var beaver))
        {
            wallet.Add(cost);
            gameObject.SetActive(false);
        }
    }
}
