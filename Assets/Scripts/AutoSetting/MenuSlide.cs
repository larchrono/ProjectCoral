using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlide : MonoBehaviour
{
    public float swipeRate;

    public AreaClass SwipeLeftTo;
    public AreaClass SwipeRightTo;

    public QueueAction WhenSwipe;

    Vector2 lastPosition;

    bool CanSwipe = true;

    // Update is called once per frame
    void Update()
    {
        if(CanSwipe == false)
            return;
        
        if(ItemDragUse.isDragging)
            return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {

            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if(touchDeltaPosition.x >swipeRate){
                if(SwipeRightTo != null){
                    CanSwipe = false;
                    SceneInteractive.main.GoToAreaNoStack(SwipeRightTo);
                    WhenSwipe.Invoke();
                }
            }

            if(touchDeltaPosition.x < -swipeRate){
                if(SwipeLeftTo != null){
                    CanSwipe = false;
                    SceneInteractive.main.GoToAreaNoStack(SwipeLeftTo);
                    WhenSwipe.Invoke();
                }
            }
        }
    }

    private void OnEnable() {
        CanSwipe = true;
    }
}
