using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health;
    public int typicalHealth;
    public int maximumHealth=100;

    private void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }

	// Update is called once per frame
	public void TakeHealth (int damage) {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
	}

    public void GiveHealth(int drug)
    {
        health += drug;
        if (health > maximumHealth)
            health = maximumHealth;
    }

    public void SetMaximumHealth(int maxHealth)
    {
        maximumHealth = maxHealth;
    }
}
