using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    SceneManager.LoadScene(0);
  }

}
