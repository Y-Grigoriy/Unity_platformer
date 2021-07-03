using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour {

    Cell[] cells;
    [SerializeField] private int cellCount;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform rootParent;

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
        if (cells == null || cells.Length <= cellCount)
            InitС();
        var inventory = GameManager.Instance.inventory;
        
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            if (i < cells.Length)
            {
                Debug.Log(inventory.Items.Count);
                cells[i].Init(inventory.Items[i]);
            }
                
        }
    }
}
