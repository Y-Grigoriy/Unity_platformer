using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    public bool isGrounded;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Trap")) // || col.gameObject.CompareTag("Enemy")
            isGrounded = true;
    }
    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground")|| col.gameObject.CompareTag("Trap")) // || col.gameObject.CompareTag("Enemy")
            isGrounded = true;
    }*/

    void OnTriggerExit2D(Collider2D isCol)
    {
        if (isCol.gameObject.CompareTag("Ground") || isCol.gameObject.CompareTag("Trap"))
            isGrounded = false;
    }
}
