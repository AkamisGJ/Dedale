using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class hublot : MonoBehaviour
{
    [SerializeField] public bool _activate = true;
    
    [ShowIfGroup("_activate")]

    [BoxGroup("_activate/Force")]
    [HideIf("_randomAmount")]
    [SerializeField] public float forceAmount = 1f;

    [BoxGroup("_activate/Force")]
    [SerializeField] public ForceMode _forceMode = ForceMode.Acceleration;

    [BoxGroup("_activate/Force")]
    [SerializeField] public bool _randomAmount;

    [BoxGroup("_activate/Force")]
    [ShowIf("_randomAmount")]
    [SerializeField] public float _minForce = 0.5f;

    [BoxGroup("_activate/Force")]
    [ShowIf("_randomAmount")]
    [SerializeField] public float _maxForce = 2f;


    private void Awake()
    {
        if (_randomAmount)
        {
            forceAmount = Random.Range(_minForce, _maxForce);
        }
    }
}
