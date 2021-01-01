using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        gameObject.transform.SetAsLastSibling();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void Concede()
    {
        SceneManager.LoadScene("App Menu", LoadSceneMode.Single);
    }
}

