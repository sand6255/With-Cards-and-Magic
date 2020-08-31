using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Add Max Mana ", menuName = "Card Effect/AddMaxMana")]
public class AddMaxMana : Effect
{
    [SerializeField]
    int manaToAdd = 1;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        BattleController.instance.IncreaseMaximumMana(manaToAdd);
    }
    override public KeyValuePair<string, int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "manaToAdd": return new KeyValuePair<string, int>(manaToAdd.ToString(), 0);
            default: return new KeyValuePair<string, int>(0.ToString(), -2);
        }
    }
}
