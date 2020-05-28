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
    private float _accelerationLerp = 0;
    private float _reductionVisionY = 0;
    private float _reductionVisionX = 0;

    public float ReductionVisionX { get => _reductionVisionX; set => _reductionVisionX = value; }
    public float ReductionVisionY { get => _reductionVisionY; set => _reductionVisionY = value; }

    public void Enter(Collider collider = null, string animation = null)
    {
        _reductionVisionY = 0;
        _reductionVisionX = 0;
        _playerController.CanMove = true;
        _acceleration = 0;
        if (_mainCamera.transform.localEulerAngles.x < _playerData.AngleXInfintyCorridor)
        {
            _rotationX = -_mainCamera.transform.localEulerAngles.x;
        }
        else
        {
            _rotationX = -_mainCamera.transform.localEulerAngles.x + 360;
        }
        if (_mainCamera.transform.localEulerAngles.x < _playerData.AngleXInfintyCorridor)
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
        _accelerationLerp = 0;
    }

    public void Update()
    {
        
        Acceleration();
        _playerController.transform.position -= Vector3.forward * _playerData.MaxSpeedInfintyCorridor * Time.deltaTime * _acceleration;
    }

    private void Acceleration()
    {
        _accelerationLerp += Time.deltaTime / _playerData.AccelerationTimeToReachMaxSpeed;
        _accelerationLerp = Mathf.Clamp(_accelerationLerp, 0, 1);
        _acceleration = _playerData.AccelerationInfintyCorridor.Evaluate(_accelerationLerp);
    }

    private void LookAtMouse(float mousePositionX, float mousePositionY)
    {
        _reductionVisionX = Mathf.Clamp(_reductionVisionX, 0, _playerData.AngleXInfintyCorridor);
        _reductionVisionY = Mathf.Clamp(_reductionVisionY, 0, _playerData.AngleYInfintyCorridor);
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
            _rotationX += _currentMouseY + mousePositionY * _playerData.SensitivityMouseX * PlayerManager.Instance.MouseSensitivityMultiplier;
            _rotationY += _currentMouseX + mousePositionX * _playerData.SensitivityMouseY * PlayerManager.Instance.MouseSensitivityMultiplier;
            _rotationY = Mathf.Clamp(_rotationY, _playerData.AngleYInfintyCorridor + ReductionVisionY, -_playerData.AngleYInfintyCorridor + 360 - ReductionVisionY);
            _rotationX = Mathf.Clamp(_rotationX, -_playerData.AngleXInfintyCorridor + ReductionVisionX, _playerData.AngleXInfintyCorridor - ReductionVisionX);
            _playerController.gameObject.transform.localEulerAngles = new Vector3(0, _rotationY, 0);
            _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
        }
    }
}