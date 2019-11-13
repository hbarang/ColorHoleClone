using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float speedMultiplier;

    // Update is called once per frame
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


}
