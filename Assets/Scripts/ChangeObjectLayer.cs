using UnityEngine;
using System.Collections;

public class ChangeObjectLayer : MonoBehaviour
{

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit;  // BallOnTable
    private HoleController holeController;
    private void Start()
    {
        holeController = transform.parent.GetComponent<HoleController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.Player && !holeController.stageChanging)
        {
            other.gameObject.layer = LayerOnEnter;

        }

        if (other.gameObject.tag == Tags.InterStageObjects)
        {
            other.gameObject.layer = LayerOnEnter;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.Player && !holeController.stageChanging)
        {
            other.gameObject.layer = LayerOnExit;

        }

        if (other.gameObject.tag == Tags.InterStageObjects)
        {
            other.gameObject.layer = LayerOnEnter;

        }
    }
}