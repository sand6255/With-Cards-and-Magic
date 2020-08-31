using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatusIconEvent : MonoBehaviour
{
    [SerializeField]
    Image statusIconImage = null;
    [SerializeField]
    TextMeshProUGUI statusIconText = null;
    Status status = null;
    int number = 0;
    public void SetStatus(Status status, int duration)
    {
        this.status = status;
        number = duration;
        statusIconText.text = number.ToString();
        statusIconImage.sprite = this.status.sprite;
    }
    public void OnPointerEnter()
    {
        if(status)
            StatusDisplayDetailsFromIcon.instance.StartDisplay(status, number);
    }
    public void OnPointerExit()
    {
        StatusDisplayDetailsFromIcon.instance.StopDisplay();
    }
}
