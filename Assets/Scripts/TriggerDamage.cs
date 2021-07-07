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
    private Health health;
    private IObjectDestroyer destroyer;

    public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == parent)
            return;
        //health = col.gameObject.GetComponent<Health>();
        //var health = col.gameObject.GetComponent<Health>();
        //if (health != null)
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            health = GameManager.Instance.healthContainer[col.gameObject];
            health.TakeHealth(damage);
            col.GetComponent<Animator>().SetTrigger("TakeHit");
            if (destroyer == null)
                Destroy(gameObject);
            else destroyer.Destroy(gameObject);
        }
    }
}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}
