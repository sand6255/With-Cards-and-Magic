using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplayIconSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject statusIconObject = null;
    HealthAndStatuses healthAndStatuses;
    List<GameObject> statusIconObjects = new List<GameObject>();
    Vector2 statusObjectRect,thisRect;
    private void Start()
    {
        RectTransform tmpRectTransform;
        tmpRectTransform = statusIconObject.GetComponent<RectTransform>();
        statusObjectRect = new Vector2(tmpRectTransform.rect.width, tmpRectTransform.rect.height);
        tmpRectTransform =  GetComponent<RectTransform>();
        thisRect = new Vector2(tmpRectTransform.rect.width, tmpRectTransform.rect.height);
    }
    public void DisplayStatusList(List<HealthAndStatuses.StatusAndDuration> statuses)
    {
        ClearStatusList();
        Vector2 newPosition = Vector2.zero;
        foreach(HealthAndStatuses.StatusAndDuration statusAndDuration in statuses)
        {

            if (newPosition.x + statusObjectRect.x >= thisRect.x)
            {
                newPosition.x = 0;
                newPosition.y -= statusObjectRect.y;
            }
            GameObject newStatus = Instantiate(statusIconObject, transform);
            newStatus.GetComponent<RectTransform>().anchoredPosition = newPosition;
            newStatus.GetComponent<StatusIconEvent>().SetStatus(statusAndDuration.status, statusAndDuration.duration);
            statusIconObjects.Add(newStatus);
            newPosition.x += statusObjectRect.x;

        }
    }
    public void ClearStatusList()
    {
        foreach (GameObject status in statusIconObjects)
            Destroy(status);
        statusIconObjects.Clear();
    }
}
