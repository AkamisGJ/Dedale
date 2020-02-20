using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLiana : MonoBehaviour
{
    private PlayerAgentController _playerController = null;

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController.ChangeState(PlayerAgentController.MyState.FALL);
        }
    }
}
