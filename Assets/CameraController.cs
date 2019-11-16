using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Hole;
    int stage;
    bool stageChanging = false;
    // Start is called before the first frame update
    void Start()
    {

        Hole.GetComponent<HoleController>().VerticalStageChangeEvent += ChangeStageState;
        GameManager.instance.StageChangedEvent += StageCounter;
    }

    // Update is called once per frame
    void Update()
    {
        if (stageChanging)
        {
            MoveVertically();
        }

    }
    void StageCounter(int stageNo)
    {
        stage = stageNo;
    }
    void ChangeStageState()
    {
        stageChanging = true;
    }
    void MoveVertically()
    {
        Vector3 objectivePosition = new Vector3(transform.position.x, transform.position.y, (-6 + (stage - 1) * 20));
        if (Mathf.Abs(transform.position.z - objectivePosition.z) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, objectivePosition, 1f / 1.5f * Time.deltaTime);
        }
        else{
            stageChanging = false;
        }
    }
}
