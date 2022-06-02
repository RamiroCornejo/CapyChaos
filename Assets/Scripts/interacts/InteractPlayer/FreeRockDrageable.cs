using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class FreeRockDrageable : MonoBehaviour,IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("DRAG")]
    [SerializeField] float objSpeed = 20;

  //  [Range(1,20)]
   // [SerializeField] float kill_force = 15;

    Vector3 offset;

    Rigidbody myRig;

    bool isOver;
    public bool isHanging;
    bool dragging;

    [Header("Feedbacks")]
    public UnityEvent EV_OnBeginDrag;
    public UnityEvent EV_OnEndDrag;
    public UnityEvent EV_OnMouseEnter;
    public UnityEvent EV_OnMouseExit;

    public void AddCapi(Character character)
    {
        //if (myRig.velocity.magnitude > kill_force)
        //{
        //    character.Kill();
        //}
    }

    private void Start()
    {

        myRig = GetComponent<Rigidbody>();

        if (!isHanging)
        {
            //if (useGravity)
            //    myRig.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void Update()
    {
        if (dragging)
        {
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

    Vector3 pointerPos;


    private void OnMouseEnter()
    {
        isOver = true;

        if (!dragging)
        {
            EV_OnMouseEnter.Invoke();
        }

        MyCursor.HOver();
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
    }

    private Vector3 GetMouseWorldPos(Vector3 pointerPos)
    {
        Vector3 mousePoint = pointerPos;
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void BeginDrag_RigidBody()
    {
        myRig.useGravity = false;
        myRig.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
       // myRig.isKinematic = true;

        
    }
    void EndDrag_RigidBody()
    {
        myRig.useGravity = true;
        myRig.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        
        
        // myRig.useGravity = true;
        // myRig.velocity = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        pointerPos = GetMouseWorldPos(eventData.position) + offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = gameObject.transform.position - GetMouseWorldPos(eventData.position);
        pointerPos = transform.position;

        EV_OnBeginDrag.Invoke();
        dragging = true;

        if (!isHanging)
        {
            BeginDrag_RigidBody();
        }

        SoundFX.Play_begin_drag_steel();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isHanging)
        {

            EndDrag_RigidBody();
        }

        EV_OnEndDrag.Invoke();
        dragging = false;
        SoundFX.Play_end_drag_steel();

        if (isOver) EV_OnMouseEnter.Invoke();
        else EV_OnMouseExit.Invoke();
    }
}
