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
    #endregion Fields

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if(_directionAction != null && PlayerManager.Instance.PlayerIsDead == false)
        {
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
        }
        if(_mousePositionAction != null && PlayerManager.Instance.PlayerIsDead == false)
        {
            _mousePositionX = Input.GetAxis("Mouse X");
            _mousePositionY = Input.GetAxis("Mouse Y");
            _mousePositionAction(_mousePositionX , _mousePositionY);
        }
        if(_interact != null && PlayerManager.Instance.PlayerIsDead == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _interact();
            }
        }
    }

    protected void FixedUpdate()
    {
        if(_directionAction != null && PlayerManager.Instance.PlayerIsDead == false)
        {
            _directionAction(_direction.x,_direction.z);
            _direction = Vector3.zero;
        }
    }
}