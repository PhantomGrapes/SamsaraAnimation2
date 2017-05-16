using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanDead : BanItem
{
    private void Start()
    {
        name = "dead";
        banList = new string[] { "roll", "move", "attack", "hit", "dead", "grabLedge", "celling", "jump", "doubleJump", "throw" };
    }
}
