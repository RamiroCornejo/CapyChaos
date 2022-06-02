using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum fromdoordir { from_up, from_down }

public class UpDownDoor : MonoBehaviour
{
    [SerializeField] Animator model = null;
    public fromdoordir from_door_dir;

    bool animate;
    bool goOpen;

    float valScale;

    float timer;

    public float speed = 2f;

    public bool startOpen;

    [Header("with Timer")]
    public bool IsTimed;
    public float time_to_switch;
    bool aux_only_manual;


    private void Start()
    {
        Reset();
        InstantScale(startOpen);
    }

    void Reset()
    {
        goOpen = false;
        animate = false;
        timer = 0;
    }

    private void Update()
    {
        if (animate)
        {
            if (timer < 1)
            {
                timer = timer + speed * Time.deltaTime;

                //aca decido si lo escalo para arriba o para abajo dependiendo del Y_direction
                float dir = from_door_dir == fromdoordir.from_down ? 1 : -1;

                //aca decido si lo abro o lo cierro
                valScale = goOpen ? Mathf.Lerp(0, dir, timer) : Mathf.Lerp(dir, 0, timer);

                Vector3 scale = new Vector3(this.transform.localScale.x, valScale, this.transform.localScale.z);
                transform.localScale = scale;
            }
            else
            {
                timer = 0;
                animate = false;
                SoundFX.Play_DoorStopMovement();

                if (IsTimed && aux_only_manual)
                {
                    aux_only_manual = false;
                    Invoke("AutoSwitch", time_to_switch);
                }
            }
        }
    }

    void InstantScale(bool open)
    {
        float dir = from_door_dir == fromdoordir.from_down ? 1 : -1;
        if (open) model.Play("Drop");
        goOpen = !open;
        //Vector3 scale = new Vector3(this.transform.localScale.x, dir, this.transform.localScale.z);
        //transform.localScale = scale;
    }

    public void Open()
    {
        if (goOpen) return;

        SoundFX.Play_DoorStartMovement();
        model.Play("Drop");
        //timer = 0;
        //animate = true;
        goOpen = true;
        if (IsTimed) aux_only_manual = true;
    }
    public void Close()
    {
        if (!goOpen) return;

        model.Play("Close");
        SoundFX.Play_DoorStartMovement();
        //timer = 0;
        //animate = true;
        goOpen = false;
        if (IsTimed) aux_only_manual = true;

    }
    public void Switch()
    {
        SoundFX.Play_DoorStartMovement();
        //timer = 0;
        if(!goOpen) model.Play("Close");
        else model.Play("Drop");

        goOpen = !goOpen;
        //animate = true;
        //if (IsTimed) aux_only_manual = true;
    }
    void AutoSwitch()
    {
        goOpen = !goOpen;
        //animate = true;
    }
}
