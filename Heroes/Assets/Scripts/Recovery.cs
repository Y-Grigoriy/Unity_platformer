using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : MonoBehaviour {

    public int rec = 5;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Health health = col.gameObject.GetComponent<Health>();
            health.GiveHealth(rec);
            Destroy(gameObject);
        }
    }
}
