using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehavirial ", menuName = "Enemy")][System.Serializable]
public class Enemy : ScriptableObject
{
    [SerializeField]
    Behaviour behaviour = null;
    [SerializeField]
    EnemyStages enemyStages = null;
    
    public int minMaximumHealth,maxMaximumHealth;
    public void ResetBehaviour()
    {
        behaviour.ResetBehaviour();
    }
    public MonsterAction GetMove(float healthPercentage)
    {
        return behaviour.GetMonsterAction(enemyStages.GetMonsterActionList(healthPercentage));
    }
    
}
