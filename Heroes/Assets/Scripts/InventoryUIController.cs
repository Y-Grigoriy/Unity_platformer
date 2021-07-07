using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour {

    Cell[] cells;
    [SerializeField] private int cellCount;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform rootParent;
    //private PlayerInventory inventory;
    //private int itemInventoryCount=0;

    /*void Start()
    {
        inventory = GameManager.Instance.inventory;
    }*/

	// Use this for initialization
	void InitС () {

        cells = new Cell[cellCount];
        for (int i=0;i<cellCount;i++)
        {
            cells[i] = Instantiate(cellPrefab, rootParent);
        }
        cellPrefab.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (cells == null || cells.Length < cellCount)
            InitС();

        //if (inventory == null)
        var inventory = GameManager.Instance.inventory;

        for (int i = 0; i < cells.Length; i++)
        {
            //Debug.LogError(i);
            cells[i].Init(i < inventory.Items.Count ? inventory.Items[i] : null);
        }

        /*Debug.Log(inventory.Items.Count + "" + itemInventoryCount);
        if (inventory.Items.Count > itemInventoryCount)
        {
            itemInventoryCount = inventory.Items.Count;
            for (int i = 0; i < cells.Length; i++)
            {
                //Debug.LogError(i);
                cells[i].Init(i < inventory.Items.Count ? inventory.Items[i] : null);
            }
        }*/

        /*for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (i < cells.Length)
            {
                cells[i].Init(inventory.Items[i]);
            }
                
        }*/
    }
}
