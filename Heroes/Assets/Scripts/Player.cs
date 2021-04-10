using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Use this for initialization
    //void Start () {

    //}
    public float speed = 4.0f;
    public float jumpforce = 6.0f;
    public Rigidbody2D playerbody;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left*Time.deltaTime*speed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerbody.AddForce(Vector2.up* jumpforce,ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
    }
}
