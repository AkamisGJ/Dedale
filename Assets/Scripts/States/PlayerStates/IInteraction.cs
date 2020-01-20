using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteraction : IPlayerState
{
    private PlayerController _playerController = null;
    private GameObject _porte = null;
    private PlayerData _playerData = null;

    public void Init(PlayerData playerData, Camera _camera)
    {
        _playerData = playerData;
    }

    public void Enter(GameObject grabObject)
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