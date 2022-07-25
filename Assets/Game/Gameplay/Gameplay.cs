using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Funzilla
{
    internal class Gameplay : Scene
    {
        public List<GameObject> DiskList = new List<GameObject>();
        private GameStates _currentGameState = GameStates.Idle;
        public GameObject startingText;
        public GameObject NextLevelText;
        public GameObject LoseText;
        [SerializeField] private Text levelText;
        [SerializeField] private GameObject[] NextDisk;
        private GameObject ArrayDisk;
        private GameObject obstacle1;
        public static int GainLevel = 50;
        private Color randomColor;
        internal Level Level { get; private set; }
        public static Gameplay Instance { get; private set; }

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
            randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            CreatDisk();
            for (int i = 0; i < 4; i++)
            {
                NextDisk[i].SetActive(false);
            }
            startingText.SetActive(true);
            NextLevelText.SetActive(false);
            LoseText.SetActive(false);
            // GameManager.Init(Init);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
               startingText.SetActive(false);
            }

            switch (_currentGameState)
            {
                case GameStates.Lose:
                    LoseText.SetActive(true);
                    if (Input.GetMouseButton(0))
                    {
                        LoseText.SetActive(false);
                        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager
                            .GetActiveScene().buildIndex);
                    }
                    break;
                case GameStates.Win:
                    if (Input.GetMouseButtonDown(0) && DiskList.Count == 2)
                    {
                        NextLevelText.SetActive(false);
                        ChangeState(GameStates.Next);
                        if (GainLevel <= 300)
                        {
                            GainLevel += 20;
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
            int RandomDisk = Random.Range(0, 3);
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
            for (int i = 0; i < (int) (DiskList.Count / 7); i++)
            {
                obstacle1 = DiskList[i].transform.GetChild(0).GetChild(0).gameObject;
                obstacle1.tag = "Black_Piece";
                obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            }

            for (int i = (int) (DiskList.Count / 7); i < (int) (DiskList.Count * 2 / 7); i += 2)
            {
                for (int j = 0; j < countPiece - countPiece / 2; j++)
                {
                    obstacle1 = DiskList[i].transform.GetChild(j).GetChild(0).gameObject;
                    obstacle1.tag = "Black_Piece";
                    obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
                }
            }

            for (int i = (int) (DiskList.Count * 2 / 7); i < DiskList.Count * 3 / 7; i++)
            {
                for (int j = 2; j < countPiece - 1; j++)
                {
                    obstacle1 = DiskList[i].transform.GetChild(j).GetChild(0).gameObject;
                    obstacle1.tag = "Black_Piece";
                    obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
                }
            }

            for (int i = (int) (DiskList.Count * 3 / 7); i < DiskList.Count - 2; i++)
            {
                int RandomPiece = Random.Range(0, countPiece - 1);
                obstacle1 = DiskList[i].transform.GetChild(RandomPiece).GetChild(0).gameObject;
                obstacle1.tag = "Black_Piece";
                obstacle1.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            }

            for (int i = 0; i < DiskList.Count; i++)
            {
                for (int j = 0; j < countPiece; j++)
                {
                    obstacle1 = DiskList[i].transform.GetChild(j).GetChild(0).gameObject;
                    if (!obstacle1.CompareTag("Black_Piece"))
                    {
                        obstacle1.GetComponentInChildren<MeshRenderer>().material.color = randomColor;
                    }
                }
            }
        }
        private void Init()
        {
            // Load level
            var levelIndex = (Profile.Level - 1) % LevelManager.Levels.Count;
            var levelAsset = LevelManager.Levels[levelIndex];
            var levelPrefab = Resources.Load<Level>($"Levels/{levelAsset}");
            Level = Instantiate(levelPrefab, transform);
            // Hide splash screen after game is initialized
            levelText.text = $"Level {Profile.Level}";
            SceneManager.HideLoading();
            SceneManager.HideSplash();
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
                case GameStates.Next:
                    Profile.Level++;
                    // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager
                    //      .GetActiveScene().buildIndex);
                    SceneManager.ReloadScene(SceneID.GameManager);
                    break;
                case GameStates.Win:
                    NextLevelText.SetActive(true);
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
}