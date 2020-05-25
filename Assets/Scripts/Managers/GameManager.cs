using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    public enum MyState
    {
        PRELOAD,
        MAINMENU,
        GAME,
        LOADINGSCREEN,
    }
    private MyState _currentState = MyState.PRELOAD;
    private Dictionary<MyState, IGameState> _states = null;
    private string _nextSceneName = "1_Introduction Audio";
    #endregion Fields

    #region Properties
    public MyState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public IGameState CurrentStateType { get { return _states[_currentState]; } }

    public string NextScene { get { return _nextSceneName; } set { _nextSceneName = value; } }
    #endregion Properties

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        _states = new Dictionary<MyState, IGameState>();
        _states.Add(MyState.PRELOAD, new PreloadState());
        _states.Add(MyState.GAME, new GameState());
        _states.Add(MyState.MAINMENU, new MainMenuState());
        _states.Add(MyState.LOADINGSCREEN, new LoadingScreenState());
        if(_currentState == MyState.GAME || _currentState == MyState.LOADINGSCREEN)
        {
            return;
        }
        _currentState = MyState.PRELOAD;
        ChangeState(MyState.MAINMENU, "0_MainMenu");
    }

    public void ChangeState(MyState nextState, string scene = null)
    {
        if (scene != null)
        {
            _nextSceneName = scene;
        }
        _states[_currentState].Exit();
        _currentState = nextState;
        _states[nextState].Enter();
    }
}