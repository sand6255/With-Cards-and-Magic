using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplayDetailsFromIcon : MonoBehaviour
{
    public static StatusDisplayDetailsFromIcon instance = null;
    [SerializeField]
    StatusDisplayDetailsHandler statusDisplayDetails;
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


    }
    private void Start()
    {
        statusDisplayDetails = GetComponentInChildren<StatusDisplayDetailsHandler>();
    }
    public void StartDisplay(Status statusToDisplay, int number)
    {
        statusDisplayDetails.SetStatusDisplay(statusToDisplay, number);
    }
    public void StopDisplay()
    {
        statusDisplayDetails.DontDisplay();
    }
}
