using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToStart : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Проверяем первый тап
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1); // Загружаем игровую сцену
    }
}
