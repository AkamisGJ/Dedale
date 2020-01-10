using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject _leverBrok = null;
    [SerializeField] private GameObject _leverRepear = null;
    [SerializeField] private GameObject _collider1 = null;
    [SerializeField] private GameObject _collider2 = null;
    private bool _activate1 = false;


    private void OnTriggerEnter(Collider other)
    {
        _activate1 = !_activate1;
        _leverBrok.SetActive(!_activate1);
        _collider1.SetActive(!_activate1);
        _leverRepear.SetActive(_activate1);
        _collider2.SetActive(_activate1);

    }
}
