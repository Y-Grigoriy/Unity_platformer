using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffReciever : MonoBehaviour {

    private List<Buff> buffs;
    public Action OnBuffsChanged;

	void Start () {
        GameManager.Instance.buffReceiverContainer.Add(gameObject, this);
        buffs = new List<Buff>();
    }

    public void AddBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
            buffs.Add(buff);

        if (OnBuffsChanged != null)
            OnBuffsChanged();
    }

    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
            buffs.Remove(buff);

        if (OnBuffsChanged != null)
            OnBuffsChanged();
    }
}
