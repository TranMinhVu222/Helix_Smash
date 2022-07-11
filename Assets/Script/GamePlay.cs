using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;


public class GamePlay : MonoBehaviour
{
    public List<GameObject> DiskList = new List<GameObject>();
    private GameObject firstDisk;
    private GameStates _currentGameState = GameStates.Idle;
    public static bool isGameStarted;
    public GameObject startingText;
    private Ball obballs = new Ball();
    [SerializeField] private GameObject[] NextDisk;
    private int NumberArray = 0;
    private GameObject ArrayDisk;
    private GameObject obstacle1;
    private int RandomPiece1;
    private int RandomPiece2;
    private int RandomPiece3;
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
        for (int i = 0; i < 4; i++)
        {
            NextDisk[i].SetActive(false);
        }
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
        int countPiece = NextDisk[2].transform.childCount;
        //Start born Disk
        for (int i = 0; i < 100; i++)
        {
            GameObject cloneDisk = Instantiate(NextDisk[2], new Vector3(0, i * -1f, 0), Quaternion
                .Euler(new Vector3(0, i * 5, 0)));
            DiskList.Add(cloneDisk);
        }
        //End born Disk
        
        //Start hard level
        for (int i = 0; i < (int)(DiskList.Count / 7); i++) 
        {
            obstacle1 = DiskList[i].transform.GetChild(0).GetChild(0).gameObject;
            obstacle1.tag = "Black_Piece";
            obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
        }

        for (int i = (int)(DiskList.Count / 7); i < (int)(DiskList.Count * 2 / 7); i += 2)
        {
            for (int j = 0; j < countPiece; j+=2)
            {
                obstacle1 = DiskList[i].transform.GetChild(j).GetChild(0).gameObject;
                obstacle1.tag = "Black_Piece";
                obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
                Debug.Log("j = " + j);
            }
            Debug.Log("i = " + i);
        }
        for (int i = (int)(DiskList.Count * 2 / 7) ; i < DiskList.Count * 3 / 7; i++)
        {
            for (int j = 3; j < countPiece ; j++)
             {
                 obstacle1 = DiskList[i].transform.GetChild(j).GetChild(0).gameObject; 
                 obstacle1.tag = "Black_Piece";
                 obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
             }
        }
        
        for (int i = (int)(DiskList.Count * 3 / 7); i < DiskList.Count; i++)
        {
            int RandomPiece = UnityEngine.Random.Range(0, countPiece - 1);
            obstacle1 = DiskList[i].transform.GetChild(RandomPiece).GetChild(0).gameObject; 
            obstacle1.tag = "Black_Piece";
            obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
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
                NumberArray = UnityEngine.Random.Range(0,3);
                break;
            case GameStates.Lose:
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