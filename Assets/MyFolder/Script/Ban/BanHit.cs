using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanHit : BanItem
{
    private void Start()
    {
        name = "Hit";
        banList = new string[] { "Hit", "Move", "Attack", "Dead", "GrabLedge", "Celling", "Jump", "DoubleJump" };
    }
}
