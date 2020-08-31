using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusDisplayDetailsSpawner : MonoBehaviour
{
    [SerializeField]
    Vector3 initialSpawnPoint = Vector3.zero;
    [SerializeField]
    GameObject statusDisplayDetailsObject = null;
    List<GameObject> allStatusDisplayDetails = new List<GameObject>();
    float statusDisplaySpriteHeight;
    
    public void AddStatus(Status newStatus, int duration)
    {
        statusDisplaySpriteHeight = statusDisplayDetailsObject.GetComponent<StatusDisplayDetailsHandler>().GetHeight();

        GameObject newStatusDisplay = Instantiate(statusDisplayDetailsObject, this.transform, false);
        newStatusDisplay.transform.localPosition = initialSpawnPoint - new Vector3(0,allStatusDisplayDetails.Count * statusDisplaySpriteHeight);
        newStatusDisplay.GetComponent<StatusDisplayDetailsHandler>().SetStatusDisplay(newStatus, duration);
        allStatusDisplayDetails.Add(newStatusDisplay);
        
    }
    public void StopDisplaying()
    {
        foreach (GameObject status in allStatusDisplayDetails)
            Destroy(status);
        allStatusDisplayDetails.Clear();
    }
}
