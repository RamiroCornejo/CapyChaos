using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] Vector3 positionB = Vector3.zero;
    [SerializeField] bool startInPositionB = false;
    [SerializeField] float lerpSpeed = 0.7f;

    Vector3 positionA;
    Quaternion resultPosition;
    Quaternion initPosition;


    bool onPosition;
    bool anim;

    float positionLerp;

    private void Start()
    {
        positionA = transform.eulerAngles;
        if (startInPositionB)
        {
            transform.localEulerAngles = positionB;
            onPosition = true;
        }
    }

    public void Switch()
    {
        if (anim) return;

        anim = true;
        if (onPosition)
        {
            initPosition = Quaternion.Euler(positionB);
            resultPosition = Quaternion.Euler(positionA);
        }
        else
        {
            initPosition = Quaternion.Euler(positionA);
            resultPosition = Quaternion.Euler(positionB);
        }
        onPosition = !onPosition;
    }

    private void Update()
    {
        if(anim)
        {
            positionLerp += Time.deltaTime * lerpSpeed;

            transform.localRotation = Quaternion.Lerp(initPosition, resultPosition, positionLerp);

            if (positionLerp > 1)
            {
                positionLerp = 0;
                transform.localRotation = resultPosition;
                anim = false;
            }
        }
    }
}
