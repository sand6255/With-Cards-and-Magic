using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnemy : Event
{
    public GameObject thisEventEnemy = null;
    public Enemy thisEventEnemyBeh = null;
    override public void OnRoomEnter(float roomMultiplier)
    {
         
        BattleController.instance.StartBattle(thisEventEnemy, thisEventEnemyBeh, roomMultiplier);
    }
    public void SetEventEnemy(GameObject enemy, Enemy enemyBeh)
    {

        thisEventEnemy = enemy;
        thisEventEnemyBeh = enemyBeh;
    }
}
