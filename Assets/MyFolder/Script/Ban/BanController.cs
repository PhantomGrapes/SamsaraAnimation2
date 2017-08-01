using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanController : MonoBehaviour {
    private BanItem[] banItems;
    private BanItem empty;

    private void Start()
    {
        banItems = FindObjectOfType<BanItem>().GetComponents<BanItem>();
        empty = new BanItem();
        empty.name = "empty";
        empty.banList = new string[] { };
    }

    public BanItem FindItem(AnimatorStateInfo s)
    {
        for (int i = 0; i < banItems.Length; i++)
        {
            if (s.IsTag(banItems[i].name))
                return banItems[i];
        }
        
        return empty;
    }

    public BanItem FindItem(string name)
    {
        for (int i = 0; i < banItems.Length; i++)
        {
            if (banItems[i].name == name)
                return banItems[i];
        }
        return empty;
    }
    public void StartBanWhile(AnimatorStateInfo s)
    {
        BanItem item = FindItem(s);
        for (int i = 0; i < item.banList.Length; i++)
        {
            FindItem(item.banList[i]).StartBan();
        }
    }

    public void StartBanWhile(BanItem item)
    {
        for (int i = 0; i < item.banList.Length; i++)
        {
            FindItem(item.banList[i]).StartBan();
        }
    }

    public void EndBanWhile(AnimatorStateInfo s)
    {
        BanItem item = FindItem(s);
        for (int i = 0; i < item.banList.Length; i++)
        {
            FindItem(item.banList[i]).EndBan();
        }
    }

    public void EndBanWhile(BanItem item)
    {
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
