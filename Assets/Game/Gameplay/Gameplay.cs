using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace Funzilla
{
	internal class Gameplay : Scene
	{
		[SerializeField] private GameObject NextLevelText;
		[SerializeField] private GameObject LoseText;
		[SerializeField] private Text levelText;
		[SerializeField] private GameObject[] NextDisk;
		public List<GameObject> DiskList = new List<GameObject>();
		private static int GainLevel = 50;
		private GameObject obstacle1;
		private Color randomColor;

		internal enum State
		{
			Init,
			Play,
			Win,
			Lose
		}

		private State _state;

		internal static Gameplay Instance { get; private set; }
		internal Level Level { get; private set; }

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
		internal void Play()
		{
			ChangeState(State.Play);
		}

		internal void Win()
		{
			ChangeState(State.Win);
		}

		internal void Lose()
		{
			ChangeState(State.Lose);
		}

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			GameManager.Init(Init);
		}

		internal void ChangeState(State newState)
		{
			if (_state == newState) return;
			ExitOldState();
			_state = newState;
			EnterNewState();
		}

		private void EnterNewState()
		{
			switch (_state)
			{
				case State.Init:
					break;
				case State.Play:
					Analytics.LogLevelStartEvent();
					break;
				case State.Win:
					Analytics.LogLevelCompleteEvent();
					Profile.Level++;
					// TODO: Show complete UI
					break;
				case State.Lose:
					LoseText.SetActive(true);
					Analytics.LogLevelFailEvent();
					// TODO: Show failure UI
					break;
				default:
					return;
			}
		}

		private void ExitOldState()
		{
			switch (_state)
			{
				case State.Init:
					break;
				case State.Play:
					break;
				case State.Win:
					break;
				case State.Lose:
					break;
				default:
					break;
			}
		}

		private void Update()
		{
			switch (_state)
			{
				case State.Init:
					randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
					CreatDisk();
					for (int i = 0; i < 4; i++)
					{
						NextDisk[i].SetActive(false);
					}
					NextLevelText.SetActive(false);
					LoseText.SetActive(false);
					break;
				case State.Play:
					break;
				case State.Win:
					if (Input.GetMouseButtonDown(0) && DiskList.Count == 2)
					{
						NextLevelText.SetActive(false);
						for (int i = 0; i < DiskList.Count; i++)
						{
							DiskList.Remove(DiskList[i]);
						}
						ChangeState(State.Init);
						if (GainLevel <= 300)
						{
							GainLevel += 10;
						} 
					} 
					break;
				case State.Lose:
					LoseText.SetActive(true);
					if (Input.GetMouseButton(0))
					{
						LoseText.SetActive(false);
						for (int i = 0; i < DiskList.Count; i++)
						{
							DiskList.Remove(DiskList[i]);
						}
						ChangeState(State.Init);
					}
					break;
				default:
					break;
			}
		}
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

        for (int i = 0; i < DiskList.Count; i++)
        {
            for (int j = 0; j < countPiece ; j++)
            {
                obstacle1 = DiskList[i].transform.GetChild(j).GetChild(0).gameObject;
                if (!obstacle1.CompareTag("Black_Piece"))
                {
                    obstacle1.GetComponentInChildren<MeshRenderer>().material.color = randomColor;
                }
            }
        }   
    }
	}
}