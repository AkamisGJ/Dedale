using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IQTE : IPlayerState
{
    private PlayerData _playerData = null;

    public void Init(PlayerData playerData, Camera _camera, CharacterController characterController)
    {
        _playerData = playerData;
    }

    void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void Exit()
    {

    }

    public void Enter()
    {

    }
}
