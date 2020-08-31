using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InitializeScene : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer = null;
    [SerializeField]
    string SceneMusicName = null;
    [SerializeField]
    string[] mixerVariableNames = new string[] { "MasterVolume", "EffectsVolume", "MusicVolume" };
    void Start()
    {
        AudioController.instance.PlayMusic(SceneMusicName);
        foreach(string mixerVariableName in mixerVariableNames)
        {
            if (!PlayerPrefs.HasKey(mixerVariableName))
                PlayerPrefs.SetFloat(mixerVariableName, 0.5f);

            mixer.SetFloat(mixerVariableName, Mathf.Log10(PlayerPrefs.GetFloat(mixerVariableName)) * 20f);
        }
        PlayerPrefs.Save();
    }
}

