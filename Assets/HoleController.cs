using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float speedMultiplier;
    private bool stageChanging = false;
    private bool horizontalStageChaning = false;
    int currentStage = 1;

    public float verticalMovementLimiter;
    public float horizontalMovementLimiter;
    public delegate void OnVerticalStageChange();
    public event OnVerticalStageChange VerticalStageChangeEvent;


    
    private void Start()
    {
        GameManager.instance.StageChangedEvent += StageChanged;
    }
    void Update()
    {
        if (!stageChanging)
        {
            MoveByArrowKeys();
        }
        else
        {
            if (horizontalStageChaning)
            {
                MoveHorizontally();
            }
            else
            {
                MoveVertically(currentStage);
            }
        }

    }

    void MoveByArrowKeys()
    {
        verticalMovementLimiter = (20 * (currentStage-1))+verticalMovementLimiter; 
        float moveByXAxis = Input.GetAxis("Horizontal") * speedMultiplier * Time.deltaTime;
        float moveByZAxis = Input.GetAxis("Vertical") * speedMultiplier * Time.deltaTime;
        transform.Translate(moveByXAxis > maxHorizontalSpeed ? maxHorizontalSpeed : moveByXAxis, 0, moveByZAxis > maxVerticalSpeed ? maxVerticalSpeed : moveByZAxis);
        if(Mathf.Abs(transform.position.x) > horizontalMovementLimiter){
            transform.position = new Vector3(transform.position.x < 0 ? (horizontalMovementLimiter*-1f) : horizontalMovementLimiter, transform.position.y, transform.position.z);
        }
        else if(Mathf.Abs(transform.position.z) > verticalMovementLimiter){
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z < 0 ? (verticalMovementLimiter*-1f) : verticalMovementLimiter);
        }
    }

    void StageChanged(int stage)
    {
        Debug.Log("stage changed");
        stageChanging = true;
        horizontalStageChaning = true;
        currentStage = stage;
    }

    private void OnDestroy()
    {
        GameManager.instance.StageChangedEvent -= StageChanged;
    }

    void MoveHorizontally()
    {
        Vector3 objectivePosition = new Vector3(0f, transform.position.y, transform.position.z);
        if (Mathf.Abs(transform.position.x) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, objectivePosition, 1f / 0.5f * Time.deltaTime);
        }
        else
        {
            horizontalStageChaning = false;
            VerticalStageChangeEvent();
        }

    }
    void MoveVertically(int stage)
    {
        Vector3 objectivePosition = new Vector3(transform.position.x, transform.position.y, (-4 + (stage - 1) * 20));
        if (Mathf.Abs(transform.position.z - objectivePosition.z) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, objectivePosition, 1f / 1.5f * Time.deltaTime);
        }
        else
        {
            stageChanging = false;
        }
    }




}
