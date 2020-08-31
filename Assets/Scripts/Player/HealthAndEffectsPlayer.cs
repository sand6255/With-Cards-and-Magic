using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class HealthAndStatusesPlayer : HealthAndStatuses
{
    // Start is called before the first frame update
    public void PlayerEndOrStartOfBattleReset()
    {
        SetArmor(0);
        ClearAllStatusesWithNoEndUpdate();
        attackPowerCoef = 1f;
        armorPowerCoef = 1f;
        armorPower = 0;
        attackPower = 0;
}
    protected override void Die()
    {
        isDead = true;
        DeathScreen.instance.PlayDeathScreen();
    }
}
