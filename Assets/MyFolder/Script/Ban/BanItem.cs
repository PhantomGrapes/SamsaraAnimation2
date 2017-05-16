using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanItem : MonoBehaviour {
    public string name;
    int current;
    public string[] banList;
	
    void Start()
    {
        current = 0;
    }

    public void StartBan()
    {
        current += 1;
    }

    public void EndBan()
    {
        current -= 1;
    }

    public bool CanDo()
    {
        return current == 0 ? true : false;
    }
}
