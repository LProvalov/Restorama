using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameState _state;

    public GameTable GameTable;

    public enum GameState
    {
        InitializeLevel = 0,
        FillingOfGameTable,
        PlayerTurn,
        PlayerTurnProcessing,
        EndingLevel,
        LevelEnded
    }

    public GameState CurrentGameState => _state;

    public void FixedUpdate()
    {
        switch (_state)
        {
            case GameState.InitializeLevel:
                if (GameTable.State == GameTable.GameTableState.ReadyToInitialization)
                {
                    GameTable.Initialization();
                }
                if (GameTable.State == GameTable.GameTableState.Initialized)
                {
                    _state = GameState.FillingOfGameTable;
                }
                break;
            case GameState.FillingOfGameTable:
                if (GameTable.State == GameTable.GameTableState.Initialized)
                {
                    GameTable.FillGameTableByItems();
                }

                if (GameTable.State == GameTable.GameTableState.FilledByItems)
                {
                    _state = GameState.PlayerTurn;
                }
                break;
            case GameState.PlayerTurn:

                break;
            case GameState.PlayerTurnProcessing:

                break;
            case GameState.EndingLevel:

                break;
            case GameState.LevelEnded:

                break;
        }
    }
}
