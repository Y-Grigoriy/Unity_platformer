using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour {

    public bool endHealing = false;

    public void EffectAction()
    {
        endHealing = true;
    }
}
