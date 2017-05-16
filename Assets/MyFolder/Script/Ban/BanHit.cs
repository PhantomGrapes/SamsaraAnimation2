using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanHit : BanItem
{
    private void Start()
    {
        name = "hit";
        banList = new string[] { "hit", "move", "attack", "dead", "grabLedge", "celling", "jump", "doubleJump", "throw" };
    }
}
