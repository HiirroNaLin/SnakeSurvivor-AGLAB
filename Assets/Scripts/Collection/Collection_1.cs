using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_1 : MonoBehaviour
{
    [SerializeField] AddInTailEventChannel add_11;
    [SerializeField] EventChannel trimatch;
    [SerializeField] EventChannel combinecolor;

    [SerializeField] private int nodeID = 1;
    [SerializeField] private int weaponID = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Head"))
        {
            add_11.Broadcast(nodeID, weaponID);
            trimatch.Broadcast();
            combinecolor.Broadcast();
            Destroy(gameObject);
        }

    }
}
