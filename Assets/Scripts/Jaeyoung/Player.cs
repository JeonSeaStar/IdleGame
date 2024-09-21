// ==============================================================
// 메인 캐릭터
//
// AUTHOR: Lim Jaeyoung
// CREATED: 2024-07-04
// UPDATED: 2024-09-03
// ==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Unit
{
    public Rigidbody2D rigid;
    private const int enemyRaylayer = 1 << 4;
    [SerializeField]
    public GameObject target;
    public GameObject attackObect;
    private Transform atkPos;

    private void Start()
    {
        atkPos = gameObject.transform.GetChild(0).transform;
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.right, 2 , enemyRaylayer);
        Vector3 rayposition = new Vector3(rigid.position.x, rigid.position.y + 0.5f);
        Debug.DrawRay(rayposition, Vector2.right * 2, new Color(1, 0, 0));
        if (hit.collider != null && target == null)
        {
            target = hit.collider.gameObject;
            GameManager.instance.SwitchingState(true);
            StartCoroutine(OnAttack());
            Debug.Log(hit.collider.name);
        }
    }

    private IEnumerator OnAttack()
    {
        while(target != null)
        {
            Attack();
            yield return new WaitForSeconds(1.0f);
        }
        yield return null;
    }
    private void Attack()
    {
        GameObject atk = Instantiate(attackObect);
        atk.transform.position = atkPos.position; 
        atk.GetComponent<Projective>().DmgTargetSetting(1.0f, enemyRaylayer);
    }
}