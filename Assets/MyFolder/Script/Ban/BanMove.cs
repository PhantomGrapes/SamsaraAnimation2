using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanMove : BanItem {

    private void Start()
    {
        name = "Move";
        banList = new string[] { "GrabLedge" };
    }
}
