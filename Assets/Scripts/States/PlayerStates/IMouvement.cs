using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class IMouvement : IPlayerState
{
    #region Fields
    private CharacterController _characterController = null;
    private PlayerAgentController _playerController = null;
    private LayerMask _layerMask;
    private Vector3 _originPositionGrabObject = Vector3.zero;
    private Quaternion _originRotationGrabObject = Quaternion.identity;
    private Camera _mainCamera = null;
    private float _baseYcamera = 0;
    private PlayerData _playerData = null;
    private Vector3 _direction = Vector3.zero;
    private float _speedSprint = 0;
    private float _currentAcceleration = 0;
    private float _currentAccelerationSprint = 0;
    private float _accelerationLerp = 0;
    private float _accelerationSprintLerp = 0;
    private float _rotationY = 0.0f;
    private float _rotationX = 0.0f;
    private bool _isCrouch = false;
    private bool _crouching = false;
    private bool _unCrouching = false;
    private bool _isZoom = false;
    private bool _zooming = false;
    private bool _unZooming = false;
    private float _timeCrouch = 0.0f;
    private float _timeZoom = 0.0f;
    private float _crouchLerp = 0;
    private float _zoomLerp = 0;
    private float _sprintCurrentTime = 0;
    private PlayerAgentController.MyState nextState = PlayerAgentController.MyState.MOVEMENT;
    private RaycastHit _raycastHit;
    private float _gravity = 9;
    private float _currentGravity = 0;
    private float _maxGravity = 45;
    private Vector3 _directionHorinzontal = Vector3.zero;
    private Vector3 _directionVertical = Vector3.zero;
    private GameObject enableHightLightObject = null;
    private Vector3 _moveModifier = Vector3.zero;
    private float _useGravity = 1;
    private float _blendValue = 0;
    private float _blendLerp = 0;
    private Vector3 OffsetSpherCast = Vector3.zero;
    private float _currentMouseX = 0;
    private float _currentMouseY = 0;
    #endregion Fields

    #region Properties
    public Vector3 MoveModifier { get => _moveModifier; set { _moveModifier = value; Debug.Log(value); } }
    public float UseGravity { get => _useGravity; set => _useGravity = value; }
    public bool IsCrouch { get => _isCrouch; }
    public float SpeedSprint { get => _speedSprint; }
    #endregion Properties

    public void Init(PlayerData playerData,Camera camera, CharacterController characterController)
    {
        _currentAccelerationSprint = 0;
        _blendValue = 0;
        _layerMask = playerData.LayerMask;
        _characterController = characterController;
        _playerData = playerData;
        _gravity = _playerData.Gravity;
        _maxGravity = _playerData.MaxFallGravity;
        _playerController = PlayerManager.Instance.PlayerController;
        _mainCamera = camera;
        _crouchLerp = 0;
        _sprintCurrentTime = 0;
        _currentAcceleration = 0;
        _accelerationLerp = 0;
        _speedSprint = playerData.MaxSprintSpeed;
        _isCrouch = false;
        _crouching = false;
        _unCrouching = false;
        _characterController.slopeLimit = _playerData.SlopeLimit;
        _characterController.stepOffset = _playerData.StepOffset;
        _directionVertical = Vector3.zero;
        _directionHorinzontal = Vector3.zero;
        _moveModifier = Vector3.zero;
    }

    public void Enter(Collider collider)
    {
        _playerController.CanMove = true;
        if(_mainCamera.transform.localEulerAngles.x < _playerData.AngleX)
        {
            _rotationX = -_mainCamera.transform.localEulerAngles.x;
        }
        else
        {
            _rotationX = -_mainCamera.transform.localEulerAngles.x + 360;
        }
        _rotationY = _playerController.gameObject.transform.localEulerAngles.y;
        _baseYcamera = _mainCamera.transform.localPosition.y;
        _timeZoom = 0;
        InputManager.Instance.Crouch += Crouch;
        InputManager.Instance.Sprint += Sprinting;
        InputManager.Instance.MousePosition += LookAtMouse;
        InputManager.Instance.Direction += SetDirection;
        //InputManager.Instance.Zoom += Zoom;
    }

    public void Update()
    {
        //RaycastInteractionObject();
        SphereCastInteractionObject();
        if (_crouching == true)
        {
            Crouching(1);
        }
        if (_unCrouching == true)
        {
            Crouching(-1);
        }
        /*if (_zooming == true)
        {
            _playerController.Zooming(1);
        }
        if (_unZooming == true)
        {
            _playerController.Zooming(-1);
        }*/
    }

    public void Exit()
    {
        InputManager.Instance.MousePosition -= LookAtMouse;
        InputManager.Instance.Direction -= SetDirection;
        InputManager.Instance.Crouch -= Crouch;
        InputManager.Instance.Sprint -= Sprinting;
        //InputManager.Instance.Zoom -= Zoom;
    }

    private void RaycastInteractionObject()
    {
        if (_mainCamera != null && Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out _raycastHit, _playerData.MaxDistanceInteractionObject, _layerMask))
        {
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("ObserveObject"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    _playerController.ChangeState(PlayerAgentController.MyState.OBSERVE);
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("InteractObject"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    Door interact = _raycastHit.transform.gameObject.GetComponent<IInteract>() as Door;
                    if ((interact.NeedKey == true && PlayerManager.Instance.HaveKey == true) || interact.NeedKey == false)
                    {
                        _raycastHit.transform.gameObject.GetComponent<IInteract>().Enter();
                        _playerController.ChangeState(PlayerAgentController.MyState.INTERACTION);
                    }
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    _playerController.CanMove = false; //_canMove = false;
                    GameLoopManager.Instance.LoopQTE += _raycastHit.transform.GetComponent<StartLadder>().StartPositionPlayer;
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("NarrowWay"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    _timeCrouch = 0;
                    _playerController.CanMove = false; //_canMove = false;
                    GameLoopManager.Instance.LoopQTE += _raycastHit.transform.GetComponent<NarrowWayTrigger>().StartPositionPlayer;
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Liana"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    _timeCrouch = 0;
                    _playerController.CanMove = false; //_canMove = false;
                    GameLoopManager.Instance.LoopQTE += _raycastHit.transform.GetComponent<LianaTrigger>().StartPositionPlayer;
                    return;
                }
            }
        }
    }

    private void SphereCastInteractionObject()
    {
        OffsetSpherCast = _mainCamera.transform.position - _mainCamera.transform.forward * _playerData.RayonInteraction;
        if(_mainCamera != null && Physics.SphereCast(OffsetSpherCast, _playerData.RayonInteraction,_mainCamera.transform.forward, out _raycastHit, _playerData.MaxDistanceInteractionObject, _layerMask))
        {
            if (enableHightLightObject != _raycastHit.collider.gameObject)
            {
                if (enableHightLightObject != null)
                {
                    enableHightLightObject.transform.GetComponent<ImageInteract>().IsFocus = false;
                }
                enableHightLightObject = _raycastHit.collider.gameObject;
                enableHightLightObject.transform.GetComponent<ImageInteract>().IsFocus = true;
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("ObserveObject"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    _playerController.ChangeState(PlayerAgentController.MyState.OBSERVE, _raycastHit.collider);
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("InteractObject"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    Door interact = _raycastHit.transform.gameObject.GetComponent<IInteract>() as Door;
                    if ((interact.NeedKey == true && PlayerManager.Instance.HaveKey == true) || interact.NeedKey == false)
                    {
                        _raycastHit.transform.gameObject.GetComponent<IInteract>().Enter();
                        _playerController.ChangeState(PlayerAgentController.MyState.INTERACTION, _raycastHit.collider);
                    }
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Ladder"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    _playerController.CanMove = false; //_canMove = false;
                    GameLoopManager.Instance.LoopQTE += _raycastHit.transform.GetComponent<StartLadder>().StartPositionPlayer;
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("NarrowWay"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    _timeCrouch = 0;
                    _playerController.CanMove = false; //_canMove = false;
                    GameLoopManager.Instance.LoopQTE += _raycastHit.transform.GetComponent<NarrowWayTrigger>().StartPositionPlayer;
                    return;
                }
            }
            if (_raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Liana"))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _blendValue = 0;
                    _timeCrouch = 0;
                    _playerController.CanMove = false; //_canMove = false;
                    GameLoopManager.Instance.LoopQTE += _raycastHit.transform.GetComponent<LianaTrigger>().StartPositionPlayer;
                    return;
                }
            }
        }
        else
        {
            if (enableHightLightObject != null)
            {
                enableHightLightObject.transform.GetComponent<ImageInteract>().IsFocus = false;
                enableHightLightObject = null;
            }
        }
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
        _direction.y -= _currentGravity * _useGravity;
        float desiredMoveX = _direction.x * _playerData.GlobalSpeed * _currentAcceleration * Time.deltaTime;
        float desiredMoveZ = _direction.z * _playerData.GlobalSpeed * _currentAcceleration * Time.deltaTime;
        float desiredMoveY = _direction.y * Time.deltaTime;
        Vector3 desiredMove = new Vector3(desiredMoveX, desiredMoveY, desiredMoveZ);
        Vector3 realMove = desiredMove + _moveModifier * Time.deltaTime;
        _characterController.Move(realMove);
    }

    private void SetDirection(float horizontalMouvement, float verticalMouvement)
    {
        if(_playerController != null)
        {
            _directionHorinzontal = horizontalMouvement * _playerController.transform.forward;
            _directionVertical = verticalMouvement * _playerController.transform.right;
            _direction = (_directionHorinzontal + _directionVertical).normalized;
            if (horizontalMouvement > 0)
            {
                _direction += _playerController.transform.forward * (_playerData.SpeedForward - 1);
            }
            if (horizontalMouvement < 0)
            {
                _direction -= _playerController.transform.forward * (_playerData.SpeedBack - 1);
            }

            if (verticalMouvement > 0)
            {
                _direction += _playerController.transform.right * (_playerData.SpeedSide - 1);
            }
            else if (verticalMouvement < 0)
            {
                _direction -= _playerController.transform.right * (_playerData.SpeedSide - 1);
            }
            if (_speedSprint > 1 && horizontalMouvement > 0)
            {
                _direction += _playerController.transform.forward * (_speedSprint - 1) * _currentAccelerationSprint;
            }
            if (_direction != Vector3.zero)
            {
                Acceleration();
                if(_speedSprint != 0 && horizontalMouvement > 0)
                {
                    AccelerationSprint();
                }
                else
                {
                    _accelerationSprintLerp = 0;
                }
            }
            else
            {
                _accelerationLerp = 0;
            }
            if (_playerController.CanMove == true) //_canMove == true)
            {
                Move();
            }
        }
    }

    private void LookAtMouse(float mousePositionX, float mousePositionY)
    {
        if(_playerController.CanMove == true && _playerController != null)
        {
            if(mousePositionX > 0)
            {
                _currentMouseX += Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, -_playerData.StackMovement, _playerData.StackMovement);
            }else if(mousePositionX < 0)
            {
                _currentMouseX -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, -_playerData.StackMovement, _playerData.StackMovement);
            }
            else if(mousePositionX == 0 && _currentMouseX > 0)
            {
                _currentMouseX -= Time.deltaTime * _playerData.SpeedToStopCamera;
                _currentMouseX = Mathf.Clamp(_currentMouseX, 0, _playerData.StackMovement);
            }else if(mousePositionX == 0 && _currentMouseX < 0)
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
            _rotationY = _playerController.gameObject.transform.localEulerAngles.y;
            _rotationX += _currentMouseY + mousePositionY * _playerData.SensitivityMouseX;
            _rotationY += _currentMouseX + mousePositionX * _playerData.SensitivityMouseY;
            _rotationX = Mathf.Clamp(_rotationX, -_playerData.AngleX, _playerData.AngleX);
            _playerController.gameObject.transform.localEulerAngles = new Vector3(0, _rotationY, 0);
            _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
        }

    }

    private void Acceleration()
    {
        _accelerationLerp += Time.deltaTime;
        _accelerationLerp = Mathf.Clamp(_accelerationLerp, 0, 1);
        _currentAcceleration = _playerData.AccelerationCurve.Evaluate(_accelerationLerp);
    }

    private void AccelerationSprint()
    {
        _accelerationSprintLerp += Time.deltaTime;
        _accelerationSprintLerp = Mathf.Clamp(_accelerationSprintLerp, 0, 1);
        _currentAccelerationSprint = _playerData.AccelerationCurve.Evaluate(_accelerationSprintLerp);
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
        if (crouchBool == false && (_crouching == true || _isCrouch == true) && !Physics.Raycast(PlayerManager.Instance.PlayerController.transform.position, PlayerManager.Instance.PlayerController.transform.up, (_playerData.MaxHeight - _playerData.DifferenceHeightCrouch) /2 + _playerData.DifferenceHeightCrouch))
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
        _timeCrouch += Time.deltaTime * inversion;
        _timeCrouch = Mathf.Clamp(_timeCrouch, 0, _playerData.CrouchCurve.keys[_playerData.CrouchCurve.length - 1].time);
        _crouchLerp = _playerData.MaxHeight - _playerData.CrouchCurve.Evaluate(_timeCrouch) * _playerData.DifferenceHeightCrouch;
        _characterController.height = _crouchLerp;
        if (inversion < 0 && _crouchLerp == _playerData.MaxHeight)
        {
            _isCrouch = false;
            _unCrouching = false;
            _crouching = false;
        }
        if (inversion > 0 && _crouchLerp == _playerData.MaxHeight - _playerData.DifferenceHeightCrouch)
        {
            _isCrouch = true;
            _crouching = false;
            _unCrouching = false;
        }
    }
    /*
    private void Zoom(bool zoomBool)
    {
        if(_canMove == true)
        {
            if (zoomBool == true)
            {
                if (_isZoom == false && _zooming == false)
                {
                    _zooming = true;
                    _unZooming = false;
                }
            }
            if (zoomBool == false && (_zooming == true || _isZoom == true))
            {
                if (_unZooming == false)
                {
                    _zooming = false;
                    _unZooming = true;
                }
            }
        }
    }

    private void Zooming(float inversion)
    {
        _timeZoom += Time.deltaTime * inversion;
        _timeZoom = Mathf.Clamp(_timeZoom, 0, _playerData.ZoomTransition.keys[_playerData.ZoomTransition.length - 1].time);
        _zoomLerp = _playerData.FieldOfView - _playerData.ZoomTransition.Evaluate(_timeZoom) * (_playerData.FieldOfView - _playerData.ZoomFieldOfView);
        _mainCamera.fieldOfView = _zoomLerp;
        if (inversion < 0 && _zoomLerp == _playerData.FieldOfView)
        {
            _isZoom = false;
            _unZooming = false;
        }
        if (inversion > 0 && _zoomLerp == _playerData.ZoomFieldOfView)
        {
            _isZoom = true;
            _zooming = false;
        }
    }
    */
    private void Sprinting(bool isSprinting)
    {
        if (isSprinting == true)
        {
            if (_sprintCurrentTime > 0 || _playerData.SprintTimeMax == 0)
            {
                _sprintCurrentTime -= Time.deltaTime;
                _sprintCurrentTime = Mathf.Clamp(_sprintCurrentTime, 0, _playerData.SprintTimeMax);
                _speedSprint = _playerData.MaxSprintSpeed;
            }
            else
            {
                _speedSprint = 1;
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
        InputManager.Instance.Crouch -= Crouch;
        InputManager.Instance.Sprint -= Sprinting;
        InputManager.Instance.MousePosition -= LookAtMouse;
        InputManager.Instance.Direction -= SetDirection;
        //InputManager.Instance.Zoom -= Zoom;
    }
}