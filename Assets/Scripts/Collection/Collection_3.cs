using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_3 : MonoBehaviour
{
    [SerializeField] AddInTailEventChannel add_13;
    [SerializeField] EventChannel trimatch;
    [SerializeField] EventChannel combinecolor;

    [SerializeField]private int nodeID = 1;
    [SerializeField]private int weaponID = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Head"))
        {
            add_13.Broadcast(nodeID, weaponID);
            trimatch.Broadcast();
            combinecolor.Broadcast();
            Destroy(gameObject);
        }
        
    }
}
