using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private bool _gameOver;
    public bool GameOver
    {
        get
        {
            return _gameOver;
        }
        set
        {
            _gameOver = value;
            if (_gameOver == true)
            {
                GameOverEvent();
            }
        }
    }
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
            if (LevelChangedEvent != null)
            {
                LevelChangedEvent(_level);
            }
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
                _stage = 1;
            }
            else
            {
                _stage = value;
                if (StageChangedEvent != null)
                {
                    StageChangedEvent(_stage);
                }
            }

        }
    }

    public delegate void OnStageChange(int currentStage);
    public event OnStageChange StageChangedEvent;

    public delegate void OnLevelChange(int level);
    public event OnLevelChange LevelChangedEvent;

    public delegate void OnGameOver();
    public event OnGameOver GameOverEvent;
    public GameObject[] levels;

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

        _gameOver = false;
        _level = 1;
        _stage = 1;
    }

    private void Start()
    {
        GameOverEvent += ResetStage;
        LevelChangedEvent += ChangeLevel;
    }

    void ResetStage()
    {
        Stage = 1;
        GameOver = false;
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        Instantiate(levels[Level-1], Vector3.zero, Quaternion.identity);
    }
    void ChangeLevel(int level){
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        if(levels.Length < Level){
            Debug.Log("This was the last level");
            return;
        }
        Instantiate(levels[Level-1], Vector3.zero, Quaternion.identity);
    }


}
