using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [SerializeField] private PlayerData _playerData = null;
    [SerializeField] private Transform _cameraHolder = null;
    [SerializeField] private Transform _objectHolder = null;
    private Vector3 _direction = Vector3.zero;
    private float _timeCrouchTime = 0.0f;
    private float _moveSpeedHorizontal = 1;
    private float _currentAcceleration = 0;
    private float _accelerationLerp = 0;
    private float _currentSpeed = 0;
    private Camera _mainCamera = null;
    [SerializeField] private Rigidbody _rb = null;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationY = 0.0f;
    private float _rotationX = 0.0f;
    public enum MyState
    {
        Mouvement,
        Observe,
        Interaction,
    }
    private MyState _currentState = MyState.Mouvement;
    private Dictionary<MyState, IPlayerState> _states = null;
    private GameObject _grabObject = null;
    private Vector3 _originPositionGrabObject = Vector3.zero;
    private Quaternion _originRotationGrabObject = Quaternion.identity;
    private Porte _porte = null;
    private RaycastHit hit;
    private bool _isCrouch = false;
    private bool _crouching = false;
    private bool _unCrouching = false;
    private float _crouchSpeed = 1;
    private float _crouchLerp = 0;
    private float _maxSprint = 100;
    private float _sprintCurrentTime = 0;
    private float _speedSprint = 0;
    private CapsuleCollider _playerCapsuleCollider = null;
    private Quaternion _grabObjectRotationWhenLooked = Quaternion.identity;
    private float _distanceGrabObjectWithCameraWhenLooked = 1.0f;
    private Vector3 _lastDirection = Vector3.zero;
    private InteractObject _currentObjectInterract = null;
    [SerializeField] private AudioSource _audioSourcePlayer = null;
    private RaycastHit _raycastHit;
    #endregion Fields

    #region Properties
    public Camera MainCamera { set { _mainCamera = value; } }
    public Transform CameraHolder { get { return _cameraHolder; } set { _cameraHolder = value; } }
    public Transform ObjectHolder { get { return _objectHolder; } }
    public AudioSource AudioSourcePlayer { get { return _audioSourcePlayer; } set { _audioSourcePlayer = value; } }

    public RaycastHit RaycastHit { get { return _raycastHit; } set { _raycastHit = value; } }

    public GameObject GrabObject { get { return _grabObject; } set { _grabObject = value; } }
    #endregion Properties

    void Start()
    {
        _states = new Dictionary<MyState, IPlayerState>();
        _states.Add(MyState.Interaction, new IInteraction());
        _states.Add(MyState.Mouvement, new IMouvement());
        _states.Add(MyState.Observe, new IObserve());
        _currentState = MyState.Mouvement;
        //_states[_currentState].Enter(_grabObject);
        
        InputManager.Instance.Direction += SetDirection;
        InputManager.Instance.MousePosition += LookAtMouse;
        InputManager.Instance.Crouch += Crouch;
        InputManager.Instance.Sprint += Sprinting;
        
        _mainCamera.transform.rotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _crouchLerp = 0;
        _sprintCurrentTime = 0;
        _currentAcceleration = 0;
        _accelerationLerp = 0;
        _playerCapsuleCollider = transform.GetComponent<CapsuleCollider>();
        _states[MyState.Interaction].Init(_playerData, _mainCamera);
        _states[MyState.Mouvement].Init(_playerData, _mainCamera);
        _states[MyState.Observe].Init(_playerData, _mainCamera);
    }

    public void ChangeState(MyState nextState)
    {
        _states[_currentState].Exit();
        _states[nextState].Enter(_grabObject);
        _currentState = nextState;
    }

    public void DeadPlayer()
    {
    }
    /*
    private void Update()
    {
        _states[_currentState].Update();
    }
    */
    private void SetDirection(float horizontalMouvement, float verticalMouvement)
    {
        Vector3 preHorizontalMouvement = -horizontalMouvement * transform.forward;
        Vector3 preVerticalMouvement = verticalMouvement * transform.right;
        _direction = (preVerticalMouvement + preHorizontalMouvement).normalized;
        if (horizontalMouvement < 0)
        {
            _direction += transform.forward * _speedSprint;
            if (horizontalMouvement < 0)
            {
                _direction += transform.forward * _playerData.SpeedForward;
            }
            else
            {
                _direction -= transform.forward * _playerData.SpeedBack;
            }
        }
        if(verticalMouvement > 0)
        {
            _direction += transform.right * _playerData.MoveSpeedSide;
        }else if(verticalMouvement < 0)
        {
            _direction -= transform.right * _playerData.MoveSpeedSide;
        }
        if(_direction != Vector3.zero)
        {
            Acceleration();
        }
        else
        {
            _accelerationLerp = 0;
        }
    }
    
    private void Update()
    {
        if (_currentState == MyState.Mouvement)
        {
            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 10.0f))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("ObserveObject"))
                {
                    Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.green);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.isTrigger = true;
                        _grabObject = hit.transform.gameObject;
                        _grabObject.transform.SetParent(_objectHolder);
                        _currentObjectInterract = _grabObject.GetComponent<InteractObject>();
                        if(_currentObjectInterract.OnTakeObject != null && _audioSourcePlayer != null)
                        {
                            _audioSourcePlayer.clip =_currentObjectInterract.OnTakeObject;
                            _audioSourcePlayer.Play();
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
                        _originPositionGrabObject = _grabObject.transform.position;
                        _originRotationGrabObject = _grabObject.transform.rotation;
                        _grabObject.transform.localPosition = Vector3.zero;
                        _objectHolder.transform.position = _mainCamera.transform.position + _mainCamera.transform.forward * _distanceGrabObjectWithCameraWhenLooked;
                        _grabObject.transform.LookAt(_mainCamera.transform);
                        _grabObject.transform.Rotate(_grabObjectRotationWhenLooked.eulerAngles);
                        _currentState = MyState.Observe;
                        _rb.isKinematic = true;
                        InputManager.Instance.Direction -= SetDirection;
                        _direction = Vector3.zero;
                        InputManager.Instance.MousePosition -= LookAtMouse;
                        InputManager.Instance.Crouch -= Crouch;
                        InputManager.Instance.MousePosition += LookObject;
                        return;
                    }
                }
                if (hit.transform.gameObject.layer == 11)
                {
                    Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.magenta);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _grabObject = hit.transform.gameObject;
                        _currentState = MyState.Interaction;
                        _rb.isKinematic = true;
                        InputManager.Instance.Direction -= SetDirection;
                        _direction = Vector3.zero;
                        InputManager.Instance.MousePosition -= LookAtMouse;
                        InputManager.Instance.Crouch -= Crouch;
                        _porte = _grabObject.GetComponent<Porte>();
                        InputManager.Instance.MousePosition += _porte.InteractPorte;
                        return;
                    }
                }
            }
            else Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.red);
            if(_crouching == true)
            {
                Crouching(1);
            }
            if(_unCrouching == true)
            {
                Crouching(-1);
            }
        }
        if (_currentState == MyState.Observe)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.isTrigger = false;
                _grabObject.transform.SetParent(null);
                if (_currentObjectInterract.OnThrowObject != null && _audioSourcePlayer != null)
                {
                    _audioSourcePlayer.clip = _currentObjectInterract.OnThrowObject;
                    _audioSourcePlayer.Play();
                }
                if (_grabObject.GetComponent<Rigidbody>())
                {
                    Rigidbody objectRb = _grabObject.GetComponent<Rigidbody>();
                    objectRb.isKinematic = false;
                }
                _rb.isKinematic = false;
                _grabObject.transform.position = _originPositionGrabObject;
                _grabObject.transform.rotation = _originRotationGrabObject;
                _currentState = MyState.Mouvement;
                InputManager.Instance.Direction += SetDirection;
                InputManager.Instance.MousePosition += LookAtMouse;
                InputManager.Instance.Crouch += Crouch;
                InputManager.Instance.MousePosition -= LookObject;
            }
        }
        if (_currentState == MyState.Interaction)
        {
            if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.Mouse0) == false)
            {
                _currentState = MyState.Mouvement;
                _rb.isKinematic = false;
                InputManager.Instance.Direction += SetDirection;
                InputManager.Instance.MousePosition += LookAtMouse;
                InputManager.Instance.Crouch += Crouch;
                InputManager.Instance.MousePosition -= _porte.InteractPorte;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LookObject(float mousePositionX, float mousePositionY)
    {
        float XaxisRotation = mousePositionX * _playerData.MouseSensitivityInteract;
        float YaxisRotation = mousePositionY * _playerData.MouseSensitivityInteract;
        _grabObject.transform.Rotate(_mainCamera.transform.up, -XaxisRotation, 0);
        _grabObject.transform.Rotate(_mainCamera.transform.right, YaxisRotation, 0);
    }

    private void Acceleration()
    {
        _accelerationLerp += Time.deltaTime * _playerData.AccelerationTime;
        _accelerationLerp = Mathf.Clamp(_accelerationLerp, 0, _playerData.AccelerationCurve.length);
        _currentAcceleration = _playerData.AccelerationCurve.Evaluate(_accelerationLerp);
    }

    private void LookAtMouse(float mousePositionX, float mousePositionY)
    {
        _rotationX += mousePositionY * _playerData.SensitivityMouseX;
        _rotationY += mousePositionX * _playerData.SensitivityMouseY;
        _rotationX = Mathf.Clamp(_rotationX, -_playerData.AngleX, _playerData.AngleX);
        transform.localEulerAngles = new Vector3(0, _rotationY, 0);
        _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
    }

    private void Move()
    {
        //Debug.Log(direction.x * Time.deltaTime * _moveSpeedMultiplier + "  ,   " + direction.y * Time.deltaTime * _moveSpeedMultiplier + "  ,  " + direction.z * Time.deltaTime * _moveSpeedMultiplier);
        _rb.MovePosition(transform.position + _direction * _playerData.MoveSpeedMultiplier * _currentAcceleration);
        /*
            _rb.velocity = _direction * Time.deltaTime * _playerData.MoveSpeedMultiplier * _currentAcceleration;
            _lastDirection = _direction;
        */
    }
    
    private void Crouch(bool crouchBool)
    {
        if(crouchBool == true)
        {
            if (_isCrouch == false && _crouching == false)
            {
                _crouching = true;
                _unCrouching = false;
            }
        }
        if (crouchBool == false)
        {
            if (_unCrouching == false)
            {
                _crouching = false;
                _unCrouching = true;
            }
        }
    }

    private void Crouching(float inversion)
    {
        _timeCrouchTime += Time.deltaTime * inversion;
        _timeCrouchTime = Mathf.Clamp(_timeCrouchTime, 0, _playerData.CrouchCurve.length);
        _crouchLerp = _playerData.CrouchCurve.Evaluate(_timeCrouchTime);
        _playerCapsuleCollider.height = _crouchLerp;
        if (inversion < 0 && _crouchLerp == 0)
        {
            _isCrouch = false;
            _unCrouching = false;
        }
        if(inversion > 0 && _crouchLerp == _playerData.CrouchCurve.length)
        {
            _isCrouch = true;
            _crouching = false;
        }
    }
    
    private void Sprinting(bool isSprinting)
    {
        if(isSprinting == true)
        {
            if (_sprintCurrentTime > 0)
            {
                _sprintCurrentTime -= Time.deltaTime;
                _sprintCurrentTime = Mathf.Clamp(_sprintCurrentTime, 0, _playerData.SprintTimeMax);
                _speedSprint = _playerData.SpeedSprintMax;
            }
            else
            {
                _speedSprint = 0;
            }
        }

        if (isSprinting == false)
        {
            _speedSprint = 0;
            if (_sprintCurrentTime < _playerData.SprintTimeMax)
            {
                _sprintCurrentTime += Time.deltaTime;
                _sprintCurrentTime = Mathf.Clamp(_sprintCurrentTime, 0, _playerData.SprintTimeMax);
            }
        }
    }

    private void OnDestroy()
    {
        InputManager.Instance.Direction -= SetDirection;
        InputManager.Instance.MousePosition -= LookAtMouse;
        InputManager.Instance.MousePosition -= LookObject;
        InputManager.Instance.Crouch -= Crouch;
        InputManager.Instance.Sprint -= Sprinting;
        if (_porte != null)
        {
            InputManager.Instance.MousePosition -= _porte.InteractPorte;
        }
    }
    
}