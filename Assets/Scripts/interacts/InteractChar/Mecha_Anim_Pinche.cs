using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha_Anim_Pinche : MonoBehaviour
{
    public Transform opened_position;
    public Transform closed_position;
    public Transform ModelObject;

    [SerializeField] Collider[] goCollidersAndSensors;

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
        }
    }
    public void UE_Close()
    {
        if (open)
        {
            open = false;
            can_anim = true;
        }
    }
    public void UE_Switch()
    {
        open = !open;
        can_anim = true;
        timer = 0;
    }
    #endregion

    #region CLAMPS
    void ClampOpen()
    {
        open = true;
        can_anim = false;
        timer = 0;
        foreach (var c in goCollidersAndSensors) c.enabled = true;
        ModelObject.transform.position = opened_position.transform.position;
        ModelObject.transform.localScale = opened_position.transform.localScale;
        if (first_play_oneshot)
        {
            SoundFX.Play_Pinche_Open();
        }
        first_play_oneshot = true;
    }
    void ClampClose()
    {
        open = false;
        can_anim = false;
        timer = 0;
        foreach (var c in goCollidersAndSensors) c.enabled = false;
        ModelObject.transform.position = closed_position.transform.position;
        ModelObject.transform.localScale = closed_position.transform.localScale;
        if (first_play_oneshot)
        {
            SoundFX.Play_Pinche_Close();
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
                    ModelObject.position = Vector3.Lerp(closed_position.transform.position, opened_position.transform.position, timer);
                    ModelObject.localScale = Vector3.Lerp(closed_position.transform.localScale, opened_position.transform.localScale, timer);
                }
                else
                {
                    ModelObject.position = Vector3.Lerp(opened_position.transform.position, closed_position.transform.position, timer);
                    ModelObject.localScale = Vector3.Lerp(opened_position.transform.localScale, closed_position.transform.localScale, timer);
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
