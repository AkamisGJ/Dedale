using UnityEngine;

public class Wind : MonoBehaviour
{
    private bool _playerIsIn = false;
    private bool _playerIsInSafeZone = false;
    private PlayerAgentController _playerController = null;
    private IMouvement _mouvement = null;
    private PlayerData _playerData = null;
    private float _windLerp = 0;
    [SerializeField] private float _timeWindActive = 5;
    [SerializeField] private float _timeNoWind = 5;
    [SerializeField] private float _ForceWindMaxMultiplier = 1.5f;
    private bool _isPreWarm = false;
    private Vector3 _windModifier = Vector3.zero;
    private float _timeWind = 0;
    private float _timeWindLerp = 0;
    [SerializeField] private AnimationCurve _windTransition = null;
    private MeshRenderer _meshRenderer = null;
    [SerializeField] Vector3 _direction = Vector3.forward;

    public bool PlayerIsInSafeZone { get => _playerIsInSafeZone; set => _playerIsInSafeZone = value; }

    void Start()
    {
        _direction = _direction.normalized;
        _meshRenderer = GetComponent<MeshRenderer>();
        _playerIsIn = false;
        _playerIsInSafeZone = false;
        _playerController = PlayerManager.Instance.PlayerController;
        _playerData = _playerController.PlayerData;
        _isPreWarm = false;
        _windModifier = Vector3.zero;
    }

    void Update()
    {
        _timeWind += Time.deltaTime;
        if (_timeWind > _timeWindActive + _timeNoWind)
        {
            _timeWind = 0;
            _timeWindLerp = 0;
            _isPreWarm = false;
        }else if (_timeWind > _timeNoWind)
        {
            _isPreWarm = true;
        }
        if (_isPreWarm == false)
        {
            _timeWindLerp = 0;
            _windLerp = 0;
            WindApplier();
        }
        else
        {
            WindForce();
        }
        if(_windLerp == 0)
        {
            _meshRenderer.material.color = Color.clear;
        }
        else if(_windLerp * _ForceWindMaxMultiplier * _playerData.MaxSprintSpeed > _playerData.MaxSprintSpeed)
        {
            _meshRenderer.material.color = Color.blue;
        }
        else if(_windLerp * _ForceWindMaxMultiplier * _playerData.MaxSprintSpeed < _playerData.MaxSprintSpeed)
        {
            _meshRenderer.material.color = Color.cyan;
        }
        if (_playerIsIn == true && _playerIsInSafeZone == false)
        {
            if (_playerController.CurrentState == PlayerAgentController.MyState.Mouvement)
            {
                _mouvement = _playerController.States[PlayerAgentController.MyState.Mouvement] as IMouvement;
                _mouvement.MoveModifier = _windModifier;
            }
        }
        else if(_mouvement != null)
        {
            _mouvement.MoveModifier = Vector3.zero;
        }
    }

    private void WindForce()
    {
        _timeWindLerp += Time.deltaTime;
        _timeWindLerp = Mathf.Clamp(_timeWindLerp, 0, _windTransition.keys[_windTransition.length - 1].time);
        _windLerp = _windTransition.Evaluate(_timeWindLerp);
        WindApplier();
    }

    private void WindApplier()
    {
        _windModifier = _direction * _playerData.SpeedForward * _playerData.GlobalSpeed * _playerData.MaxSprintSpeed * Time.deltaTime * _ForceWindMaxMultiplier * _windLerp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerIsIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerIsIn = false;
        }
    }
}
