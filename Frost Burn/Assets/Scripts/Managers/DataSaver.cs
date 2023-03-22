using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public static DataSaver instance { get; private set; }

    private const float defaultVolume = 0.5f;
    public float sfxVolume;
    public float musicVolume;
    public float ambiVolume;
    private const float defaultSensitivity = 0.5f;
    public float sensitivity;
    public int levelsCompleted;

    public bool isLoaded;

    [System.Serializable]
    class Settings
    {
        public float sfxVolume;
        public float musicVolume;
        public float ambiVolume;
        public float sensitivity;
    }

    [System.Serializable]
    class SaveData
    {
        public int levelsCompleted;
    }

    void Awake()
    {
        isLoaded = false;
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadProgress();
        LoadSettings();
        isLoaded = true;
    }

    static public IEnumerator WaitForData()
    {
        while (instance == null || !instance.isLoaded)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public void SaveSettings()
    {        
        // load slider values into Settings structure
        UIManager.instance.SaveVolumes();
        UIManager.instance.SaveSensitivity();

        Settings settings = new Settings
        {
            ambiVolume = this.ambiVolume,
            sfxVolume = this.sfxVolume,
            musicVolume = this.musicVolume,
            sensitivity = this.sensitivity
        };

        string json = JsonUtility.ToJson(settings);
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);

    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Settings settings = JsonUtility.FromJson<Settings>(json);

            ambiVolume = settings.ambiVolume;
            musicVolume = settings.musicVolume;
            sfxVolume = settings.sfxVolume;
            sensitivity = settings.sensitivity;
        }
        else
        {
            LoadDefaultSettings();
        }
    }

    private void LoadDefaultSettings()
    {
        ambiVolume = musicVolume = sfxVolume = defaultVolume;
        sensitivity = defaultSensitivity;
    }

    public void SaveProgress()
    {
        levelsCompleted = Mathf.Max(SceneLoader.instance.GetLevel(), levelsCompleted);
        SaveData saveData = new SaveData
        {
            levelsCompleted = this.levelsCompleted
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/saveData.json", json);

    }

    public void LoadProgress()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            levelsCompleted = saveData.levelsCompleted;
        }
        else
        {
            levelsCompleted = 0;
        }

        Debug.Log("Levels completed: " + levelsCompleted);
    }
}
