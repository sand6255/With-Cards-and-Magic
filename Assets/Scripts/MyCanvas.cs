using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCanvas : MonoBehaviour
{
    public bool isAvailable = true;
    Vector3 newPosition = Vector3.zero;
    float timeToMove;
    bool needToMove = false;
    public void MoveCanvas(Vector2 newPoint, float timeToMove)
    {
        newPosition = newPoint;
        this.timeToMove = timeToMove;
        needToMove = true;
    }
    private void Update()
    {
        if(needToMove)
        {
            if (timeToMove > Time.deltaTime)
            {
                transform.localPosition += (newPosition - transform.localPosition) / timeToMove * Time.deltaTime;

                timeToMove -= Time.deltaTime;
            }
            else
            {
                transform.localPosition = newPosition;
                needToMove = false;
            }
        }
    }
}
