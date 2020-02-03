using UnityEngine;

public class IInteraction : IPlayerState
{
    private PlayerAgentController _playerController = null;
    private GameObject _interactGameObject = null;
    private IInteract _interactObject = null;
    private PlayerData _playerData = null;
    private RaycastHit _raycastHit;
    private Camera _camera = null;
    private LayerMask _layerMask;

    public void Init(PlayerData playerData, Camera camera, CharacterController characterController)
    {
        _layerMask = playerData.LayerMask;
        _playerData = playerData;
        _camera = camera;
        _playerController = PlayerManager.Instance.PlayerController;
    }

    public void Enter()
    {
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _raycastHit, _playerData.MaxDistanceInteractionObject, _layerMask);
        _interactGameObject = _raycastHit.collider.gameObject;
        _interactObject = _interactGameObject.GetComponent<IInteract>();
        InputManager.Instance.MousePosition += _interactObject.Interact;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.Mouse0) == false)
        {
            _playerController.ChangeState(PlayerAgentController.MyState.Mouvement);
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
        _interactGameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    void OnDestroy()
    {
        InputManager.Instance.MousePosition -= _interactObject.Interact;
    }
}