using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
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
    public GameObject NextLevelText;
    public GameObject LoseText;
    private Ball obballs = new Ball();
    [SerializeField] private GameObject[] NextDisk;
    private GameObject ArrayDisk;
    private GameObject obstacle1;
    public int GainLevel = 50;
    public static GamePlay Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public enum GameStates
    {
        Idle,
        Next,
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
        NextLevelText.SetActive(false);
        LoseText.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
        switch (_currentGameState)
        {
            case GameStates.Lose:
                LoseText.SetActive(true);
                if (Input.GetMouseButton(0))
                {
                    LoseText.SetActive(false);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
            case GameStates.Win:
                if (Input.GetMouseButtonDown(0) && DiskList.Count == 2)
                {
                    NextLevelText.SetActive(false);
                    ChangeState(GameStates.Next);
                    if (GainLevel <= 300)
                    {
                        GainLevel += 10;
                    } 
                } 
                break;
            default:
                return;
        }
    }

    //Start make Disk
    public void CreatDisk()
    {
        int RandomDisk = UnityEngine.Random.Range(0, 3);
        int countPiece = NextDisk[RandomDisk].transform.childCount;
        //Start born Disk
        for (int i = 0; i < GainLevel; i++)
        {
            GameObject cloneDisk = Instantiate(NextDisk[RandomDisk], new Vector3(0, i * -1f, 0), Quaternion
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
            for (int j = 0; j < countPiece - countPiece/2; j++)
            {
                obstacle1 = DiskList[i].transform.GetChild(j).GetChild(0).gameObject;
                obstacle1.tag = "Black_Piece";
                obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            }
        }
        for (int i = (int)(DiskList.Count * 2 / 7) ; i < DiskList.Count * 3 / 7; i++)
        {
            for (int j = 2; j < countPiece - 1  ; j++)
             {
                 obstacle1 = DiskList[i].transform.GetChild(j).GetChild(0).gameObject; 
                 obstacle1.tag = "Black_Piece";
                 obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
             }
        }
        
        for (int i = (int)(DiskList.Count * 3 / 7); i < DiskList.Count - 2; i++)
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
        Debug.Log(_currentGameState);
        switch (_currentGameState)
        {
            case GameStates.Idle:
                break;
            case GameStates.Next:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case GameStates.Win:
                NextLevelText.SetActive(true);
                GainLevel += 10;
                break;
            case GameStates.Lose:
                LoseText.SetActive(true);
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
            case GameStates.Next:
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