using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGameManager : Singleton<ExampleGameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    void Start() => ChangeGameState(GameState.Starting);

    public void ChangeGameState(GameState newState)
    {
        //if (State == newState)
        //    return;

        OnBeforeStateChanged?.Invoke(newState);

        State = newState;

        switch (newState)
        {
            case GameState.Starting:

                break;

            case GameState.SpawningPlayer:

                break;

            case GameState.SpawningEnemies:

                break;

            case GameState.Win:

                break;

            case GameState.Lose:

                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New State: {newState}");
    }

    private void HandleStarting()
    {
        //Fazer todo o start setup. Poderia ser algo do environment ou até msm uma cinematic;

        ChangeGameState(GameState.SpawningPlayer);
    }

    private void HandleSpawningPlayer()
    {
        //Chamar metodo de spawn do player
        ExampleUnitManager.Instance.SpawnPlayer();

        ChangeGameState(GameState.SpawningEnemies);
    }

    private void HandleSpawningEnemies()
    {
        //Chamar metodo de spawn dos inimigos
        ExampleUnitManager.Instance.SpawnUnit();

        ChangeGameState(GameState.SpawningPlayer);
    }
}

[Serializable]
public enum GameState
{
    Starting = 0,
    SpawningPlayer = 1,
    SpawningEnemies = 2,
    Win = 3,
    Lose = 4
}
