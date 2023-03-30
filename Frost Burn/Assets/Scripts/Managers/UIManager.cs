using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private Transform gameMenus;
    private GameObject pauseMenu;
    private GameObject gameOverMenu;
    private GameObject winMenu;

    [SerializeField]
    private Transform settingsMenu;
    private Slider musicSlider;
    private Slider sfxSlider;
    private Slider ambiSlider;
    private Slider mouseSlider;

    [SerializeField]
    private AudioClip winSound;
    [SerializeField]
    private AudioClip loseSound;
    private AudioSource audioSource;

    [SerializeField]
    private Transform levelMenu;

    //todo: move audio into own class

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        gameMenus = GameObject.Find("UI/Canvas/Menus").transform;

        if(SceneLoader.IsMainMenu())
        {
            StartCoroutine(InitSettings());
            StartCoroutine(InitializeLevelMenu());
        }
        else
        {
            InitMenus();
        }
    }

    private void InitMenus()
    {
        // find the three menus
        winMenu = gameMenus.Find("Win Menu").gameObject;
        gameOverMenu = gameMenus.Find("Game Over Menu").gameObject;
        pauseMenu = gameMenus.Find("Pause Menu").gameObject;

        // add functionality to their buttons
        winMenu.SetActive(true);
        gameOverMenu.SetActive(true);
        pauseMenu.SetActive(true);
        InitButtons();

        // hide the menues
        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void InitButtons()
    {
        Button[] buttons = gameMenus.GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            UnityAction buttonFunction = null;
            string name = button.gameObject.name;
            if(name.Equals("Restart Level Button"))
            {
                buttonFunction = SceneLoader.instance.ReloadScene;
            }
            else if(name.Equals("Main Menu Button"))
            {
                buttonFunction = SceneLoader.instance.LoadMainMenu;
            }
            else if (name.Equals("Quit Game Button"))
            {
                buttonFunction = SceneLoader.instance.ExitGame;
            }
            else if (name.Equals("Unpause Button"))
            {
                buttonFunction = GameManager.instance.ResumeGame;
            }
            else if (name.Equals("Next Level Button"))
            {
                buttonFunction = SceneLoader.instance.NextLevel;
            }
            else
            {
                Debug.Log("Unautomated Button: " + name);
            }

            // add listener to button to call appropriate function
            if (buttonFunction != null)
                button.onClick.AddListener(buttonFunction);
        }
    }

    private IEnumerator InitSettings()
    {
        yield return StartCoroutine(DataSaver.WaitForData());

        Transform volumes = settingsMenu.Find("Volume");

        musicSlider = volumes.Find("Music Slider").GetComponent<Slider>();
        sfxSlider = volumes.Find("Sound FX Slider").GetComponent<Slider>();
        ambiSlider = volumes.Find("Ambiance Slider").GetComponent<Slider>();

        musicSlider.value = DataSaver.instance.musicVolume;
        sfxSlider.value = DataSaver.instance.sfxVolume;
        ambiSlider.value = DataSaver.instance.ambiVolume;

        mouseSlider = settingsMenu.Find("Mouse Sensitivity").Find("Slider").GetComponent<Slider>();
        mouseSlider.value = DataSaver.instance.sensitivity;

        Button saveButton = settingsMenu.Find("Save Button").GetComponent<Button>();
        saveButton.onClick.AddListener(DataSaver.instance.SaveSettings);
    }

    public IEnumerator InitializeLevelMenu()
    {
        yield return StartCoroutine(DataSaver.WaitForData());

        Transform unlockableLevels = levelMenu.Find("Unlockable Levels");
        int levelsToUnlock = DataSaver.instance.levelsCompleted;

        // loop through unlockable levels and unlock them based on progress
        foreach(Transform level in unlockableLevels)
        {
            if(levelsToUnlock > 0)
            {
                level.GetComponent<Button>().interactable = true;
                levelsToUnlock--;
            }
            else
            {
                yield return null;
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        audioSource.PlayOneShot(loseSound);
    }

    public void GameWin()
    {
        winMenu.SetActive(true);
        audioSource.PlayOneShot(winSound);
    }

    // when the slider is edited, save update to Settings struct
    public void SaveVolumes()
    {
        DataSaver.instance.musicVolume = musicSlider.value;
        DataSaver.instance.sfxVolume = sfxSlider.value;
        DataSaver.instance.ambiVolume = ambiSlider.value;
    }

    public void SaveSensitivity()
    {
        DataSaver.instance.sensitivity = mouseSlider.value;
    }
}
