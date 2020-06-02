using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyReductionAngleCameraInfinityCorridor : MonoBehaviour
{
    private float _reductionVisionY = 0;
    private float _reductionVisionX = 0;
    [SerializeField] private float _timeToReachTheEndOfTimeline = 1;
    [SerializeField] private float _maxReductionX = 40;
    [SerializeField] private float _maxReductionY = 40;
    [SerializeField] private AnimationCurve _animationCurveAngleY;
    [SerializeField] private AnimationCurve _animationCurveAngleX;
    private IInfintyCorridor _infintyCorridor = null;
    private float _currentTime = 0;

    private void Start()
    {
        GameLoopManager.Instance.LastStart += OnStart;
    }

    void OnStart()
    {
        _infintyCorridor = PlayerManager.Instance.PlayerController.States[PlayerAgentController.MyState.INFINITYCORRIDOR] as IInfintyCorridor;
        _reductionVisionX = 0;
        _reductionVisionY = 0;
        _currentTime = 0;
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
        GameLoopManager.Instance.LastStart -= OnStart;
    }

    void OnUpdate()
    {
        _currentTime += Time.deltaTime / _timeToReachTheEndOfTimeline;
        _currentTime = Mathf.Clamp(_currentTime, 0, 1);
        _reductionVisionX = _animationCurveAngleX.Evaluate(_currentTime) * _maxReductionX;
        _reductionVisionY = _animationCurveAngleY.Evaluate(_currentTime) * _maxReductionY;
        _infintyCorridor.ReductionVisionX = _reductionVisionX;
        _infintyCorridor.ReductionVisionY = _reductionVisionY;
    }

    private void OnDestroy()
    {
        if(GameLoopManager.Instance != null)
        {
            GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
        }
    }
}
