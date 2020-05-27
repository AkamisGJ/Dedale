using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;


public class ModifyChromaticAberration : MonoBehaviour
{
    [SerializeField] private Volume _volume = null;
    private ChromaticAberration _chromaticAberration = null;

    [Range(0, 1)]
    [SerializeField] private float _intensityValue = 0;
    private float _intensityValueOld = 0;

    void Start()
    {
        if (_volume.sharedProfile.Has<ChromaticAberration>())
        {
            _volume.sharedProfile.TryGet<ChromaticAberration>(out _chromaticAberration);
            _chromaticAberration.active = true;
        }
        else
        {
            _chromaticAberration = _volume.sharedProfile.Add<ChromaticAberration>();
            _chromaticAberration.active = true;
        }
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    void OnUpdate()
    {
        if (_intensityValueOld != _intensityValue)
        {
            ModifyIntensity(_intensityValue);
        }
    }

    private void ModifyIntensity(float intensityValue)
    {
        _intensityValueOld = _intensityValue;
        if (_chromaticAberration.intensity.overrideState == false)
        {
            _chromaticAberration.intensity.overrideState = true;
        }
        _intensityValue = Mathf.Clamp(intensityValue, 0, 1);
        _chromaticAberration.intensity.value = _intensityValue;
    }

    private void OnDestroy()
    {
        if (GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}
