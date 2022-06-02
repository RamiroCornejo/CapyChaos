using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mecha_Anim_SimpleObject : MonoBehaviour
{
    public Transform opened_position;
    public Transform closed_position;
    public Transform ModelObject;

    public UnityEvent OnFinishOpen;
    public UnityEvent OnFinishClose;

    public UnityEvent OnStartAnim;
    public UnityEvent OnStopAnim;

    public bool USE_POSITION = true;
    public bool USE_SCALE = true;
    public bool USE_ROTATION = true;

    bool open = true;
    bool can_anim;

    float timer;
    public float speed = 4;

    public bool BeginOpened;

    bool first_play_oneshot;

    private void Start()
    {
        first_play_oneshot = false;//para que no reproduzca particulas ni sonidos en el start

        if (BeginOpened) ClampOpen();
        else ClampClose();
    }

    #region Publicas
    public void UE_Open()
    {
        if (!open)
        {
            open = true;
            can_anim = true;
            OnStartAnim.Invoke();
        }
    }
    public void UE_Close()
    {
        if (open)
        {
            open = false;
            can_anim = true;
            OnStartAnim.Invoke();
        }
    }
    public void UE_Switch()
    {
        open = !open;
        can_anim = true;
        OnStartAnim.Invoke();
        timer = 0;
    }
    #endregion

    #region CLAMPS
    void ClampOpen()
    {
        open = true;
        can_anim = false;
        timer = 0;
        if (USE_POSITION) ModelObject.transform.position = opened_position.transform.position;
        if (USE_SCALE) ModelObject.transform.localScale = opened_position.transform.localScale;
        if (USE_ROTATION) ModelObject.transform.eulerAngles = opened_position.transform.eulerAngles;

        if (first_play_oneshot)
        {
            OnFinishOpen.Invoke();
            OnStopAnim.Invoke();
        }

        first_play_oneshot = true;

    }
    void ClampClose()
    {
        open = false;
        can_anim = false;
        timer = 0;
        if (USE_POSITION) ModelObject.transform.position = closed_position.transform.position;
        if (USE_SCALE) ModelObject.transform.localScale = closed_position.transform.localScale;
        if (USE_ROTATION) ModelObject.transform.eulerAngles = closed_position.transform.eulerAngles;

        if (first_play_oneshot)
        {
            OnFinishClose.Invoke();
            OnStopAnim.Invoke();
        }

        first_play_oneshot = true;
    }
    #endregion

    #region Anim Logic

    private void Update()
    {
        if (can_anim)
        {
            if (timer < 1)
            {
                timer = timer + speed * Time.deltaTime;
                if (open)
                {
                    if (USE_POSITION) ModelObject.position = Vector3.Lerp(closed_position.transform.position, opened_position.transform.position, timer);
                    if (USE_SCALE) ModelObject.localScale = Vector3.Lerp(closed_position.transform.localScale, opened_position.transform.localScale, timer);
                    if (USE_ROTATION) ModelObject.eulerAngles = Vector3.Lerp(closed_position.transform.eulerAngles, opened_position.transform.eulerAngles, timer);
                }
                else
                {
                    if (USE_POSITION) ModelObject.position = Vector3.Lerp(opened_position.transform.position, closed_position.transform.position, timer);
                    if (USE_SCALE) ModelObject.localScale = Vector3.Lerp(opened_position.transform.localScale, closed_position.transform.localScale, timer);
                    if (USE_ROTATION) ModelObject.eulerAngles = Vector3.Lerp(opened_position.transform.eulerAngles, closed_position.transform.eulerAngles, timer);
                }
            }
            else
            {
                if (open)
                {
                    ClampOpen();
                }
                else
                {
                    ClampClose();
                }
            }
        }
    }
    #endregion
}
