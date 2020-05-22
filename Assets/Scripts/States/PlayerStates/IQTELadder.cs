using UnityEngine;

public class IQTELadder : IPlayerState
{
    private PlayerData _playerData = null;
    private bool _canMove = true;
    private Camera _camera = null;
    private CharacterController _characterController = null;
    private Vector3 _directionHorizontal = Vector3.zero;
    private Vector3 _orientation = Vector3.zero;
    private bool _isAtTheEnd = false;
    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _currentMouseY = 0;
    private float _currentMouseX = 0;
    private PlayerAgentController _playerController = null;
    private float _startYPlayer = 0;

    public bool CanMove { get => _canMove; set => _canMove = value; }
    public bool IsAtTheEnd { get => _isAtTheEnd; set { _isAtTheEnd = value; SetOrientation(); } }

    public void Init(PlayerData playerData, Camera camera, CharacterController characterController)
    {
        _playerData = playerData;
        _characterController = characterController;
        _camera = camera;
        _playerController = PlayerManager.Instance.PlayerController;
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        InputManager.Instance.Direction -= SetDirection;
        InputManager.Instance.MousePosition -= LookAtMouse;
        _playerController.CanMove = false;
    }

    public void Enter(Collider collider, string animation)
    {
        SetOrientation();
        InputManager.Instance.Direction += SetDirection;
        PlayerManager.Instance.PlayerController.TimeZoom = 0;
        InputManager.Instance.MousePosition += LookAtMouse;
        _playerController.CanMove = true;
        _startYPlayer = _characterController.transform.position.y;
    }

    private void SetOrientation()
    {
        if (_isAtTheEnd == false)
        {
            _orientation = _characterController.transform.up;
        }
        else
        {
            _orientation = _characterController.transform.forward;
        }
    }

    private void SetDirection(float horizontalMouvement, float verticalMouvement)
    {
        _directionHorizontal = Vector3.zero;
        if(horizontalMouvement < 0 && _playerController.transform.position.y > _startYPlayer)
        {
            _directionHorizontal = horizontalMouvement * _orientation * _playerData.SpeedLadder;
        }
        else if (horizontalMouvement > 0)
        {
            _directionHorizontal = horizontalMouvement * _orientation * _playerData.SpeedLadder;
        }
        Move();
    }

    private void Move()
    {
        if(_canMove == true)
        {
            Vector3 wantedDirection = _directionHorizontal * _playerData.GlobalSpeed * Time.deltaTime;
            _characterController.Move(wantedDirection);
        }
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
            _rotationY = Mathf.Clamp(_rotationY, _playerData.LadderMinAngleY, _playerData.LadderMinAngleY + _playerData.LadderAngleY);
            _rotationX = Mathf.Clamp(_rotationX, -_playerData.AngleX, _playerData.AngleX);
            _playerController.gameObject.transform.localEulerAngles = new Vector3(_playerController.gameObject.transform.rotation.eulerAngles.x, _rotationY, 0);
            _camera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
        }
    }
}