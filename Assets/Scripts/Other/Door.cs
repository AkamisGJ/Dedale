using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    private Rigidbody _rb = null;
    private HingeJoint _hj = null;
    private float _initalPositionMouseX = 0;
    [Tooltip("Speed of rotation of door")]
    [SerializeField] private float _speed = 1;
    private JointLimits _limits;
    [SerializeField] private float _bounciness = 0.05f;
    [SerializeField] private bool _needKey = false;
    [SerializeField] private float minLimit = -90f;
    [SerializeField] private float maxLimit = 90f;
    public bool NeedKey { get => _needKey; }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _hj = GetComponent<HingeJoint>();
        _limits.bounciness = _bounciness;
        _hj.limits = _limits;
    }

    public void Enter()
    {
        UnClick();
        OnClick();
        JointMotor motor = _hj.motor;
        _hj.useMotor = true;
        motor.freeSpin = false;
        _limits.bounciness = 0f;
        _limits.min = minLimit;
        _limits.max = maxLimit;
        _hj.limits = _limits;
    }

    public void Exit()
    {
        UnClick();
        JointMotor motor = _hj.motor;
        motor.freeSpin = true;
        _hj.useMotor = false;
        _limits.bounciness = _bounciness;
        _hj.limits = _limits;
    }

    public void Interact(float mousePositionX, float mousePositionY)
    {
        if(_needKey == false || ( _needKey == true && PlayerManager.Instance.HaveKey == true))
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
            motor.targetVelocity = mousePositionY * _speed;
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
}