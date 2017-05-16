using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanGrabLedge : BanItem {

    private void Start()
    {
        name = "GrabLedge";
        banList = new string[] { "Walk", "Jump", "DoubleJump" };
    }
}
