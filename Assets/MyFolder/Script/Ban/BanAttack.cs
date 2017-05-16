using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanAttack : BanItem {

    private void Start()
    {
        name = "Attack";
        banList = new string[] { "Walk", "Jump", "DoubleJump" };
    }
}
