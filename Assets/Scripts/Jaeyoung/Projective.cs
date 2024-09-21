using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projective : MonoBehaviour
{
    private float dmg;
    public int targetLayer;
    private const int enemyRaylayer = 1 << 4;
    void Start()
    {
        StartCoroutine(Projectile());
    }

    public void DmgTargetSetting(float dmgSet, int _targetLayer)
    {
        dmg = dmgSet;
        targetLayer = _targetLayer;
    }

    private IEnumerator Projectile()
    {
        while(true)
        {
            transform.position += Vector3.right * 3.0f * Time.deltaTime;
            yield return null;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            collision.gameObject.GetComponent<Enemy>().Damaged(dmg);
            Destroy(gameObject);
        }
        
    }

}
