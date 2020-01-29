using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    

    #region Loop

    
    void Start()
    {
        if(_startPlayer != null){
            _startPlayer();
        }
        if(_startInputManager != null){
            _startInputManager();
        }
    }

    void Update()
    {
        if (_gameLoopInputManager != null)
        {
            _gameLoopInputManager();
        }

        if (_gameLoopPlayer != null){
            _gameLoopPlayer();
        }

        if(_managerLoop != null){
            _managerLoop();
        }
    }

    private void FixedUpdate() {
        if(_fixedGameLoop != null){
            _fixedGameLoop();
        }
    }

    private void LateUpdate() {
        if(_lateGameLoop != null){
            _lateGameLoop();
        }
    }

    #endregion
}
