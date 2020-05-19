using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ModifyDepthOfField : MonoBehaviour
{
    [SerializeField] private Volume _volume = null;
    private DepthOfField _depthOfField = null;

    [Min(0.1f)]
    [SerializeField] private float _focusDistanceValue = 0.1f;
    private float _focusDistanceValueOld = 0.1f;

    void Start()
    {
        if (_volume.sharedProfile.Has<DepthOfField>())
        {
            _volume.sharedProfile.TryGet<DepthOfField>(out _depthOfField);
            _depthOfField.active = true;
        }
        else
        {
            _depthOfField = _volume.sharedProfile.Add<DepthOfField>();
            _depthOfField.active = true;
        }
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    void OnUpdate()
    {
        if(_focusDistanceValueOld != _focusDistanceValue)
        {
            ModifyFocusDistance(_focusDistanceValue);
        }
    }

    private void ModifyFocusDistance(float focusDistanceValue)
    {
        _focusDistanceValueOld = _focusDistanceValue;
        if (_depthOfField.focusDistance.overrideState == false)
        {
            _depthOfField.focusDistance.overrideState = true;
        }
        _focusDistanceValue = Mathf.Clamp(focusDistanceValue, 0.1f, Mathf.Infinity);
        _depthOfField.focusDistance.value = _focusDistanceValue;
    }

    private void OnDestroy()
    {
        if (GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}
