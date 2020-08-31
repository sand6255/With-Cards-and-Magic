using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EndTurn : MonoBehaviour
{
    [SerializeField]
    GameObject outLineOnMouse = null, outLineNothingToDo = null;
    public static EndTurn instance = null;
    [SerializeField]
    AudioClip onPointerEnterClip = null, onPointerClickClip = null;
    bool mouseIn = false;
     private void Awake()
    {
        instance = this;
    }
    public void OnPointerClick()
    {
        if (BattleController.instance.IsPlayerTurn())
        {
            outLineOnMouse.SetActive(false);
            outLineNothingToDo.SetActive(false);
            BattleController.instance.EndTurnPlayer();
            AudioController.instance.PlayEffect(onPointerClickClip);
        }
    }
    public void OnPointerEnter()
    {
        mouseIn = true;
        if (BattleController.instance.IsPlayerTurn()) 
            outLineOnMouse.SetActive(true);
        else 
            outLineOnMouse.SetActive(false);
        AudioController.instance.PlayEffect(onPointerEnterClip);
    }
    public void OnPointerExit()
    {
        mouseIn = false;
         outLineOnMouse.SetActive(false);
    }
    public void Update()
    {
        if (BattleController.instance.IsPlayerTurn() && mouseIn)
            outLineOnMouse.SetActive(true);
        else
            outLineOnMouse.SetActive(false);
    }
    public void NothingToDoOnTurn()
    {
        outLineNothingToDo.SetActive(true);
    }
    public void SomethingToDoOnTurn()
    {
        outLineNothingToDo.SetActive(false);
    }
}
