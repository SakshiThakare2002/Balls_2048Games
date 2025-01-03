using System;
using UnityEngine;
using Yudiz.StarterKit.Utilities;

public class GameStateManager : Singleton<GameStateManager>
{
    private GameState currentGameState;

    public delegate void GameStateChanged(GameState gameState);
    public static event GameStateChanged OnGameStateChanged;

    public void ChangeGameState(GameState newGameState)
    {
        if (currentGameState == newGameState) return;
        
        currentGameState = newGameState;
        OnGameStateChanged?.Invoke(currentGameState); 
    }
}

public enum GameState
{
    MainMenu,
    Gameplay,
    GameOver,
    Pause
}
