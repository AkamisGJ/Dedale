using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterIKEA : MonoBehaviour
{
    [SerializeField] private Transform _otherPos = null;
    private Transform _player;
    [SerializeField] private bool _isTp = false;
    private void Start()
    {
        _player = PlayerManager.Instance.PlayerController.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isTp == false && other.GetComponent<PlayerAgentController>() != null)
        {
             _player.position = _otherPos.position;
            _isTp = true;
        }
    }
}
