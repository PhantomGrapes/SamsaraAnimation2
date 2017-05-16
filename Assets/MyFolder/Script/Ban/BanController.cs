using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanController : MonoBehaviour {
    public BanItem[] banItems;
	
    BanItem FindItem(string name)
    {
        for (int i = 0; i < banItems.Length; i++)
        {
            if (banItems[i].name == name)
                return banItems[i];
        }
        throw new System.Exception("Can find corresponding ban item.");
    }
    public void StartBanWhile(string name)
    {
        BanItem item = FindItem(name);
        for (int i = 0; i < item.banList.Length; i++)
        {
            FindItem(item.banList[i]).StartBan();
        }
    }

    public void EndBanWhile(string name)
    {
        BanItem item = FindItem(name);
        for (int i = 0; i < item.banList.Length; i++)
        {
            FindItem(item.banList[i]).EndBan();
        }
    }

    public bool Check(string name)
    {

        BanItem item = FindItem(name);
        return item.CanDo();
    }
}
