using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelContact : MonoBehaviour {

    public List<GameObject> go = new List<GameObject>();

    /*private void OnCollisionEnter2D(Collision2D col)
    {
        DamageCollision tra= GameObject.Find("Player").GetComponent<traps>();
        for (int j = 0; j < tra.Length; j++)
            if ((col.gameObject.name == tra[j].name) && (!go.Contains(col.gameObject)))
                go.Add(col.gameObject);
    }*/
}
