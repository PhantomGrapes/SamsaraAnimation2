using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanGrabLedge : BanItem {

    private void Start()
    {
        name = "grabLedge";
        banList = new string[] { "move", "jump", "doubleJump", "roll", "attack", "hit", " dead", "throw" };
    }
}
