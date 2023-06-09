using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceAnimation : MonoBehaviour
{
    public Animator animator;

    public bool fireHigh = false;

    private void Update()
    {
        animator.SetBool("FireHigh", fireHigh);

    }
}
