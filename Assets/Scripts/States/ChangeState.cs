using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    private PlayerAgentController _playerAgentController;

    public void ChangeStatePlayer()
    {
        _playerAgentController = PlayerManager.Instance.PlayerController;
        _playerAgentController.ChangeState(PlayerAgentController.MyState.INFINITYCORRIDOR);
    }
}
