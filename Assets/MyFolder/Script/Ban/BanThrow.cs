using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanThrow : BanItem
{
    private void Start()
    {
        name = "throw";
        banList = new string[] { "throw", "move", "attack", "grabLedge", "celling"};
    }
}
