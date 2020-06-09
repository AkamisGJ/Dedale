using UnityEngine;

public class INarrowWay : IPlayerState
{
    private PlayerData _playerData = null;
    private Camera _camera = null;
    private CharacterController _characterController = null;
    private Vector3 _direction = Vector3.zero;
    private float _lerpCamera = 0;
    private float _currentAngleCamera = 0;
    private float _currentTimeFootStepPlayer = 0;
    private FMOD.Studio.EventInstance _footsteps;

    public void Init(PlayerData playerData, Camera camera, CharacterController characterController = null)
    {
        _playerData = playerData;
        _camera = camera;
        _characterController = characterController;
    }

    public void Enter(Collider collider, string animation)
    {
        _lerpCamera = 0.5f;
        _currentTimeFootStepPlayer = 0;
        InputManager.Instance.Direction += SetOrientation;
        PlayerManager.Instance.PlayerController.TimeZoom = 0;
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        InputManager.Instance.Direction -= SetOrientation;
    }

    private void SetOrientation(float horizontalMouvement, float verticalMouvement)
    {
        if (verticalMouvement > 0)
        {
            RotationCamera(1);
        }
        else if (verticalMouvement < 0)
        {
            RotationCamera(-1);
        }
        _direction = verticalMouvement * _characterController.transform.right * _playerData.SpeedNarrowWay;
        Move();
        FootStepSpeed(_direction);
    }

    private void Move()
    {
        Vector3 wantedDirection = _direction * _playerData.GlobalSpeed * Time.deltaTime;
        _characterController.Move(wantedDirection);
    }

    private void RotationCamera(int inversion)
    {
        _lerpCamera += Time.deltaTime * inversion * _playerData.SpeedRotationCamera;
        _lerpCamera = Mathf.Clamp(_lerpCamera, 0, 1);
        _currentAngleCamera = Mathf.Lerp(-_playerData.MaxAngleNarrowWay, _playerData.MaxAngleNarrowWay, _lerpCamera);
        _camera.transform.localEulerAngles = new Vector3(0, _currentAngleCamera, 0);
    }

    private void FootStepSpeed(Vector3 realMove)
    {
        float speedPlayer = new Vector3(realMove.x, 0, realMove.z).magnitude;
        if (speedPlayer > 0 && _currentTimeFootStepPlayer > (_playerData.TimeStep * 2) / Mathf.Log(1.25f + speedPlayer, 2))
        {
            _footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Sound Design/Grotte/Glisser entre pierres");
            _footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(PlayerManager.Instance.PlayerController.gameObject));
            _footsteps.start();
            _footsteps.release();
            _currentTimeFootStepPlayer = 0;
        }
        else if (speedPlayer > 0 && _currentTimeFootStepPlayer < (_playerData.TimeStep * 2) / Mathf.Log(1.25f + speedPlayer, 2))
        {
            _currentTimeFootStepPlayer += Time.deltaTime;
        }
    }
}
