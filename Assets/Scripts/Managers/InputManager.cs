using UnityEngine;
using Prof.Utils;
using System;

public class InputManager : Singleton<InputManager>
{
    #region Fields
    private Vector4 _direction = Vector3.zero;
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

    private Vector3 _mousePosition = Vector3.zero;
    private event Action<Vector3> _mousePositionAction = null;
    public event Action<Vector3> MousePosition
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
                _direction.x = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _direction.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _direction.z = 1;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                _direction.z = -1;
            }
        }
        if(_mousePositionAction != null && PlayerManager.Instance.PlayerIsDead == false)
        {
            _mousePosition = Input.mousePosition;
            _mousePositionAction(_mousePosition);
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