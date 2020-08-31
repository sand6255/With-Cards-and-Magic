using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    [SerializeField]
    AudioSource musicSource = null, effectSourcePrefab = null;
    [System.Serializable]
    struct NameAndClip
    {
        public string name;
        public AudioClip clip;
    }
    [SerializeField]
    List<NameAndClip> musicList = null;
    Dictionary<string, AudioClip> musicDictionary = new Dictionary<string, AudioClip>();
    Coroutine fadeOutCoroutine = null, fadeInCoroutine = null;
    AudioClip fadeInClip = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(instance);

        foreach (NameAndClip nameAndClip in musicList)
        {
            musicDictionary.Add(nameAndClip.name, nameAndClip.clip);
        }
    }
    
    public void PlayMusic(string musicName)
    {
        if (musicDictionary.ContainsKey(musicName))
        {
            if (musicSource.clip == musicDictionary[musicName])
            { 
                fadeInClip = musicDictionary[musicName];
                if(fadeOutCoroutine != null) StopCoroutine(fadeOutCoroutine);
                if(fadeInCoroutine != null) StopCoroutine(fadeInCoroutine);
                fadeOutCoroutine = StartCoroutine(FadeIn());
            }
            else if(fadeInClip != musicDictionary[musicName])
            {
                //musicSource.clip = musicDictionary[musicName];
                fadeInClip = musicDictionary[musicName];
                if(fadeOutCoroutine != null) StopCoroutine(fadeOutCoroutine);
                if(fadeInCoroutine != null) StopCoroutine(fadeInCoroutine);
                fadeOutCoroutine = StartCoroutine(FadeOut());
            }
        }
        else
            Debug.LogWarning("Music " + musicName + " not found.");
    }
    public void PlayEffect(AudioClip effectSoundClip)
    {
        if (effectSoundClip != null)
        {
            AudioSource newEffectSource = Instantiate(effectSourcePrefab, transform);
            newEffectSource.clip = effectSoundClip;
        }
        else
            Debug.LogWarning("Sound effect == null");
    }

    private IEnumerator FadeOut()
    {
        float speed = 0.1f;
        while (musicSource.isPlaying && musicSource.volume > 0.3f)
        {
            musicSource.volume -= speed;
            yield return new WaitForSeconds(0.1f);
        }
        musicSource.volume = 0.3f;
        fadeOutCoroutine = null;
        fadeInCoroutine = StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float speed = 0.1f;
        if (musicSource.clip != fadeInClip)
        {
            musicSource.clip = fadeInClip;
            musicSource.Play();
        }
        while (musicSource.volume < 1)
        {
            musicSource.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
        musicSource.volume = 1;
        fadeInCoroutine = null;
    }

}
