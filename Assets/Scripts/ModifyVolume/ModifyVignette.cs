using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ModifyVignette : MonoBehaviour
{
    [SerializeField] private Volume _volume = null;
    private Vignette _vignette = null;

    [SerializeField] private Color _colorValue = Color.black;
    private Color _colorValueOld = Color.black;

    [SerializeField] private Vector2 _centerValue = Vector2.zero;
    private Vector2 _centerValueOld = Vector2.zero;

    [Range(0,1)]
    [SerializeField] private float _intensityValue = 0;
    private float _intensityValueOld = 0;

    [Range(0.01f, 1)]
    [SerializeField] private float _smoothnessValue = 0.01f;
    private float _smoothnessValueOld = 0.01f;

    [Range(0, 1)]
    [SerializeField] private float _roundnessValue = 0;
    private float _roundnessValueOld = 0;

    [SerializeField] private bool _rounded = false;
    private bool _roundedOld = false;

    void Start()
    {
        if (_volume.sharedProfile.Has<Vignette>())
        {
            _volume.sharedProfile.TryGet<Vignette>(out _vignette);
            _vignette.active = true;
        }
        else
        {
            _vignette = _volume.sharedProfile.Add<Vignette>();
            _vignette.active = true;
        }
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    void OnUpdate()
    {
        if (_colorValueOld != _colorValue)
        {
            ModifyColor(_colorValue);
        }
        if(_centerValueOld != _centerValue)
        {
            ModifyCenter(_centerValue);
        }
        if(_intensityValueOld != _intensityValue)
        {
            ModifyIntensity(_intensityValue);
        }
        if(_smoothnessValueOld != _smoothnessValue)
        {
            ModifySmoothness(_smoothnessValue);
        }
        if(_roundnessValueOld != _roundnessValue)
        {
            ModifyRoundness(_roundnessValue);
        }
        if(_roundedOld != _rounded)
        {
            ModifyRounded(_rounded);
        }
    }

    private void ModifyRounded(bool roundedValue)
    {
        _roundedOld = _rounded;
        if (_vignette.rounded.overrideState == false)
        {
            _vignette.rounded.overrideState = true;
        }
        _rounded = roundedValue;
        _vignette.rounded.value = _rounded;
    }

    private void ModifyRoundness(float roundnessValue)
    {
        _roundnessValueOld = _roundnessValue;
        if (_vignette.roundness.overrideState == false)
        {
            _vignette.roundness.overrideState = true;
        }
        _roundnessValue = Mathf.Clamp(roundnessValue, 0, 1);
        _vignette.roundness.value = _roundnessValue;
    }

    private void ModifySmoothness(float smoothnessValue)
    {
        _smoothnessValueOld = _smoothnessValue;
        if (_vignette.smoothness.overrideState == false)
        {
            _vignette.smoothness.overrideState = true;
        }
        _smoothnessValue = Mathf.Clamp(smoothnessValue, 0, 1);
        _vignette.smoothness.value = _smoothnessValue;
    }

    private void ModifyIntensity(float intensityValue)
    {
        _intensityValueOld = _intensityValue;
        if (_vignette.intensity.overrideState == false)
        {
            _vignette.intensity.overrideState = true;
        }
        _intensityValue = Mathf.Clamp(intensityValue,0,1);
        _vignette.intensity.value = _intensityValue;
    }

    private void ModifyCenter(Vector2 centerValue)
    {
        _centerValueOld = _centerValue;
        if (_vignette.center.overrideState == false)
        {
            _vignette.center.overrideState = true;
        }
        _centerValue = centerValue;
        _vignette.center.value = _centerValue;
    }

    private void ModifyColor(Color colorValue)
    {
        _colorValueOld = _colorValue;
        if (_vignette.color.overrideState == false)
        {
            _vignette.color.overrideState = true;
        }
        _colorValue = colorValue;
        _vignette.color.value = _colorValue;
    }

    private void OnDestroy()
    {
        if (GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}
