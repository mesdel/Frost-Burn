using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver { private set; get; }
    public bool gameWon { private set; get; }
    public static GameManager instance;
    private bool gamePaused;
    KeyCode pauseKey;

    void Awake()
    {
        Time.timeScale = 1;
        gamePaused = false;
        gameOver = false;
        gameWon = false;
        instance = this;

        // set pause key depending on editor/application
        pauseKey = KeyCode.Escape;
#if UNITY_EDITOR
        pauseKey = KeyCode.P;
#endif
        
    }

    private void Update()
    {
        if (gameWon)
            return;

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
        else if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void Goal()
    {
        gameWon = true;
        StopTime();
        UIManager.instance.GameWin();
        //DataSaver.instance.SaveProgress();
    }

    public void Death()
    {
        gameOver = true;
        StopTime();
        UIManager.instance.GameOver();
    }

    private void StopTime()
    {
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        StopTime();
        UIManager.instance.PauseGame();
    }
    public void ResumeGame ()
    {
        if(!gameOver)
        {
            Time.timeScale = 1;
            UIManager.instance.ResumeGame();
        }
    }

    public void RestartLevel()
    {
        SceneLoader.instance.ReloadScene();
    }
}
