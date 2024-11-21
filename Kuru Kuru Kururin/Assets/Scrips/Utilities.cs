using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{

    public void GoToGame()
    {
        // go to game scene
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        // quit application
        Application.Quit();
        Debug.Log("Quit");
    }
}
