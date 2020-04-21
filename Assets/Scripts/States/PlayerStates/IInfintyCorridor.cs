using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInfintyCorridor : IPlayerState
{
    private PlayerAgentController _playerController = null;
    private float _currentMouseX = 0;
    private PlayerData _playerData = null;
    private float _currentMouseY = 0;
    private Camera _mainCamera = null;
    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _acceleration = 0;

    public void Enter(Collider collider = null)
    {
        _playerController.CanMove = true;
        _acceleration = 1;
        if (_mainCamera.transform.localEulerAngles.x < _playerData.AngleX)
        {
            _rotationX = -_mainCamera.transform.localEulerAngles.x;
        }
        else
        {
            _rotationX = -_mainCamera.transform.localEulerAngles.x + 360;
        }
        if (_mainCamera.transform.localEulerAngles.x < _playerData.AngleX)
        {
            _rotationY = -_mainCamera.transform.localEulerAngles.y;
        }
        else
        {
            _rotationY = -_mainCamera.transform.localEulerAngles.y + 360;
        }
        InputManager.Instance.MousePosition += LookAtMouse;
    }

    public void Exit()
    {
        InputManager.Instance.MousePosition -= LookAtMouse;
    }

    public void Init(PlayerData playerData, Camera camera, CharacterController playerController = null)
    {
        _playerController = PlayerManager.Instance.PlayerController;
        _playerData = playerData;
        _mainCamera = camera;
    }

    public void Update()
    {
        _playerController.transform.position -= Vector3.forward * _playerData.SpeedForward * Time.fixedDeltaTime * _acceleration;
        _acceleration += Time.deltaTime;
    }

    private void LookAtMouse(float mousePositionX, float mousePositionY)
    {
        if (_playerController.CanMove == true && _playerController != null)
        {
            if (mousePositionX > 0)
            {
                _currentMouseX += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if (mousePositionX < 0)
            {
                _currentMouseX -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if (mousePositionX == 0 && _currentMouseX > 0)
            {
                _currentMouseX -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, 0, _playerData.StackMovement);
            }
            else if (mousePositionX == 0 && _currentMouseX < 0)
            {
                _currentMouseX += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, -_playerData.StackMovement, 0);
            }
            if (mousePositionY > 0)
            {
                _currentMouseY += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseY = Mathf.Clamp(_currentMouseY, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if (mousePositionY < 0)
            {
                _currentMouseY -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseY = Mathf.Clamp(_currentMouseY, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if (mousePositionY == 0 && _currentMouseY > 0)
            {
                _currentMouseY -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseY = Mathf.Clamp(_currentMouseY, 0, _playerData.StackMovement);
            }
            else if (mousePositionY == 0 && _currentMouseY < 0)
            {
                _currentMouseY += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseY = Mathf.Clamp(_currentMouseY, -_playerData.StackMovement, 0);
            }
            _rotationY = _playerController.gameObject.transform.localEulerAngles.y;
            _rotationX += _currentMouseY + mousePositionY * _playerData.SensitivityMouseX;
            _rotationY += _currentMouseX + mousePositionX * _playerData.SensitivityMouseY;
            _rotationY = Mathf.Clamp(_rotationY, _playerData.AngleY, -_playerData.AngleY + 360);
            _rotationX = Mathf.Clamp(_rotationX, -_playerData.AngleX, _playerData.AngleX);
            _playerController.gameObject.transform.localEulerAngles = new Vector3(0, _rotationY, 0);
            _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
            Debug.Log(_rotationY);
        }
    }
}