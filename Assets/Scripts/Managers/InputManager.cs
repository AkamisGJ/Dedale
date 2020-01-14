using UnityEngine;
using Prof.Utils;
using System;

public class InputManager : Singleton<InputManager>
{
    #region Fields
    private Vector3 _direction = Vector3.zero;
    private Vector3 _lastDirection = Vector3.zero;
    private event Action<float, float> _directionAction = null;
    public event Action<float, float> Direction
    {
        add
        {
            _directionAction -= value;
            _directionAction += value;
        }
        remove
        {
            _directionAction -= value;
        }
    }

    private float _mousePositionX = 0;
    private float _mousePositionY = 0;
    private event Action<float, float> _mousePositionAction = null;
    public event Action<float, float> MousePosition
    {
        add
        {
            _mousePositionAction -= value;
            _mousePositionAction += value;
        }
        remove
        {
            _mousePositionAction -= value;
        }
    }

    private event Action _interact = null;
    public event Action Interact
    {
        add
        {
            _interact -= value;
            _interact += value;
        }
        remove
        {
            _interact -= value;
        }
    }

    private event Action<bool> _sprintAction = null;
    public event Action<bool> Sprint
    {
        add
        {
            _sprintAction -= value;
            _sprintAction += value;
        }
        remove
        {
            _sprintAction -= value;
        }
    }

    private bool toggleCrouch = false;
    private event Action<bool> _crouchAction = null;
    public event Action<bool> Crouch
    {
        add
        {
            _crouchAction -= value;
            _crouchAction += value;
        }
        remove
        {
            _crouchAction -= value;
        }
    }

    private bool _sprint = false;
    private bool _crouch = false;
    #endregion Fields

    protected override void Start()
    {
        base.Start();
        _sprint = false;
        _crouch = false;
    }

    protected override void Update()
    {
        if(PlayerManager.Instance.PlayerIsDead == false)
        {
            if (_directionAction != null)
            {
                _direction.z = Input.GetAxisRaw("Horizontal");
                _direction.x = Input.GetAxisRaw("Vertical");
                /*
                if (Input.GetKey(KeyCode.Z))
                {
                    _direction.x += 1;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    _direction.x -= 1;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    _direction.z += 1;
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    _direction.z -= 1;
                }
                _direction.x = Mathf.Clamp(_direction.x, -1, 1);
                _direction.z = Mathf.Clamp(_direction.z, -1, 1);
                */
                _directionAction(_direction.x, _direction.z);
                _direction = Vector3.zero;
            }
            if (_mousePositionAction != null)
            {
                _mousePositionX = Input.GetAxis("Mouse X");
                _mousePositionY = Input.GetAxis("Mouse Y");
                _mousePositionAction(_mousePositionX, _mousePositionY);
            }
            if (_interact != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _interact();
                }
            }

            if (_sprintAction != null)
            {
                if (Input.GetButton("Sprint"))
                {
                    _sprint = !_sprint;
                }
                _sprintAction(_sprint);
            }
            if(_crouchAction != null && toggleCrouch == false)
            {
                if (Input.GetButton("Crouch"))
                {
                    _crouch = true;
                }
                else
                {
                    _crouch = false;
                }
                _crouchAction(_crouch);
            }
        }
    }
}