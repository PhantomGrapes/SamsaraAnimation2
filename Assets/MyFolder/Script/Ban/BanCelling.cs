using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanCelling : BanItem
{
    private void Start()
    {
        name = "Celling";
        banList = new string[] { "Roll", "Move", "Attack", "Hit", "Dead", "GrabLedge" };
    }
}
