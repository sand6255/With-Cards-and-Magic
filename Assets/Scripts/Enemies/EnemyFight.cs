using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class EnemyFight : MonoBehaviour
{
    static public EnemyFight instance;
    Enemy enemy;
    GameObject enemyObject;
    TextMeshProUGUI enemyHealthText;
    public HealthAndStatuses enemyHealthAndStatus;
    MonsterAction nextMonsterAction = null;
    public float statsMultiplier = 1f;
    [HideInInspector]
    public float attackPowerLevel, armorPowerLevel;
    [SerializeField]
    Vector2 vectorToMove = new Vector2(-100,-100);
    [SerializeField]
    int minShard = 30,  maxShards = 50;
    CustomAnimation customAnimation;
    [SerializeField]
    StatusDisplayIconSpawner statusDisplayer = null;
    [SerializeField]
    AudioClip movementClip = null, attackClip = null, healClip = null, statusClip = null, takeDamageClip = null;
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
    void InitializeManager()
    {
    }
    public void TakeTurn()
    {
        AudioController.instance.PlayEffect(movementClip);
        customAnimation.PlayMoveAnimation();
    }
    public void PlayAction()
    {
        nextMonsterAction.PlayAction();
        if(nextMonsterAction.isAttack)
           AudioController.instance.PlayEffect(attackClip);
        if (nextMonsterAction.isHeal)
            AudioController.instance.PlayEffect(healClip);
        if (nextMonsterAction.isStatus)
            AudioController.instance.PlayEffect(statusClip);

    }
    public void EndTurn()
    {
        BattleController.instance.EndTurnEnemy();
    }

    public void SetNextMove()
    {
        nextMonsterAction = enemy.GetMove(enemyHealthAndStatus.currentHealth / enemyHealthAndStatus.maximumHealth);
        NextMoveDisplay.instance.SetNextMove(nextMonsterAction);
    }
    public void GenerateEnemyFight(GameObject _enemyObject, Enemy _enemy, float roomMultiplier)
    {
        statsMultiplier = roomMultiplier;
        enemy = _enemy;
        enemy.ResetBehaviour();
        enemyObject = _enemyObject;
        customAnimation = enemyObject.GetComponentInChildren<CustomAnimation>();

        enemyHealthAndStatus = new HealthAndStatuses();
        enemyHealthAndStatus.SetStatusDisplayer(statusDisplayer);

        enemyHealthText = enemyObject.GetComponentInChildren<TextMeshProUGUI>();
        enemyHealthAndStatus.SetComponent(Mathf.RoundToInt(Random.Range(enemy.minMaximumHealth, enemy.maxMaximumHealth + 1) * statsMultiplier), enemyHealthText, statsMultiplier);
    }
    public void TookDamage()
    {
        AudioController.instance.PlayEffect(takeDamageClip);
        customAnimation.PlayTakeDamageAnimation();
    }
    public void Die()
    {
        BattleController.instance.EnemyDead();
        PlayerInformation.instance.AddWindShards(Mathf.RoundToInt(Random.Range(minShard, maxShards + 1) * statsMultiplier));
        DestroyEnemy();
    }
    public void DestroyEnemy()
    {
        if(enemyObject != null) 
            Destroy(enemyObject);
    }
    
}
