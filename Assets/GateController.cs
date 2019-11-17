using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.StageChangedEvent += OpenTheGate;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OpenTheGate(int stage)
    {
        Debug.Log("Stage");
        if (stage == 2)
        {
            StartCoroutine(MoveVertically(2f));
        }
    }
    IEnumerator MoveVertically(float duration)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (transform.gameObject.tag == "SecondGate")
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(originalPosition.x, 0.5f, originalPosition.z), duration);
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(originalPosition.x, 1.3f, originalPosition.z), duration);
            }

            elapsed += Time.deltaTime;

            yield return null;
        }

    }
    private void OnDestroy()
    {
        GameManager.instance.StageChangedEvent -= OpenTheGate;
    }
}
