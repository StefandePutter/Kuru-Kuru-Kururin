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
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;

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
        // check player
        if (player.state == PlayerState.died)
        {
            GameOver();
        }
        if (player.state == PlayerState.won)
        {
            Won();
        }

        if (gameState == GameState.gameOver)
        {
            if (inputManager.isUsingSpecial) 
            {                
                gameOverScreen.SetActive(false);
                
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
        gameState = GameState.won;

        winScreen.SetActive(true);
    }

    public void GameOver()
    {
        gameState = GameState.gameOver;
        
        gameOverScreen.SetActive(true);

        // to show school what i had first
        // reload current scene
        // string currentSceneName = SceneManager.GetActiveScene().name;
        // SceneManager.LoadScene(currentSceneName);
    }
}