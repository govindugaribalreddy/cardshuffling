using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private int touchID = -1; // To track the touch ID

    public void OnDrag(PointerEventData eventData)
    {
        if (touchID != -1)
        {
            cardManager.instance.moveCard(Input.GetTouch(touchID).position);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId >= 0 && eventData.pointerId < Input.touchCount)
        {
            Touch touch = Input.GetTouch(eventData.pointerId);

            if (touch.phase == TouchPhase.Began)
            {
                if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<cardView>() != null)
                {
                    cardManager.instance.SetSelectedCard(eventData.pointerCurrentRaycast.gameObject.GetComponent<cardView>());
                    touchID = eventData.pointerId; // Set the touch ID
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (touchID == eventData.pointerId)
        {
            cardManager.instance.ReleseCard();
            touchID = -1; // Reset touch ID
        }
    }
}

