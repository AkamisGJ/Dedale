using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyReductionAngleCameraInfinityCorridor : MonoBehaviour
{
    private float _reductionVisionY = 0;
    private float _reductionVisionX = 0;
    private IInfintyCorridor _infintyCorridor = null;

    public float ReductionVisionX { get => _reductionVisionX; set => _reductionVisionX = value; }
    public float ReductionVisionY { get => _reductionVisionY; set => _reductionVisionY = value; }

    void Start()
    {
        _infintyCorridor = PlayerManager.Instance.PlayerController.States[PlayerAgentController.MyState.INFINITYCORRIDOR] as IInfintyCorridor;
        _reductionVisionX = 0;
        _reductionVisionY = 0;
        GameLoopManager.Instance.GameLoopModifyVolume += OnUpdate;
    }

    void OnUpdate()
    {
        _infintyCorridor.ReductionVisionX = _reductionVisionX;
        _infintyCorridor.ReductionVisionY = _reductionVisionY;
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.GameLoopModifyVolume -= OnUpdate;
    }
}
