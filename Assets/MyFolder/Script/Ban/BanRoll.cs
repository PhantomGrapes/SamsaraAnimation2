using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanRoll : BanItem {

    private void Start()
    {
        name = "roll";
        banList = new string[] { "roll", "move", "attack", "hit", "dead", "grabLedge", "celling", "throw" };
    }
}
