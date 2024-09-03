// ==============================================================
// 유닛 전체
//
// AUTHOR: Lim Jaeyoung
// CREATED: 2024-09-03
// UPDATED: 2024-09-03
// ==============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSlide : MonoBehaviour
{
    public Sprite bg;        //백그라운드 이미지 샘플
    public GameObject[] bgs; //이미지 모음

    private Coroutine mapSlide;
    private float bgX;
    private float moveX;
    private int bgCount;

    public bool testBattle;

    private void Start()
    {
        bgX = bg.bounds.size.x;
        moveX = 0;
    }

    private void Update()  //테스트용 삭제예정
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!testBattle)
            {
                testBattle = true;
            }
            else
            {
                testBattle = false;
            }
            SwitchMove(testBattle);
        }
    }

    public void SwitchMove(bool isBattle)
    {
        if(isBattle)
        {
            mapSlide = StartCoroutine(StartOn());
        }
        else
        {
            StopCoroutine(mapSlide);
            mapSlide = null;
        }
    }

    private IEnumerator StartOn()
    {
        while (true)
        {
            transform.position += Vector3.left * 3.0f * Time.deltaTime;
            moveX += 3.0f * Time.deltaTime;

            if (moveX > bgX )
            {
                bgs[bgCount].gameObject.transform.localPosition += new Vector3(bgX * bgs.Length, 0, 0);
                moveX = 0;
                bgCount++;
                if(bgCount >= bgs.Length)
                {
                    bgCount = 0;
                }
            }
            yield return null;
        }
    }
    
}
