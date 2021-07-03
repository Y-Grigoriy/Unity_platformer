using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    [SerializeField] private Image icon;
    //[SerializeField] private Image soundButtonRender;
    private Item item;

	void Awake () {
        icon.sprite = null;
	}
	
	public void Init (Item item) {
        this.item = item;
        icon.sprite = item.Icon;
	}

    public void OnClickCell()
    {
        if (item == null)
            return;
        GameManager.Instance.inventory.Items.Remove(item);
        Buff buff = new Buff
        {
            type = item.Type,
            additiveBonus = item.Value
        };
        GameManager.Instance.inventory.buffReceiver.AddBuff(buff);
    }
}
