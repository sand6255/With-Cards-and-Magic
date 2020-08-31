using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NextMoveDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject MoveHeart = null, MoveClaw = null, MoveStatus = null, MoveHeal = null;
    [SerializeField]
    TextMeshProUGUI moveNumber = null;
    public static NextMoveDisplay instance = null;
    MonsterAction currentAction = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

    }
    public void SetNextMove(MonsterAction action)
    {
        bool isAttack = action.isAttack, isStatus = action.isStatus, isHeal = action.isHeal;
        currentAction = action;
        MoveHeart.SetActive(false);
        MoveClaw.SetActive(false);
        MoveStatus.SetActive(false);
        MoveHeal.SetActive(false); 
        if (isAttack || isHeal || isStatus)
        {
            MoveHeart.SetActive(true);
        }
        if (isAttack)
        {
            MoveClaw.SetActive(true);
        }
        if(isStatus)
        {
            MoveStatus.SetActive(true);
        }
        if(isHeal)
        {
            MoveHeal.SetActive(true);
        }
        if (isAttack) moveNumber.text = action.GetNumberAttack();
        else moveNumber.text = "";
    }
    public void Update()
    {
        if (currentAction!= null && currentAction.isAttack && EnemyFight.instance.enemyHealthAndStatus != null) moveNumber.text = currentAction.GetNumberAttack();
    }
}
