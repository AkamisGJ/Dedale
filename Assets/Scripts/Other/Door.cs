using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    private Rigidbody _rb = null;
    private HingeJoint _hj = null;
    private float _initalPositionMouseX = 0;
    [Tooltip("Speed of rotation of door")]
    [SerializeField] private float speed = 1;
    private JointLimits _limits;
    [SerializeField] private float _bounciness = 0.05f;

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
        _limits.min = -90;
        _limits.max = 90;
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
        JointMotor motor = _hj.motor;
        if(mousePositionY < 0)
        {
            motor.force = -mousePositionY * speed;
        }
        if (mousePositionY > 0)
        {
            motor.force = mousePositionY * speed;
        }
        motor.targetVelocity = mousePositionY * speed;
        _hj.motor = motor;
    }

    void OnClick()
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    void UnClick()
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.magenta;
    }
}