using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public PlayerControllerSO playerData;
  public PlayerController controller;
  public static GameManager Instance;
  public GameState State;

  public static event Action<GameState> OnGameStateChanged;

  private void Awake()
  {
    Instance = this;
  }

  void Start()
  {
    UpdateGameState(GameState.SelectCharacter);
  }

  public static void HandleSelectChar(int charIndex)
  {
    // Do something
    Debug.Log("Aqui");
    controller.SelectCharacter(charIndex);
  }

  public void UpdateGameState(GameState newState)
  {
    State = newState;
    switch (newState)
    {
      case GameState.SelectCharacter:
        // Do something
        break;
      case GameState.Battle:
        // Do something
        break;
      case GameState.Victory:
        // Do something
        break;
      case GameState.Lose:
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
  SelectCharacter,
  Battle,
  Victory,
  Lose
}
