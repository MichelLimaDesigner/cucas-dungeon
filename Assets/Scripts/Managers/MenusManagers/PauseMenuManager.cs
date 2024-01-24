using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
  [SerializeField] private GameObject pauseMenu;

  void Awake()
  {
    GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
  }

  void OnDestroy()
  {
    GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
  }

  private void GameManagerOnGameStateChanged(GameState state)
  {
    pauseMenu.SetActive(state == GameState.Paused);
  }

  public void PauseGame()
  {
    GameManager.Instance.UpdateGameState(GameState.Paused);
  }

  public void ContinueGame()
  {
    GameManager.Instance.UpdateGameState(GameState.Battle);
  }

  public void ExitGame()
  {
    Debug.Log("Saiu da partida");
  }

}
