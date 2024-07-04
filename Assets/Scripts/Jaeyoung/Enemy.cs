// ==============================================================
// Àû±º
//
// AUTHOR: Lim Jaeyoung
// CREATED: 2024-07-04
// UPDATED: 2024-07-04
// ==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    //public float moveSpeed;
    //public Animator animator;

    protected override IEnumerator StartOn()
    {
        animator.SetTrigger("doMove");
        while (1 - transform.position.x <= 0)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger("doStop");
    }
}
