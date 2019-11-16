using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Hole;
    int stage;
    bool stageChanging = false;
    Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        Hole.GetComponent<HoleController>().VerticalStageChangeEvent += ChangeStageState;
        GameManager.instance.StageChangedEvent += StageCounter;
        GameManager.instance.GameOverEvent += GameOver;
        GameManager.instance.LevelChangedEvent += ChangeLevel;
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
            transform.position = Vector3.Lerp(transform.position, objectivePosition, 1f / 0.6f * Time.deltaTime);
        }
        else{
            stageChanging = false;
        }
    }
    void GameOver(){
        transform.position = originalPosition;
    }
    void ChangeLevel(int level){
        GameOver();
    }

}
