using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public float maxHorizontalSpeed;
    public float maxVerticalSpeed;
    public float speedMultiplier;
    private bool stageChanging = false;
    private bool horizontalStageChanging = false;
    int currentStage = 1;

    private Vector3 originalPosition;
    private float originalVerticalMovementLimiter;
    public float verticalMovementLimiter;
    public float horizontalMovementLimiter;
    public delegate void OnVerticalStageChange();
    public event OnVerticalStageChange VerticalStageChangeEvent;



    private void Start()
    {
        GameManager.instance.StageChangedEvent += StageChanged;
        GameManager.instance.LevelChangedEvent += LevelChanged;
        GameManager.instance.GameOverEvent += GameOver;

        originalVerticalMovementLimiter = verticalMovementLimiter;
        originalPosition = transform.position;
    }
    void Update()
    {
        if (!stageChanging)
        {
            MoveByArrowKeys();
        }
        else
        {
            GetComponent<Collider>().enabled = false;
            if (horizontalStageChanging)
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
        float moveByXAxis = Input.GetAxis("Horizontal") * speedMultiplier * Time.deltaTime;
        float moveByZAxis = Input.GetAxis("Vertical") * speedMultiplier * Time.deltaTime;
        transform.Translate(moveByXAxis > maxHorizontalSpeed ? maxHorizontalSpeed : moveByXAxis, 0, moveByZAxis > maxVerticalSpeed ? maxVerticalSpeed : moveByZAxis);
        if (Mathf.Abs(transform.position.x) > horizontalMovementLimiter)
        {
            transform.position = new Vector3(transform.position.x < 0 ? (horizontalMovementLimiter * -1f) : horizontalMovementLimiter, transform.position.y, transform.position.z);
        }
        if (transform.position.z < verticalMovementLimiter)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, verticalMovementLimiter);
        }
        if (transform.position.z > verticalMovementLimiter + 9)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, verticalMovementLimiter + 9);
        }
    }

    void StageChanged(int stage)
    {
        if (stage != 1)
        {
            stageChanging = true;
            horizontalStageChanging = true;
            currentStage = stage;
            verticalMovementLimiter += 20;
        }
    }
    void LevelChanged(int level)
    {
        verticalMovementLimiter = originalVerticalMovementLimiter;
        transform.position = originalPosition;
    }

    void GameOver()
    {
        verticalMovementLimiter = originalVerticalMovementLimiter;
        transform.position = originalPosition;
    }
    private void OnDestroy()
    {
        GameManager.instance.StageChangedEvent -= StageChanged;
        GameManager.instance.LevelChangedEvent -= LevelChanged;
        GameManager.instance.GameOverEvent -= GameOver;
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
            horizontalStageChanging = false;
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
            GetComponent<Collider>().enabled = true;
        }
    }




}
