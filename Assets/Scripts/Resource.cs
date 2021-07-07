using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    [SerializeField] private Animator animator;

    private void Start()
    {
        GameManager.Instance.resourcesContainer.Add(gameObject, this);
    }

    public void StartDestroy () {
        animator.SetTrigger("Destruction");
	}

	public void EndDestroy () {
        Destroy(gameObject);
	}
}
