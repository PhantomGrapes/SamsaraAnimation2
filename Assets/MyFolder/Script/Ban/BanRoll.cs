using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanRoll : BanItem {

    private void Start()
    {
        name = "Roll";
        banList = new string[] { "Roll", "Walk", "Attack", "Hit", "Dead", "GrabLedge" };
    }
}
