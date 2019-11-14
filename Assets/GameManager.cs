using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool gameOver;
    private int _level;
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
            LevelChangedEvent(_level);
        }
    }
    private int _stage;

    public int Stage
    {
        get
        {
            return _stage;
        }
        set
        {
            if (value == 4)
            {
                Level += 1;
            }
            else
            {
                _stage = value;
                StageChangedEvent(_stage);
            }

        }
    }

    public delegate void OnStageChange(int currentStage);
    public event OnStageChange StageChangedEvent;

    public delegate void OnLevelChange(int level);
    public event OnLevelChange LevelChangedEvent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance);
        }

        gameOver = false;
        _level = 1;
        _stage = 1;
    }



}
