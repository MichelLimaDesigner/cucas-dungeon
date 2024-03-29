using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameMenuManager : MonoBehaviour
{
  [SerializeField] private GameObject inGameMenu;
  [SerializeField] private PlayerSO playerData;
  [SerializeField] private TextMeshProUGUI charName;
  [SerializeField] private TextMeshProUGUI switchCount;
  public static InGameMenuManager Instance;

  void Awake()
  {
    Instance = this;
    GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
  }

  void Start()
  {
    HandleSwitchesCount();
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

  public void HandleSwitchesCount()
  {
    switchCount.text = GameManager.Instance.switchersCount.ToString();
  }

  public void HandleCharName()
  {
    if (playerData.transformation) charName.text = playerData.transformation.characterName;
    else charName.text = playerData.characterName;
  }
}
