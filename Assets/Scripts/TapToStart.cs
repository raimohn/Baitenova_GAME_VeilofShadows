using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToStart : MonoBehaviour, IGameStateListener
{
    private GameState currentGameState;

    private void Start()
    {
        // Устанавливаем начальное состояние
        currentGameState = GameState.MENU;
    }

    void Update()
    {
        if (currentGameState == GameState.MENU)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        GameManager.instance.SetGameState(GameState.GAME);
        SceneManager.LoadScene(1); // Загружаем игровую сцену
    }

    public void GameStateChangedCallBack(GameState gameState)
    {
        currentGameState = gameState;
    }
}
