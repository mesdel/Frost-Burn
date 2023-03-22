using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    private static int MAIN_MENU_INDEX = 0;
    private static int TUTORIAL_INDEX = 1;
    private static int NUM_LEVELS = 8;
    private static int LEVEL_OFFSET = 1;

    private void Awake()
    {
        instance = this;
        if (SceneManager.GetActiveScene().buildIndex == MAIN_MENU_INDEX)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public int GetLevel()
    {
        return SceneManager.GetActiveScene().buildIndex - LEVEL_OFFSET;
    }

    public static bool IsMainMenu()
    {
        return SceneManager.GetActiveScene().buildIndex == MAIN_MENU_INDEX;
    }

    public static bool IsTutorial()
    {
        return SceneManager.GetActiveScene().buildIndex == TUTORIAL_INDEX;
    }

    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum + LEVEL_OFFSET);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(UIManager.instance.InitializeLevelMenu());
        SceneManager.LoadScene(MAIN_MENU_INDEX);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
    public void NextLevel()
    {
        int currLevel = GetLevel();
        if (currLevel == NUM_LEVELS)
        {
            LoadMainMenu();
        }
        else
        {
            LoadLevel(currLevel + 1);
        }
    }
}
