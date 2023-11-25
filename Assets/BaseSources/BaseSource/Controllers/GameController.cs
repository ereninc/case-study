using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    private readonly ObservedValue<GameStates> GameStatus = new(GameStates.Loading);
    private IList<IGameStateObserver> _gameStatObservers;

    public GameStates currentGameState;

    public override void Initialize()
    {
        base.Initialize();
        SetGameState(GameStates.Loading);
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = -1;
    }

    [Button]
    public void SetGameState(GameStates targetState)
    {
        if (GameStatus.Value == targetState) return;
        ChangeState(targetState);
    }

    public void AddListener(IGameStateObserver gameStateObserver)
    {
        _gameStatObservers ??= new List<IGameStateObserver>();
        if (!_gameStatObservers.Contains(gameStateObserver))
        {
            _gameStatObservers.Add(gameStateObserver);
        }
    }

    private void OnGameStateChanged()
    {
        currentGameState = GameStatus.Value;
        if (_gameStatObservers == null) return;

        for (int i = 0; i < _gameStatObservers.Count; i++)
        {
            if (_gameStatObservers[i] != null) _gameStatObservers[i].OnGameStateChanged();
        }
    }

    private void ChangeState(GameStates targetState)
    {
        if (targetState == GameStates.Win)
        {
            Timing.CallDelayed(2f, () => GameStatus.Value = targetState);
            return;
        }
        GameStatus.Value = targetState;
    }

    #region [ Subscriptions ]

    private void OnEnable()
    {
        GameStatus.OnValueChange += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStatus.OnValueChange -= OnGameStateChanged;
    }

    #endregion
}