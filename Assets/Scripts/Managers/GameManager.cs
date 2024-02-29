using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public PlayerSO playerData;
  public static GameManager Instance;
  public GameState State;
  private CinemachineVirtualCamera vcam;
  private List<GameObject> levelSwitchers;
  public int switchersCount;
  private int switchersActived = 0;
  public bool canPassLevel = false;
  public string nextLevelName;

  public static event Action<GameState> OnGameStateChanged;

  private void Awake()
  {
    Instance = this;
    vcam = GameObject.FindGameObjectWithTag("VirtualCam").GetComponent<CinemachineVirtualCamera>();
  }

  void Start()
  {
    UpdateGameState(GameState.Battle);
    GameObject[] levelSwitcherArray = GameObject.FindGameObjectsWithTag("LevelSwitch");
    levelSwitchers = new List<GameObject>(levelSwitcherArray);
    switchersCount = levelSwitchers.Count;
  }

  public void HandleCameraPlayer(GameObject player)
  {
    if (player) vcam.Follow = player.transform;
  }

  public static void HandleSpawnChar()
  {
    Time.timeScale = 1;
  }

  static void PauseGame()
  {
    Time.timeScale = 0;
  }

  public void HandleLevelPortal()
  {
    switchersActived += 1;
    switchersCount -= 1;
    InGameMenuManager.Instance.HandleSwitchesCount();
    if (switchersActived == levelSwitchers.Count)
    {
      canPassLevel = true;
    }
  }

  public void HandleFinishStage()
  {
    if (State != GameState.Victory)
    {
      UpdateGameState(GameState.Victory);
      LoadNextScene();
    }
  }

  void LoadNextScene()
  {
    // Load the next level scene if it exist
    if (nextLevelName != null) StartCoroutine(ChangeLevelTransition());
    else UpdateGameState(GameState.Finish);
  }

  IEnumerator ChangeLevelTransition()
  {
    LevelTransition.Instance.Transition();
    yield return new WaitForSeconds(1.5f);
    SceneManager.LoadScene(nextLevelName);
  }

  public void UpdateGameState(GameState newState)
  {
    State = newState;
    switch (newState)
    {
      case GameState.Battle:
        // Do something
        HandleSpawnChar();
        break;
      case GameState.Paused:
        PauseGame();
        break;
      case GameState.Victory:
        Debug.Log("VITORIAAAAA");
        break;
      case GameState.Finish:
        Debug.Log("FIM DE JOGO");
        break;
      case GameState.GameOver:
        ReloadScene();
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }
    OnGameStateChanged?.Invoke(newState);
  }

  // Função para recarregar a cena
  private void ReloadScene()
  {
    // Obtém o índice da cena atual
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    // Recarrega a cena atual
    SceneManager.LoadScene(currentSceneIndex);
  }
}

public enum GameState
{
  Battle,
  Paused,
  Victory,
  GameOver,
  Finish
}
