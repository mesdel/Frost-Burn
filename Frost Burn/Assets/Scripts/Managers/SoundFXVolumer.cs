using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXVolumer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadVolume());
    }

    private IEnumerator LoadVolume()
    {
        yield return StartCoroutine(DataSaver.WaitForData());
        AudioSource[] sfx = GetComponents<AudioSource>();
        foreach (AudioSource soundEffect in sfx)
        {
            soundEffect.volume = DataSaver.instance.sfxVolume;
        }
    }
}
