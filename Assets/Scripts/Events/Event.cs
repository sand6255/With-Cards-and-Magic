using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public abstract class Event : MonoBehaviour
{
    List<Event> nextEvents = new List<Event>();
    bool isAvailable = false;
    bool isPointed = false;
    float scaleStart = 0f;
    Image thisImage;
    Outline thisOutline;
    MyCanvas canvas;
    [SerializeField]
    AudioClip OnPointerEnter = null, OnPointerClick = null;

    private void Start()
    {
        thisImage = GetComponent<Image>();
        thisOutline = GetComponent<Outline>();
        thisOutline.enabled = false;
        canvas = GetComponentInParent<MyCanvas>();
    }
    public void AddWay(Event nextEvent)
    {
        nextEvents.Add(nextEvent);
    }
    public List<Event> GetWays()
    {
        return nextEvents;
    }
    public void Clicked()
    {
        if (isAvailable && canvas.isAvailable)
        {
            AudioController.instance.PlayEffect(OnPointerClick);
            MapMovement.instance.EventClicked(this);
            UnPointedMouse();
        }
    }
    public void PointedMouse()
    {
        if (isAvailable && canvas.isAvailable)
        {
            AudioController.instance.PlayEffect(OnPointerEnter);
            isPointed = true;
            thisOutline.enabled = true;
        }
    }
    public void UnPointedMouse()
    {
        isPointed = false;
        if(thisOutline)
            thisOutline.enabled = false;   
    }
    private void Update()
    {
        if (isAvailable)
            thisImage.rectTransform.localScale = Vector3.one * (1 + Mathf.Abs(Mathf.Sin(Time.time - scaleStart) /4));
    }
    public void SetAvailable(bool isAvailable)
    {
        this.isAvailable = isAvailable;
        if (isAvailable == true)
            scaleStart = Random.Range(0, Mathf.PI);
        else
            transform.localScale = Vector3.one;
    }
    public abstract void OnRoomEnter(float roomPowerMultiplier);
}
