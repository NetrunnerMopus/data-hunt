using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void PlaySoloRunner()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

