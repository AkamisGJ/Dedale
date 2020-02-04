using UnityEngine;

public class Wind : MonoBehaviour
{

    private bool _playerIsIn = false;
    private PlayerAgentController _playerController = null;

    void Start()
    {
        _playerIsIn = false;
        _playerController = PlayerManager.Instance.PlayerController;
    }

    void Update()
    {
        if(_playerIsIn == true)
        {
            //IMouvement mouvement = (_playerController.CurrentState)(IPlayerState) as IMouvement;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerIsIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerIsIn = false;
        }
    }
}
