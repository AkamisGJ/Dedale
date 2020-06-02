using UnityEngine;

public class ICinematics : IPlayerState
{
    private PlayerAgentController _playerAgentController = null;

    public void Enter(Collider collider = null, string animation = null)
    {
        _playerAgentController.AnimatorCamera.SetBool("WakeUp", true);
    }

    public void Exit()
    {
        _playerAgentController.AnimatorCamera.SetBool("WakeUp", false);
    }

    public void Init(PlayerData playerData, Camera camera, CharacterController playerController = null)
    {
        _playerAgentController = PlayerManager.Instance.PlayerController;
    }

    public void Update()
    {
        if (_playerAgentController.AnimatorCamera.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _playerAgentController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        }
    }
}
