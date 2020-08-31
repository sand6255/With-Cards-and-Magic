using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehavirial ", menuName = "Enemy Behaviour/ConsecutiveBehaviour")] [System.Serializable]
public class BehaviourConsecutive : Behaviour
{
    int previousActionNum = -1;
    override public void ResetBehaviour()
    {
        previousActionNum = -1;
    }
    override public MonsterAction GetMonsterAction(List<MonsterAction> monsterActions)
    {
        if (monsterActions.Count == 0)
            return null;
        else if (monsterActions.Count == 1)
            return monsterActions[0];
        else
        {
            int currentActionNum = (previousActionNum + 1) % monsterActions.Count;
            previousActionNum = currentActionNum;
            return monsterActions[currentActionNum];
        }
    }
}
