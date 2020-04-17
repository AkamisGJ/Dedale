using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partie_dispawn : MonoBehaviour
{
    [SerializeField] private GameObject _partieDesactivate = null;
    [SerializeField] private GameObject _partieActivate = null;

    private void OnTriggerEnter(Collider other)
    {
        _partieDesactivate.SetActive(false);

        _partieActivate.SetActive(true);
    }
}
