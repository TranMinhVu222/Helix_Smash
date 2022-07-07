using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePlay : MonoBehaviour
{
    public List<GameObject> DiskList = new List<GameObject>();
    [SerializeField]private GameObject ObDisk;
    private GameObject firstDisk;
    private GameStates _currentGameState = GameStates.Idle;
    public static bool isGameStarted;
    public GameObject startingText;
    private Ball obballs = new Ball();
    public static GamePlay Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public enum GameStates
    {
        Idle,
        Furry,
        Win,
        Lose
    }

    private void Start()
    {
        CreatDisk();
        isGameStarted = false;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }

    //Start make Disk
    public void CreatDisk()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject cloneDisk = Instantiate(ObDisk, new Vector3(0, i * -1f, 0), Quaternion.Euler(new Vector3(0, i * 5, 0)));
            DiskList.Add(cloneDisk);
        }
    }
    public void ChangeState(GameStates newState)
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
            case GameStates.Furry:
                break;
            case GameStates.Win:
                obballs._currentState = Ball.State.Fall;
                break;
            case GameStates.Lose:
                Debug.Log("as");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            case GameStates.Furry:
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