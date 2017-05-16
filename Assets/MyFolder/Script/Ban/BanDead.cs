using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanDead : BanItem
{
    private void Start()
    {
        name = "Dead";
        banList = new string[] { "Roll", "Move", "Attack", "Hit", "Dead", "GrabLedge", "Celling", "Jump", "DoubleJump" };
    }
}
