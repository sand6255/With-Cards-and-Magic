using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatusDisplayDetailsHandler : MonoBehaviour
{

    [SerializeField]
    Image imageHolder = null, borders = null;
    [SerializeField]
    TextMeshProUGUI descriptionHolder = null, nameHolder = null;
    Status currentStatus;
    RectTransform rectTransform;
    const float spacing = 8f;
    private void Awake()
    {
        rectTransform = borders.GetComponent<RectTransform>();
    }   
    public void SetStatusDisplay(Status status, int duration)
    {
        SetVisible(true);
        nameHolder.text = status.name;
        descriptionHolder.text = status.GetStatusText(duration);
        imageHolder.sprite = status.sprite;
    }
    public void DontDisplay()
    {
        SetVisible(false);
    }
    void SetVisible(bool isVisible)
    {
        borders.enabled = isVisible;
        imageHolder.enabled = isVisible;
        descriptionHolder.enabled = isVisible;
        nameHolder.enabled = isVisible;
    }
    public float GetHeight()
    {
        if(rectTransform == null)
            rectTransform = borders.GetComponent<RectTransform>();
        return rectTransform.rect.height + spacing;
    }
}
