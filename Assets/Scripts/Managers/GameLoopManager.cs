using System;

public class GameLoopManager : Singleton<GameLoopManager>
{
    #region Event

    private Action _startPlayer = null;
    public event Action StartPlayer
    {
        add{
            _startPlayer -= value;
            _startPlayer += value;
        }
        remove{
            _startPlayer -= value;
        }
    }

    private Action _startInputManager = null;
    public event Action StartInputManager
    {
        add{
            _startInputManager -= value;
            _startInputManager += value;
        }
        remove{
            _startInputManager -= value;
        }
    }

    private Action _lastStart = null;
    public event Action LastStart
    {
        add
        {
            _lastStart -= value;
            _lastStart += value;
        }
        remove
        {
            _lastStart -= value;
        }
    }

    private Action _gameLoopPlayer = null;
    public event Action GameLoopPlayer
    {
        add{
            _gameLoopPlayer -= value;
            _gameLoopPlayer += value;
        }
        remove{
            _gameLoopPlayer -= value;
        }
    }

    private Action _gameLoopLoadingScene = null;
    public event Action GameLoopLoadingScene
    {
        add
        {
            _gameLoopLoadingScene -= value;
            _gameLoopLoadingScene += value;
        }
        remove
        {
            _gameLoopLoadingScene -= value;
        }
    }

    private Action _gameLoopModifyVolume = null;
    public event Action GameLoopModifyVolume
    {
        add
        {
            _gameLoopModifyVolume -= value;
            _gameLoopModifyVolume += value;
        }
        remove
        {
            _gameLoopModifyVolume -= value;
        }
    }

    private Action _lateLoopDialogue = null;
    public event Action LateLoopDialogue
    {
        add
        {
            _lateLoopDialogue -= value;
            _lateLoopDialogue += value;
        }
        remove
        {
            _lateLoopDialogue -= value;
        }
    }

    private Action _loopQTE = null;
    public event Action LoopQTE
    {
        add
        {
            _loopQTE -= value;
            _loopQTE += value;
        }
        remove
        {
            _loopQTE -= value;
        }
    }

    private Action _gameLoopInputManager = null;
    public event Action GameLoopInputManager
    {
        add{
            _gameLoopInputManager -= value;
            _gameLoopInputManager += value;
        }
        remove{
            _gameLoopInputManager -= value;
        }
    }

    private Action _gameLoopPortal = null;
    public event Action GameLoopPortal
    {
        add{
            _gameLoopPortal -= value;
            _gameLoopPortal += value;
        }
        remove{
            _gameLoopPortal -= value;
        }
    }

    private Action _fixedGameLoop = null;
    public event Action FixedGameLoop{
        add{
            _fixedGameLoop -= value;
            _fixedGameLoop += value;
        }
        remove{
            _fixedGameLoop -= value;
        }
    }

    private Action _lateGameLoop = null;
    public event Action LateGameLoop{
        add{
            _lateGameLoop -= value;
            _lateGameLoop += value;
        }
        remove{
            _lateGameLoop -= value;
        }
    }

    private Action _managerLoop = null;
    public event Action ManagerLoop{
        add{
            _managerLoop -= value;
            _managerLoop += value;
        }
        remove{
            _managerLoop -= value;
        }
    }

    #endregion

    private bool _isPaused = false;

    public bool IsPaused { get => _isPaused; set => _isPaused = value; }

    #region Loop


    void Start()
    {
        if (_startPlayer != null){
            _startPlayer();
        }
        if(_startInputManager != null){
            _startInputManager();
        }
    }

    void Update()
    {
        if (_isPaused == false)
        {
            if (_lastStart != null)
            {
                _lastStart();
            }
            if(_gameLoopModifyVolume != null)
            {
                _gameLoopModifyVolume();
            }
            if (_gameLoopInputManager != null)
            {
                _gameLoopInputManager();
            }

            if (_loopQTE != null)
            {
                _loopQTE();
            }

            if (_gameLoopPlayer != null)
            {
                _gameLoopPlayer();
            }

            if (_managerLoop != null)
            {
                _managerLoop();
            }
        }
        if(_gameLoopLoadingScene != null)
        {
            _gameLoopLoadingScene();
        }
    }

    private void FixedUpdate()
    {
        if (_isPaused == false)
        {
            if (_fixedGameLoop != null)
            {
                _fixedGameLoop();
            }
        }
    }

    private void LateUpdate() {
        if (_isPaused == false)
        {
            if (_lateGameLoop != null)
            {
                _lateGameLoop();
            }

            if (_lateLoopDialogue != null)
            {
                _lateLoopDialogue();
            }

            if (_gameLoopPortal != null)
            {
                _gameLoopPortal();
            }
        }
    }

    #endregion
}
