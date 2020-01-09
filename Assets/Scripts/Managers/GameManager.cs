﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prof.Utils;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    public enum MyState
    {
        Preload,
        MainMenu,
        Game,
    }
    private MyState _currentState = MyState.Preload;
    private Dictionary<MyState, IGameState> _states = null;
    private string _nextSceneName = "Level1";
    #endregion Fields

    #region Properties
    public MyState CurrentState { get { return _currentState; } }
    public IGameState CurrentStateType { get { return _states[_currentState]; } }

    public string NextScene { get { return _nextSceneName; } set { _nextSceneName = value; } }
    #endregion Properties

    protected override void Start()
    {
        base.Start();
        _states = new Dictionary<MyState, IGameState>();
        _states.Add(MyState.Preload, new PreloadState());
        _states.Add(MyState.Game, new GameState());
        _states.Add(MyState.MainMenu, new MainMenuState());
        _currentState = MyState.Preload;
        ChangeState(MyState.MainMenu, "Main Menu");
    }

    public void ChangeState(MyState nextState, string scene)
    {
        if(_currentState == MyState.Game && _nextSceneName != null)
        {
            _states[_currentState].Exit();
        }
        _currentState = nextState;
        if(_currentState == MyState.Game)
        {
            _nextSceneName = scene;
        }
        _states[nextState].Enter();
    }
}