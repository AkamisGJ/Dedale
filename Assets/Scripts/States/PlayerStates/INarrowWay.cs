﻿using UnityEngine;

public class INarrowWay : IPlayerState
{
    private PlayerData _playerData = null;
    private Camera _camera = null;
    private CharacterController _characterController = null;
    private Vector3 _direction = Vector3.zero;
    private float _angleMaxCamera = 0;
    private float _lerpCamera = 0;
    private float _currentAngleCamera = 0;

    public void Init(PlayerData playerData, Camera camera, CharacterController characterController = null)
    {
        _playerData = playerData;
        _camera = camera;
        _characterController = characterController;
    }

    public void Enter()
    {
        _lerpCamera = 0.5f;
        InputManager.Instance.Direction += SetOrientation;
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        InputManager.Instance.Direction -= SetOrientation;
    }

    private void SetOrientation(float horizontalMouvement, float verticalMouvement)
    {
        if (verticalMouvement > 0)
        {
            RotationCamera(1);
        }
        else if (verticalMouvement < 0)
        {
            RotationCamera(-1);
        }
        _direction = verticalMouvement * _characterController.transform.right * _playerData.SpeedLadder;
        Move();
    }

    private void Move()
    {
        Vector3 wantedDirection = _direction * _playerData.GlobalSpeed * Time.deltaTime;
        _characterController.Move(wantedDirection);
    }

    private void RotationCamera(int inversion)
    {
        _lerpCamera += Time.deltaTime * inversion;
        _lerpCamera = Mathf.Clamp(_lerpCamera, 0, 1);
        _currentAngleCamera = Mathf.Lerp(-_angleMaxCamera, _angleMaxCamera, _lerpCamera);
        _camera.transform.localEulerAngles = new Vector3(0, -_currentAngleCamera, 0);
    }
}
