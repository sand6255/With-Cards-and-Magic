using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlyAnimation : MonoBehaviour
{
    [SerializeField]
    Vector2 startingPoint = Vector2.zero, peakPoint = Vector2.zero;
    Vector2 endPoint = new Vector2(-1110f, 279.2f);
    [SerializeField]
    float zoomOnPeak=1.5f, timeToMoveToPeak,timeToMoveFromPeak, pauseTime;
    [SerializeField]
    CardSlot flyingCard = null;
    bool wasOnPeak = false;
    RectTransform rectTransform;
    CardSlot futureCardSlot;
    float scale = 1, currentZoom = 0.8f;
    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = startingPoint;
        scale = rectTransform.localScale.x;
    }
    public void SetEndPointAndCardSlot(Card card,CardSlot cardSlot)
    {

        endPoint = cardSlot.GetComponent<RectTransform>().localPosition;
        futureCardSlot = cardSlot;
        flyingCard.SetCard(card);
        cardSlot.SetCardFly(this);
    }
    public void DestroyThisCardFly()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        
        //Debug.Log(Time.frameCount + " Was on peak = " + wasOnPeak + " rect transform = " + rectTransform.localScale.ToString("F3") + " zoom = " + currentZoom.ToString("F3") + "timeToMoveToPeak = " + timeToMoveToPeak.ToString("F3") + " Time.DeltaTime = " + Time.deltaTime.ToString("F3") + " before frame") ;
        if (!wasOnPeak)
        {
            if (timeToMoveToPeak <= Time.deltaTime + Mathf.Epsilon)
            {
                currentZoom = zoomOnPeak;
                rectTransform.localScale = new Vector3(scale * currentZoom, scale * currentZoom, 1);
                rectTransform.localPosition = peakPoint;
                wasOnPeak = true;
                timeToMoveToPeak -= Time.deltaTime;
                pauseTime -= timeToMoveToPeak;
           
            }

            else
            {
                
                currentZoom += ((zoomOnPeak - currentZoom) * Time.deltaTime / timeToMoveToPeak);
                rectTransform.localScale = new Vector3(scale * currentZoom, scale * currentZoom, 1);
                Vector3 movementVector = (peakPoint - (Vector2)rectTransform.localPosition) * Time.deltaTime / timeToMoveToPeak;
                movementVector.z = 0;
                timeToMoveToPeak -= Time.deltaTime;
                rectTransform.localPosition += movementVector;
            }
        }
        else if(pauseTime >=0f)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime < 0f)
                timeToMoveFromPeak -= pauseTime;
        }
        else
        {
            //Debug.Log("Enter this if");
            if (timeToMoveFromPeak <= Time.deltaTime + Mathf.Epsilon)
            {
                currentZoom = 1f;
                rectTransform.localScale = new Vector3(scale * currentZoom, scale * currentZoom, 1);
                rectTransform.localPosition = endPoint;
                if(futureCardSlot != null)
                    futureCardSlot.SetDisplay(true);
                Destroy(gameObject);
            }
            else
            {
                currentZoom += (1f - currentZoom) * Time.deltaTime / timeToMoveFromPeak;
                rectTransform.localScale = new Vector3(scale * currentZoom, scale * currentZoom, 1);
                Vector3 movementVector = (endPoint - (Vector2)rectTransform.localPosition) * Time.deltaTime / timeToMoveFromPeak;
                movementVector.z = 0;
                rectTransform.localPosition += movementVector;
                timeToMoveFromPeak -= Time.deltaTime;
            
            }
        }
        //Debug.Log(Time.frameCount + " Was on peak = " + wasOnPeak + " rect transform = " + rectTransform.localScale.ToString("F3") + " zoom = " + currentZoom.ToString("F3") + "timeToMoveToPeak = " + timeToMoveToPeak.ToString("F3") + " Time.DeltaTime = " + Time.deltaTime.ToString("F3") + " after frame");

    }
}
