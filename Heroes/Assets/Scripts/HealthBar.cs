using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField] private Image health;
    private float healthValue, currentHealthValue;
    [SerializeField] private GameObject player;
    private float delta = 0.01f;

	// Use this for initialization
	void Start () {
        currentHealthValue = 1;
    }
	
	// Update is called once per frame
	void Update () {
        healthValue = GameManager.Instance.healthContainer[player].health / 100.0f;
        if (healthValue < currentHealthValue)
            currentHealthValue -= delta;
        if (healthValue > currentHealthValue)
            currentHealthValue += delta;
        if (Mathf.Abs(currentHealthValue - healthValue)<delta)
        {
            currentHealthValue = healthValue;
        }
        health.fillAmount = currentHealthValue;

    }
}
