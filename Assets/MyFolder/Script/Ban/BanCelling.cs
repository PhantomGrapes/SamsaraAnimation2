using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanCelling : BanItem
{
    private void Start()
    {
        name = "celling";
        banList = new string[] { "roll", "move", "attack", "hit", "dead", "grabLedge", "celling" };
    }
}
