using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanJump : BanItem {

    private void Start()
    {
        name = "jump";
        banList = new string[] { "move", "jump", "roll" };
    }
}
