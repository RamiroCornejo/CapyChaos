using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AvancedButtons : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] UnityEvent highlightedEvent;
    [SerializeField] UnityEvent clickEvent;
    [SerializeField] bool buttonAttached = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonAttached && !GetComponent<Button>().interactable) return;
        highlightedEvent.Invoke();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonAttached && !GetComponent<Button>().interactable) return;
        clickEvent.Invoke();
    }

}
