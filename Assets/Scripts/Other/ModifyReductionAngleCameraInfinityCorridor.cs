using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyReductionAngleCameraInfinityCorridor : MonoBehaviour
{
    [SerializeField] private float _reductionVisionY = 0;
    [SerializeField] private float _reductionVisionX = 0;
    private IInfintyCorridor _infintyCorridor = null;

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
