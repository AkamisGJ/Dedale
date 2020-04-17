using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chambre_dispawn : MonoBehaviour
{
    [SerializeField] private GameObject _chambre1 = null;
    [SerializeField] private GameObject _chambre2 = null;

    private void OnTriggerEnter(Collider other)
    {
        _chambre1.SetActive(false);

        _chambre2.SetActive(false);
    }
}
