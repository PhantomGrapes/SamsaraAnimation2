using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanDoubleJump : BanItem {

    private void Start()
    {
        name = "DoubleJump";
        banList = new string[] { "Jump", "", "" };
    }
}
