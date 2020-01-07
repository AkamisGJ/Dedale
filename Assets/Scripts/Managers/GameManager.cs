using System.Collections.Generic;
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
    #endregion Fields

    #region Properties
    public MyState CurrentState { get { return _currentState; } }
    public IGameState CurrentStateType { get { return _states[_currentState]; } }
    #endregion Properties

    protected override void Start()
    {
        base.Start();
        _states = new Dictionary<MyState, IGameState>();
        _states.Add(MyState.Preload, new PreloadState());
        _states.Add(MyState.Game, new GameState());
        _states.Add(MyState.MainMenu, new MainMenuState());
        _currentState = MyState.Preload;
        ChangeState(MyState.MainMenu);
    }

    public void ChangeState(MyState nextState)
    {
        _states[_currentState].Exit();
        _states[nextState].Enter();
        _currentState = nextState;
    }
}