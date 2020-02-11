using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telepot : MonoBehaviour
{

    [SerializeField] private Transform _player = null;
    [SerializeField] private Transform _reciever = null;

    private bool playerIsOverlapping = false;


    private void Start()
    {
        _player = PlayerManager.Instance.PlayerController.transform;
    }
    void Update()
    {
        if (playerIsOverlapping == true)
        {
            Vector3 portalToPlayer = _player.localPosition - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, _reciever.rotation);
                rotationDiff += 180;
                _player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                _player.position = _reciever.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsOverlapping = false;
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }

}
