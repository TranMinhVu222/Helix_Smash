using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GamePlay : MonoBehaviour
{
    public List<GameObject> DiskList = new List<GameObject>();
    [SerializeField] private GameObject ObDisk;
    private GameObject firstDisk;
    private GameStates _currentGameState = GameStates.Idle;
    public static GamePlay Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private enum GameStates
    {
        Idle,
        Smash,
        Win,
        Lose
    }

    private void Start()
    {
        CreatDisk();
        // DiskList[0].SetActive(false);
    }

    public void Update()
    {
    }

    //Start make Disk
    public void CreatDisk()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject cloneDisk = Instantiate(ObDisk, new Vector3(0, i * -1f, 0), Quaternion.Euler(new Vector3(0, i * 10, 0)));
            DiskList.Add(cloneDisk);
        }
    }
    //Start make Level

    //End make Level
    
    //Color Black piece disk
   
    private void ChangeState(GameStates newState)
    {
        if (newState == _currentGameState) return;
        ExitCurrentState();
        _currentGameState = newState;
        EnterNewState();
    }

    private void EnterNewState()
    {
        switch (_currentGameState)
        {
            case GameStates.Idle:
                break;
            case GameStates.Smash:
                break;
            case GameStates.Win:
                break;
            case GameStates.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ExitCurrentState()
    {
        switch (_currentGameState)
        {
            case GameStates.Idle:
                break;
            case GameStates.Smash:
                break;
            case GameStates.Win:
                break;
            case GameStates.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}