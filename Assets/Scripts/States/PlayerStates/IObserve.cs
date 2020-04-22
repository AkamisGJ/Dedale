using UnityEngine;

public class IObserve : IPlayerState
{
    private PlayerAgentController _playerAgentController = null;
    private GameObject _grabObject = null;
    private Vector3 _originPositionGrabObject = Vector3.zero;
    private Quaternion _originRotationGrabObject = Quaternion.identity;
    private float _mouseSensitivityInteract = 0;
    private Camera _mainCamera = null;
    private PlayerData _playerData = null;
    private Collider _grabObjectCollider = null;
    private Transform _objectHolder = null;
    private InteractObject _currentObjectInterract = null;
    private Quaternion _grabObjectRotationWhenLooked = Quaternion.identity;
    private float _distanceGrabObjectWithCameraWhenLooked = 1.0f;
    private AudioSource _audioSourcePlayer;
    private Collider _objectCollider = null;
    private LayerMask _layerMask;
    private Vector3 OffsetSpherCast = Vector3.zero;

    public void Init(PlayerData playerData,Camera _camera, CharacterController characterController)
    {
        _layerMask = playerData.LayerMask;
        _audioSourcePlayer = PlayerManager.Instance.PlayerController.GetComponent<AudioSource>();
        _playerData = playerData;
        _mouseSensitivityInteract = _playerData.MouseSensitivityInteract;
        _mainCamera = _camera;
        _playerAgentController = PlayerManager.Instance.PlayerController;
    }

    public void Enter(Collider interactObjectCollider)
    {
        _objectCollider = interactObjectCollider;
        _grabObjectCollider = _objectCollider;
        _grabObject = _objectCollider.transform.gameObject;
        _originPositionGrabObject = _grabObject.transform.position;
        _originRotationGrabObject = _grabObject.transform.rotation;
        _objectHolder = _playerAgentController.ObjectHolder;
        _grabObjectCollider.isTrigger = true;
        _grabObject.transform.SetParent(_objectHolder);
        _currentObjectInterract = _grabObject.GetComponent<InteractObject>();
        if (_currentObjectInterract.OnTakeObject != null)
        {
            _currentObjectInterract.OnTakeObject.Invoke();
        }
        if (_grabObject.GetComponent<Rigidbody>())
        {
            Rigidbody objectRb = _grabObject.GetComponent<Rigidbody>();
            objectRb.isKinematic = true;
        }
        if (_grabObject.GetComponent<InteractObject>())
        {
            Vector4 infoWhenLooked = _grabObject.GetComponent<InteractObject>().Interact();
            Vector3 grabObjectRotationWhenLooked = infoWhenLooked;
            _distanceGrabObjectWithCameraWhenLooked = infoWhenLooked.w;
            _grabObjectRotationWhenLooked = Quaternion.Euler(grabObjectRotationWhenLooked);
        }
        else
        {
            _distanceGrabObjectWithCameraWhenLooked = 1.0f;
            _grabObjectRotationWhenLooked = Quaternion.identity;
        }

        _grabObject.transform.localPosition = Vector3.zero;
        _objectHolder.transform.position = _mainCamera.transform.position + _mainCamera.transform.forward * _distanceGrabObjectWithCameraWhenLooked;
        _grabObject.transform.LookAt(_mainCamera.transform);
        _grabObject.transform.Rotate(_grabObjectRotationWhenLooked.eulerAngles);
        InputManager.Instance.MousePosition += LookObject;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(_currentObjectInterract.IsKey == true)
            {
                PlayerManager.Instance.HaveKey = true;
                PlayerManager.Instance.DestroyThis(_currentObjectInterract.gameObject);
            }
            _playerAgentController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        }
    }

    public void Exit()
    {
        _objectCollider.isTrigger = false;
        _grabObject.transform.SetParent(null);
        if (_currentObjectInterract.OnThrowObject != null)
        {
            _currentObjectInterract.OnThrowObject.Invoke();
        }
        if (_grabObject.GetComponent<Rigidbody>())
        {
            Rigidbody objectRb = _grabObject.GetComponent<Rigidbody>();
            objectRb.isKinematic = false;
        }
        _grabObject.transform.position = _originPositionGrabObject;
        _grabObject.transform.rotation = _originRotationGrabObject;
        _grabObject = null;
        InputManager.Instance.MousePosition -= LookObject;
    }

    private void LookObject(float mousePositionX, float mousePositionY)
    {
        float XaxisRotation = mousePositionX * _mouseSensitivityInteract;
        float YaxisRotation = mousePositionY * _mouseSensitivityInteract;
        _grabObject.transform.Rotate(_mainCamera.transform.up, -XaxisRotation, 0);
        _grabObject.transform.Rotate(_mainCamera.transform.right, YaxisRotation, 0);
    }
}
