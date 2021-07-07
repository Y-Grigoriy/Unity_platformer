using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageCollision : MonoBehaviour {

    public int damage = 10;
    int wheelDamage = 2;
    [SerializeField] private string collisionTag;
    public Rigidbody2D[] traps = { };
    List<GameObject> go = new List<GameObject>();
    public float repeat_time=10, repeat_time2 = 5;
    private float curr_time;
    private Health health;
    private Animator animator;
    private GroundDetection GD;
    private float direction;
    public float Direction
    {
        get { return direction; }
    }

    void Start()
    {
        curr_time = repeat_time;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            health = GameManager.Instance.healthContainer[col.gameObject];
            animator = GameManager.Instance.animatorContainer[col.gameObject];
            direction = (col.transform.position - transform.position).x;
            if (col.gameObject.CompareTag("Enemy") && gameObject.tag == "projectile")
            {
                animator.SetFloat("Direction", Mathf.Abs(direction));
                GameManager.Instance.animatorContainer[gameObject].SetFloat("Direction", Mathf.Abs(direction));
            }
            else if (gameObject.tag != "projectile") 
            {
                if (collisionTag == "Enemy")
                    animator.SetFloat("Direction", Mathf.Abs(direction));
                if (collisionTag == "Player")
                    animator.SetTrigger("TakeHit");
            }
        }

        for (int j = 0; j < traps.Length; j++)
            if ((col.gameObject.name == traps[j].name) && (!go.Contains(col.gameObject)))
                go.Add(col.gameObject);
        /*if (go.Count >= 2)
        {
            Health health = GameObject.Find("Player").GetComponent<Health>();
            health.TakeHealth(wheelDamage);
        }
        Debug.Log(go.Count);*/
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        curr_time -= Time.deltaTime; // subtract frame time
        if (col.gameObject.CompareTag(collisionTag) && (curr_time <= 0))
        {
            if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
            {
                health = GameManager.Instance.healthContainer[col.gameObject];
                animator = GameManager.Instance.animatorContainer[col.gameObject];
                direction = (col.transform.position - transform.position).x;
                if (col.gameObject.CompareTag("Enemy") && gameObject.tag == "projectile")
                {
                    animator.SetFloat("Direction", Mathf.Abs(direction));
                    GameManager.Instance.animatorContainer[gameObject].SetFloat("Direction", Mathf.Abs(direction));
                }
                else if (gameObject.tag != "projectile")
                {
                    if (collisionTag == "Enemy")
                        animator.SetFloat("Direction", Mathf.Abs(direction));
                    if (collisionTag == "Player")
                        animator.SetTrigger("TakeHit");
                }
            }
            curr_time = repeat_time2; // update timer
        }
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag(collisionTag) && gameObject.CompareTag("Enemy"))
        {
            direction = 0.0f;
            GameManager.Instance.animatorContainer[gameObject].SetFloat("Direction", direction);
        }
        else if (collisionInfo.gameObject.CompareTag(collisionTag) && gameObject.CompareTag("Player"))
        {
            direction = 0.0f;
            animator.SetFloat("Direction", direction);
        }
        for (int j = 0; j < traps.Length; j++)
            if ((collisionInfo.gameObject.name == traps[j].name) && (go.Contains(collisionInfo.gameObject)))
                this.go.Remove(collisionInfo.gameObject);
    }

    public void SetDamage()
    {
        if (health != null)
            health.TakeHealth(damage);
        health = null;
        if (collisionTag == "Enemy")
        {
            direction = 0.0f;
            if (animator != null)
                animator.SetFloat("Direction", Mathf.Abs(direction));
        }
    }

    void Update()
    {
        //Debug.Log(go.Count);
        //Debug.Log(curr_time);
        curr_time -= Time.deltaTime; // subtract frame time
        if ((go.Count >= 2) && (curr_time <= 0))
        {
            health = GameManager.Instance.healthContainer[gameObject];
            health.TakeHealth(wheelDamage);
            health = null;
            GameManager.Instance.animatorContainer[gameObject].SetTrigger("TakeHit");
            curr_time = repeat_time; // update timer
        }
    }
}
