using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplyStatus ", menuName = "Card Effect/ApplyStatus")]
public class ApplyStatus : Effect, IGotStatus
{
    [System.Serializable]
    enum Mods
    {
        NoMod,
        AttackMod,
        ArmorMod
    }
    [SerializeField]
    int duration = 2;
    [SerializeField]
    public Status status = null;
    [SerializeField]
    bool applyOnCaster = false;
    [SerializeField]
    Mods mod = Mods.NoMod;
    override public void OnEffectPlay(HealthAndStatuses caster, HealthAndStatuses casterOpponent, int power = 0)
    {
        if (applyOnCaster)
            caster.TakeStatus(status, GetDurationWithMod(caster).Key);
        else
            casterOpponent.TakeStatus(status, GetDurationWithMod(caster).Key);
    }
    override public KeyValuePair<string,int> GetNumberByString(string numberName, HealthAndStatuses caster)
    {
        switch (numberName)
        {
            case "duration":
                KeyValuePair<int, int> tmpPair = GetDurationWithMod(caster);
                return new KeyValuePair<string,int>(tmpPair.Key.ToString(), tmpPair.Value);
            case "status": return new KeyValuePair<string,int>(status.name.ToString(),2);
            default: return new KeyValuePair<string,int>(duration.ToString(),-2);
        }
    }
    KeyValuePair<int, int> GetDurationWithMod(HealthAndStatuses caster)
    {
        int code, newDuration;
        switch(mod)
        {
            case Mods.NoMod:
                return new KeyValuePair<int, int>(duration,0);
            case Mods.ArmorMod:
                code = 0; 
                newDuration = Mathf.RoundToInt((duration + caster.armorPower) * caster.armorPowerCoef);
                if (newDuration > duration)
                    code = 1;
                else if (newDuration < duration)
                    code = -1;
                return new KeyValuePair<int, int>(newDuration, code);
            case Mods.AttackMod:
                code = 0;
                newDuration = Mathf.RoundToInt((duration + caster.attackPower) * caster.attackPowerCoef);
                return new KeyValuePair<int, int>(newDuration, code);
            default:
                return new KeyValuePair<int, int>(duration, 0);
        }
    }
    public int GetDuration(HealthAndStatuses caster = null)
    {
        if (caster != null)
            return GetDurationWithMod(caster).Key;
        else 
            return duration;
    }
    public Status GetStatus()
    {
        return status;
    }

}
