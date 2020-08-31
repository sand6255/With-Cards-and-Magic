using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;
    [SerializeField]
    Canvas battleCanvas = null, mapCanvas = null, rewardCanvas = null;
    [HideInInspector]
    public MyCanvas battleMyCanvas, mapMyCanvas, rewardMyCanvas;
    private Vector2 OutOfScrene = new Vector2( 1920 , 0);
    private float transitionTime = 0.6f;
    [SerializeField]
    AudioClip winSoundEffect = null, onMapEnter = null;
    public enum SceneState
    {
        Map,
        Battle,
        Reward
    };
    SceneState currentSceneState ;
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
        battleMyCanvas = battleCanvas.GetComponentInChildren<MyCanvas>();
        mapMyCanvas = mapCanvas.GetComponentInChildren<MyCanvas>();
        rewardMyCanvas = rewardCanvas.GetComponentInChildren<MyCanvas>();

        battleMyCanvas.MoveCanvas(Vector2.zero, 0f);
        mapMyCanvas.MoveCanvas(Vector2.zero, 0f);
        rewardMyCanvas.MoveCanvas(-OutOfScrene, 0);
        currentSceneState = SceneState.Map;
        Activate(SceneState.Map);
    }
   
    public void ChangeScene(SceneState newState)
    {
        if (newState == currentSceneState)
            return;
        switch(currentSceneState)
        {
            case SceneState.Map:
                mapMyCanvas.MoveCanvas(OutOfScrene , transitionTime );
                break;
            case SceneState.Battle:
                EnemyFight.instance.DestroyEnemy();
                AudioController.instance.PlayEffect(winSoundEffect);
                break;
            case SceneState.Reward:
                rewardMyCanvas.MoveCanvas(-OutOfScrene, transitionTime );
                break;
        }
        switch (newState)
        {
            case SceneState.Map:
                if (EventsSpawner.instance.currentLevel >= 2)
                    SceneManager.LoadScene("VictoryScene");
                    
                mapMyCanvas.MoveCanvas(Vector2.zero, transitionTime);
                AudioController.instance.PlayMusic("Map");
                AudioController.instance.PlayEffect(onMapEnter);
                break;  
            case SceneState.Battle:
                EnemyFight.instance.DestroyEnemy();
                AudioController.instance.PlayMusic("Battle");
                break;
            case SceneState.Reward:
                rewardMyCanvas.MoveCanvas(Vector2.zero, transitionTime);
                AudioController.instance.PlayMusic("Reward");
                break;
        }
        Activate(newState);
        currentSceneState = newState;
        //canvasArray[newStateNum].enabled;
    }
    private void Activate(SceneState state)
    {
        battleMyCanvas.isAvailable = false;
        mapMyCanvas.isAvailable = false;
        rewardMyCanvas.isAvailable = false;
        switch (state)
        {
            case SceneState.Battle:
                battleMyCanvas.isAvailable = true;
                break;
            case SceneState.Map:
                mapMyCanvas.isAvailable = true;
                break;
            case SceneState.Reward:
                rewardMyCanvas.isAvailable = true;
                break;
        }
    }
    public void RestartGame()
    {
        EventsSpawner.instance.LoadLevel(0);
        MapMovement.instance.InitializeManager();
        EnemyFight.instance.DestroyEnemy();
        InitializeManager();
        PlayerInformation.instance.InitializeManager();
    }
    
}
