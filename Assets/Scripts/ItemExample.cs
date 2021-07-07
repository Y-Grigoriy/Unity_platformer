using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item2",menuName ="New Entity/New Item2")]

public class ItemExample : ScriptableObject {

	[SerializeField] private int ID;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private BuffType type;
    [SerializeField] private float value;
}
