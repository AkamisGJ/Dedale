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
    private Light _lightPlayer = null;
    private float _lerpObject = 0;
    private bool _comeToPlayer = false;
    private Quaternion _quaternionExitObserve = Quaternion.identity;
    private Quaternion _quaternionEnterObserve = Quaternion.identity;

    public void Init(PlayerData playerData,Camera _camera, CharacterController characterController)
    {
        _layerMask = playerData.LayerMask;
        _audioSourcePlayer = PlayerManager.Instance.PlayerController.GetComponent<AudioSource>();
        _playerData = playerData;
        _mouseSensitivityInteract = _playerData.MouseSensitivityInteract;
        _mainCamera = _camera;
        _playerAgentController = PlayerManager.Instance.PlayerController;
        _lightPlayer = _mainCamera.GetComponent<Light>();
        _lightPlayer.enabled = false;
    }

    public void Enter(Collider interactObjectCollider, string animation)
    {
        _lightPlayer.enabled = true;
        _objectCollider = interactObjectCollider;
        _grabObjectCollider = _objectCollider;
        _grabObject = _objectCollider.transform.gameObject;
        _originPositionGrabObject = _grabObject.transform.position;
        _originRotationGrabObject = _grabObject.transform.rotation;
        _objectHolder = _playerAgentController.ObjectHolder;
        _objectHolder.transform.position = _grabObject.transform.position;
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
        _lerpObject = 0;
        //_grabObject.transform.localPosition = Vector3.zero;
        _comeToPlayer = true;
        //_objectHolder.transform.position = _mainCamera.transform.position + _mainCamera.transform.forward * _distanceGrabObjectWithCameraWhenLooked;
        //_grabObject.transform.LookAt(_mainCamera.transform);
        //_grabObject.transform.Rotate(_grabObjectRotationWhenLooked.eulerAngles);
        _quaternionEnterObserve.eulerAngles = _objectHolder.rotation.eulerAngles + _grabObjectRotationWhenLooked.eulerAngles + new Vector3 (-_mainCamera.transform.rotation.eulerAngles.x, 180, 0);
        //_grabObject.transform.rotation = _originRotationGrabObject;
        //InputManager.Instance.MousePosition += LookObject;
        GameLoopManager.Instance.LoopQTE += LerpingGrabObject;
        _playerAgentController.AnimatorCamera.SetBool("Idle", true);
        _playerAgentController.AnimatorCamera.SetBool("Walk", false);
        _playerAgentController.AnimatorCamera.SetBool("Run", false);
        _playerAgentController.AnimatorCamera.SetBool("NoAnim", false);
    }

    private void LerpingGrabObject()
    {
        float inversion = 0;
        inversion = _comeToPlayer == true ? 1 : -1;
        _lerpObject = Mathf.Clamp(_lerpObject + (Time.deltaTime * inversion * _playerData.SpeedLerp), 0, 1);
        _objectHolder.transform.position = Vector3.Lerp(_originPositionGrabObject, _mainCamera.transform.position + _mainCamera.transform.forward * _distanceGrabObjectWithCameraWhenLooked, _lerpObject);
        if(_comeToPlayer == false)
        {
            _grabObject.transform.rotation = Quaternion.Lerp(_originRotationGrabObject, _quaternionExitObserve, _lerpObject);
        }
        else
        {
            _grabObject.transform.rotation = Quaternion.Lerp(_originRotationGrabObject, _quaternionEnterObserve, _lerpObject);
        }
        if(_lerpObject == 1)
        {
            InputManager.Instance.MousePosition += LookObject;
            GameLoopManager.Instance.LoopQTE -= LerpingGrabObject;
            GameLoopManager.Instance.LateGameLoop += OnLateUpdate;
        }
        else if (_lerpObject == 0)
        {
            GameLoopManager.Instance.LoopQTE -= LerpingGrabObject;
            _playerAgentController.ChangeState(PlayerAgentController.MyState.MOVEMENT, null, "Idle");
        }
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
            _comeToPlayer = false;
            if (_currentObjectInterract != null)
            {
                _quaternionExitObserve = _currentObjectInterract.transform.rotation;
                GameLoopManager.Instance.LoopQTE += LerpingGrabObject;
                GameLoopManager.Instance.LateGameLoop -= OnLateUpdate;
            }
            else
            {
                _playerAgentController.ChangeState(PlayerAgentController.MyState.MOVEMENT, null,"Idle");
            }
        }
    }

    private void OnLateUpdate()
    {
        _objectHolder.transform.position = _mainCamera.transform.position + _mainCamera.transform.forward * _distanceGrabObjectWithCameraWhenLooked;
    }

    public void Exit()
    {
        InputManager.Instance.MousePosition -= LookObject;
        _lightPlayer.enabled = false;
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
        //_grabObject.transform.position = _originPositionGrabObject;
        //_grabObject.transform.rotation = _originRotationGrabObject;
        _grabObject = null;
        _playerAgentController.AnimatorCamera.SetBool("Idle", true);
        _playerAgentController.AnimatorCamera.SetBool("Walk", false);
        _playerAgentController.AnimatorCamera.SetBool("Run", false);
        _playerAgentController.AnimatorCamera.SetBool("NoAnim", false);
    }

    private void LookObject(float mousePositionX, float mousePositionY)
    {
        if(_grabObject != null)
        {
            float XaxisRotation = mousePositionX * _mouseSensitivityInteract;
            float YaxisRotation = mousePositionY * _mouseSensitivityInteract;
            _grabObject.transform.Rotate(_mainCamera.transform.up, -XaxisRotation, 0);
            _grabObject.transform.Rotate(_mainCamera.transform.right, YaxisRotation, 0);
        }
    }
}