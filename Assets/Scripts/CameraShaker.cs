using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraShaker : MonoBehaviour
{
    public float shakeDuration;
    public float magnitude;
    public static CameraShaker instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance);
        }

    }

    public void ShakeVertical()
    {
        StartCoroutine(ShakeVerticalCoroutine(shakeDuration, magnitude));
    }

    IEnumerator ShakeVerticalCoroutine(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(originalPosition.x, originalPosition.y + y, originalPosition.z), duration);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPosition;
    }



}
