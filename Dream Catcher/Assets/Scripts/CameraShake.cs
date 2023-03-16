using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 initialPosition;
    void Awake()
    {
        initialPosition = transform.position;
    }

    public void Shake(float duration, float intensity)
    {
        StartCoroutine(Play(duration, intensity));
    }

    IEnumerator Play(float duration, float intensity)
    {
        for(var totalTime = 0f; totalTime <= duration; totalTime += Time.deltaTime)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * intensity;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
