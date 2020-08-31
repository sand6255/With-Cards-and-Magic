using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen instance = null;
    Animator animator;
    public AudioClip clockClipBackwards;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
        transform.localPosition = new Vector2(9999, 9999);
        animator.enabled = false;
    }
    public void PlayDeathScreen()
    {
        animator.enabled = true;
        transform.localPosition = Vector2.zero;
        animator.SetTrigger("BlackFade");
        AudioController.instance.PlayMusic("Lost");
    }
    public void OnPointerEnter()
    {
        animator.SetBool("Reverse",true);
        
    }
    public void OnPointerExit()
    {
        animator.SetBool("Reverse",false);
    }
    IEnumerator coroutine()
    {
        yield return new WaitForSeconds(0.25f);
        transform.localPosition = new Vector2(9999, 9999);
        animator.SetTrigger("BlackFade");
    }
    public void OnPointerClick()
    {
        StartCoroutine(coroutine());
        SceneManager.LoadScene("MainMenuScene");
    }
    public void PlayClockSoundOnBackwards()
    {
        AudioController.instance.PlayEffect(clockClipBackwards);
    }
}
