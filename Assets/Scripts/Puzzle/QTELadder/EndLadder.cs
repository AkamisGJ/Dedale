using UnityEngine;

public class EndLadder : MonoBehaviour
{
    private PlayerAgentController _playerController = null;
    private IQTELadder _iQTELadder = null;
    private bool _isAtTheEnd = false;

    private void Start()
    {
        _playerController = PlayerManager.Instance.PlayerController;
        _isAtTheEnd = false;
    }

    private void Update()
    {
        if(_isAtTheEnd == true && _playerController.transform.position.y > transform.position.y)
        {
            _iQTELadder.IsAtTheEnd = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isAtTheEnd = true;
            _iQTELadder = _playerController.States[PlayerAgentController.MyState.QTELADDER] as IQTELadder;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _iQTELadder.IsAtTheEnd = false;
            _isAtTheEnd = false;
            _playerController.ChangeState(PlayerAgentController.MyState.MOVEMENT);
        }
    }
}