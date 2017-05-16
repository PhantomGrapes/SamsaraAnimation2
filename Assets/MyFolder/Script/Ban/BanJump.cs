using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanJump : BanItem {

    private void Start()
    {
        name = "Jump";
        banList = new string[] { "Walk", "Jump", "Roll" };
    }
}
