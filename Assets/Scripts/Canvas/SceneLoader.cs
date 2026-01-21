using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
        // Deze functie kun je aanroepen vanuit de OnClick van een Button
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
       Application.Quit();
    }
    public void MainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
