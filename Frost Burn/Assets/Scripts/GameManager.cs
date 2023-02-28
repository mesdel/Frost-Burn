using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIController uiController;
    private bool gamePaused;

    void Awake()
    {
        Time.timeScale = 1.0f;
        gamePaused = false;
        uiController = gameObject.GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gamePaused && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void Death()
    {
        Debug.Log("Player Death");
        uiController.LoseScreen();
        PauseGame();
    }

    public void Goal()
    {
        Debug.Log("Goal Reached");
        uiController.WinScreen();
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;
        gamePaused = true;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
