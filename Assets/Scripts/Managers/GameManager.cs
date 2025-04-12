using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void StartGame()             => SetGameState(GameState.GAME);
    public void StartWeaponSelection()  => SetGameState(GameState.WEAPONSELECTION);
    public void StartShop()             => SetGameState(GameState.SHOP);

    public void SetGameState(GameState gameState)
    {
        IEnumerable<IGameStateListener> gameStateListeners = 
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IGameStateListener>();

        foreach (IGameStateListener gameStateListener in gameStateListeners)
            gameStateListener.GameStateChangedCallBack(gameState);
    }

    public void WaveCompletedCallBack()
    {
        if (Player.instance.HasLeveledUp())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }

    public void ManageGameOver()
    {
        SceneManager.LoadScene(1);
    }
}

public interface IGameStateListener
{
    void GameStateChangedCallBack(GameState gameState);
}
