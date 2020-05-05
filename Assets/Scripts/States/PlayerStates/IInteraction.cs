using UnityEngine;

public class IInteraction : IPlayerState
{
    private PlayerAgentController _playerController = null;
    private GameObject _interactGameObject = null;
    private IInteract _interactObject = null;
    private PlayerData _playerData = null;
    private Camera _camera = null;
    private LayerMask _layerMask;
    private Vector3 OffsetSpherCast = Vector3.zero;

    public void Init(PlayerData playerData, Camera camera, CharacterController characterController)
    {
        _layerMask = playerData.LayerMask;
        _playerData = playerData;
        _camera = camera;
        _playerController = PlayerManager.Instance.PlayerController;
    }

    public void Enter(Collider colliderInteraction)
    {
        _interactGameObject = colliderInteraction.gameObject;
        _interactObject = _interactGameObject.GetComponent<IInteract>();
        InputManager.Instance.MousePosition += _interactObject.Interact;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        }
    }

    public void Exit()
    {
        OnExit();
        InputManager.Instance.MousePosition -= _interactObject.Interact;
    }

    private void OnExit()
    {
        _interactObject.Exit();
    }

    void OnDestroy()
    {
        InputManager.Instance.MousePosition -= _interactObject.Interact;
    }
}