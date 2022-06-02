using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Lever_Mode { Auto_return, One_Simple_Pulse }
public class Lever : MonoBehaviour
{
    public Lever_Mode mode;

    public Transform level_element;
    public Transform active_pos;
    public Transform deactive_pos;

    public UnityEvent ONE_PULSE_MODE_OnExecute;

    public UnityEvent AUTORET_On_State_Active;
    public UnityEvent AUTORET_On_State_Deactive;
    public float time_to_return;
    float timer_cd;
    bool cd_return;

    TicTac tic_tac_sound;
    

    bool isActiveState;

    bool anim_lever;
    float timer_anim;
    float speed_anim = 3;
    bool in_animation_state;

    private void Start()
    {
        level_element.eulerAngles = isActiveState ? active_pos.transform.eulerAngles : deactive_pos.transform.eulerAngles;
        tic_tac_sound = gameObject.GetComponent<TicTac>();
        cd_return = false;
    }

    public void INPUT_PressDown()
    {
        if (in_animation_state) return;
        if (mode == Lever_Mode.Auto_return)
        {
            cd_return = false;
            timer_cd = 0;
            tic_tac_sound.Stop();

            if (!isActiveState)
            {
                AUTORET_On_State_Active.Invoke();
                isActiveState = true;
                anim_lever = true;
                in_animation_state = true;
                SoundFX.Play_TouchHandle_Go();
            }
        }

        if (mode == Lever_Mode.One_Simple_Pulse)
        {
            ONE_PULSE_MODE_OnExecute.Invoke();
            isActiveState = !isActiveState;
            anim_lever = true;
            in_animation_state = true;
            if(isActiveState) SoundFX.Play_TouchHandle_Go();
            else SoundFX.Play_TouchHandle_Back();
        }
    }
    public void INPUT_PressUp()
    {
        Debug.Log("PressUP");
        if (mode == Lever_Mode.Auto_return)
        {
            cd_return = true;
            tic_tac_sound.Begin();
            timer_cd = 0;
        }
    }

    void Auto_Disable()
    {
        AUTORET_On_State_Deactive.Invoke();
        SoundFX.Play_TouchHandle_Back();
        tic_tac_sound.Stop();
        isActiveState = false;
        anim_lever = true;
        timer_anim = 0;
        in_animation_state = false;
    }
    

    private void Update()
    {
        if (mode == Lever_Mode.Auto_return)
        {
            if (cd_return)
            {
                if (timer_cd < time_to_return) timer_cd = timer_cd + 1 * Time.deltaTime;
                else
                {
                    Auto_Disable();
                    timer_cd = 0;
                    cd_return = false;
                }
            }
        }

        if (anim_lever)
        {
            if (timer_anim < 1)
            {
                timer_anim = timer_anim + speed_anim * Time.deltaTime;

                if (isActiveState)
                {
                    level_element.eulerAngles = Vector3.Lerp(deactive_pos.transform.eulerAngles, active_pos.transform.eulerAngles, timer_anim);
                }
                else
                {
                    level_element.eulerAngles = Vector3.Lerp(active_pos.transform.eulerAngles, deactive_pos.transform.eulerAngles, timer_anim);
                }
            }
            else
            {
                level_element.eulerAngles = isActiveState ? active_pos.transform.eulerAngles : deactive_pos.transform.eulerAngles;
                in_animation_state = false;
                timer_anim = 0;
                anim_lever = false;
            }
        }
    }
}
