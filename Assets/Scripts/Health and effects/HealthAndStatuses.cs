using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class HealthAndStatuses
{
    [System.Serializable]
    public struct StatusAndDuration
    {
        public int duration;
        public Status status;
        public StatusAndDuration(Status _status, int _duration)
        {
            status = _status;
            duration = _duration;
        }
    }

    List<StatusAndDuration> statuses = new List<StatusAndDuration>();
    [HideInInspector]
    public int currentHealth, maximumHealth;
    [HideInInspector]
    public int armor = 0;
    //[HideInInspector]
    public float attackPowerCoef = 1f, armorPowerCoef = 1f;
    [HideInInspector]
    public float startingCoef = 1f;
    protected TextMeshProUGUI healthText;
    protected bool isDead = false;
    //[HideInInspector]
    public int armorPower = 0, attackPower = 0;
    protected StatusDisplayIconSpawner statusDisplayer;
    public void SetStatusDisplayer(StatusDisplayIconSpawner newStatusDisplayer)
    {
        statusDisplayer = newStatusDisplayer;
    }

    public HealthAndStatuses GetOpponent(HealthAndStatuses opponentOf)
    {
        HealthAndStatusesPlayer castHealthAndStatuses = opponentOf as HealthAndStatusesPlayer;
        if (castHealthAndStatuses != null)   
            return EnemyFight.instance.enemyHealthAndStatus;
        else return PlayerInformation.instance.playerHealthAndEffects;
    }
    public void MultiplyStatus(Status status, int multiplier)
    {
        foreach(StatusAndDuration statusAndDuration in statuses)
        {
            if (statusAndDuration.status == status)
            {
                TakeStatus(status, statusAndDuration.duration);
                break;
            }
        }
    }
    public void IncreaseMaximumHealth(int onHealh)
    {
        maximumHealth += onHealh;
        currentHealth += onHealh;
        UpdateText();
    }
    public virtual void ChangeHealthToMax(){
        currentHealth = maximumHealth;
        UpdateText();
    }
    public virtual void ChangeHealth(int health)
    {
        currentHealth = health;
        UpdateText();
    }
    public void SetComponent(int maxHealth, TextMeshProUGUI _healthText, float _startingCoef = 1f)
    {
        isDead = false;
        healthText = _healthText;
        maximumHealth = maxHealth;
        ChangeHealthToMax();
        attackPowerCoef = _startingCoef;
        armorPowerCoef = _startingCoef;
        armor = 0;
    }
    private void UpdateText()
    {
        if(armor != 0)
            healthText.text = currentHealth + " / " + maximumHealth + " + " + armor;
        else
            healthText.text = currentHealth + " / " + maximumHealth;
    }
    public void TakeDamage(int damage, HealthAndStatuses damager)
    {
        if (isDead) return;
        damage = Mathf.RoundToInt((damage + damager.attackPower) * damager.attackPowerCoef);
        if (damage < 0)
            damage = 0;
        damage = TakeArmor(damage);
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateText();
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            TakeDamageStatusUpdate();
            if (this as HealthAndStatusesPlayer == null)
                EnemyFight.instance.TookDamage();
            else
                PlayerInformation.instance.TookDamage();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        damage = Mathf.RoundToInt(damage);
        damage = TakeArmor(damage);
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateText();
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            TakeDamageStatusUpdate();
            if (this as HealthAndStatusesPlayer == null)
                EnemyFight.instance.TookDamage();
            else
                PlayerInformation.instance.TookDamage();
        }
    }

    public void GiveArmor(int armor, HealthAndStatuses caster)
    {
        armor = Mathf.RoundToInt((armor + caster.armorPower) * caster.armorPowerCoef);
        if (armor < 0)
            armor = 0;
        this.armor += armor;
        UpdateText();
    }
    public void GiveArmor(int armor)
    {
        if (armor < 0)
            armor = 0;
        this.armor += armor;
        UpdateText();
    }
    public void SetArmor(int armor)
    {
        this.armor = armor;
        UpdateText();
    }
    int TakeArmor(int damage)
    {
        int exDmg = damage - armor;
        if (exDmg < 0)
            exDmg = 0;
        armor = Mathf.Max(armor - damage, 0);
        UpdateText();
        return exDmg;
    }
    
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        ClearAllStatusesWithNoEndUpdate();
        EnemyFight.instance.Die();
    }
    
    public void TakeStatus(Status status, int duration)
    {
        if (isDead) return;
        bool applyedStatus = false;
        for (int i = 0; i < statuses.Count; i++)
        {
            if(status == statuses[i].status)
            {
                StatusAndDuration tempStatus = statuses[i];
                tempStatus.duration += duration;
                if (tempStatus.duration == 0 || tempStatus.duration < 0 && !tempStatus.status.canBeNegative)
                {
                    statuses[i].status.OnStatusEnd(this, GetOpponent(this), statuses[i].duration);
                    if (isDead) return;
                    statuses[i].status.OnDurationChangeEvent(this, GetOpponent(this), tempStatus.duration);
                    statuses.Remove(statuses[i]);
                    i--;
                }
                else
                {
                    statuses[i] = tempStatus;
                    statuses[i].status.OnDurationChangeEvent(this, GetOpponent(this), tempStatus.duration);
                }
                applyedStatus = true;
            }
        }
        if (!applyedStatus)
        {
            statuses.Add(new StatusAndDuration(status, duration));
            status.OnStatusStart(this, GetOpponent(this), duration);
            status.OnDurationChangeEvent(this, GetOpponent(this), duration);
        }
        statusDisplayer.DisplayStatusList(statuses);
    }
    public void TakeDamageStatusUpdate()
    {
        if (isDead) return;
        for (int i = 0; i < statuses.Count; i++)
        {
            statuses[i].status.OnTakeDamage(this, GetOpponent(this), statuses[i].duration);
            if (isDead || i >= statuses.Count) return;
            if (statuses[i].status.TickOnTakeDamage)  
            {
                StatusAndDuration tempStatus = statuses[i];
                if (statuses[i].status.oneTurnOnly)
                    tempStatus.duration = 0;
                else
                    tempStatus.duration -= 1;
                if (tempStatus.duration == 0 || tempStatus.duration < 0 && !tempStatus.status.canBeNegative)
                {
                    statuses[i].status.OnStatusEnd(this, GetOpponent(this), statuses[i].duration);
                    if (isDead) return;
                    statuses.Remove(statuses[i]);
                    i--;
                }
                else
                    statuses[i] = tempStatus;
            }
        }
        statusDisplayer.DisplayStatusList(statuses);
    }
    public void EndOfTurnStatusUpdate()
    {
        if (isDead) return;
        for (int i = 0; i < statuses.Count; i++)
        {
            statuses[i].status.OnEndOfTurnEvent(this, GetOpponent(this), statuses[i].duration);
            if (isDead || i>= statuses.Count) return;
            if (statuses[i].status.TickOnEndOfTurn)
            {
                StatusAndDuration tempStatus = statuses[i];
                if (statuses[i].status.oneTurnOnly)
                    tempStatus.duration = 0;
                else
                    tempStatus.duration -= 1;
                if (tempStatus.duration == 0 || tempStatus.duration < 0 && !tempStatus.status.canBeNegative)
                {
                    statuses[i].status.OnStatusEnd(this, GetOpponent(this), statuses[i].duration);
                    if (isDead) return;
                    statuses.Remove(statuses[i]);
                    i--;
                }
                else
                    statuses[i] = tempStatus;
            }
        }
        statusDisplayer.DisplayStatusList(statuses);
    }
    public void StartOfTurnStatusUpdate()
    {
        if (isDead) return;
        for (int i = 0; i < statuses.Count; i++)
        {
            statuses[i].status.OnStartOfTurnEvent(this, GetOpponent(this), statuses[i].duration);
            if (isDead || i >= statuses.Count) return;
            if (statuses[i].status.TickOnStartOfTurn)
            {
                StatusAndDuration tempStatus = statuses[i];
                if (statuses[i].status.oneTurnOnly)
                    tempStatus.duration = 0;
                else
                    tempStatus.duration -= 1;
                if (tempStatus.duration == 0 || tempStatus.duration < 0 && !tempStatus.status.canBeNegative)
                {
                    statuses[i].status.OnStatusEnd(this, GetOpponent(this), statuses[i].duration);
                    if (isDead) return;
                    statuses.Remove(statuses[i]);
                    i--;
                }
                else
                    statuses[i] = tempStatus;
            }
        }
        statusDisplayer.DisplayStatusList(statuses);
    }
    public void OnCardPlayStatusUpdate()
    {
        if (isDead ) return;
        for (int i = 0; i < statuses.Count; i++)
        {
            statuses[i].status.OnCardPlayEvent(this, GetOpponent(this), statuses[i].duration);
            if (isDead || i >= statuses.Count) return;
            if (statuses[i].status.TickOnCardPlay)
            {
                StatusAndDuration tempStatus = statuses[i];
                if (statuses[i].status.oneTurnOnly)
                    tempStatus.duration = 0;
                else
                    tempStatus.duration -= 1;
                if (tempStatus.duration == 0 || tempStatus.duration < 0 && !tempStatus.status.canBeNegative)
                {
                    statuses[i].status.OnStatusEnd(this, GetOpponent(this), statuses[i].duration);
                    if (isDead) return;
                    statuses.Remove(statuses[i]);
                    i--;
                }
                else
                    statuses[i] = tempStatus;
            }
        }
        statusDisplayer.DisplayStatusList(statuses);
    }
    
    public void ClearAllStatusesWithNoEndUpdate()
    {
        statuses.Clear();
        statusDisplayer.DisplayStatusList(statuses);
    }

}
