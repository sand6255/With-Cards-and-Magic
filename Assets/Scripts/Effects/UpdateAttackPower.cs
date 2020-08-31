using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Update Attack Power", menuName = "Card Effect/UpdateAttackPower")]
public class UpdateAttackPower : Effect
{
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        caster.attackPower = power;
    }

    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}
