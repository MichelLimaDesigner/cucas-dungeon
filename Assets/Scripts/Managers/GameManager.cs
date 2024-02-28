using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
  public PlayerSO playerData;
  public static GameManager Instance;
  public GameState State;
  private CinemachineVirtualCamera vcam;

  public static event Action<GameState> OnGameStateChanged;

  private void Awake()
  {
    Instance = this;
    vcam = GameObject.FindGameObjectsWithTag("VirtualCam")[0].GetComponent<CinemachineVirtualCamera>();
  }

  void Start()
  {
    UpdateGameState(GameState.Battle);
  }

  public void HandleCameraPlayer(GameObject player)
  {
    Debug.Log("Cheguei aqui");
    Debug.Log(player);
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
        // Do something
        break;
      case GameState.GameOver:
        // Do something
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }
    OnGameStateChanged?.Invoke(newState);
  }
}

public enum GameState
{
  Battle,
  Paused,
  Victory,
  GameOver
}
