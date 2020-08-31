using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayMoveAnimation()
    {
        animator.SetBool("MoveAnimation", true);
    }
    public void MoveImpact()
    {
        EnemyFight.instance.PlayAction();
    }
    public void EndMoveAnimation()
    {
        animator.SetBool("MoveAnimation", false);
        animator.SetBool("TakeDamageAnimation", false);
        EnemyFight.instance.EndTurn();
    }
    public void PlayTakeDamageAnimation()
    {
        animator.SetBool("TakeDamageAnimation", true);
    }
    public void EndTakeDamageAnimation()
    {
        animator.SetBool("TakeDamageAnimation", false);
    }

}
