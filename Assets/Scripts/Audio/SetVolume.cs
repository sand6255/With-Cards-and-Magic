using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetVolume : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer = null;
    [SerializeField]
    string mixerValueName = null;
    Slider slider = null;
    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(mixerValueName);
    }
    public void SetMixerVolume()
    {
        mixer.SetFloat(mixerValueName, Mathf.Log10(slider.value) * 20f);
        PlayerPrefs.SetFloat(mixerValueName, slider.value);
    }
}
