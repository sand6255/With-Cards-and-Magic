using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStage ", menuName = "Enemy Stages")][System.Serializable]
public class EnemyStages : ScriptableObject
{
    [System.Serializable]
    struct enemyStage
    {
        public List<MonsterAction> monsterActions;
        public float hpPerantageToEnterStage;
    }
    [SerializeField]
    List<enemyStage> enemyStages = new List<enemyStage>();
    public List<MonsterAction>GetMonsterActionList(float healthPercantage)
    {
        foreach (enemyStage stage in enemyStages)
        {
            if (healthPercantage+Mathf.Epsilon >= stage.hpPerantageToEnterStage)
                return stage.monsterActions;
        }
        return new List<MonsterAction>();
    }
}
