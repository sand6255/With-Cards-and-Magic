using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAction ", menuName = "Enemy Action")][System.Serializable]
public class MonsterAction : ScriptableObject
{
    [SerializeField]
    List<Effect> actionEffects = null;
    public bool isAttack, isStatus, isHeal;
    public void PlayAction()
    {
        foreach (Effect effect in actionEffects)
        {
            effect.OnEffectPlay(EnemyFight.instance.enemyHealthAndStatus, PlayerInformation.instance.playerHealthAndEffects);
        }
    }
    public string GetNumberAttack()
    {
        int numberAttack = 0;
        foreach (Effect effect in actionEffects)
        {
            IDamage castEffect = effect as IDamage;

            if(castEffect != null) 
            {
                numberAttack += castEffect.GetDamage(EnemyFight.instance.enemyHealthAndStatus);
            }
        }
        if (numberAttack == 0)
            return "";
        else return numberAttack.ToString();
    }
    
}
