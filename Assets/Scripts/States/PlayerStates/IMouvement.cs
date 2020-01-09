using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMouvement : IPlayerState
{
    private PlayerController _playerController = null;
    private GameObject _grabObject = null;
    private Vector3 _originPositionGrabObject = Vector3.zero;
    private Quaternion _originRotationGrabObject = Quaternion.identity;
    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _angleX = 0;
    private float _sensitivityMouseX;
    private float _sensitivityMouseY;
    private Camera _mainCamera = null;

    public void Init()
    {

    }

    public void Enter()
    {
        InputManager.Instance.Direction += Move;
        InputManager.Instance.MousePosition += LookAtMouse;
    }

    public void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 10.0f))
        {
            if (hit.transform.gameObject.layer == 10)
            {
                Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.green);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _grabObject = hit.transform.gameObject;
                    _originPositionGrabObject = _grabObject.transform.position;
                    _originRotationGrabObject = _grabObject.transform.rotation;
                    _grabObject.transform.position = _mainCamera.transform.position + _mainCamera.transform.forward;
                    _grabObject.transform.rotation = Quaternion.identity;
                    return;
                }
            }
            if (hit.transform.gameObject.layer == 11)
            {
                Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.magenta);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _grabObject = hit.transform.gameObject;
                    return;
                }
            }
        }
        else Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward, Color.red);
    }

    public void Exit()
    {
        InputManager.Instance.Direction -= Move;
        InputManager.Instance.MousePosition -= LookAtMouse;
    }

    
    private void Move(float horizontalMouvement, float verticalMouvement)
    {
        /*
        if (horizontalMouvement < 0)
        {
            if (_moveSpeedHorizontal > _moveSpeedBackMax)
            {
                _moveSpeedHorizontal += _accelerationBack;
            }
            if (_moveSpeedHorizontal < _moveSpeedBackMax)
            {
                _moveSpeedHorizontal = _moveSpeedBackMax;
            }
        }
        if (horizontalMouvement > 0)
        {
            if (_moveSpeedHorizontal < _moveSpeedForwardMax)
            {
                _moveSpeedHorizontal += _accelerationForward;
            }
            if (_moveSpeedHorizontal > _moveSpeedForwardMax)
            {
                _moveSpeedHorizontal = _moveSpeedForwardMax;
            }
        }
        if (verticalMouvement < 0)
        {
            if (_moveSpeedSide > _moveSpeedBackMax)
            {
                _moveSpeedSide += _accelerationSide;
            }
            if (_moveSpeedSide < _moveSpeedSideMax)
            {
                _moveSpeedSide = _moveSpeedSideMax;
            }
        }
        if (verticalMouvement > 0)
        {
            if (_moveSpeedSide < _moveSpeedSideMax)
            {
                _moveSpeedSide += _accelerationSide;
            }
            if (_moveSpeedSide > _moveSpeedSideMax)
            {
                _moveSpeedSide = _moveSpeedSideMax;
            }
        }
        transform.position += (verticalMouvement * transform.right * _moveSpeedSide + horizontalMouvement * transform.forward * _moveSpeedHorizontal).normalized * Time.deltaTime;
        
        _moveDirection = (verticalMouvement * transform.right + horizontalMouvement * transform.forward).normalized;
        Vector3 yVel = new Vector3(0, _rb.velocity.y, 0);
        _rb.velocity = _moveDirection * _moveSpeed * Time.deltaTime;
        _rb.velocity += yVel;
        */
    }


        private void LookAtMouse(float mousePositionX, float mousePositionY)
    {
        _rotationX += mousePositionY * _sensitivityMouseX;
        _rotationY += mousePositionX * _sensitivityMouseY;
        _rotationX = Mathf.Clamp(_rotationX, -_angleX, _angleX);
        _playerController.gameObject.transform.localEulerAngles = new Vector3(0, _rotationY, 0);
        _mainCamera.transform.localEulerAngles = new Vector3(-_rotationX, 0, 0);
    }

}