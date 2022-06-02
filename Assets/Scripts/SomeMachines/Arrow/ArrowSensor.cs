using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrowSensor : MonoBehaviour
{
    Action<bool> DestroyProyectile; //bool para animacion
    Action StopProyectile;
    Action ShutDownSensors;

    [SerializeField] GameObject myParent;

    bool active;
    

    public void ConfigureSensor(Action<bool> DestroyProyectile, Action StopProyectile, Action ShutDownSensors)
    {
        this.DestroyProyectile = DestroyProyectile;
        this.StopProyectile = StopProyectile;
        this.ShutDownSensors = ShutDownSensors;
        active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active) return;

        Debug.Log("other: " + other.gameObject.name);

        var gentrigger = other.GetComponent<GenTrigger>();
        if (gentrigger != null) return;

        var physicpart = other.GetComponent<CapiPhysicPart>();
        if (physicpart != null) return;

        var otherarrow = other.GetComponent<Arrow_Destructible>();
        if (otherarrow != null)
        {
            var arrow = otherarrow.myArrow;
            var own = this.GetComponentInParent<Arrow>();
            arrow.SetMyTail(own);

            active = false;
            SoundFX.Play_arrow_Sticks();
            StopProyectile.Invoke();

            return;
        }

        var capiSensor = other.GetComponent<CapiSensorPart>();
        if (capiSensor != null)
        {
            ShutDownSensors.Invoke();
            capiSensor.Hit(myParent, () => DestroyProyectile.Invoke(false));
            SoundFX.Play_arrow_hit_capi();
            active = false;
            return;
        }

        var button = other.GetComponent<ArrowActivable>();
        if (button != null)
        {
           
            DestroyProyectile.Invoke(true);
            SoundFX.Play_arrow_hit_destroy();
            button.Activate();
            return;
        }

        if (other.tag == "capi_part") return;

        var character = other.GetComponent<Character>();
        if (character != null)
        {
            return;
        }

            //var character = other.GetComponent<Character>();
            //if (character != null)
            //{
            //    character.Kill();
            // DestroyProyectile.Invoke();
            // SoundFX.Play_arrow_hit_capi();
            //    return;
            //}

            var slab = other.GetComponent<Slabon>();

        if (slab != null)
        {
            slab.ImCutted();
            SoundFX.Play_arrow_hit_destroy();
            DestroyProyectile.Invoke(true);
            active = false;
            return;
        }

        active = false;
        SoundFX.Play_arrow_Sticks();
        StopProyectile.Invoke();
    }
}
