using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void PlayGame()
    {
        LoadNextLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        int nextLevelIndex = activeScene.buildIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            Debug.LogErrorFormat("Can't find any level with index {0}. Please add one to the Build Profiles", nextLevelIndex);
        }
    }
}
