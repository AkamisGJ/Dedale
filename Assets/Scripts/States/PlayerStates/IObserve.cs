using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IObserve : IPlayerState
{
    private PlayerController _playerController = null;
    private GameObject _grabObject = null;
    private Vector3 _originPositionGrabObject = Vector3.zero;
    private Quaternion _originRotationGrabObject = Quaternion.identity;
    private float _mouseSensitivityInteract = 0;
    private Camera _mainCamera = null;


    public void Init()
    {

    }

    public void Enter()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _grabObject.transform.position = _originPositionGrabObject;
            _grabObject.transform.rotation = _originRotationGrabObject;
        }
    }

    public void Exit()
    {
        InputManager.Instance.MousePosition -= LookObject;
    }

    private void LookObject(float mousePositionX, float mousePositionY)
    {
        float XaxisRotation = mousePositionX * _mouseSensitivityInteract;
        float YaxisRotation = mousePositionY * _mouseSensitivityInteract;
        _grabObject.transform.Rotate(_mainCamera.transform.up, -XaxisRotation, 0);
        _grabObject.transform.Rotate(_mainCamera.transform.right, YaxisRotation, 0);
    }
}
