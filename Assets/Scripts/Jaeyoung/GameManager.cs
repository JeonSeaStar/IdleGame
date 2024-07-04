// ==============================================================
// 게임매니저
//
// AUTHOR: Lim Jaeyoung
// CREATED: 2024-07-04
// UPDATED: 2024-07-04
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
    public GameObject BG;
    public Player mainPlayer;
    public Enemy enemy;

    private void Start()
    {
        CurrentState = GameState.Ready;
    }
}