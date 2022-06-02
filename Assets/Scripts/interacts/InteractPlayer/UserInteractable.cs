using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserInteractable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    [Header("Simple Events")]
    public UnityEvent EV_OnMouseDown;
    public UnityEvent EV_OnMouseUp;
    public UnityEvent EV_OnMouseEnter;
    public UnityEvent EV_OnMouseExit;

    [Header("DRAG")]
    public bool UseDrag;
    [SerializeField] bool useLimits = false;
    [SerializeField] float objSpeed = 20;

    [SerializeField] Transform pos_A;
    [SerializeField] Transform pos_B;

    bool useGravity;

    Vector3 offset;

    Rigidbody myRig;
    public UnityEvent EV_OnBeginDrag;
    public UnityEvent EV_OnEndDrag;

    //public GameObject[] toHightLight;
    //List<SimpleShaderHightLights> shaders_to_paint = new List<SimpleShaderHightLights>();

    public bool UseParentingElement;
    ParentCharacter reparent;

    bool isOver;

    public bool isHanging;

    Vector3 pointerPos;

    private void Start()
    {
        reparent = GetComponentInChildren<ParentCharacter>();
        if (reparent) reparent.gameObject.SetActive(UseParentingElement);

        myRig = GetComponent<Rigidbody>();
        if (myRig != null)
        {
            if (!isHanging)
            {
                useGravity = myRig.useGravity;
                if (useGravity)
                    myRig.constraints = RigidbodyConstraints.FreezeAll;
            }
            //myRig.WakeUp();
        }

        if (UseDrag && useLimits)
        {
            Vector3 newPos = transform.position;
            if (useLimits)
            {
                newPos.y = Mathf.Clamp(newPos.y, pos_A.transform.position.y, pos_B.transform.position.y);
                newPos.x = Mathf.Clamp(newPos.x, pos_A.transform.position.x, pos_B.transform.position.x);
            }
            transform.position = newPos;
        }

        //for (int i = 0; i < toHightLight.Length; i++)
        //{
        //    var shader = toHightLight[i].GetComponentInChildren<SimpleShaderHightLights>();
        //    shaders_to_paint.Add(shader);
        //}
    }

    private void Update()
    {
        if (dragging)
        {
            if (useLimits)
            {
                pointerPos.y = Mathf.Clamp(pointerPos.y, pos_A.transform.position.y, pos_B.transform.position.y);
                pointerPos.x = Mathf.Clamp(pointerPos.x, pos_A.transform.position.x, pos_B.transform.position.x);
            }
            if (myRig != null)
            {
                if (Vector3.Distance(pointerPos, transform.position) < 0.5f) { myRig.velocity = Vector2.zero; return; }

                Vector3 dir = (pointerPos - transform.position).normalized;

                myRig.velocity = dir * objSpeed;

            }
            else
                transform.position = pointerPos;
        }
    }

    //void Hover_HightLight() { foreach (var s in shaders_to_paint) s.UE_Enter(); }
    //void Exit_HightLight() { foreach (var s in shaders_to_paint) s.UE_Exit(); }

    public void CancelLimits()
    {
        useLimits = false;
    }

    private void OnMouseEnter()
    {
        isOver = true;

        if (!dragging)
        {
            EV_OnMouseEnter.Invoke();
        }


        MyCursor.HOver();
        //Hover_HightLight();
    }
    private void OnMouseExit()
    {
        isOver = false;

        if (!dragging)
        {
            isOver = false;
            EV_OnMouseExit.Invoke();
        }

        MyCursor.Normal();
        //Exit_HightLight();
    }

    bool dragging;

    private Vector3 GetMouseWorldPos(Vector3 _pointerPos)
    {
        //obtengo el punto del mouse real
        Vector3 mousePoint = _pointerPos;

        //uso la profundidad del objeto en cuestion, la profundidad del objeto lo traduzco a coordenada pantalla
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //ahora que tengo la posicion de mouse que quiero lo traduzco a coordenada de mundo
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (UseDrag)
        {
            //este offset lo que hace es obtener la diferencia entre el lugar del click y la posicion del objeto
            offset = gameObject.transform.position - GetMouseWorldPos(eventData.position);
            pointerPos = transform.position;
            EV_OnBeginDrag.Invoke();
            dragging = true;

            if (!isHanging) { if (myRig) { myRig.useGravity = false; if (!useGravity) myRig.constraints = RigidbodyConstraints.FreezeRotation; } }

            SoundFX.Play_begin_drag_steel();
        }
        else
        {
            EV_OnMouseDown.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (UseDrag)
        {
            if (!isHanging)
            {
                if (myRig != null)
                {
                    if (useGravity) myRig.useGravity = true;
                    else myRig.constraints = RigidbodyConstraints.FreezeAll;
                    myRig.velocity = Vector3.zero;
                }
            }

            EV_OnEndDrag.Invoke();
            dragging = false;
            SoundFX.Play_end_drag_steel();
        }

        if (isOver)
        {
            EV_OnMouseEnter.Invoke();
        }
        else
        {
            EV_OnMouseExit.Invoke();
        }
    }
    public bool moving;
    public void OnDrag(PointerEventData eventData)
    {
        if (UseDrag)
        {
            pointerPos = GetMouseWorldPos(eventData.position) + offset;
            moving = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        moving = false;
    }
}
