using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IInteraction : IPlayerState
{
    private PlayerAgentController _playerController = null;
    private GameObject _porte = null;
    private PlayerData _playerData = null;

    public void Init(PlayerData playerData, Camera _camera, CharacterController characterController)
    {
        _playerData = playerData;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.Mouse0) == false)
        {

        }
    }

    public void Exit()
    {

    }
}