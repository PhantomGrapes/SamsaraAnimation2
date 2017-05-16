using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanMove : BanItem {

    private void Start()
    {
        name = "move";
        banList = new string[] { "grabLedge", "celling" };
    }
}
