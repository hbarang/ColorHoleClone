using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectController : MonoBehaviour
{
    public Color cubeColor;
    public bool isObjective;
    private GameObject stageGameObject;
    private void Start()
    {
        var cubeRenderer = this.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", cubeColor);

        stageGameObject = transform.parent.gameObject;
    }
    private void Update()
    {
        CheckInTheHole();
    }
    void CheckInTheHole()
    {
        if (transform.position.y < -1)
        {
            if (cubeColor == new Color(1, 1, 1, 0))
            {
                if (isObjective)
                {
                    stageGameObject.GetComponent<StageController>().ObjectiveCount -= 1;
                    CameraShaker.instance.ShakeVertical();
                }
                Destroy(this.gameObject);
            }
            else
            {
                GameManager.instance.GameOver = true;
            }
        }
    }

}
