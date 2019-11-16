using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public int firstObjectiveCount;
    private int _objectiveCount;
    public int ObjectiveCount
    {
        get { return _objectiveCount; }
        set
        {
            if (value == 0)
            {
                GameManager.instance.Stage += 1;
            }
            else
            {
                _objectiveCount = value;
                if (ObjectiveCountChangeEvent != null)
                {
                    ObjectiveCountChangeEvent();
                }

            }

        }
    }
    private void Start()
    {
        _objectiveCount = firstObjectiveCount;
    }

    public delegate void OnObjectiveCountChange();
    public event OnObjectiveCountChange ObjectiveCountChangeEvent;

}
