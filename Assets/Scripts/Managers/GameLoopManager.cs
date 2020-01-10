using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Prof.Utils;

public class GameLoopManager : Singleton<GameLoopManager>
{
    #region Event

    private Action _startLoop_1 = null;
    public event Action StartLoop_1{
        add{
            _startLoop_1 -= value;
            _startLoop_1 += value;
        }
        remove{
            _startLoop_1 -= value;
        }
    }

    private Action _startLoop_2 = null;
    public event Action StartLoop_2{
        add{
            _startLoop_2 -= value;
            _startLoop_2 += value;
        }
        remove{
            _startLoop_2 -= value;
        }
    }
    private Action _gameLoop = null;
    public event Action GameLoop{
        add{
            _gameLoop -= value;
            _gameLoop += value;
        }
        remove{
            _gameLoop -= value;
        }
    }

    private Action _secondGameLoop = null;
    public event Action SecondGameLoop{
        add{
            _secondGameLoop -= value;
            _secondGameLoop += value;
        }
        remove{
            _secondGameLoop -= value;
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
        if(_startLoop_1 != null){
            _startLoop_1();
        }
        if(_startLoop_2 != null){
            _startLoop_2();
        }
    }

    void Update()
    {
        if(_gameLoop != null){
            _gameLoop();
        }
        if(_secondGameLoop != null){
            _secondGameLoop();
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
