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
    private int platformAmount = 20 ;
    private GameStates _currentGameState = GameStates.Idle;
    public static GamePlay Instance {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }
    private enum GameStates
    {
        Idle, Smash, Win, Lose
    }

    private void Start()
    {
        CreatDisk();
    }

    public void Update()
    {
       
    }
    //Start make Disk
    public void CreatDisk()
    {
        for(int i = 1; i <= platformAmount ; i++)
        {
            GameObject cloneDisk = Instantiate(ObDisk, new Vector3(0,  i + 0.5f, 0), Quaternion.Euler(new Vector3(0, i*10, 0)));
            DiskList.Add(cloneDisk);
        }
    }
    
    public GameObject GetTopLayer()
    { 
        Debug.Log("abc");
        firstDisk = DiskList[platformAmount];
        return firstDisk;
    }
    //End make Disk
    
    //Start make Level
    
    //End make Level
    
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
