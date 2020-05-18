using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ModifyExposure : MonoBehaviour
{
    [SerializeField] private Volume _volume = null;
    private Exposure _exposure = null;

    [SerializeField] private float _fixedExposureValue = 0;
    private float _fixedExposureValueOld = 0;

    [SerializeField] private float _compensationValue = 0;
    private float _compensationValueOld = 0;

    [SerializeField] private float _limitMinValue = 0;
    private float _limitMinValueOld = 0;

    [SerializeField] private float _limitMaxValue = 0;
    private float _limitMaxValueOld = 0;

    [Min(0.001f)]
    [SerializeField] private float _speedDarkToLightValue = 0;
    private float _speedDarkToLightValueOld = 0;

    [Min(0.001f)]
    [SerializeField] private float _speedLightToDarkValue = 0;
    private float _speedLightToDarkValueOld = 0;

    void Start()
    {
        if (_volume.sharedProfile.Has<Exposure>())
        {
            _volume.sharedProfile.TryGet<Exposure>(out _exposure);
            _exposure.active = true;
        }
        else
        {
            _exposure = _volume.sharedProfile.Add<Exposure>();
            _exposure.active = true;
        }
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    void OnUpdate()
    {
        if(_exposure.mode == ExposureMode.Fixed && _fixedExposureValueOld != _fixedExposureValue)
        {
            ModifyFixedExposure(_fixedExposureValue);
        }
        if (_exposure.mode == ExposureMode.CurveMapping || _exposure.mode == ExposureMode.Automatic)
        {
            if (_limitMinValueOld != _limitMinValue)
            {
                ModifyLimitMin(_limitMinValue);
            }
            if (_limitMaxValueOld != _limitMaxValue)
            {
                ModifyLimitMax(_limitMaxValue);
            }
            if (_speedDarkToLightValueOld != _speedDarkToLightValue)
            {
                ModifySpeedDarkToLight(_speedDarkToLightValue);
            }
            if (_speedLightToDarkValueOld != _speedLightToDarkValue)
            {
                ModifySpeedLightToDark(_speedLightToDarkValue);
            }
        }
        if (_exposure.mode != ExposureMode.Fixed && _compensationValueOld != _compensationValue)
        {
            ModifyCompensation(_compensationValue);
        }
    }

    private void ModifySpeedLightToDark(float speedLightToDarkValue)
    {
        _speedLightToDarkValueOld = _speedLightToDarkValue;
        if (_exposure.adaptationSpeedLightToDark.overrideState == false)
        {
            _exposure.adaptationSpeedLightToDark.overrideState = true;
        }
        _speedLightToDarkValue = speedLightToDarkValue;
        _exposure.adaptationSpeedLightToDark.value = _speedLightToDarkValue;
    }

    private void ModifySpeedDarkToLight(float speedDarkToLightValue)
    {
        _speedDarkToLightValueOld = _speedDarkToLightValue;
        if (_exposure.adaptationSpeedDarkToLight.overrideState == false)
        {
            _exposure.adaptationSpeedDarkToLight.overrideState = true;
        }
        _speedDarkToLightValue = speedDarkToLightValue;
        _exposure.adaptationSpeedDarkToLight.value = _speedDarkToLightValue;
    }

    private void ModifyLimitMax(float limitMaxValue)
    {
        _limitMaxValueOld = _limitMaxValue;
        if (_exposure.limitMax.overrideState == false)
        {
            _exposure.limitMax.overrideState = true;
        }
        _limitMaxValue = limitMaxValue;
        _exposure.limitMax.value = _limitMaxValue;
    }

    private void ModifyLimitMin(float limitMinValue)
    {
        _limitMinValueOld = _limitMinValue;
        if (_exposure.limitMin.overrideState == false)
        {
            _exposure.limitMin.overrideState = true;
        }
        _limitMinValue = limitMinValue;
        _exposure.limitMin.value = _limitMinValue;
    }

    private void ModifyCompensation(float compensationValue)
    {
        _compensationValueOld = _compensationValue;
        if (_exposure.compensation.overrideState == false)
        {
            _exposure.compensation.overrideState = true;
        }
        _compensationValue = compensationValue;
        _exposure.compensation.value = _compensationValue;
    }

    private void ModifyFixedExposure(float fixedExposureValue)
    {
        _fixedExposureValueOld = _fixedExposureValue;
        if (_exposure.fixedExposure.overrideState == false)
        {
            _exposure.fixedExposure.overrideState = true;
        }
        _fixedExposureValue = fixedExposureValue;
        _exposure.fixedExposure.value = _fixedExposureValue;
    }

    private void OnDestroy()
    {
        if (GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}