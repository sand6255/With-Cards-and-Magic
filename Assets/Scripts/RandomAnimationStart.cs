using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationStart : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Animator tempAnimator = GetComponent<Animator>();
        tempAnimator.Play(0, -1, Random.value);
    }

}
