using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Change Attack Coef ", menuName = "Card Effect/ChangeAttackCoef")]
public class ChangeAttackCoef : Effect
{
    [SerializeField]
    float percentage = 0.5f;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        caster.attackPowerCoef *= percentage;
    }

    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}
