using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class hublot : MonoBehaviour
{
    [SerializeField] private bool _activate = true;
    
    [ShowIfGroup("_activate")]

    [BoxGroup("_activate/Force")]
    [HideIf("_random")]
    [SerializeField] private float forceAmount = 1f;

    [BoxGroup("_activate/Force")]
    [SerializeField] private ForceMode _forceMode = ForceMode.Acceleration;

    [BoxGroup("_activate/Force")]
    [SerializeField] private bool _random;

    [BoxGroup("_activate/Force")]
    [ShowIf("_random")]
    [SerializeField] private float _minForce = 0.5f;

    [BoxGroup("_activate/Force")]
    [ShowIf("_random")]
    [SerializeField] private float _maxForce = 2f;


    private void Start()
    {
        if (_random)
        {
            forceAmount = Random.Range(_minForce, _maxForce);
        }
    }

    void Update()
    {
        if (_activate)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<Rigidbody>().AddTorque(transform.up * forceAmount * Time.deltaTime, _forceMode);
            }
        }
    }
}
