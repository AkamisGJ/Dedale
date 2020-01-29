using UnityEngine;
using UnityEngine.AI;

public class IMouvement : IPlayerState
{
    private CharacterController _characterController = null;
    private PlayerAgentController _playerController = null;
    private GameObject _grabObject = null;
    private Vector3 _originPositionGrabObject = Vector3.zero;
    private Quaternion _originRotationGrabObject = Quaternion.identity;
    private Camera _mainCamera = null;
    private float _baseYcamera = 0;
    private PlayerData _playerData = null;
    private Vector3 _direction = Vector3.zero;
    private float _speedSprint = 0;
    private float _currentAcceleration = 0;
    private float _accelerationLerp = 0;
    private float _rotationY = 0.0f;
    private float _rotationX = 0.0f;
    private bool _isCrouch = false;
    private bool _crouching = false;
    private bool _unCrouching = false;
    private float _timeCrouchTime = 0.0f;
    private float _crouchLerp = 0;
    private CapsuleCollider _playerCapsuleCollider = null;
    private float _sprintCurrentTime = 0;
    private PlayerAgentController.MyState nextState = PlayerAgentController.MyState.Mouvement;
    private RaycastHit _raycastHit;
    private float _gravity = 9;
    private float _currentGravity = 0;
    private float _maxGravity = 45;

    public void Init(PlayerData playerData,Camera camera, CharacterController characterController)
    {
        _characterController = characterController;
        _playerData = playerData;
        _gravity = _playerData.Gravity;
        _maxGravity = _playerData.MaxGravity;
        _playerController = PlayerManager.Instance.Player;
        _mainCamera = camera;
        _playerCapsuleCollider = _playerController.gameObject.GetComponent<CapsuleCollider>();
        _crouchLerp = 0;
        _sprintCurrentTime = 0;
        _currentAcceleration = 0;
        _accelerationLerp = 0;
        _isCrouch = false;
        _crouching = false;
        _unCrouching = false;
    }

    public void Enter()
    {
        _baseYcamera = _mainCamera.transform.localPosition.y;
        InputManager.Instance.Crouch += Crouch;
        InputManager.Instance.Sprint += Sprinting;
        InputManager.Instance.MousePosition += LookAtMouse;
        InputManager.Instance.Direction += SetDirection;
    }

    public void Update()
    {
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out _raycastHit, 10.0f))
        {
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("ObserveObject"))
            {
                Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.green);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _playerController.ChangeState(PlayerAgentController.MyState.Observe);
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == 11)
            {
                Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.magenta);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _grabObject = _raycastHit.transform.gameObject;
                    _playerController.ChangeState(PlayerAgentController.MyState.Interaction);
                    return;
                }
            }
        }
        else Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.red);
        if (_crouching == true)
        {
            Crouching(1);
        }
        if (_unCrouching == true)
        {
            Crouching(-1);
        }
    }

    public void Exit()
    {
        InputManager.Instance.MousePosition -= LookAtMouse;
        InputManager.Instance.Direction -= SetDirection;
        InputManager.Instance.Crouch -= Crouch;
        InputManager.Instance.Sprint -= Sprinting;
    }

    private void Move()
    {
        if (_characterController.isGrounded)
        {
            _currentGravity = _gravity;
        }
        else
        {
            _currentGravity += _gravity/_currentGravity;
            _currentGravity = Mathf.Clamp(_currentGravity, _gravity, _maxGravity);
        }
        _direction.y -= _currentGravity;
        float desiredMoveX = _direction.x * _playerData.MoveSpeedMultiplier * _currentAcceleration * Time.deltaTime;
        float desiredMoveZ = _direction.z * _playerData.MoveSpeedMultiplier * _currentAcceleration * Time.deltaTime;
        float desiredMoveY = _direction.y * Time.deltaTime;
        Vector3 desiredMove = new Vector3(desiredMoveX, desiredMoveY, desiredMoveZ);
        _characterController.Move(desiredMove);
    }

    private void SetDirection(float horizontalMouvement, float verticalMouvement)
    {
        Vector3 preHorizontalMouvement = horizontalMouvement * _playerController.transform.forward;
        Vector3 preVerticalMouvement = verticalMouvement * _playerController.transform.right;
        _direction = (preVerticalMouvement + preHorizontalMouvement).normalized;
        if (horizontalMouvement > 0)
        {
            _direction += _playerController.transform.forward * _speedSprint;
            if (horizontalMouvement > 0)
            {
                _direction += _playerController.transform.forward * _playerData.SpeedForward;
            }
            else
            {
                _direction -= _playerController.transform.forward * _playerData.SpeedBack;
            }
        }
        if (verticalMouvement > 0)
        {
            _direction += _playerController.transform.right * _playerData.MoveSpeedSide;
        }
        else if (verticalMouvement < 0)
        {
            _direction -= _playerController.transform.right * _playerData.MoveSpeedSide;
        }
        if (_direction != Vector3.zero)
        {
            Acceleration();
        }
        else
        {
            _accelerationLerp = 0;
        }
        Move();
    }

    private void LookAtMouse(float mousePositionX, float mousePositionY)
    {
        _rotationX += mousePositionY * _playerData.SensitivityMouseX;
        _rotationY += mousePositionX * _playerData.SensitivityMouseY;
        _rotationX = Mathf.Clamp(_rotationX, -_playerData.AngleX, _playerData.AngleX);
        _playerController.gameObject.transform.localEulerAngles = new Vector3(0, _rotationY, 0);
        _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
    }

    private void Acceleration()
    {
        _accelerationLerp += Time.deltaTime * _playerData.AccelerationTime;
        _accelerationLerp = Mathf.Clamp(_accelerationLerp, 0, _playerData.AccelerationCurve.length);
        _currentAcceleration = _playerData.AccelerationCurve.Evaluate(_accelerationLerp);
    }

    private void Crouch(bool crouchBool)
    {
        if (crouchBool == true)
        {
            if (_isCrouch == false && _crouching == false)
            {
                _baseYcamera = _mainCamera.transform.position.y;
                _crouching = true;
                _unCrouching = false;
            }
        }
        if (crouchBool == false && _crouching == true || _isCrouch == true)
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
        _characterController.height = _crouchLerp;
        if (inversion < 0 && _crouchLerp == 0)
        {
            _isCrouch = false;
            _unCrouching = false;
        }
        if (inversion > 0 && _crouchLerp == _playerData.CrouchCurve.length)
        {
            _isCrouch = true;
            _crouching = false;
        }
    }

    private void Sprinting(bool isSprinting)
    {
        if (isSprinting == true)
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
}