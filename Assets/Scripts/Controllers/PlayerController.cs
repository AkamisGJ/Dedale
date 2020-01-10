using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region Fields
    private Vector3 direction = Vector3.zero;
    [SerializeField] private Transform _cameraHolder = null;
    [SerializeField] private AnimationCurve _accelerationCurve = null;
    private float _accelerationTime = 0.0f;
    [SerializeField] private float _timeMultiplier = 1.0f;
    [SerializeField] private float _moveSpeedHorizontal = 1;
    [SerializeField] private float _moveSpeedSide = 1;
    [SerializeField] private float _moveSpeedMultiplier = 1;
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
        _mainCamera.transform.rotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
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
        Vector3 preHorizontalMouvement = horizontalMouvement * transform.forward * _moveSpeedHorizontal;
        Vector3 preVerticalMouvement = verticalMouvement * transform.right * _moveSpeedSide;
        direction = (preVerticalMouvement + preHorizontalMouvement).normalized;
    }

    private void Update()
    {
        if (_currentState == MyState.Mouvement)
        {
            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 10.0f))
            {
                if (hit.transform.gameObject.layer == 10)
                {
                    Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.green);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.isTrigger = true;
                        _grabObject = hit.transform.gameObject;
                        _originPositionGrabObject = _grabObject.transform.position;
                        _originRotationGrabObject = _grabObject.transform.rotation;
                        _grabObject.transform.position = _mainCamera.transform.position + _mainCamera.transform.forward;
                        _grabObject.transform.rotation = Quaternion.identity;
                        _currentState = MyState.Observe;
                        InputManager.Instance.Direction -= SetDirection;
                        direction = Vector3.zero;
                        InputManager.Instance.MousePosition -= LookAtMouse;
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
                        _porte = _grabObject.GetComponent<Porte>();
                        InputManager.Instance.MousePosition += _porte.InteractPorte;
                        return;
                    }
                }
            }
            else Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.red);
        }
        if (_currentState == MyState.Observe)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.isTrigger = false;
                _grabObject.transform.position = _originPositionGrabObject;
                _grabObject.transform.rotation = _originRotationGrabObject;
                _currentState = MyState.Mouvement;
                InputManager.Instance.Direction += SetDirection;
                InputManager.Instance.MousePosition += LookAtMouse;
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

    private void OnDestroy()
    {
        InputManager.Instance.Direction -= SetDirection;
        InputManager.Instance.MousePosition -= LookAtMouse;
        InputManager.Instance.MousePosition -= LookObject;
        if(_porte != null)
        {
            InputManager.Instance.MousePosition -= _porte.InteractPorte;
        }
    }
}