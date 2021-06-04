using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    [SerializeField] private SpriteRenderer sR;
    [SerializeField] private Rigidbody2D rB;
    [SerializeField] private float force;
    [SerializeField] private float lifeTime;
    [SerializeField] private TriggerDamage triggerDamage;
    public float Force
    {
        get { return force; }
        set { force = value;}
    }

    // Use this for initialization
    public void SetImpulse (Vector2 direction, float force, GameObject parent)
    {
        triggerDamage.Parent = parent;
        if (force < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        rB.AddForce(direction * force, ForceMode2D.Impulse);
        if (gameObject != null)
            StartCoroutine(StartLife());
	}

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        if (transform.position.y < -5f)
            Destroy(gameObject);
        else
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<Animator>());
            Destroy(gameObject.GetComponent("TriggerDamage"));
            //gameObject.GetComponent<Rigidbody2D>().enable = false;
        }
        yield break;
    }
}
