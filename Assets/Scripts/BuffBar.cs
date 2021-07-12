using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuffBar : MonoBehaviour {

	[SerializeField] private Image buff;
    public float timeBeforeEnd=0;

    void Start()
    {
        Debug.Log(timeBeforeEnd);
    }

    void FixedUpdate()
    {
        //Debug.Log(timeBeforeEnd);
        timeBeforeEnd += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        buff.fillAmount = (2f - timeBeforeEnd) / 2f;
        if (timeBeforeEnd >= 2f)
            timeBeforeEnd = 0;
    }
}
