using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public bool gameOver { private set; get; }
    public static GameManager2 instance;
    private bool gamePaused;
    KeyCode pauseKey;

    void Awake()
    {
        Time.timeScale = 1;
        gamePaused = false;
        gameOver = false;
        instance = this;

        // todo: possibly change
        // set pause key depending on editor/application
        pauseKey = KeyCode.Escape;
#if UNITY_EDITOR
        pauseKey = KeyCode.P;
#endif

    }

    private void Update()
    {
        if (!gameOver && Input.GetKeyDown(pauseKey))
        {
            if(gamePaused)
            {
                ResumeGame();
                gamePaused = false;
            }
            else
            {
                PauseGame();
                gamePaused = true;
            }
        }
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void Goal()
    {
        // todo: winstate; pause game and prompt user to go to main menu or next level
        StopTime();
        //UIManager.instance.GameWin();
        DataSaver.instance.SaveProgress();
    }

    public void Death()
    {
        gameOver = true;
        StopTime();
        //UIManager.instance.GameOver();
    }

    private void StopTime()
    {
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        StopTime();
        //UIManager.instance.PauseGame();
    }
    public void ResumeGame ()
    {
        if(!gameOver)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            //UIManager.instance.ResumeGame();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
