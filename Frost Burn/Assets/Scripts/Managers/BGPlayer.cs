using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGPlayer : MonoBehaviour
{
    public static BGPlayer instance { get; private set; }

    private AudioSource[] audioSources;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider ambiSlider;

    // todo: remove singleton/dontdestroy status
    // and instead use different music/ambiance from main menu

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSources = GetComponents<AudioSource>();
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return StartCoroutine(DataSaver.WaitForData());

        // the first audio source is music, 2nd is ambiance
        audioSources[0].volume = DataSaver.instance.musicVolume;
        audioSources[0].Play();

        audioSources[1].volume = DataSaver.instance.ambiVolume;
        audioSources[1].Play();
    }

    public void AdjustVolume()
    {
        audioSources[0].volume = musicSlider.value;
        audioSources[1].volume = ambiSlider.value;
    }
}
