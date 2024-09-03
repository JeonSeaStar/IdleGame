// ==============================================================
// 게임매니저
//
// AUTHOR: Lim Jaeyoung
// CREATED: 2024-07-04
// UPDATED: 2024-09-03
// ==============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Ready,
    Battle,
    Move,
    End
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameState CurrentState = GameState.None;
    public BGSlide BG;
    public Player mainPlayer;
    public Enemy enemy;
    public static GameManager instance = null;

    #region 싱글턴 Awake

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    private void Start()
    {
        CurrentState = GameState.Ready;
        BG.SwitchMove(false);
    }

    public void SwitchingState(bool isBattle)
    {
        if(isBattle && CurrentState != GameState.Battle)
        {
            CurrentState = GameState.Battle;
            StartBattle();
        }
    }

    private void StartBattle()
    {
        BG.SwitchMove(true);
    }
}