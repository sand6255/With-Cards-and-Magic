using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Add Temp Mana ", menuName = "Card Effect/AddTempMana")]
public class AddTempMana : Effect
{
    [SerializeField]
    int manaToAdd = 1;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        BattleController.instance.IncreaseCurrentMana(manaToAdd);
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
