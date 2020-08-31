using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Behaviour : ScriptableObject
{
    abstract public MonsterAction GetMonsterAction(List<MonsterAction> monsterActions);
    abstract public void ResetBehaviour();
   

}   
