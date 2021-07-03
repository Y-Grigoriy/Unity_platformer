using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : MonoBehaviour {

    [SerializeField] private int rec = 5;
    private Health health;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            health = GameManager.Instance.healthContainer[col.gameObject];
            health.GiveHealth(rec);
            Destroy(gameObject);
        }
    }
}
