// ==============================================================
// À¯´Ö ÀüÃ¼
//
// AUTHOR: Lim Jaeyoung
// CREATED: 2024-07-04
// UPDATED: 2024-07-04
// ==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UnitState
{
    None,
    Run,
    Attack
}
public class Unit : MonoBehaviour
{
    [SerializeField]
    protected float hp;
    public float moveSpeed;
    public Animator animator;
    protected virtual void Start()
    {
        StartCoroutine(StartOn());
    }

    protected virtual IEnumerator StartOn()
    {
        yield return null;
    }
}
