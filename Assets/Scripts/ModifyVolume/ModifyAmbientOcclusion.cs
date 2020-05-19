using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ModifyAmbientOcclusion : MonoBehaviour
{
    [SerializeField] private Volume _volume = null;
    private AmbientOcclusion _ambientOcclusion = null;

    [Range(0,4)]
    [SerializeField] private float _intensity = 0;
    private float _intensityOld = 0;
    [Range(0,1)]
    [SerializeField] private float _directLightingStrenght = 0;
    private float _directLightingStrenghtOld = 0;
    [Range(0.25f, 5)]
    [SerializeField] private float _radius = 0.25f;
    private float _radiusOld = 0.25f;
    private enum EQuality
    {
        LOW = 0,
        MEDIUM = 1,
        HIGH = 2
    }
    [SerializeField] private EQuality _ambientOcclusionQuality;
    private EQuality _ambientOcclusionQualityOld = EQuality.LOW;
    [Range(0,1)]
    [SerializeField] private float _ghostingReduction = 0;
    private float _ghostingReductionOld = 0;

    void Start()
    {
        if (_volume.sharedProfile.Has<AmbientOcclusion>())
        {
            _volume.sharedProfile.TryGet<AmbientOcclusion>(out _ambientOcclusion);
            _ambientOcclusion.active = true;
        }
        else
        {
            _ambientOcclusion = _volume.sharedProfile.Add<AmbientOcclusion>();
            _ambientOcclusion.active = true;
        }
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    void OnUpdate()
    {
        if(_intensityOld != _intensity)
        {
            ModifyIntensity(_intensity);
        }
        if(_directLightingStrenghtOld != _directLightingStrenght)
        {
            ModifyDirectLightingStrenght(_directLightingStrenght);
        }
        if(_radiusOld != _radius)
        {
            ModifyRadius(_radius);
        }
        if(_ambientOcclusionQualityOld != _ambientOcclusionQuality)
        {
            ModifyQuality(_ambientOcclusionQuality);
        }
        if(_ghostingReductionOld != _ghostingReduction)
        {
            ModifyGhostingReduction(_ghostingReduction);
        }
    }

    private void ModifyIntensity(float intensity)
    {
        _intensityOld = _intensity;
        if (_ambientOcclusion.intensity.overrideState == false)
        {
            _ambientOcclusion.intensity.overrideState = true;
        }
        _intensity = Mathf.Clamp(intensity, 0, 4);
        _ambientOcclusion.intensity.value = _intensity;
    }

    private void ModifyDirectLightingStrenght(float directLightingStrenght)
    {
        _directLightingStrenghtOld = _directLightingStrenght;
        if (_ambientOcclusion.directLightingStrength.overrideState == false)
        {
            _ambientOcclusion.directLightingStrength.overrideState = true;
        }
        _directLightingStrenght = Mathf.Clamp(directLightingStrenght, 0, 4);
        _ambientOcclusion.directLightingStrength.value = _directLightingStrenght;
    }

    private void ModifyRadius(float radius)
    {
        _radiusOld = _radius;
        if (_ambientOcclusion.radius.overrideState == false)
        {
            _ambientOcclusion.radius.overrideState = true;
        }
        _radius = Mathf.Clamp(radius, 0.25f, 5);
        _ambientOcclusion.radius.value = _radius;
    }

    private void ModifyQuality(EQuality quality)
    {
        _ambientOcclusionQualityOld = _ambientOcclusionQuality;
        if(_ambientOcclusion.quality.overrideState == false)
        {
            _ambientOcclusion.quality.overrideState = true;
        }
        _ambientOcclusionQuality = quality;
        _ambientOcclusion.quality.value = (int)_ambientOcclusionQuality;
    }

    private void ModifyGhostingReduction(float ghostingReduction)
    {
        _ghostingReductionOld = _ghostingReduction;
        if (_ambientOcclusion.ghostingReduction.overrideState == false)
        {
            _ambientOcclusion.ghostingReduction.overrideState = true;
        }
        _ghostingReduction = Mathf.Clamp(ghostingReduction, 0, 1);
        _ambientOcclusion.ghostingReduction.value = _ghostingReduction;
    }

    private void OnDestroy()
    {
        if (GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}
