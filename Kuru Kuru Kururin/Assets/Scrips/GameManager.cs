using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public enum GameState
{
    playing = 0,
    gameOver,
    won
}
public class GameManager : MonoBehaviour
{
    private GameObject gameOverScreen;

    public GameState gameState;

    private InputManager inputManager;

    private Player player;

    private void Start()
    {
        GameObject tmp = GameObject.Find("Player");
        player = tmp.GetComponent<Player>();

        GameObject tmpInput = GameObject.Find("InputManager");
        inputManager = tmpInput.GetComponent<InputManager>();
    }

    private void Update()
    {
        // check player call functions once
        if (player.state == PlayerState.died && gameState != GameState.gameOver)
        {
            GameOver();
        }
        if (player.state == PlayerState.won && gameState != GameState.won)
        {
            Won();
        }

        if (gameState == GameState.gameOver)
        {
            if (inputManager.isUsingSpecial) 
            {                
                Destroy(gameOverScreen);

                player.Respawn();

                gameState = GameState.playing;
            }
        }

        if (gameState == GameState.won)
        {
            if (inputManager.isUsingSpecial)
            {
                int sceneAmount = SceneManager.sceneCountInBuildSettings;
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

                if (currentSceneIndex == sceneAmount-1)
                {
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    SceneManager.LoadScene(currentSceneIndex+1);
                }

            }
        }
    }

    public void Won()
    {
        // only want to do once
        if (gameState == GameState.won)
        {
            return;
        }
        
        // save data in json
        SaveLoad.SaveLevel(SceneManager.GetActiveScene().buildIndex);

        Instantiate(Resources.Load<GameObject>("PreFabs/Win"));
        gameState = GameState.won;
    }

    public void GameOver()
    {
        gameOverScreen = Instantiate(Resources.Load<GameObject>("PreFabs/GameOver"));

        gameState = GameState.gameOver;

        // to show school what i had first
        // reload current scene
        // string currentSceneName = SceneManager.GetActiveScene().name;
        // SceneManager.LoadScene(currentSceneName);
    }
}