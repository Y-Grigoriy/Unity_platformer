using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private bool visitSpring = false;
    [SerializeField] private int rec = 20;
    private List<GameObject> effectsPool;
    [SerializeField] private GameObject effect;
    private Health health;

    void Start()
    {
        effectsPool = new List<GameObject>();
        var effectTemp = Instantiate(effect, gameObject.transform);
        effectsPool.Add(effectTemp);
        effectTemp.gameObject.SetActive(false);
        //GameManager.Instance.animatorContainer.Add(effectTemp.gameObject, effectTemp.gameObject.GetComponent<Animator>());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject) && !visitSpring)
        {
            visitSpring = true;
            StartCoroutine(SpringRecreation());
            GameObject effectTemp = ApplyEffect(col.gameObject);
            StartCoroutine(EndHealing(col.gameObject, effectTemp));
        }
    }

    private GameObject ApplyEffect(GameObject hObject)
    {
        if (effectsPool.Count > 0)
        {
            var effectTemp = effectsPool[0];
            effectsPool.Remove(effectTemp);
            effectTemp.gameObject.SetActive(true);
            effectTemp.transform.parent = hObject.transform;
            effectTemp.transform.position = hObject.transform.position;
            effectTemp.transform.rotation = Quaternion.identity;
            return effectTemp;
        }
        return Instantiate(effect, gameObject.transform.position, Quaternion.identity); // Запасное создание объекта
    }

    public void ReturnEffectToPool(GameObject effectTemp)
    {
        if (!effectsPool.Contains(effectTemp))
            effectsPool.Add(effectTemp);

        effectTemp.transform.parent = gameObject.transform;
        effectTemp.transform.position = gameObject.transform.position;
        effectTemp.gameObject.SetActive(false);
    }

    private IEnumerator SpringRecreation()
    {
        yield return new WaitForSecondsRealtime(10);
        visitSpring = false;
        yield break;
    }

    private IEnumerator EndHealing(GameObject hObject, GameObject effectTemp)
    {
        yield return new WaitForSecondsRealtime(0.30f);
        health = GameManager.Instance.healthContainer[hObject];
        health.GiveHealth(rec);
        ReturnEffectToPool(effectTemp);
        yield break;
    }
}
