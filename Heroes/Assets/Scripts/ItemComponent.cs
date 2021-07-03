using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IObjectDestroyer {

	[SerializeField] private ItemType type;
    [SerializeField] private SpriteRenderer itemRenderer;
    private Item item;
    public Item NewItem
    {
        get { return item; }
    }

    public void Destroy(GameObject gameobject)
    {
        MonoBehaviour.Destroy(gameobject);
    }

	void Start () {
        item = GameManager.Instance.itemDataBase.GetItemOfID((int)type);
        itemRenderer.sprite = item.Icon;
        GameManager.Instance.itemsContainer.Add(gameObject,this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum ItemType
{
    DamagePoison=1,
    ForceDrink=2,
    ArmorObject=3
}
