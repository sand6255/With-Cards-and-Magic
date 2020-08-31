using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class MenuButton : MonoBehaviour
{
    [SerializeField]
    AudioClip buttonOnEnterClip = null, buttonOnClickSound = null;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerEnter()
    {
        animator.SetBool("PointerOnObject", true);
        AudioController.instance.PlayEffect(buttonOnEnterClip);
    }
    public void OnPointerExit()
    {
        animator.SetBool("PointerOnObject", false);
    }
    public void OnPointerClick()
    {
        AudioController.instance.PlayEffect(buttonOnClickSound);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
    public void DisplaySettings(GameObject gameobject)
    {

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
