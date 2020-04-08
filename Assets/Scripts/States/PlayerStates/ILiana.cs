using UnityEngine;

public class ILiana : IPlayerState
{
    private PlayerData _playerData = null;
    private Camera _camera = null;
    private CharacterController _characterController = null;
    private Vector3 _directionHorizontal = Vector3.zero;
    private Vector3 _orientation = Vector3.zero;

    public void Init(PlayerData playerData, Camera camera, CharacterController characterController = null, Animator animator = null)
    {
        _playerData = playerData;
        _characterController = characterController;
        _camera = camera;
    }

    public void Enter(Collider collider)
    {
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
        if(horizontalMouvement < 0)
        {
            _orientation = _characterController.transform.up;
            _directionHorizontal = horizontalMouvement * _orientation * _playerData.SpeedLadder;
            Move();
        }
    }

    private void Move()
    {
        Vector3 wantedDirection = _directionHorizontal * _playerData.GlobalSpeed * Time.deltaTime;
        _characterController.Move(wantedDirection);
    }
}
