using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public enum GameMode
    {
        SinglePlayer,
        OnlineSinglePlayer,
        OnlineMultiPlayer

    }
    public static GameMode CurrentGameMode => GameMode.SinglePlayer;
}