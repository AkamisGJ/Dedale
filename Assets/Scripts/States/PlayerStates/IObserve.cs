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
    private PlayerData _playerData = null;
    private Collider _grabObjectCollider = null;
    private Transform _objectHolder = null;
    private InteractObject _currentObjectInterract = null;
    private Quaternion _grabObjectRotationWhenLooked = Quaternion.identity;
    private float _distanceGrabObjectWithCameraWhenLooked = 1.0f;

    public void Init(PlayerData playerData,Camera _camera)
    {
        _playerData = playerData;
    }

    public void Enter(GameObject grabObject)
    {
        _grabObjectCollider = _playerController.RaycastHit.collider;
        _grabObject = grabObject;
        _objectHolder = _playerController.ObjectHolder;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _grabObjectCollider.isTrigger = true;
            _grabObject.transform.SetParent(_objectHolder);
            _currentObjectInterract = _grabObject.GetComponent<InteractObject>();
            if (_currentObjectInterract.OnTakeObject != null && _playerController.AudioSourcePlayer != null)
            {
                _playerController.AudioSourcePlayer.clip = _currentObjectInterract.OnTakeObject;
                _playerController.AudioSourcePlayer.Play();
            }
            if (_grabObject.GetComponent<Rigidbody>())
            {
                Rigidbody objectRb = _grabObject.GetComponent<Rigidbody>();
                objectRb.isKinematic = true;
            }
            if (_grabObject.GetComponent<InteractObject>())
            {
                Vector4 infoWhenLooked = _grabObject.GetComponent<InteractObject>().Interact();
                Vector3 grabObjectRotationWhenLooked = infoWhenLooked;
                _distanceGrabObjectWithCameraWhenLooked = infoWhenLooked.w;
                _grabObjectRotationWhenLooked = Quaternion.Euler(grabObjectRotationWhenLooked);
            }
            else
            {
                _distanceGrabObjectWithCameraWhenLooked = 1.0f;
                _grabObjectRotationWhenLooked = Quaternion.identity;
            }
            _originPositionGrabObject = _grabObject.transform.position;
            _originRotationGrabObject = _grabObject.transform.rotation;
            _grabObject.transform.localPosition = Vector3.zero;
            _objectHolder.transform.position = _mainCamera.transform.position + _mainCamera.transform.forward * _distanceGrabObjectWithCameraWhenLooked;
            _grabObject.transform.LookAt(_mainCamera.transform);
            _grabObject.transform.Rotate(_grabObjectRotationWhenLooked.eulerAngles);
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
