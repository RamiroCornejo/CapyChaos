using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowActivable : MonoBehaviour
{
    public TriggerReceptor_UnityEvent receptor;
    public Feedback_SimpleButton feedback;

    public void Activate()
    {
        receptor.EV_OnExecute.Invoke();
        feedback.OnExecute();
    }
}
