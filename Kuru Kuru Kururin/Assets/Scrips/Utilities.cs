using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{

    public void GoToGame()
    {
        // go to Tutorial scene
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        // quit application
        Application.Quit();
        Debug.Log("Quit");
    }
}
