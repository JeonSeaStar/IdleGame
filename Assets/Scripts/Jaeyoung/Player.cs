// ==============================================================
// 메인 캐릭터
//
// AUTHOR: Lim Jaeyoung
// CREATED: 2024-07-04
// UPDATED: 2024-07-04
// ==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Unit
{
    public Rigidbody2D rigid;
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.right, 4 ,4);
        Vector3 rayposition = new Vector3(rigid.position.x, rigid.position.y + 0.5f);
        Debug.DrawRay(rayposition, Vector2.right * 4, new Color(1, 0, 0));
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
    }
}