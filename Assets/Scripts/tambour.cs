using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class tambour : MonoBehaviour
{
    [SerializeField] private bool _random;

    [ShowIf("_random")]
    [SerializeField] private float _minSpeed = 0.5f;

    [ShowIf("_random")]
    [SerializeField] private float _maxSpeed = 2f;


    void Awake()
    {
        if (_random)
        {
            GetComponent<Animator>().speed = Random.Range(_minSpeed, _maxSpeed);
        }
    }
}
