using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public int goldCount;
    public int gemsCount;
    public int kristallsCount;

    void Awake()
    {
        Instance = this;
    }

    public static PlayerInventory Instance { get; set; }
}
