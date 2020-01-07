using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    #region Fields
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float _acceleration = 0.5f;
    private Camera _mainCamera = null;
    private float _currentTime = 0.0f;
    [SerializeField] private float _timeToWait = 0.5f;
    [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private Animator _animator = null;
    private Vector3 _moveDirection = Vector3.zero;
    [SerializeField] private float _mouseSensitivity = 1.0f;
    [SerializeField] private float _maxVelocity = 2.0f;
    [SerializeField] private float _angleX = 60f;
    [SerializeField] private float _sensitivityMouseX = 10f;
    [SerializeField] private float _sensitivityMouseY = 10f;
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
    #endregion Fields

    #region Properties
    public Camera MainCamera { set { _mainCamera = value; } }

    public Animator Animator { get { return _animator; } set { _animator = value; } }
    #endregion Properties

    void Start()
    {
        InputManager.Instance.Direction += Move;
        InputManager.Instance.MousePosition += LookAtMouse;
        _currentTime = _timeToWait;
        _mainCamera.transform.rotation = transform.rotation;
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

    private void Move(float horizontalMouvement, float verticalMouvement)
    {
        _moveDirection = (verticalMouvement * transform.right + horizontalMouvement * transform.forward).normalized;
        Vector3 yVel = new Vector3(0, _rb.velocity.y, 0);
        _rb.velocity = _moveDirection * _moveSpeed * Time.deltaTime;
        _rb.velocity += yVel;
    }

    private void LookAtMouse(Vector3 mouseDirection)
    {
        _rotationX = mouseDirection.y * _sensitivityMouseX;
        _rotationY = mouseDirection.x * _sensitivityMouseY;
        _rotationX = Mathf.Clamp(_rotationX, -_angleX, _angleX);
        transform.localEulerAngles = new Vector3(0, _rotationY, 0);
        _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
    }

    private void OnDestroy()
    {
        InputManager.Instance.Direction -= Move;
        InputManager.Instance.MousePosition -= LookAtMouse;
    }

    private void TimerShoot()
    {
        _currentTime += Time.deltaTime;
    }
}