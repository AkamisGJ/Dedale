using UnityEngine;
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

    private event Action<KeyCode> _qTEAction = null;
    public event Action<KeyCode> QTE
    {
        add
        {
            _qTEAction -= value;
            _qTEAction += value;
        }
        remove
        {
            _qTEAction -= value;
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

    private bool _toggleCrouch = false;
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

    private event Action<bool> _zoomAction = null;
    public event Action<bool> Zoom
    {
        add
        {
            _zoomAction -= value;
            _zoomAction += value;
        }
        remove
        {
            _zoomAction -= value;
        }
    }

    private bool _sprint = false;
    private bool _crouch = false;
    private bool _zoom = false;
    #endregion Fields

    protected override void Awake()
    {
        base.Awake();
        GameLoopManager.Instance.StartInputManager += OnStart;
    }

    public void OnStart()
    {
        _sprint = false;
        _crouch = false;
        _zoom = false;
        GameLoopManager.Instance.GameLoopInputManager += OnUpdate;
    }

    protected void OnUpdate()
    {
        if(PlayerManager.Instance.PlayerIsDead == false && GameManager.Instance.CurrentState == GameManager.MyState.GAME)
        {
            if (_directionAction != null)
            {
                _direction.z = Input.GetAxisRaw("Horizontal");
                _direction.x = Input.GetAxisRaw("Vertical");
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
                    _sprint = true;
                }
                else
                {
                    _sprint = false;
                }
                _sprintAction(_sprint);
            }
            if(_crouchAction != null && _toggleCrouch == false)
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
            if (_zoomAction != null)
            {
                if (Input.GetButton("Zoom"))
                {
                    _zoom = true;
                }
                else
                {
                    _zoom = false;
                }
                _zoomAction(_zoom);
            }



            if(_qTEAction != null)
            {
                if(Input.GetKeyDown(KeyCode.D))
                {
                    _qTEAction(KeyCode.D);
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    _qTEAction(KeyCode.Z);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    _qTEAction(KeyCode.Q);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    _qTEAction(KeyCode.S);
                }
            }
        }
    }
}