using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShootBar : MonoBehaviour
{

    [SerializeField] private Image shoot;
    [SerializeField] private AnimationClip shootClip;
    private float clipTime;
    public float timeBeforeShoot = 0.0f;
    //private float delta = 0.008f;

    void Start()
    {
        clipTime = shootClip.length;
        //Debug.Log(clipTime);
    }

    void FixedUpdate()
    {
        if (PlayerTools.Player.Instance.shoot == true)
            timeBeforeShoot -= Time.deltaTime;
        else
            timeBeforeShoot = clipTime;
    }

    // Update is called once per frame
    void Update()
    {
        shoot.fillAmount = PlayerTools.Player.Instance.shoot == true ? (timeBeforeShoot / clipTime) : 0;
    }
}
