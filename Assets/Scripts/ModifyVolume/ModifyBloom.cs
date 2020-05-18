using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ModifyBloom : MonoBehaviour
{
    [SerializeField] private Volume _volume = null;
    private Bloom _bloom = null;

    [Range(0,1)]
    [SerializeField] private float _bloomIntensityValue = 0;
    private float _bloomIntensityValueOld = 0;
    private enum EQuality
    {
        LOW = 0,
        MEDIUM = 1,
        HIGH = 2
    }
    [SerializeField] private EQuality _bloomQuality;
    private EQuality _bloomQualityOld = EQuality.LOW;
    [Min(0)]
    [SerializeField] private float _bloomThresholdValue = 0;
    private float _bloomThresholdValueOld = 0;
    [Range(0, 1)]
    [SerializeField] private float _bloomScatterValue = 0;
    private float _bloomScatterValueOld = 0;
    [SerializeField] private Color _bloomTintValue = Color.white;
    private Color _bloomTintValueOld = Color.white;
    [Min(0)]
    [SerializeField] private float _lensDirtIntensity = 0;
    private float _lensDirtIntensityOld = 0;

    private void Start()
    {
        if (_volume.sharedProfile.Has<Bloom>())
        {
            _volume.sharedProfile.TryGet<Bloom>(out _bloom);
            _bloom.active = true;
        }
        else
        {
            _bloom = _volume.sharedProfile.Add<Bloom>();
            _bloom.active = true;
        }
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    private void OnUpdate()
    {
        if(_bloomIntensityValueOld != _bloomIntensityValue)
        {
            BloomIntensityOverride(_bloomIntensityValue);
        }
        if(_bloomQualityOld != _bloomQuality)
        {
            BloomQualityOverride(_bloomQuality);
        }
        if(_bloomThresholdValueOld != _bloomThresholdValue)
        {
            BloomThresholdOverride(_bloomThresholdValue);
        }
        if (_bloomScatterValueOld != _bloomScatterValue)
        {
            BloomScatterOverride(_bloomScatterValue);
        }
        if (_bloomTintValueOld != _bloomTintValue)
        {
            BloomTintOverride(_bloomTintValue);
        }
        if(_lensDirtIntensityOld != _lensDirtIntensity)
        {
            LensDirtIntensityOverride(_lensDirtIntensity);
        }
    }

    private void BloomIntensityOverride(float intensityvolume)
    {
        _bloomIntensityValueOld = intensityvolume;
        if(_bloom.intensity.overrideState == false)
        {
            _bloom.intensity.overrideState = true;
        }
        _bloomIntensityValue = Mathf.Clamp(intensityvolume, 0, 1);
        _bloom.intensity.value = _bloomIntensityValue;
    }

    private void BloomQualityOverride(EQuality quality)
    {
        _bloomQualityOld = quality;
        if (_bloom.quality.overrideState == false)
        {
            _bloom.quality.overrideState = true;
        }
        _bloomQuality = quality;
        _bloom.quality.value = (int)_bloomQuality;
    }

    private void BloomThresholdOverride(float thresholdValue)
    {
        _bloomThresholdValueOld = thresholdValue;
        if (_bloom.threshold.overrideState == false)
        {
            _bloom.threshold.overrideState = true;
        }
        _bloomThresholdValue = Mathf.Clamp(thresholdValue, 0, Mathf.Infinity);
        _bloom.threshold.value = _bloomThresholdValue;
    }

    private void BloomScatterOverride(float scatterValue)
    {
        _bloomScatterValueOld = scatterValue;
        if (_bloom.scatter.overrideState == false)
        {
            _bloom.scatter.overrideState = true;
        }
        _bloomScatterValue = Mathf.Clamp(scatterValue, 0, 1);
        _bloom.scatter.value = _bloomScatterValue;
    }

    private void BloomTintOverride(Color tintValue)
    {
        _bloomTintValueOld = tintValue;
        if (_bloom.tint.overrideState == false)
        {
            _bloom.tint.overrideState = true;
        }
        _bloomTintValue = tintValue;
        _bloom.tint.value = _bloomTintValue;
    }

    private void LensDirtIntensityOverride(float lensDirtIntensity)
    {
        _lensDirtIntensityOld = lensDirtIntensity;
        if (_bloom.dirtIntensity.overrideState == false)
        {
            _bloom.dirtIntensity.overrideState = true;
        }
        _lensDirtIntensity = Mathf.Clamp(lensDirtIntensity, 0, Mathf.Infinity);
        _bloom.dirtIntensity.value = _lensDirtIntensity;
    }

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}