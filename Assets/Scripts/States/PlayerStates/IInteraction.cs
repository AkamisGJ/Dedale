using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteraction : IPlayerState
{
    private PlayerController _playerController = null;
    private GameObject _porte = null;

    public void Init()
    {

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