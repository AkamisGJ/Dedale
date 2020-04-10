using UnityEngine;

[HierarchyIcon("flag")]
public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField] private GameManager.MyState _myState = GameManager.MyState.GAME;
    [SerializeField] private PlayerAgentController _playerController = null;
    [SerializeField] private Camera _cameraPlayer = null;
    [SerializeField] private Camera _cameraUI = null;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (GameManager.Instance.CurrentState != GameManager.MyState.LOADINGSCREEN)
        {
            GameManager.Instance.CurrentState = _myState;
        }
        PlayerManager.Instance.Player = _playerController;
        PlayerManager.Instance.CameraPlayerInstantiate = _cameraPlayer;
        PlayerManager.Instance.CameraUIInstantiate = _cameraUI;
        PlayerManager.Instance.InstantiatePlayer(transform);
        Destroy(gameObject);
    }
    private void Start()
    {
        Destroy(gameObject);
    }
}