using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject leftBorder;
    public GameObject rightBorder;
    public bool isRightMotion;
    public Rigidbody2D Body;
    public GroundDetection gD;
    [SerializeField] private float speed = 0.7f;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > 0.1)
                speed = value;
        }
    }
    [SerializeField] private SpriteRenderer EnemyRender;
    [SerializeField] private DamageCollision dG;
	
    void Start()
    {
        GameManager.Instance.animatorContainer.Add(gameObject, gameObject.GetComponent<Animator>());
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (gD.isGrounded && Body != null)
        {
            //Debug.Log(dG.Direction);
            if (transform.position.x > rightBorder.transform.position.x || dG.Direction < 0)
                isRightMotion = false;
            else if (transform.position.x < leftBorder.transform.position.x || dG.Direction > 0)
                isRightMotion = true;
            Body.velocity = isRightMotion ? Vector2.right : Vector2.left;
            Body.velocity = Body.velocity * speed;
        }
        //Debug.Log(Body.velocity.x);
        if (Body.velocity.x < 0 && Body != null)
            EnemyRender.flipX = true;
        if (Body.velocity.x > 0 && Body != null)
            EnemyRender.flipX = false;
        /*if (isRightMotion && groundDetection.isGrounded)
        {
            Body.velocity = Vector2.right * speede;
            playerRender.flipX = false;
            if (transform.position.x > rightBorder.transform.position.x)
                isRightMotion = !isRightMotion;
        }
        else if (groundDetection.isGrounded)
        {
            Body.velocity = Vector2.left * speede;
            playerRender.flipX = true;
            if (transform.position.x < leftBorder.transform.position.x)
                isRightMotion = !isRightMotion;
        }*/
        if (transform.position.y < -5f)
            Destroy(gameObject);
    }
}
