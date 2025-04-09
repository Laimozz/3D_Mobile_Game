using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickCtrl : MonoBehaviour, IPointerUpHandler, IDragHandler,IPointerDownHandler
{
    [SerializeField] private RectTransform moveStick;
    [SerializeField] private RectTransform moveStickBG;

    public delegate void InputDataUpdate(Vector2 input);
    public event InputDataUpdate inputData;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPos = eventData.position;
        Vector2 startPos = moveStickBG.position;

        Vector2 localPos = Vector2.ClampMagnitude(touchPos - startPos, moveStickBG.sizeDelta.x/2);
        moveStick.position = startPos + localPos;

        Vector2 data = localPos / (moveStickBG.sizeDelta.x / 2);
        inputData?.Invoke(data);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputData?.Invoke(Vector2.zero);
        moveStick.position = moveStickBG.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
