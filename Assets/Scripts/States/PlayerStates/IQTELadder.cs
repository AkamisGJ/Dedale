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

    public bool CanMove { get => _canMove; set => _canMove = value; }
    public bool IsAtTheEnd { get => _isAtTheEnd; set => _isAtTheEnd = value; }

    public void Init(PlayerData playerData, Camera camera, CharacterController characterController)
    {
        _playerData = playerData;
        _characterController = characterController;
        _camera = camera;
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        InputManager.Instance.Direction -= SetOrientation;
    }

    public void Enter(Collider collider)
    {
        InputManager.Instance.Direction += SetOrientation;
        PlayerManager.Instance.PlayerController.TimeZoom = 0;
    }

    private void SetOrientation(float horizontalMouvement, float verticalMouvement)
    {
        if(_isAtTheEnd == false)
        {
            _orientation = _characterController.transform.up;
        }
        else
        {
            _orientation = _characterController.transform.forward;
        }
        _directionHorizontal = horizontalMouvement * _orientation * _playerData.SpeedLadder;
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
}
