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
        if (player.hasDied)
        {
            GameOver();
        }
        if (player.hasWon)
        {
            Won();
        }

        if (gameState == GameState.gameOver)
        {
            if (inputManager.isUsingSpecial) 
            {
                SceneManager.LoadScene("Game");
            }
        }

        if (gameState == GameState.won)
        {
            if (inputManager.isUsingSpecial)
            {
                SceneManager.LoadScene("Menu");
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
    }
}