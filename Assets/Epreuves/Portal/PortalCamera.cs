using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    private Transform _playerCamera = null;
    [SerializeField] private Transform _portal = null;
    [SerializeField] private Transform _otherPortal = null;

    private void Start()
    {
        _playerCamera = PlayerManager.Instance.CameraPlayer.transform;
    }

    void Update()
    {
        Vector3 playerOffsetFromPortal = _playerCamera.position - _portal.position;
        Vector3 correctRotationBetweenPortal = Vector3.zero;
        correctRotationBetweenPortal = _otherPortal.position.x * _otherPortal.forward;
        correctRotationBetweenPortal += _otherPortal.position.y * _otherPortal.forward;
        correctRotationBetweenPortal += _otherPortal.position.z * _otherPortal.forward;
        transform.position = correctRotationBetweenPortal + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotation = Quaternion.Angle(_portal.rotation, _otherPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotation, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * _playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
