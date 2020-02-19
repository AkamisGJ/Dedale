using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUIFollow : MonoBehaviour
{
    void Start()
    {
        GameLoopManager.Instance.LateGameLoop += OnLateUpdate;
    }

    void OnLateUpdate()
    {
        transform.position = PlayerManager.Instance.CameraPlayer.transform.position;
        transform.rotation = PlayerManager.Instance.CameraPlayer.transform.rotation;
    }
}
