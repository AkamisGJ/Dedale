using UnityEngine;

public class WindSafeZone : MonoBehaviour
{
    [SerializeField] Wind _wind = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _wind.PlayerIsInSafeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _wind.PlayerIsInSafeZone = false;
        }
    }
}
