using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OverlayBook : MonoBehaviour
{
    const int max_page = 5;
    public static OverlayBook main;
    public QueueAction closeOverlay;

    public PageClass LeftPage;
    public PageClass RightPage;
    public PageItemClass nowPageItem;

    public float SwipeTime;
    public float FingerSwipeRate;
    public float EverySwipeDelay;

    public QueueAction WhenSwipe;

    public QueueAction WhenAddpaper;

    public List<PageItemClass> AllPageItems;

    bool _canSwipe;

    public enum SwipeDirect
    {
        LEFT,
        RIGHT
    }
    //Need set Active in Scene or Set it into Init scripts
    void Awake()
    {
        main = this;
        gameObject.SetActive(false);
    }

    void Start()
    {
        _canSwipe = true;
    }

    void OnEnable() {
        _canSwipe = true;
    }

    public void OnCloseOverlay()
    {
        closeOverlay.Invoke();
    }

    public void AddPaper(int paperId)
    {
        if (paperId < AllPageItems.Count)
        {
            AllPageItems[paperId].SetPageItemAttribute(true);
            WhenAddpaper.Invoke();

            for (int i = 0; i < 10; i++)
            {
                if(nowPageItem.PageItemID != (paperId-1)){
                    if(nowPageItem.PageItemID < (paperId-1)){
                        SwipePage(SwipeDirect.LEFT);
                    }
                    if(nowPageItem.PageItemID > (paperId-1)){
                        SwipePage(SwipeDirect.RIGHT);
                    }
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwipePage(SwipeDirect.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwipePage(SwipeDirect.RIGHT);
        }

        if(_canSwipe == false)
            return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (touchDeltaPosition.x > FingerSwipeRate)
            {
                _canSwipe = false;
                SwipePage(SwipeDirect.RIGHT);
                StartCoroutine(ResumeSwipe());
            }

            if (touchDeltaPosition.x < -FingerSwipeRate)
            {
                _canSwipe = false;
                SwipePage(SwipeDirect.LEFT);
                StartCoroutine(ResumeSwipe());
            }
        }
    }

    IEnumerator ResumeSwipe(){
        yield return new WaitForSeconds(EverySwipeDelay);
        _canSwipe = true;
    }

    public void SwipePage(SwipeDirect dir)
    {
        switch (dir)
        {
            case SwipeDirect.LEFT:
                if (nowPageItem.RightPageItem != null)
                {
                    //目前的右頁設置為不可見 (目前的右頁為下個道具的右頁)
                    if (nowPageItem.RightPageItem.RightPage != null)
                    {
                        //nowPageItem.RightPageItem.RightPage.gameObject.SetActive(false);
                        FlipPageOut(nowPageItem.RightPageItem.RightPage);
                    }
                    //下一頁的左頁設置為可見 (下一頁的左頁為下一個道具的左頁)
                    if (nowPageItem.RightPageItem.LeftPage != null)
                    {
                        //nowPageItem.RightPageItem.LeftPage.gameObject.SetActive(true);
                        FlipPageIn(nowPageItem.RightPageItem.LeftPage, SwipeTime);
                    }
                    nowPageItem = nowPageItem.RightPageItem;
                    WhenSwipe.Invoke();
                }
                break;
            case SwipeDirect.RIGHT:
                if (nowPageItem.LeftPageItem != null)
                {
                    //目前的左頁設置為不可見 (目前的左頁為目前道具的左頁)
                    if (nowPageItem.LeftPage != null)
                    {
                        //nowPageItem.LeftPage.gameObject.SetActive(false);
                        FlipPageOut(nowPageItem.LeftPage);
                    }
                    //下一頁的右頁設置為可見 (下一頁的右頁為目前道具的右頁)
                    if (nowPageItem.RightPage != null)
                    {
                        //nowPageItem.RightPage.gameObject.SetActive(true);
                        FlipPageIn(nowPageItem.RightPage, SwipeTime);
                    }
                    nowPageItem = nowPageItem.LeftPageItem;
                    WhenSwipe.Invoke();
                }
                break;
            default:
                break;
        }
    }

    void FlipPageOut(PageClass page)
    {
        RectTransform rect = page.GetComponent<RectTransform>();
        rect.DOScaleX(0, SwipeTime);
    }

    void FlipPageIn(PageClass page, float delay)
    {
        RectTransform rect = page.GetComponent<RectTransform>();
        Sequence seq = DOTween.Sequence()
        .AppendInterval(SwipeTime)
        .Append(rect.DOScaleX(1, SwipeTime));
    }
}
