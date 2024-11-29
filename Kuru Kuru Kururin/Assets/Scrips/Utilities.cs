using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{

    public void GoToGame()
    {
        // go to Tutorial scene
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ResetData()
    {
        SaveLoad.ResetData();
        GoToLevelSelect();
    }

    public void QuitGame()
    {
        // quit application
        Application.Quit();
        Debug.Log("Quit");
    }
}
