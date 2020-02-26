using UnityEngine;

public class ExitLiana : MonoBehaviour
{
    private PlayerAgentController _playerController = null;

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        }
    }
}
