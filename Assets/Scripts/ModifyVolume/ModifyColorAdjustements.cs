using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ModifyColorAdjustements : MonoBehaviour
{
    [SerializeField] private Volume _volume = null;
    private ColorAdjustments _colorAdjustments = null;

    [SerializeField] private float _postExposureValue = 0;
    private float _postExposureValueOld = 0;

    [Range(-100, 100)]
    [SerializeField] private float _contrastValue = 0;
    private float _contrastValueOld = 0;

    [SerializeField] private Color _colorFilterValue = Color.white;
    private Color _colorFilterValueOld = Color.white;

    [Range(-180, 180)]
    [SerializeField] private float _hueShiftValue = 0;
    private float _hueShiftValueOld = 0;

    [Range(-100, 100)]
    [SerializeField] private float _saturationValue = 0;
    private float _saturationValueOld = 0;

    void Start()
    {
        if (_volume.sharedProfile.Has<ColorAdjustments>())
        {
            _volume.sharedProfile.TryGet<ColorAdjustments>(out _colorAdjustments);
            _colorAdjustments.active = true;
        }
        else
        {
            _colorAdjustments = _volume.sharedProfile.Add<ColorAdjustments>();
            _colorAdjustments.active = true;
        }
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    void OnUpdate()
    {
        if(_postExposureValueOld != _postExposureValue)
        {
            ModifyExposure(_postExposureValue);
        }
        if(_contrastValueOld != _contrastValue)
        {
            ModifyContrast(_contrastValue);
        }
        if(_colorFilterValueOld != _colorFilterValue)
        {
            ModifyColorFilter(_colorFilterValue);
        }
        if(_hueShiftValueOld != _hueShiftValue)
        {
            ModifyHueShift(_hueShiftValue);
        }
        if(_saturationValueOld != _saturationValue)
        {
            ModifySaturation(_saturationValue);
        }
    }

    private void ModifySaturation(float saturationValue)
    {
        _saturationValueOld = _saturationValue;
        if (_colorAdjustments.saturation.overrideState == false)
        {
            _colorAdjustments.saturation.overrideState = true;
        }
        _saturationValue = Mathf.Clamp(saturationValue, -100, 100);
        _colorAdjustments.saturation.value = _saturationValue;
    }

    private void ModifyHueShift(float hueShiftValue)
    {
        _hueShiftValueOld = _hueShiftValue;
        if (_colorAdjustments.hueShift.overrideState == false)
        {
            _colorAdjustments.hueShift.overrideState = true;
        }
        _hueShiftValue = Mathf.Clamp(hueShiftValue, -180, 180);
        _colorAdjustments.hueShift.value = _hueShiftValue;
    }

    private void ModifyColorFilter(Color colorFilterValue)
    {
        _colorFilterValueOld = _colorFilterValue;
        if (_colorAdjustments.colorFilter.overrideState == false)
        {
            _colorAdjustments.colorFilter.overrideState = true;
        }
        _colorFilterValue = colorFilterValue;
        _colorAdjustments.colorFilter.value = _colorFilterValue;
    }

    private void ModifyContrast(float contrastValue)
    {
        _contrastValueOld = _contrastValue;
        if (_colorAdjustments.contrast.overrideState == false)
        {
            _colorAdjustments.contrast.overrideState = true;
        }
        _contrastValue = Mathf.Clamp(contrastValue, -100, 100);
        _colorAdjustments.contrast.value = _contrastValue;
    }

    private void ModifyExposure(float postExposureValue)
    {
        _postExposureValueOld = _postExposureValue;
        if (_colorAdjustments.postExposure.overrideState == false)
        {
            _colorAdjustments.postExposure.overrideState = true;
        }
        _postExposureValue = postExposureValue;
        _colorAdjustments.postExposure.value = _postExposureValue;
    }

    private void OnDestroy()
    {
        if (GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}
