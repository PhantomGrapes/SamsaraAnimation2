using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanAttack : BanItem {

    private void Start()
    {
        name = "attack";
        banList = new string[] { "move", "jump", "doubleJump", "roll", "grabLedge", "celling", "attack", "throw"};
    }
}
