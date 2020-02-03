using UnityEngine;

public class SliderObject : MonoBehaviour, IInteract
{
    private enum AxeObject
    {
        X,
        Y,
        Z
    }
    [SerializeField] private AxeObject _axeObject = AxeObject.X;

    private enum AxeMouse
    {
        X,
        Y
    }
    [SerializeField] private AxeMouse _axeMouse = AxeMouse.X;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _maxDistance = 2;
    private float _currentDistance = 0;
    [SerializeField] private Vector3 _startPosition = Vector3.zero;
    private float _startPositionAxe = 0;
    private Vector3 _nextPosition = Vector3.zero;
    private Vector3 _nextDeplacement = Vector3.zero;
    private Vector3 _correctionDeplacement = Vector3.zero;

    private void Start()
    {
        switch (_axeObject)
        {
            case AxeObject.X:
                _startPositionAxe = _startPosition.x;
                _nextDeplacement = transform.forward;
                break;
            case AxeObject.Y:
                _startPositionAxe = _startPosition.y;
                _nextDeplacement = transform.up;
                break;
            case AxeObject.Z:
                _startPositionAxe = _startPosition.z;
                _nextDeplacement = transform.right;
                break;
            default:
                break;
        }
        _currentDistance = _startPositionAxe;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Interact(float mousePositionX, float mousePositionY)
    {
        switch (_axeMouse)
        {
            case AxeMouse.X:
                _correctionDeplacement = transform.localPosition;
                _nextPosition = transform.localPosition + Deplacement(mousePositionX) * _nextDeplacement - _correctionDeplacement + _startPositionAxe * _nextDeplacement;
                transform.localPosition = _nextPosition;
                break;
            case AxeMouse.Y:
                _nextPosition = transform.localPosition + Deplacement(mousePositionY) * _nextDeplacement - _correctionDeplacement;
                transform.localPosition = _nextPosition;
                break;
            default:
                break;
        }
    }

    private float Deplacement(float mousePosition)
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _currentDistance += mousePosition * _speed * Time.deltaTime;
            Debug.Log(_currentDistance);
            if(_maxDistance > 0)
            {
                if (_maxDistance > _currentDistance && _currentDistance > _startPositionAxe)
                {
                    return _currentDistance;
                }
                else if (_maxDistance < _currentDistance)
                {
                    _currentDistance = _maxDistance;
                    return _currentDistance;
                }
                else if (_currentDistance < _startPositionAxe)
                {
                    _currentDistance = _startPositionAxe;
                    return _currentDistance;
                }
            }

            if (_maxDistance < 0)
            {
                if (_maxDistance < _currentDistance && _currentDistance < _startPositionAxe)
                {
                    return _currentDistance;
                }
                else if (_maxDistance > _currentDistance)
                {
                    _currentDistance = _maxDistance;
                    return _currentDistance;
                }
                else if (_currentDistance > _startPositionAxe)
                {
                    _currentDistance = _startPositionAxe;
                    return _currentDistance;
                }
            }
        }
        return _currentDistance;
    }
}
