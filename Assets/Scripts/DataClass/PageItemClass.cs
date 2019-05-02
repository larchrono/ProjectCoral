using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageItemClass : MonoBehaviour
{
    public PageClass LeftPage;
    public PageClass RightPage;

    public PageItemClass LeftPageItem;
    public PageItemClass RightPageItem;

    public int PageItemID;

    public bool isGetThisPage;

    // Start is called before the first frame update
    void Start()
    {
        if(PageItemID == -1)
            return;
        isGetThisPage = GetPageItemIDStat(PageItemID);
        if (!isGetThisPage)
        {
            if (LeftPage != null)
                LeftPage.SetPageAttribute(false);
            if (RightPage != null)
                RightPage.SetPageAttribute(false);
        }
    }

    public void SetPageItemAttribute(bool src)
    {
        if (LeftPage != null)
            LeftPage.SetPageAttribute(src);
        if (RightPage != null)
            RightPage.SetPageAttribute(src);

        if(src)
            SetSavedPageItemIDStat(PageItemID, 1);
        else
            SetSavedPageItemIDStat(PageItemID, 0);
    }

    void SetSavedPageItemIDStat(int pid, int stat)
    {
        GlobalVariables.instance.SetPageItemStat(pid,stat);
    }

    bool GetPageItemIDStat(int pid)
    {
        int result = GlobalVariables.instance.PageItems[pid];

        if (result > 0)
            return true;
        return false;
    }
}
