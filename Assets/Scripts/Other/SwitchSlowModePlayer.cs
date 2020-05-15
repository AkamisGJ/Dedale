using UnityEngine;

public class SwitchSlowModePlayer : MonoBehaviour
{
    [SerializeField] private bool _isSlow = false;
    private PlayerAgentController _playerAgentController = null;

    private void Start()
    {
        _playerAgentController = PlayerManager.Instance.PlayerController;
    }

    public void SwitchSlowMode()
    {
        _playerAgentController.IsSlow = _isSlow;
    }
}
