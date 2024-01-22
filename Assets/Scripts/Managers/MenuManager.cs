using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
  [SerializeField] private GameObject selectCharMenu;
  [SerializeField] private PlayerControllerSO playerData;
  [SerializeField] private TextMeshProUGUI charName;
  private int currentChar = 0;

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
    selectCharMenu.SetActive(state == GameState.SelectCharacter);
    HandleSelectedChar();
  }

  public void HandleNextChar()
  {
    if (currentChar < playerData.characters.Count - 1)
    {
      currentChar += 1;
      HandleSelectedChar();
    }
  }

  public void HandlePrevChar()
  {
    if (currentChar >= 1)
    {
      currentChar -= 1;
      HandleSelectedChar();
    }
  }

  private void HandleSelectedChar()
  {
    charName.text = playerData.characters[currentChar].characterName;
  }

  public void HandleSelectChar()
  {
    playerData.currentCharacter = playerData.characters[currentChar];
    GameManager.Instance.UpdateGameState(GameState.Battle);
  }

  // Start is called before the first frame update
  void Start()
  {
    HandleSelectedChar();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
