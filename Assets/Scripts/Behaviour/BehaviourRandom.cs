using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBehavirial ", menuName = "Enemy Behaviour/RandomBehaviour")][System.Serializable]
public class BehaviourRandom : Behaviour
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
            int currentActionNum = Random.Range(0,monsterActions.Count);
            while(previousActionNum == currentActionNum)
                currentActionNum = Random.Range(0, monsterActions.Count);
            previousActionNum = currentActionNum;
            return monsterActions[currentActionNum];
        }
    }
}
