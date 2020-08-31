using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimator : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerEnter()
    {
        animator.SetBool("OnMouseEnter", true);
    }
    public void OnPointerExit()
    {
        animator.SetBool("OnMouseEnter", false);
    }
}
