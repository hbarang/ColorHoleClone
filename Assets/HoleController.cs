using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float speedMultiplier;

    private void Start() {
        GameManager.instance.StageChangedEvent += StageChanged;
    }
    void Update()
    {
        MoveByArrowKeys();
    }

    void MoveByArrowKeys()
    {
        float moveByXAxis = Input.GetAxis("Horizontal") * speedMultiplier * Time.deltaTime;
        float moveByZAxis = Input.GetAxis("Vertical") * speedMultiplier * Time.deltaTime;
        transform.Translate(moveByXAxis > maxHorizontalSpeed ? maxHorizontalSpeed : moveByXAxis, 0, moveByZAxis > maxVerticalSpeed ? maxVerticalSpeed : moveByZAxis);

    }

    void StageChanged(int stage){
        Debug.Log(stage);
    }

    private void OnDestroy() {
        GameManager.instance.StageChangedEvent -= StageChanged;
    }

}
