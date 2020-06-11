using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private HingeJoint _hj = null;
    private float _initalPositionMouseX = 0;
    [Tooltip("Speed of rotation of door")]
    [SerializeField] private float _speed = 1;
    [SerializeField] private bool _inverseRotation = false;
    private JointLimits _limits;
    [SerializeField] private float _bounciness = 0.05f;
    [SerializeField] private bool _isLocked = false;
    [SerializeField] private float _minLimit = -90f;
    [SerializeField] private float _maxLimit = 90f;
    private Transform _startTransformDoor = null;
    private int _inversion = 1;
    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _currentMouseY = 0;
    private float _currentMouseX = 0;
    private PlayerAgentController _playerController = null;
    private PlayerData _playerData = null;
    private Camera _mainCamera = null;
    private float _inversionSense = 1;
    private float _startLockAngleDoor = 0;
    private float _lerpCloseDoor = 0;
    [SerializeField] private float _speedCloseDoor = 10;
    public bool IsLocked { get => _isLocked; set => LockDoor(value); }

    void Start()
    {
        int _inversion = 1;
        if(_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
            Debug.LogWarning(" Insert rigidbody on door script to optimise  :   " + gameObject);
        }
        if(_hj == null)
        {
            _hj = GetComponent<HingeJoint>();
            Debug.LogWarning(" Insert hingeJoint on door script to optimise  :   " + gameObject);
        }
        _limits.bounciness = _bounciness;
        _hj.limits = _limits;
        _playerController = PlayerManager.Instance.PlayerController;
        _playerData = _playerController.PlayerData;
        _mainCamera = PlayerManager.Instance.CameraPlayer;
        _startTransformDoor = transform;
    }

    private void LockDoor (bool isLocked)
    {
        _isLocked = isLocked;
        if(_isLocked == true)
        {
            _startLockAngleDoor = _hj.angle;
            _lerpCloseDoor = 0;
            JointMotor motor = _hj.motor;
            _hj.useMotor = true;
            motor.freeSpin = false;
            _limits.bounciness = 0f;
            _limits.min = _minLimit;
            _limits.max = _maxLimit;
            _hj.limits = _limits;
            _hj.motor = motor;
            motor.force = _speed;
            GameLoopManager.Instance.GameLoopModifyVolume += LockDoorAnim;
        }
    }

    private void LockDoorAnim()
    {
        JointMotor motor = _hj.motor;
        if(_hj.angle > 0)
        {
            _inversion = -1;
        }else if(_hj.angle < 0)
        {
            _inversion = 1;
        }
        _speed = Mathf.Abs(_hj.angle);
        motor.targetVelocity = _speed * _inversion * _inversionSense * _speedCloseDoor;
        _hj.motor = motor;
        if (Mathf.Abs(_hj.angle) <= 0.1f)
        {
            _hj.useMotor = false;
            GameLoopManager.Instance.GameLoopModifyVolume -= LockDoorAnim;
        }
    }

    public void SetInverseRotation(bool state){
        _inverseRotation = state;
        if (_inverseRotation == true)
        {
            _inversionSense = -1;
        }
        else
        {
            _inversionSense = 1;
        }
    }

    public void Enter()
    {
        _rotationY = _playerController.gameObject.transform.localEulerAngles.y;
        IMouvement mouvement = _playerController.States[PlayerAgentController.MyState.MOVEMENT] as IMouvement;
        _rotationX = mouvement.RotationX;
        JointMotor motor = _hj.motor;
        _hj.useMotor = true;
        motor.freeSpin = false;
        _limits.bounciness = 0f;
        _limits.min = _minLimit;
        _limits.max = _maxLimit;
        _hj.limits = _limits;
        Transform playerTransform = PlayerManager.Instance.PlayerController.transform;
        Vector3 vectorDoorPlayer = playerTransform.position - _startTransformDoor.position;
        float anglePortePlayer = Vector3.Dot(transform.right, vectorDoorPlayer);
        if (_inverseRotation == true)
        {
            _inversionSense = -1;
        }
        else
        {
            _inversionSense = 1;
        }
        if (anglePortePlayer < 0)
        {
            _inversion = -1;
        }
        else
        {
            _inversion = 1;
        }
        InputManager.Instance.MousePosition += LookAtMouse;
    }

    public void Exit()
    {
        IMouvement mouvement = _playerController.States[PlayerAgentController.MyState.MOVEMENT] as IMouvement;
        mouvement.RotationX = _rotationX;
        JointMotor motor = _hj.motor;
        motor.freeSpin = true;
        _hj.useMotor = false;
        _limits.bounciness = _bounciness;
        _hj.limits = _limits;
        InputManager.Instance.MousePosition -= LookAtMouse;
    }

    public void Interact(float mousePositionX, float mousePositionY)
    {
        if (_isLocked == false)
        {
            JointMotor motor = _hj.motor;
            if (mousePositionY < 0)
            {
                motor.force = -mousePositionY * _speed;
            }
            if (mousePositionY > 0)
            {
                motor.force = mousePositionY * _speed;
            }
            motor.targetVelocity = mousePositionY * _speed * _inversion * _inversionSense;
            _hj.motor = motor;
        }
    }

    private void LookAtMouse(float mousePositionX, float mousePositionY)
    {
        if (_playerController.CanMove == true && _playerController != null)
        {
            if (mousePositionX > 0)
            {
                _currentMouseX += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if (mousePositionX < 0)
            {
                _currentMouseX -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if (mousePositionX == 0 && _currentMouseX > 0)
            {
                _currentMouseX -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, 0, _playerData.StackMovement);
            }
            else if (mousePositionX == 0 && _currentMouseX < 0)
            {
                _currentMouseX += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, -_playerData.StackMovement, 0);
            }
            if (mousePositionY > 0)
            {
                _currentMouseY += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseY = Mathf.Clamp(_currentMouseY, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if (mousePositionY < 0)
            {
                _currentMouseY -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseY = Mathf.Clamp(_currentMouseY, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if (mousePositionY == 0 && _currentMouseY > 0)
            {
                _currentMouseY -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseY = Mathf.Clamp(_currentMouseY, 0, _playerData.StackMovement);
            }
            else if (mousePositionY == 0 && _currentMouseY < 0)
            {
                _currentMouseY += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseY = Mathf.Clamp(_currentMouseY, -_playerData.StackMovement, 0);
            }
            _rotationX += (_currentMouseY + mousePositionY * _playerData.SensitivityMouseX) * _playerData.SpeedCameraDoor * PlayerManager.Instance.MouseSensitivityMultiplier;
            _rotationY += (_currentMouseX + mousePositionX * _playerData.SensitivityMouseY) * _playerData.SpeedCameraDoor * PlayerManager.Instance.MouseSensitivityMultiplier;
            _rotationX = Mathf.Clamp(_rotationX, -_playerData.AngleX, _playerData.AngleX);
            _playerController.gameObject.transform.localEulerAngles = new Vector3(0, _rotationY, 0);
            _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
        }
    }

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= LockDoorAnim;
        }
    }
}