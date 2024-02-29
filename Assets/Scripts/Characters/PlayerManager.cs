using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  [SerializeField] private Transform spawnPoint;
  [SerializeField] private PlayerSO playerData;
  [SerializeField] static private GameObject characterInstance;
  [SerializeField] public bool isIntangible = false;
  public static PlayerManager Instance;

  void Awake()
  {
    Instance = this;
    GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
  }

  void OnDestroy()
  {
    GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
  }

  private void GameManagerOnGameStateChanged(GameState state)
  {
    // Do something
  }

  // Start is called before the first frame update
  void Start()
  {
    // Instatiate a Chico's prefab on start scene
    characterInstance = Instantiate(playerData.prefab, spawnPoint.position, Quaternion.identity);
    if (characterInstance != null) GameManager.Instance?.HandleCameraPlayer(characterInstance);
  }

  // Function to spawn Chico's prefab
  public void SpawnChico()
  {
    Transform transf = characterInstance.transform;
    GameObject character = playerData.prefab;
    Player.Instance.transformationAnim.SetActive(true); // Transformation animation
    StartCoroutine(InstantiateChar(character, transf));
  }

  // Function to spawn any Chico's transformation
  public void SpawnTransformation()
  {
    Transform transf = characterInstance.transform;
    GameObject character = playerData.transformation.prefab;
    Player.Instance.transformationAnim.gameObject.SetActive(true); // Transformation animation
    StartCoroutine(InstantiateChar(character, transf));
  }

  IEnumerator InstantiateChar(GameObject character, Transform transf)
  {
    yield return new WaitForSeconds(0.6f);
    DestroyChar();
    characterInstance = Instantiate(character, transf.position, Quaternion.identity);
    InGameMenuManager.Instance.HandleCharName();
    GameManager.Instance.HandleCameraPlayer(characterInstance);
  }

  // Function to destroy player instance in scene
  private void DestroyChar()
  {
    if (characterInstance) Destroy(characterInstance);
  }

  public void TransformPlayer(PlayerSO transformation)
  {
    playerData.transformation = transformation;
    SpawnTransformation();
  }

  private void Intangible()
  {
    isIntangible = true;
  }

  private IEnumerator SetPlayerAsTangible()
  {
    yield return new WaitForSeconds(2f);
    isIntangible = false;
  }


  // Funtion excute when Player get a hit
  public void TakeDamage()
  {
    if (playerData.hasShield)
    {
      playerData.hasShield = false;
      Intangible();
      StartCoroutine(SetPlayerAsTangible());
    }
    else if (!playerData.hasShield && playerData.transformation != null)
    {
      playerData.transformation = default;
      SpawnChico();
      Intangible();
      StartCoroutine(SetPlayerAsTangible());
    }
    else
    {
      DestroyChar();
      GameManager.Instance.UpdateGameState(GameState.GameOver);
    }
  }
}
