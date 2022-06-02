using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserSimpleTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Simple Events")]
    public UnityEvent EV_OnPointerDown;
    public UnityEvent EV_OnPointerUpa;
    public void OnPointerDown(PointerEventData eventData) => EV_OnPointerDown.Invoke();
    public void OnPointerUp(PointerEventData eventData) => EV_OnPointerUpa.Invoke();
}