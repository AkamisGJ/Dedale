using UnityEngine;

public class MoveCameraMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _lookAtThis = null;
    [SerializeField] private float _maxDeplacementX = 0.01f;
    [SerializeField] private float _maxDeplacementY = 0.01f;
    [SerializeField] private float _speedDeplacement = 0.005f;
    private Vector3 _startPosition = Vector3.zero;
    private float _mousePositionX = 0;
    private float _mousePositionY = 0;
    private Vector3 _desiredPosition = Vector3.zero;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        LookAtMouse();
        if(_lookAtThis != null)
        {
            gameObject.transform.LookAt(_lookAtThis.transform);
        }
    }

    private void LookAtMouse()
    {
        _mousePositionX = Input.GetAxis("Mouse X");
        _mousePositionY = Input.GetAxis("Mouse Y");
        _desiredPosition += _mousePositionX * _speedDeplacement * Vector3.right;
        _desiredPosition += _mousePositionY * _speedDeplacement * Vector3.up;
        _desiredPosition.x = Mathf.Clamp(_desiredPosition.x, _startPosition.x - _maxDeplacementX, _startPosition.x + _maxDeplacementX);
        _desiredPosition.y = Mathf.Clamp(_desiredPosition.y, _startPosition.y - _maxDeplacementY, _startPosition.y + _maxDeplacementY);
        transform.position = _startPosition + _desiredPosition;
    }
}