using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    [SerializeField] private Image icon;
    [SerializeField] private Sprite emptySprite;
    private Item item;
    private Buff buff = new Buff();


    void Awake () {
        //icon.sprite = null;
	}

    public void Init (Item item) {
        this.item = item;
        //icon.sprite = item.Icon;
        icon.sprite = item != null ? item.Icon : emptySprite;
    }

    public void OnClickCell()
    {
        if (item == null)
            return;

        Debug.Log(!GameManager.Instance.inventory.buffReceiver.Buffs.Contains(buff));
        if (!GameManager.Instance.inventory.buffReceiver.Buffs.Contains(buff) &&
            !(PlayerTools.Player.Instance.bonusForce == item.Value && item.Type == BuffType.Force)
            && !(PlayerTools.Player.Instance.bonusDamage == item.Value && item.Type == BuffType.Damage)
            && !(PlayerTools.Player.Instance.bonusArmor == item.Value && item.Type == BuffType.Armor))
        {
            buff.type = item.Type;
            buff.additiveBonus = item.Value;
            if (item.Type != BuffType.Armor)
            {
                GameManager.Instance.inventory.Items.Remove(item);
                item = null;
                icon.sprite = emptySprite;
            }
            GameManager.Instance.inventory.buffReceiver.AddBuff(buff);
        }
    }
}
