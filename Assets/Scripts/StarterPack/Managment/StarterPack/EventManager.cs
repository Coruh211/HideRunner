using System;
using System.Collections.Generic;

public static class EventManager
{
    public static GlobalEvent OnStartGame = new GlobalEvent();
    public static GlobalEvent OnRestartGame = new GlobalEvent();
    public static GlobalEvent<EndGameStatus> OnEndGame = new GlobalEvent<EndGameStatus>();

    public static void Reset()
    {
        OnEndGame = new GlobalEvent<EndGameStatus>();
        OnStartGame = new GlobalEvent();
    }
}
