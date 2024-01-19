using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
  [SerializeField] private GameObject _selectCharMenu, _attackButton;
  [SerializeField] private PlayerControllerSO _playerData;
  [SerializeField] private TextMeshProUGUI _stateText;
  [SerializeField] private TextMeshProUGUI _charName;
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
    _selectCharMenu.SetActive(state == GameState.SelectCharacter);
  }

  public void HandleNextChar()
  {
    if (currentChar < _playerData.characters.Count - 1)
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
    _charName.text = _playerData.characters[currentChar].characterName;
  }

  public void HandleSelectChar()
  {
    // _playerData.currentCharacter = _playerData.characters[currentChar];
    GameManager.HandleSelectChar(currentChar);
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
