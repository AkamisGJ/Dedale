using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region Fields
    private Vector3 direction = Vector3.zero;
    [SerializeField] private Transform _cameraHolder = null;
    [SerializeField] private Transform _objectHolder = null;
    [SerializeField] private AnimationCurve _accelerationCurve = null;
    private float _accelerationTime = 0.0f;
    [SerializeField] private float _timeMultiplier = 1.0f;
    private float _moveSpeedHorizontal = 1;
    [SerializeField] private float _speedForward = 1;
    [SerializeField] private float _speedBack = 0.5f;
    [SerializeField] private float _moveSpeedSide = 0.5f;
    [SerializeField] private float _moveSpeedMultiplier = 1;
    [SerializeField] private float _gravity = 1;
    private float _currentSpeed = 0;
    private Camera _mainCamera = null;
    [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private Animator _animator = null;
    private Vector3 _moveDirection = Vector3.zero;
    [SerializeField] private float _mouseSensitivityInteract = 1.0f;
    [SerializeField] private float _angleX = 60f;
    [SerializeField] private float _sensitivityMouseX = 1f;
    [SerializeField] private float _sensitivityMouseY = 1f;
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
    private float _sprintJauge = 100;
    [SerializeField] float _speedSprint = 2;
    [SerializeField] float _sprintJaugeDecrementation = 10;
    private CapsuleCollider _playerCapsuleCollider = null;
    private Quaternion _grabObjectRotationWhenLooked = Quaternion.identity;
    private float _distanceGrabObjectWithCameraWhenLooked = 1.0f;
    #endregion Fields

    #region Properties
    public Camera MainCamera { set { _mainCamera = value; } }

    public Animator Animator { get { return _animator; } set { _animator = value; } }

    public Transform CameraHolder { get { return _cameraHolder; } set { _cameraHolder = value; } }
    #endregion Properties

    void Start()
    {
        _states = new Dictionary<MyState, IPlayerState>();
        _states.Add(MyState.Interaction, new IInteraction());
        _states.Add(MyState.Mouvement, new IMouvement());
        _states.Add(MyState.Observe, new IObserve());
        _currentState = MyState.Mouvement;
        InputManager.Instance.Direction += SetDirection;
        InputManager.Instance.MousePosition += LookAtMouse;
        InputManager.Instance.Crouch += Crouch;
        InputManager.Instance.Sprint += Sprinting;
        _mainCamera.transform.rotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _crouchLerp = 0;
        _sprintJauge = 100;
        _playerCapsuleCollider = transform.GetComponent<CapsuleCollider>();
    }

    public void ChangeState(MyState nextState)
    {
        _states[_currentState].Exit();
        _states[nextState].Enter();
        _currentState = nextState;
    }

    public void DeadPlayer()
    {
    }

    private void SetDirection(float horizontalMouvement, float verticalMouvement)
    {
        Vector3 preHorizontalMouvement = -horizontalMouvement * transform.forward;
        Vector3 preVerticalMouvement = verticalMouvement * transform.right;
        direction = (preVerticalMouvement + preHorizontalMouvement).normalized;
        if (horizontalMouvement < 0)
        {
            direction += transform.forward * _speedSprint;
            if (horizontalMouvement < 0)
            {
                direction += transform.forward * _speedForward;
            }
            else
            {
                direction -= transform.forward * _speedBack;
            }
        }
        if(verticalMouvement > 0)
        {
            direction += transform.right * _moveSpeedSide;
        }else if(verticalMouvement < 0)
        {
            direction -= transform.right * _moveSpeedSide;
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
                        _currentState = MyState.Observe;
                        _rb.isKinematic = true;
                        InputManager.Instance.Direction -= SetDirection;
                        direction = Vector3.zero;
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
                        direction = Vector3.zero;
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
        float XaxisRotation = mousePositionX * _mouseSensitivityInteract;
        float YaxisRotation = mousePositionY * _mouseSensitivityInteract;
        _grabObject.transform.Rotate(_mainCamera.transform.up, -XaxisRotation, 0);
        _grabObject.transform.Rotate(_mainCamera.transform.right, YaxisRotation, 0);
    }

    private void LookAtMouse(float mousePositionX, float mousePositionY)
    {
        _rotationX += mousePositionY * _sensitivityMouseX;
        _rotationY += mousePositionX * _sensitivityMouseY;
        _rotationX = Mathf.Clamp(_rotationX, -_angleX, _angleX);
        transform.localEulerAngles = new Vector3(0, _rotationY, 0);
        _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
    }

    private void Move()
    {
        //Debug.Log(direction.x * Time.deltaTime * _moveSpeedMultiplier + "  ,   " + direction.y * Time.deltaTime * _moveSpeedMultiplier + "  ,  " + direction.z * Time.deltaTime * _moveSpeedMultiplier);
        _rb.MovePosition(transform.position + direction * Time.deltaTime * _moveSpeedMultiplier);
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
        _crouchLerp += inversion * Time.deltaTime * _crouchSpeed;
        _crouchLerp = Mathf.Clamp(_crouchLerp, 0, 1);
        _playerCapsuleCollider.height = Mathf.Lerp(2, 1f, _crouchLerp);
        if (inversion < 0 && _crouchLerp == 0)
        {
            _isCrouch = false;
            _unCrouching = false;
        }
        if(inversion > 0 && _crouchLerp == 1)
        {
            _isCrouch = true;
            _crouching = false;
        }
    }
    
    private void Sprinting(bool isSprinting)
    {
        if(isSprinting == true)
        {
            if (_sprintJauge > 0)
            {
                _sprintJauge -= _sprintJaugeDecrementation;
                _speedSprint = 2;
            }
            else
            {
                _speedSprint = 0;
            }
        }

        if (isSprinting == false)
        {
            _speedSprint = 0;
            if (_sprintJauge < 100)
            {
                _sprintJauge += _sprintJaugeDecrementation;
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