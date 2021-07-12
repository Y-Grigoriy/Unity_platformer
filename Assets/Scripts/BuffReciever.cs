using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class BuffReciever : MonoBehaviour {

    private List<Buff> buffs;
    public List<Buff> Buffs
    {
        get { return buffs; }
    }
    public Action OnBuffsChanged;
    //public Action<int> OnBuffsChanged;

    private List<GameObject> buffPool = new List<GameObject>();
    [SerializeField] private GameObject[] buffEffect = { };
    [SerializeField] private GameObject[] buffEffectImage = { };
    [SerializeField] private GameObject inventoryPanel;

    void Start () {
        GameManager.Instance.buffReceiverContainer.Add(gameObject, this);
        buffs = new List<Buff>();

        for (int i = 0; i < buffEffect.Length; i++)
        {
            var effectTemp = Instantiate(buffEffect[i], gameObject.transform);
            buffPool.Add(effectTemp);
            effectTemp.gameObject.SetActive(false);
        }
    }

    public void AddBuff(Buff buff)
    {
        //Debug.Log(!buffs.Contains(buff) + "," +buffs.Count);
        if (!buffs.Contains(buff))
        {
            buffs.Add(buff);
            if (buff.type == BuffType.Damage)
                StartCoroutine(CloseInventory(PlayerTools.Player.Instance.gameObject, 0));
            if (buff.type == BuffType.Force)
                StartCoroutine(CloseInventory(PlayerTools.Player.Instance.gameObject, 1));
            //if (buff.type != BuffType.Armor)
            if (buff.type == BuffType.Damage)
                StartCoroutine(EndBuffEffect(buff, 0));
            if (buff.type == BuffType.Force)
                StartCoroutine(EndBuffEffect(buff, 1));
            if (OnBuffsChanged != null)
                OnBuffsChanged();
        } 
    }

    public void RemoveBuff(Buff buff)
    {
        if (buffs.Contains(buff))
            buffs.Remove(buff);

        if (OnBuffsChanged != null)
            OnBuffsChanged();
    }

    private GameObject ApplyEffect(GameObject hObject, int buffID)
    {
        if (buffPool[buffID] != null)
        {
            var effectTemp = buffPool[buffID];
            //buffPool.Remove(effectTemp);
            effectTemp.gameObject.SetActive(true);
            effectTemp.transform.parent = hObject.transform;
            effectTemp.transform.position = hObject.transform.position;
            effectTemp.transform.rotation = Quaternion.identity;
            return effectTemp;
        }
        return Instantiate(buffEffect[buffID], gameObject.transform.position, Quaternion.identity); // Запасное создание объекта
    }

    private IEnumerator CloseInventory(GameObject hObject, int buffID)
    {
        yield return new WaitUntil(() => !inventoryPanel.activeSelf);
        buffEffectImage[buffID].SetActive(true);
        GameObject effectTemp = ApplyEffect(PlayerTools.Player.Instance.gameObject, buffID);
        StartCoroutine(EndEffectAnimation(effectTemp));
        yield break;
    }

    private IEnumerator EndEffectAnimation(GameObject effectTemp)
    {
        yield return new WaitForSecondsRealtime(0.30f);
        effectTemp.gameObject.SetActive(false);
        yield break;
    }

    private IEnumerator EndBuffEffect(Buff buff, int buffID)
    {
        yield return new WaitUntil(() => !inventoryPanel.activeSelf);
        yield return new WaitForSeconds(2.0f);
        RemoveBuff(buff);
        buffEffectImage[buffID].SetActive(false);
        yield break;
    }
}
