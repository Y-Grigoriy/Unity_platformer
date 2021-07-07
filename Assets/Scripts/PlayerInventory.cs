using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public int goldCount;
    public int gemsCount;
    public int kristallsCount;

    [SerializeField] private Text goldText;
    [SerializeField] private Text gemText;
    [SerializeField] private Text kristallText;

    public BuffReciever buffReceiver;
    private List<Item> items;
    public List<Item> Items
    {
        get { return items; }
    }

    void Awake()
    {
        Instance = this;
    }

    public static PlayerInventory Instance { get; set; }

    // Initialization
    void Start()
    {
        GameManager.Instance.inventory = this;
        goldText.text = "0";
        gemText.text = "0";
        kristallText.text = "0";
        items = new List<Item>();
    }

    // collection of resources
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GameManager.Instance.itemsContainer.ContainsKey(col.gameObject))
        {
            var itemComponent = GameManager.Instance.itemsContainer[col.gameObject];
            items.Add(itemComponent.NewItem);
            itemComponent.Destroy(col.gameObject);
        }

        ResourceCollection(col.gameObject);
        /*if (col.gameObject.CompareTag("Gold") && GameManager.Instance.resourcesContainer.ContainsKey(col.gameObject))
        {
            PlayerInventory.Instance.goldCount++;
            Debug.Log("Amount of gold" + PlayerInventory.Instance.goldCount);
            GameManager.Instance.resourcesContainer[col.gameObject].StartDestroy();
            GameManager.Instance.resourcesContainer.Remove(col.gameObject);
        }

        if (col.gameObject.CompareTag("Gems") && GameManager.Instance.resourcesContainer.ContainsKey(col.gameObject))
        {
            PlayerInventory.Instance.gemsCount++;
            Debug.Log("Amount of gems" + PlayerInventory.Instance.gemsCount);
            GameManager.Instance.resourcesContainer[col.gameObject].StartDestroy();
            GameManager.Instance.resourcesContainer.Remove(col.gameObject);
        }

        if (col.gameObject.CompareTag("Kristall") && GameManager.Instance.resourcesContainer.ContainsKey(col.gameObject))
        {
            PlayerInventory.Instance.kristallsCount++;
            Debug.Log("Amount of kristalls" + PlayerInventory.Instance.kristallsCount);
            GameManager.Instance.resourcesContainer[col.gameObject].StartDestroy();
            GameManager.Instance.resourcesContainer.Remove(col.gameObject);
        }*/
    }

    private void ResourceCollection(GameObject resource)
    {
        if (GameManager.Instance.resourcesContainer.ContainsKey(resource))
        {
            if (resource.CompareTag("Kristall"))
            {
                kristallsCount++;
                kristallText.text = "" + kristallsCount;
            }
            if (resource.CompareTag("Gold"))
            {
                goldCount++;
                goldText.text = "" + goldCount;
            }
            if (resource.CompareTag("Gems"))
            {
                gemsCount++;
                gemText.text = "" + gemsCount;
            }
            //Debug.Log("Amount of "+ resource.tag + PlayerInventory.Instance.kristallsCount);
            GameManager.Instance.resourcesContainer[resource].StartDestroy();
            GameManager.Instance.resourcesContainer.Remove(resource);
        }
    }
}
