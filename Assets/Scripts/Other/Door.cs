using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    private Rigidbody _rb = null;
    private HingeJoint _hj = null;
    private float _initalPositionMouseX = 0;
    [Tooltip("Speed of rotation of door")]
    [SerializeField] private float _speed = 1;
    [SerializeField] private bool _inverseRotation = false;
    private JointLimits _limits;
    [SerializeField] private float _bounciness = 0.05f;
    [SerializeField] private bool _needKey = false;
    [SerializeField] private float minLimit = -90f;
    [SerializeField] private float maxLimit = 90f;
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
    public bool NeedKey { get => _needKey; }

    void Start()
    {
        int _inversion = 1;
        _rb = GetComponent<Rigidbody>();
        _hj = GetComponent<HingeJoint>();
        _limits.bounciness = _bounciness;
        _hj.limits = _limits;
        _playerController = PlayerManager.Instance.PlayerController;
        _playerData = _playerController.PlayerData;
        _mainCamera = PlayerManager.Instance.CameraPlayer;
        _startTransformDoor = transform;
    }

    public void Enter()
    {
        _rotationY = _playerController.gameObject.transform.localEulerAngles.y;
        IMouvement mouvement = _playerController.States[PlayerAgentController.MyState.MOVEMENT] as IMouvement;
        _rotationX = mouvement.RotationX;
        UnClick();
        OnClick();
        JointMotor motor = _hj.motor;
        _hj.useMotor = true;
        motor.freeSpin = false;
        _limits.bounciness = 0f;
        _limits.min = minLimit;
        _limits.max = maxLimit;
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
        UnClick();
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
        if (_needKey == false || (_needKey == true && PlayerManager.Instance.HaveKey == true))
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

    void OnClick()
    {
        //transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    void UnClick()
    {
        //transform.GetComponent<MeshRenderer>().material.color = Color.magenta;
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
            _rotationX += (_currentMouseY + mousePositionY * _playerData.SensitivityMouseX) * _playerData.SpeedCameraDoor;
            _rotationY += (_currentMouseX + mousePositionX * _playerData.SensitivityMouseY) * _playerData.SpeedCameraDoor;
            _rotationX = Mathf.Clamp(_rotationX, -_playerData.AngleX, _playerData.AngleX);
            _playerController.gameObject.transform.localEulerAngles = new Vector3(0, _rotationY, 0);
            _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
        }
    }
}