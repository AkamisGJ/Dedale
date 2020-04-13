using UnityEngine;

public class CameraUIFollow : MonoBehaviour
{
    [SerializeField] private Camera _cameraUi = null;

    private void Start()
    {
        GameLoopManager.Instance.LateGameLoop += OnLateUpdate;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLateUpdate()
    {
        transform.position = PlayerManager.Instance.CameraPlayer.transform.position;
        transform.rotation = PlayerManager.Instance.CameraPlayer.transform.rotation;
        _cameraUi.fieldOfView = PlayerManager.Instance.CameraPlayer.fieldOfView;
    }

    private void OnDestroy()
    {
        GameLoopManager.Instance.LateGameLoop -= OnLateUpdate;
    }
}