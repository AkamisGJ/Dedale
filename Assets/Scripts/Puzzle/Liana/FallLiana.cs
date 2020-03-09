using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallLiana : MonoBehaviour
{
    private PlayerAgentController _playerController = null;
    private float _lerpStartPositionPlayer = 0;
    private float _lerpFallStartPositionPlayer = 0;
    [SerializeField] private float _speedRotateCamera = 0;
    [SerializeField] Transform _enterTransform = null;
    [SerializeField] Transform _endTransform = null;
    private CharacterController _characterController = null;
    [SerializeField] private float _timeToWaitOnFloor = 0;
    private float _currentWaitTime = 0;

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
        _characterController = _playerController.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameLoopManager.Instance.LoopQTE += OnUpdate;
            GameLoopManager.Instance.LoopQTE += OnStartFalling;
            _playerController.ChangeState(PlayerAgentController.MyState.FALL);
            _lerpFallStartPositionPlayer = 0;
            _lerpStartPositionPlayer = 0;
            _currentWaitTime = 0;
        }
    }

    private void OnUpdate()
    {
        if (_characterController.isGrounded == true)
        {
            _currentWaitTime += Time.deltaTime;
            if(_currentWaitTime >= _timeToWaitOnFloor)
            {
                Recovery();
                GameLoopManager.Instance.LoopQTE -= OnStartFalling;
            }
        }
    }

    private void OnStartFalling()
    {
        _lerpStartPositionPlayer += Time.deltaTime * _speedRotateCamera;
        _playerController.transform.rotation = Quaternion.Lerp(_playerController.transform.rotation, _enterTransform.rotation, _lerpStartPositionPlayer);
        if (_lerpStartPositionPlayer > 1)
        {
            PlayerManager.Instance.IsInNarrowWay = true;
            _playerController.ChangeState(PlayerAgentController.MyState.FALL);
            GameLoopManager.Instance.LoopQTE -= OnStartFalling;
            _lerpStartPositionPlayer = 0;
        }
    }

    private void Recovery()
    {
        _lerpFallStartPositionPlayer += Time.deltaTime;
        _lerpFallStartPositionPlayer = Mathf.Clamp(_lerpFallStartPositionPlayer, 0, 1);
        _playerController.transform.rotation = Quaternion.Lerp(_playerController.transform.rotation, _endTransform.rotation, _lerpFallStartPositionPlayer);
        if (_lerpFallStartPositionPlayer >= 1)
        {
            GameLoopManager.Instance.LoopQTE -= OnUpdate;
            _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        }
    }
}
