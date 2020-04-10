using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBack : MonoBehaviour
{
    [SerializeField] CouloirInfinieManager _couloirInfinieManager = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _couloirInfinieManager.PlayerGoInBack();
        }
    }
}
