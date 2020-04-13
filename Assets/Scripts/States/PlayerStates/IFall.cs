using UnityEngine;

public class IFall : IPlayerState
{
    private PlayerData _playerData = null;
    private Camera _camera = null;
    private PlayerAgentController _playerController = null;
    private CharacterController _characterController = null;

    public void Init(PlayerData playerData, Camera camera, CharacterController characterController = null, Animator animator = null)
    {
        _playerData = playerData;
        _characterController = characterController;
        _playerController = _characterController.GetComponent<PlayerAgentController>();
        _camera = camera;
    }

    public void Enter(Collider collider)
    {
    }

    public void Update()
    {
        _characterController.Move(-Vector3.up * _playerData.Gravity * Time.deltaTime);
    }

    public void Exit()
    {
    }


}
