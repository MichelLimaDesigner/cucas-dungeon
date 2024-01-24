using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameMenuManager : MonoBehaviour
{
  [SerializeField] private GameObject inGameMenu;
  [SerializeField] private PlayerControllerSO playerData;
  [SerializeField] private TextMeshProUGUI charName;

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
    inGameMenu.SetActive(state == GameState.Battle);
    if (state == GameState.Battle) HandleCharName();
  }

  void HandleCharName()
  {
    charName.text = playerData.currentCharacter.characterName;
  }
}
