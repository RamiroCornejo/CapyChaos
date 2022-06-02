using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private void Awake()
    {
        instance = this;
    }
    public float duration = 0.15f;
    public float magnitude = 0.4f;

    public static void Shake()
    {
        instance.StartCoroutine(instance.DoShake(instance.duration, instance.magnitude));
    }

    public static void Shake(float duration = 0.15f, float magnitude = 0.4f)
    {
        instance.StartCoroutine(instance.DoShake(duration, magnitude));
    }

    public IEnumerator DoShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
