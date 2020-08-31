using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EventsSpawner : MonoBehaviour
{
    public static EventsSpawner instance = null;
    public GameObject prefabEventEnemy;
    public GameObject prefabEventAltar;
    [SerializeField]
    private GameObject mapLine = null;
    
    private int standardScreenWidth = 1920, standardScreenHeight = 1080;
    private const int maximumEventsInRow = 5;
    [System.Serializable]
    public struct EnemySpawn
    {
        public GameObject enemyObject;
        public Enemy enemy;
    }
    [System.Serializable]
    public struct Stage
    {
        public EnemySpawn[] listOfEnemies;
        [Range(1, 5)]
        public int minimumWidth, maximumWidth;
    };
    [System.Serializable]
    public struct Level
    {
        
        public Stage[] stages;
    }
    

    private List<GameObject>[] connectingLines = new List<GameObject>[maximumEventsInRow];
    
    [SerializeField] 
    private Level[] levels = null;
    public int currentLevel;
    private int[] placementArray = new int[maximumEventsInRow];
    private GameObject[] currentEvents = new GameObject[maximumEventsInRow];
    private int previousWidth; 
    private int[] previousPlacementArray = new int[maximumEventsInRow];
    private GameObject[] previousEvents = new GameObject[maximumEventsInRow];

    private RectTransform eventRectTransform;

    void ShufflePlacementArray()
    {
        for(int i = placementArray.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            int tempVar = placementArray[i];
            placementArray[i] = placementArray[j];
            placementArray[j] = tempVar;
        }
    }

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
    void Start()
    {
        InitializeManager();
    }
    private void InitializeManager()
    {
        eventRectTransform = prefabEventEnemy.GetComponentInChildren<Button>().GetComponent<RectTransform>();
        // read from file / or new, for now always new
        standardScreenWidth = 1920;
        standardScreenHeight = 1080;
       // standardScreenWidth = Screen.width;
       // standardScreenHeight = Screen.height;
        currentLevel = 0;
        previousWidth = 0;
        for (int i = 0; i < maximumEventsInRow; i++)
        {
            placementArray[i] = i;
            previousPlacementArray[i] = i;
            connectingLines[i] = new List<GameObject>();
        }
        LoadLevel(currentLevel);
    }

    public EnemySpawn RandomEnemy(int currentStage)
    {
        float value = Random.Range(0f, 1f);
        float currentValue = 0f;
        EnemySpawn randomEnemy = levels[currentLevel].stages[currentStage].listOfEnemies[0];
        foreach (EnemySpawn enemy in levels[currentLevel].stages[currentStage].listOfEnemies)
        {
            currentValue += 1f/ levels[currentLevel].stages[currentStage].listOfEnemies.Length;
            if(value <= currentValue)
            {
                randomEnemy = enemy;
                break;
            }
        }
        return randomEnemy;
    }
    public void LoadLevel(int level)
    {
        float currentScreenRatio = (float)Screen.width / Screen.height;
        float standardScreenRatio = (float)standardScreenWidth / standardScreenHeight;
        float multiplierScreenRatio = currentScreenRatio / standardScreenRatio;



        if (level >= levels.Length)
            currentLevel = levels.Length - 1;
        else
        currentLevel = level;
        ClearChildren();
        for (int i = 0; i < levels[currentLevel].stages.Length; i++)
        {
            int currentWidth = RandomWidth(levels[currentLevel].stages[i].minimumWidth, levels[currentLevel].stages[i].maximumWidth);
            
            ShufflePlacementArray();
            System.Array.Sort(placementArray, 0, currentWidth);
            for(int j = 0; j < currentWidth; j++)
            {
                GameObject prefabEventCurrent;
                if (levels[currentLevel].stages[i].listOfEnemies.Length == 0 || Random.Range(0, 8) == 0 && i!=0 && i!= levels[currentLevel].stages.Length - 1)
                    prefabEventCurrent = prefabEventAltar;
                else
                    {
                        prefabEventCurrent = prefabEventEnemy;
                        EnemySpawn enemy = RandomEnemy(i);

                        (prefabEventCurrent).GetComponentInChildren<EventEnemy>().SetEventEnemy(enemy.enemyObject,enemy.enemy);
                    }

                // add random for different tiles, currently only enemy tile
                Vector3 newEventPosition = Vector3.zero;
                
                if (i == levels[currentLevel].stages.Length - 1)
                {
                    newEventPosition.x += -standardScreenWidth / 2 + standardScreenWidth / (levels[currentLevel].stages.Length + 1) * (i + 1);
                    newEventPosition.y += -standardScreenHeight / 2 + standardScreenHeight / (maximumEventsInRow + 1) * ((levels[currentLevel].stages[i].minimumWidth + levels[currentLevel].stages[i].maximumWidth) / 2 + 2);
                    newEventPosition.y += Random.Range(-standardScreenHeight / (maximumEventsInRow + 1), standardScreenHeight / (maximumEventsInRow + 1)) / 4; // little movement to look less tilelike

                    //newEventPosition *= currentRatio / standardRatio;
                   // if(prefabEventCurrent.GetComponentInChildren<EventEnemy>())Debug.Log(prefabEventCurrent.GetComponentInChildren<EventEnemy>().thisEventEnemy.name);
                    currentEvents[j] = Instantiate(prefabEventCurrent, newEventPosition, Quaternion.identity, this.transform);
                    currentEvents[j].GetComponentInChildren<RectTransform>().localPosition = newEventPosition;
                }
                else
                {
                    newEventPosition.x += -standardScreenWidth / 2 + standardScreenWidth / (levels[currentLevel].stages.Length + 1) * (i + 1); // set them even across the width
                    newEventPosition.x += Random.Range(-standardScreenWidth / (levels[currentLevel].stages.Length + 1), standardScreenWidth / (levels[currentLevel].stages.Length + 1)) / 4; // little movement to look less tilelike
                    newEventPosition.y += -standardScreenHeight / 2 + standardScreenHeight / (maximumEventsInRow + 1) * (placementArray[j] + 1); // set them even across the height
                    newEventPosition.y += Random.Range(-standardScreenHeight / (maximumEventsInRow + 1), standardScreenHeight / (maximumEventsInRow + 1)) / 4; // little movement to look less tilelike

                    //newEventPosition *= currentRatio / standardRatio;
                    //Debug.Log(newEventPosition);
                    
                    currentEvents[j] = Instantiate(prefabEventCurrent, Vector3.zero, Quaternion.identity, this.transform);
                    currentEvents[j].GetComponentInChildren<RectTransform>().localPosition = newEventPosition;
                }

            }

            if (i != 0)
            {
                for (int j = 0; j < previousWidth; j++)
                {

                    int closestElement = 0;
                    for (int k = 1; k < currentWidth; k++)
                        closestElement = (IsFirstCloser(previousEvents[j].transform.position, currentEvents[closestElement].transform.position, currentEvents[k].transform.position)) ? closestElement : k;

                    connectingLines[closestElement].Add(Instantiate(mapLine, currentEvents[closestElement].transform));
                    connectingLines[closestElement][connectingLines[closestElement].Count - 1].name = "Connecting line";


                    Vector3 currentPoint = currentEvents[closestElement].transform.position;
                    Vector3 previousPoint = previousEvents[j].transform.position;

                    currentPoint.x -= eventRectTransform.rect.width / 2f * multiplierScreenRatio;
                    previousPoint.x += eventRectTransform.rect.width / 2f * multiplierScreenRatio;

                    previousEvents[j].GetComponentInChildren<Event>().GetComponentInChildren<Event>().AddWay(currentEvents[closestElement].GetComponentInChildren<Event>());
                    SetLine(connectingLines[closestElement][connectingLines[closestElement].Count - 1], previousPoint, currentPoint);

                }
                for (int j = 0; j < currentWidth; j++)
                {
                    if (connectingLines[j].Count == 0)
                    {
                        int closestElement = 0;
                        for (int k = 1; k < previousWidth; k++)
                            closestElement = (IsFirstCloser(currentEvents[j].transform.position, previousEvents[closestElement].transform.position, previousEvents[k].transform.position)) ? closestElement : k;

                        connectingLines[j].Add(Instantiate(mapLine, currentEvents[j].transform));
                        connectingLines[j][connectingLines[j].Count - 1].name = "Connecting line";


                        Vector3 currentPoint = currentEvents[j].transform.position;
                        Vector3 previousPoint = previousEvents[closestElement].transform.position;

                        currentPoint.x -= eventRectTransform.rect.width / 2f * multiplierScreenRatio;
                        previousPoint.x += eventRectTransform.rect.width / 2f * multiplierScreenRatio;

                        previousEvents[closestElement].GetComponentInChildren<Event>().AddWay(currentEvents[j].GetComponentInChildren<Event>());
                        SetLine(connectingLines[j][connectingLines[j].Count - 1], previousPoint, currentPoint);
                        
                    }

                }

            }
            else
            {
                AddCurrentEventsToMapMovement(currentWidth);
            }
                SaveCurrentLayer(currentWidth, placementArray, currentEvents);

        }
    }
    void AddCurrentEventsToMapMovement(int currentWidth)
    {
        List<Event> activeEvents = new List<Event>();

        for(int i = 0; i < currentWidth; i++)
        {
           activeEvents.Add(currentEvents[i].GetComponentInChildren<Event>());
        }
        MapMovement.instance.SetNewAvaivableEvents(activeEvents);
    }
    void SetLine(GameObject line, Vector3 from, Vector3 to)
    {
        float currentScreenRatio = (float)Screen.width / Screen.height;
        float standardScreenRatio = (float)standardScreenWidth / standardScreenHeight;
        float multiplierScreenRatio = standardScreenRatio / currentScreenRatio;
        Vector3 centerPos = (from + to) / 2f;
        line.transform.position = centerPos;
        Vector3 direction = to - from;
        direction = Vector3.Normalize(direction);
        line.transform.right = direction;
        
        Vector3 scale = new Vector3(1, 1, 1);
        RectTransform rect = line.GetComponent<RectTransform>();
        rect.sizeDelta =new Vector2(Vector2.Distance(from, to) * multiplierScreenRatio, rect.rect.height);
        
    }
    bool IsFirstCloser(Vector3 from, Vector3 first, Vector3 second)
    {
        if (Mathf.Abs(from.y - first.y) <= standardScreenHeight / (maximumEventsInRow + 1) / 2)
            return true;
        else if (Mathf.Abs(from.y - second.y) <= standardScreenHeight / (maximumEventsInRow + 1) / 2)
            return false;
        else
            return Vector2.Distance(from, first) < Vector2.Distance(from, second);
    }
    void SaveCurrentLayer(int currentWidth, int[] placementArray, GameObject[] currentEvents)
    {
        previousWidth = currentWidth;
        for (int i = 0; i < currentWidth; i++)
        {
            previousPlacementArray[i] = placementArray[i];
            previousEvents[i] = currentEvents[i];
        }
        foreach(List<GameObject> child in connectingLines)
        {
            child.Clear();
        }
    }
    int RandomWidth(int minWidth, int maxWidth)
    {

        int currentWidth = Mathf.RoundToInt(RandomGaussian(minWidth,maxWidth));
        if (currentWidth > maximumEventsInRow) currentWidth = maximumEventsInRow;
        if (currentWidth < 1) currentWidth = 1;
        return currentWidth;
    }
    float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }
    void ClearChildren()
    {     
        int i = 0;
        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];
        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }    
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
        
    }
   
}
