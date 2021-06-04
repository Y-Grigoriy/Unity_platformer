using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour {

    private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == parent)
            return;
        var health = col.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeHealth(damage);
            col.GetComponent<Animator>().SetTrigger("TakeHit");
            Destroy(gameObject);
        }
    }
}
