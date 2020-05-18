using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ModifyLensDistortion : MonoBehaviour
{
    [SerializeField] private Volume _volume = null;
    private LensDistortion _lensDistortion = null;

    [Range(-1, 1)]
    [SerializeField] private float _intensityValue = 0;
    private float _intensityValueOld = 0;

    [Range(0, 1)]
    [SerializeField] private float _xMultiplierValue = 0;
    private float _xMultiplierValueOld = 0;

    [Range(0, 1)]
    [SerializeField] private float _yMultiplierValue = 0;
    private float _yMultiplierValueOld = 0;

    [SerializeField] private Vector2 _centerValue = Vector2.zero;
    private Vector2 _centerValueOld = Vector2.zero;

    [Range(0.01f, 5)]
    [SerializeField] private float _scaleValue = 0;
    private float _scaleValueOld = 0;

    void Start()
    {
        if (_volume.sharedProfile.Has<LensDistortion>())
        {
            _volume.sharedProfile.TryGet<LensDistortion>(out _lensDistortion);
            _lensDistortion.active = true;
        }
        else
        {
            _lensDistortion = _volume.sharedProfile.Add<LensDistortion>();
            _lensDistortion.active = true;
        }
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    void OnUpdate()
    {
        if (_intensityValueOld != _intensityValue)
        {
            ModifyIntensity(_intensityValue);
        }
        if(_xMultiplierValueOld != _xMultiplierValue)
        {
            ModifyXMultiplier(_xMultiplierValue);
        }
        if (_yMultiplierValueOld != _yMultiplierValue)
        {
            ModifyXMultiplier(_yMultiplierValue);
        }
        if(_centerValueOld != _centerValue)
        {
            ModifyCenter(_centerValue);
        }
        if(_scaleValueOld != _scaleValue)
        {
            ModifyScale(_scaleValue);
        }
    }

    private void ModifyScale(float scaleValue)
    {
        _scaleValueOld = _scaleValue;
        if (_lensDistortion.scale.overrideState == false)
        {
            _lensDistortion.scale.overrideState = true;
        }
        _scaleValue = Mathf.Clamp(scaleValue, 0.01f, 5);
        _lensDistortion.scale.value = _scaleValue;
    }

    private void ModifyCenter(Vector2 centerValue)
    {
        _centerValueOld = _centerValue;
        if (_lensDistortion.center.overrideState == false)
        {
            _lensDistortion.center.overrideState = true;
        }
        _centerValue = centerValue;
        _lensDistortion.center.value = _centerValue;
    }

    private void ModifyYMultiplier(float yMultiplierValue)
    {
        _yMultiplierValueOld = _yMultiplierValue;
        if (_lensDistortion.yMultiplier.overrideState == false)
        {
            _lensDistortion.yMultiplier.overrideState = true;
        }
        _yMultiplierValue = Mathf.Clamp(yMultiplierValue, 0, 1);
        _lensDistortion.yMultiplier.value = _yMultiplierValue;
    }

    private void ModifyXMultiplier(float xMultiplierValue)
    {
        _xMultiplierValueOld = _xMultiplierValue;
        if (_lensDistortion.xMultiplier.overrideState == false)
        {
            _lensDistortion.xMultiplier.overrideState = true;
        }
        _xMultiplierValue = Mathf.Clamp(xMultiplierValue, 0, 1);
        _lensDistortion.xMultiplier.value = _xMultiplierValue;
    }

    private void ModifyIntensity(float intensityValue)
    {
        _intensityValueOld = _intensityValue;
        if (_lensDistortion.intensity.overrideState == false)
        {
            _lensDistortion.intensity.overrideState = true;
        }
        _intensityValue = Mathf.Clamp(intensityValue, -1, 1);
        _lensDistortion.intensity.value = _intensityValue;
    }

    private void OnDestroy()
    {
        if (GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}
