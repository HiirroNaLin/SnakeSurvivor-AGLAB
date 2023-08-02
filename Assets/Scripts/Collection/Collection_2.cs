using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_2 : MonoBehaviour
{
    [SerializeField] AddInTailEventChannel add_12;
    [SerializeField] EventChannel trimatch;
    [SerializeField] EventChannel combinecolor;

    [SerializeField] private int nodeID = 1;
    [SerializeField] private int weaponID = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Head"))
        {
            add_12.Broadcast(nodeID, weaponID);
            trimatch.Broadcast();
            combinecolor.Broadcast();
            Destroy(gameObject);
        }

    }
}
