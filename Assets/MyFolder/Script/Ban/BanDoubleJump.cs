using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanDoubleJump : BanItem {

    private void Start()
    {
        name = "doubleJump";
        banList = new string[] { "jump", "doubleJump", "move", "roll" };
    }
}
