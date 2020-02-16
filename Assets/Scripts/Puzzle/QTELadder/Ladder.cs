using UnityEngine;

public class Ladder : MonoBehaviour
{
    private PlayerAgentController _playerController = null;

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        }
    }
}
