using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIFollow : MonoBehaviour
{
    private void Start()
    {
        GameLoopManager.Instance.LateGameLoop += OnLateUpdate;
    }

    private void OnLateUpdate()
    {
        transform.position = PlayerManager.Instance.CameraPlayer.transform.position;
        transform.rotation = PlayerManager.Instance.CameraPlayer.transform.rotation;
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.LateGameLoop -= OnLateUpdate;
    }
}
