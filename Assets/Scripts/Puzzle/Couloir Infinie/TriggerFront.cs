using UnityEngine;

public class TriggerFront : MonoBehaviour
{
    [SerializeField] CouloirInfinieManager _couloirInfinieManager = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _couloirInfinieManager.PlayerGoInFront();
        }
    }
}
