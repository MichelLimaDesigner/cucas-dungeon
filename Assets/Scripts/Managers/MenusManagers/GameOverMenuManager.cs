using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuManager : MonoBehaviour
{
  [SerializeField] private GameObject gameOverMenu;

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
    gameOverMenu.SetActive(state == GameState.GameOver);
  }

  public void Restart()
  {
    Debug.Log("Reinicou o jogos");
  }

  public void ExitToMenu()
  {
    Debug.Log("Vamos para o menu principal");
  }
}
