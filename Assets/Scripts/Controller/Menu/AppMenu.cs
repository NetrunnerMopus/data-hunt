using UnityEngine;
using UnityEngine.SceneManagement;

public class AppMenu : MonoBehaviour
{
    public void PlaySoloRunner()
    {
        SceneManager.LoadScene("Runner Game", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

