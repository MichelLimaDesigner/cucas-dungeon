using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
  [SerializeField] private GameObject pauseMenu;

  public void StartGame()
  {
    // Certifique-se de que você tem cenas adicionadas no Build Settings no Unity
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    // Verifica se há uma cena seguinte
    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
      SceneManager.LoadScene(nextSceneIndex);
    }
  }

  public void Credits()
  {
    SceneManager.LoadScene("Credits");
  }

  public void ExitGame()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }
}
